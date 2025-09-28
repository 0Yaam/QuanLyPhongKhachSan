using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ThongKeDAO
    {
        private readonly string connectionString = Config.ConnectionString;

        public DataTable GetDailyCustomerStats(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT CAST(NgayThamGia AS DATE) AS Ngay, COUNT(*) AS SoKhach
                        FROM KhachHang
                        WHERE NgayThamGia IS NOT NULL
                          AND NgayThamGia BETWEEN @FromDate AND @ToDate
                        GROUP BY CAST(NgayThamGia AS DATE)
                        ORDER BY Ngay";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(dt);
                        }
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi GetDailyCustomerStats: " + ex.Message);
                throw new Exception("Lỗi khi lấy thống kê theo ngày: " + ex.Message);
            }
        }

        public int GetCustomerCountInMonth(DateTime firstDay, DateTime lastDay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT COUNT(*)
                        FROM KhachHang
                        WHERE NgayThamGia IS NOT NULL
                          AND NgayThamGia BETWEEN @FirstDay AND @LastDay";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstDay", firstDay);
                        cmd.Parameters.AddWithValue("@LastDay", lastDay);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi GetCustomerCountInMonth: " + ex.Message);
                throw new Exception("Lỗi khi lấy số lượng khách trong tháng: " + ex.Message);
            }
        }

        public (DataTable dailyStats, int totalCustomers, decimal commission)
            GetCustomerStatistics(DateTime from, DateTime to, int? maNV)
        {
            var daily = new DataTable();
            int totalCustomers = 0;
            decimal commission = 0m;

            using (var cn = new SqlConnection(connectionString))
            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = @"
;WITH Days AS (
    SELECT CAST(@From AS date) d
    UNION ALL
    SELECT DATEADD(day,1,d) FROM Days WHERE d < CAST(@To AS date)
)
SELECT d.d AS Ngay,
       ISNULL(x.SoKhach,0) AS SoKhach
FROM Days d
LEFT JOIN (
   SELECT CAST(l.ThoiGianIn AS date) Ngay,
          COUNT(DISTINCT dp.MaKH)     AS SoKhach
   FROM LichSuHoaDon l
   LEFT JOIN HoaDon h   ON h.MaHD  = l.MaHD
   LEFT JOIN DatPhong dp ON dp.MaDat = l.MaDat
   WHERE l.ThoiGianIn BETWEEN @From AND @To
     AND (@MaNV IS NULL OR l.MaNV = @MaNV)
   GROUP BY CAST(l.ThoiGianIn AS date)
) x ON x.Ngay = d.d
OPTION (MAXRECURSION 0);
";
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@MaNV", (object)maNV ?? DBNull.Value);

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(daily);
                }

                // Tổng KH
                cmd.Parameters.Clear();
                cmd.CommandText = @"
SELECT COUNT(DISTINCT dp.MaKH)
FROM LichSuHoaDon l
LEFT JOIN DatPhong dp ON dp.MaDat = l.MaDat
WHERE l.ThoiGianIn BETWEEN @From AND @To
  AND (@MaNV IS NULL OR l.MaNV = @MaNV);
";
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@MaNV", (object)maNV ?? DBNull.Value);

                cn.Open();
                var objTotal = cmd.ExecuteScalar();
                totalCustomers = (objTotal == null || objTotal == DBNull.Value) ? 0 : Convert.ToInt32(objTotal);

                // Tổng tiền (commission tuỳ công thức của bạn)
                cmd.Parameters.Clear();
                cmd.CommandText = @"
SELECT ISNULL(SUM(h.TongThanhToan),0)
FROM HoaDon h
JOIN LichSuHoaDon l ON l.MaHD = h.MaHD
WHERE l.ThoiGianIn BETWEEN @From AND @To
  AND (@MaNV IS NULL OR l.MaNV = @MaNV);
";
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@MaNV", (object)maNV ?? DBNull.Value);

                var objCommission = cmd.ExecuteScalar();
                commission = (objCommission == null || objCommission == DBNull.Value) ? 0m : Convert.ToDecimal(objCommission);
                cn.Close();
            }

            return (daily, totalCustomers, commission);
        }
    }
}
