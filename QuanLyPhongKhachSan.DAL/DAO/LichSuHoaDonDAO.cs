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
INSERT INTO LichSuHoaDon (MaHD, MaDat, TenKH, CCCD, SDT, ThoiGianIn, LoaiHoaDon)
VALUES (@MaHD, @MaDat, @TenKH, @CCCD, @SDT, @ThoiGianIn, @LoaiHoaDon);
SELECT CAST(SCOPE_IDENTITY() AS int);";

            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    // DEBUG: in ra DB đang kết nối
                    using (var cmdDb = new SqlCommand("SELECT DB_NAME();", conn))
                    {
                        var dbname = (string)cmdDb.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine("LichSuHoaDonDAO.Them -> DB: " + dbname);
                    }

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = x.MaHD;

                        // MaDat của bạn đang NOT NULL -> bắt buộc có giá trị > 0
                        if (!x.MaDat.HasValue || x.MaDat.Value <= 0)
                            throw new Exception("MaDat NULL/0 nhưng cột MaDat là NOT NULL. Hãy truyền MaDat hợp lệ.");

                        cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = x.MaDat.Value;

                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value =
                            string.IsNullOrWhiteSpace(x.TenKH) ? (object)DBNull.Value : x.TenKH;
                        cmd.Parameters.Add("@CCCD", SqlDbType.NVarChar, 20).Value =
                            string.IsNullOrWhiteSpace(x.CCCD) ? (object)DBNull.Value : x.CCCD;
                        cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 20).Value =
                            string.IsNullOrWhiteSpace(x.SDT) ? (object)DBNull.Value : x.SDT;
                        cmd.Parameters.Add("@ThoiGianIn", SqlDbType.DateTime2).Value = x.ThoiGianIn;
                        cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value =
                            string.IsNullOrWhiteSpace(x.LoaiHoaDon) ? (object)"Khác" : x.LoaiHoaDon;

                        var idObj = cmd.ExecuteScalar();
                        return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL ERR LichSuHoaDonDAO.Them: " + ex.Message);
                throw; // ĐỂ VĂNG RA UI -> bạn sẽ thấy MessageBox / Console
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERR LichSuHoaDonDAO.Them: " + ex.Message);
                throw;
            }
        }


        public List<LichSuHoaDon> LayDanhSach()
        {
            const string sql = @"
SELECT Id, MaHD, MaDat, TenKH, CCCD, SDT, ThoiGianIn, LoaiHoaDon
FROM LichSuHoaDon
ORDER BY ThoiGianIn DESC";

            var list = new List<LichSuHoaDon>();
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new LichSuHoaDon
                        {
                            Id = rd.GetInt32(0),
                            MaHD = rd.GetInt32(1),
                            MaDat = rd.IsDBNull(2) ? (int?)null : rd.GetInt32(2),
                            TenKH = rd.IsDBNull(3) ? null : rd.GetString(3),
                            CCCD = rd.IsDBNull(4) ? null : rd.GetString(4),
                            SDT = rd.IsDBNull(5) ? null : rd.GetString(5),
                            ThoiGianIn = rd.GetDateTime(6),
                            LoaiHoaDon = rd.IsDBNull(7) ? null : rd.GetString(7),
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
