using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ThongKeNhanVienDAO
    {
        private readonly string _cs = Config.ConnectionString;

        public DataTable LayDanhSachNhanVien()
        {
            const string sql = @"
SELECT 
    ISNULL(nv.MaNV, 0) AS MaNV,
    CASE 
        WHEN nv.TenNV IS NOT NULL AND nv.TenNV <> '' THEN nv.TenNV
        ELSE tk.TenDangNhap
    END AS TenHienThi
FROM dbo.TaiKhoan tk
LEFT JOIN dbo.NhanVien nv ON nv.MaNV = tk.MaNV
ORDER BY TenHienThi;";

            using (var conn = new SqlConnection(_cs))
            using (var da = new SqlDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Thống kê số KH theo ngày theo nhân viên (dựa vào LichSuHoaDon.MaNV).
        /// </summary>
        public DataTable ThongKeKhachTheoNgay(DateTime from, DateTime to, int? maNV)
        {
            const string sql = @"
;WITH Days AS (
    SELECT CAST(@From AS date) d
    UNION ALL
    SELECT DATEADD(day, 1, d) FROM Days WHERE d < CAST(@To AS date)
)
SELECT 
    d AS Ngay,
    ISNULL(t.SoKhach, 0) AS SoKhach
FROM Days
LEFT JOIN (
    SELECT 
        CAST(l.ThoiGianIn AS date) Ngay,
        COUNT(DISTINCT dp.MaKH) AS SoKhach
    FROM dbo.LichSuHoaDon l
    LEFT JOIN dbo.DatPhong dp ON dp.MaDat = l.MaDat
    WHERE l.ThoiGianIn >= @From AND l.ThoiGianIn <= @To
      AND (@MaNV IS NULL OR l.MaNV = @MaNV)
    GROUP BY CAST(l.ThoiGianIn AS date)
) t ON t.Ngay = Days.d
OPTION (MAXRECURSION 0);";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@From", SqlDbType.DateTime2).Value = from;
                cmd.Parameters.Add("@To", SqlDbType.DateTime2).Value = to;
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = (object)maNV ?? DBNull.Value;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Tổng KH & Doanh thu & Hoa hồng (tùy công thức) theo nhân viên
        /// </summary>
        public (int TotalCustomers, decimal Revenue) TongHop(DateTime from, DateTime to, int? maNV)
        {
            const string sql = @"
SELECT 
    COUNT(DISTINCT dp.MaKH) AS TotalCustomers,
    ISNULL(SUM(hd.TongThanhToan), 0) AS Revenue
FROM dbo.LichSuHoaDon l
LEFT JOIN dbo.DatPhong dp ON dp.MaDat = l.MaDat
LEFT JOIN dbo.HoaDon hd ON hd.MaHD = l.MaHD
WHERE l.ThoiGianIn >= @From AND l.ThoiGianIn <= @To
  AND (@MaNV IS NULL OR l.MaNV = @MaNV);";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@From", SqlDbType.DateTime2).Value = from;
                cmd.Parameters.Add("@To", SqlDbType.DateTime2).Value = to;
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = (object)maNV ?? DBNull.Value;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        int total = rd.IsDBNull(0) ? 0 : rd.GetInt32(0);
                        decimal rev = rd.IsDBNull(1) ? 0 : rd.GetDecimal(1);
                        return (total, rev);
                    }
                }
            }
            return (0, 0m);
        }
    }
}
