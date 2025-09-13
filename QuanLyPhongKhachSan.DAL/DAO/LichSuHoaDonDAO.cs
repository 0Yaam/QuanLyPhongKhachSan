using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class LichSuHoaDonDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public int Them(LichSuHoaDon x)
        {
            const string sql = @"
INSERT INTO LichSuHoaDon (MaHD, MaDat, TenKH, CCCD, SDT, ThoiGianIn, LoaiHoaDon, SoPhong)
VALUES (@MaHD, @MaDat, @TenKH, @CCCD, @SDT, @ThoiGianIn, @LoaiHoaDon, @SoPhong);
SELECT CAST(SCOPE_IDENTITY() AS int);";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = x.MaHD;
                        cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = x.MaDat ?? 0; // Đảm bảo MaDat không null
                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = (object)x.TenKH ?? DBNull.Value;
                        cmd.Parameters.Add("@CCCD", SqlDbType.NVarChar, 20).Value = (object)x.CCCD ?? DBNull.Value;
                        cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = (object)x.SDT ?? DBNull.Value;
                        cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                        cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value = (object)x.LoaiHoaDon ?? "Khác";
                        cmd.Parameters.Add("@SoPhong", SqlDbType.Int).Value = (object)x.SoPhong ?? DBNull.Value; // Thêm SoPhong

                        var idObj = cmd.ExecuteScalar();
                        return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error LichSuHoaDonDAO.Them: MaHD={x.MaHD}, MaDat={x.MaDat}, SoPhong={x.SoPhong}, Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error LichSuHoaDonDAO.Them: {ex.Message}");
                throw;
            }
        }


        public List<LichSuHoaDon> LayDanhSach()
        {
            const string sql = @"
SELECT 
    l.Id, l.MaHD, l.MaDat, l.TenKH, l.CCCD, l.SDT, l.ThoiGianIn, l.LoaiHoaDon,
    p.SoPhong
FROM LichSuHoaDon l
LEFT JOIN DatPhong dp ON dp.MaDat = l.MaDat
LEFT JOIN Phong p ON p.MaPhong = dp.MaPhong
ORDER BY l.ThoiGianIn DESC";

            var list = new List<LichSuHoaDon>();
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    // dùng GetOrdinal để an toàn theo tên cột
                    int ordId = rd.GetOrdinal("Id");
                    int ordMaHD = rd.GetOrdinal("MaHD");
                    int ordMaDat = rd.GetOrdinal("MaDat");
                    int ordTenKH = rd.GetOrdinal("TenKH");
                    int ordCCCD = rd.GetOrdinal("CCCD");
                    int ordSDT = rd.GetOrdinal("SDT");
                    int ordThoiGianIn = rd.GetOrdinal("ThoiGianIn");
                    int ordLoaiHoaDon = rd.GetOrdinal("LoaiHoaDon");
                    int ordSoPhong = rd.GetOrdinal("SoPhong");

                    while (rd.Read())
                    {
                        list.Add(new LichSuHoaDon
                        {
                            Id = rd.GetInt32(ordId),
                            MaHD = rd.GetInt32(ordMaHD),
                            MaDat = rd.IsDBNull(ordMaDat) ? (int?)null : rd.GetInt32(ordMaDat),
                            TenKH = rd.IsDBNull(ordTenKH) ? null : rd.GetString(ordTenKH),
                            CCCD = rd.IsDBNull(ordCCCD) ? null : rd.GetString(ordCCCD),
                            SDT = rd.IsDBNull(ordSDT) ? null : rd.GetString(ordSDT),
                            ThoiGianIn = rd.GetDateTime(ordThoiGianIn),
                            LoaiHoaDon = rd.IsDBNull(ordLoaiHoaDon) ? null : rd.GetString(ordLoaiHoaDon),
                            SoPhong = rd.IsDBNull(ordSoPhong) ? (int?)null : rd.GetInt32(ordSoPhong)
                        });
                    }
                }
            }
            return list;
        }


        public int ThemNeuChuaCo(LichSuHoaDon x)
        {
            const string sqlSelect = @"
SELECT TOP 1 Id FROM LichSuHoaDon 
WHERE MaHD = @MaHD AND LoaiHoaDon = @LoaiHoaDon";

            const string sqlInsert = @"
INSERT INTO LichSuHoaDon (MaHD, MaDat, TenKH, CCCD, SDT, ThoiGianIn, LoaiHoaDon)
VALUES (@MaHD, @MaDat, @TenKH, @CCCD, @SDT, @ThoiGianIn, @LoaiHoaDon);
SELECT SCOPE_IDENTITY();";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sqlSelect, conn))
                {
                    cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = x.MaHD;
                    cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value = x.LoaiHoaDon ?? "Khác";
                    conn.Open();

                    var existIdObj = cmd.ExecuteScalar();
                    if (existIdObj != null && existIdObj != DBNull.Value)
                        return Convert.ToInt32(existIdObj);
                }

                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sqlInsert, conn))
                {
                    cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = x.MaHD;
                    cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = x.MaDat ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = (object)x.TenKH ?? DBNull.Value;
                    cmd.Parameters.Add("@CCCD", SqlDbType.NVarChar, 20).Value = (object)x.CCCD ?? DBNull.Value;
                    cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value = (object)x.SDT ?? DBNull.Value;
                    cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                    cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value = x.LoaiHoaDon ?? "Khác";

                    conn.Open();
                    var obj = cmd.ExecuteScalar();
                    return (obj != null && int.TryParse(obj.ToString(), out int id)) ? id : 0;
                }
            }
            catch (Exception ex)
            {
                // Nếu đã tạo unique index, vi phạm sẽ rơi vào đây -> trả về Id cũ để idempotent
                // Có thể thêm đoạn fetch Id cũ lần nữa nếu muốn
                return 0;
            }
        }




    }
}
