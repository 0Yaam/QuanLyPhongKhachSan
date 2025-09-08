// QuanLyPhongKhachSan.DAL.DAO.HoaDonDAO.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class HoaDonDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public List<HoaDon> LayDanhSach()
        {
            var list = new List<HoaDon>();
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                const string sql = @"SELECT MaHD, MaDat, NgayLap, LoaiHoaDon, TongThanhToan, GhiChu FROM HoaDon";
                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new HoaDon
                        {
                            MaHD = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                            MaDat = rd.IsDBNull(1) ? 0 : rd.GetInt32(1),
                            NgayLap = rd.IsDBNull(2) ? DateTime.Now : rd.GetDateTime(2),
                            LoaiHoaDon = rd.IsDBNull(3) ? "" : rd.GetString(3),
                            TongThanhToan = rd.IsDBNull(4) ? (decimal?)null : rd.GetDecimal(4),
                            GhiChu = rd.IsDBNull(5) ? "" : rd.GetString(5)
                        });
                    }
                }
            }
            return list;
        }

        // Thêm mới và LẤY MaHD (OUTPUT INSERTED.MaHD)
        public int ThemVaLayMa(HoaDon hd)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                const string sql = @"
INSERT INTO HoaDon (MaDat, NgayLap, LoaiHoaDon, TongThanhToan, GhiChu)
OUTPUT INSERTED.MaHD
VALUES (@MaDat, @NgayLap, @LoaiHoaDon, @TongThanhToan, @GhiChu);";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = hd.MaDat;
                    cmd.Parameters.Add("@NgayLap", SqlDbType.DateTime2).Value = hd.NgayLap;
                    cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 100).Value = (object)hd.LoaiHoaDon ?? DBNull.Value;

                    var pTong = cmd.Parameters.Add("@TongThanhToan", SqlDbType.Decimal);
                    pTong.Precision = 18; pTong.Scale = 2; pTong.Value = (object)hd.TongThanhToan ?? DBNull.Value;

                    cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value = (object)hd.GhiChu ?? DBNull.Value;

                    var id = cmd.ExecuteScalar();
                    return id == null ? 0 : Convert.ToInt32(id);
                }
            }
        }
    }
}
