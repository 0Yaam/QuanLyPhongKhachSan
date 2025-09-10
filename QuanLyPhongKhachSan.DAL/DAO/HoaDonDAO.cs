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

        public int ThemVaLayMa(HoaDon hd)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                const string sql = @"
INSERT INTO HoaDon (MaDat, NgayLap, LoaiHoaDon, TongThanhToan, GhiChu)
VALUES (@MaDat, @NgayLap, @LoaiHoaDon, @TongThanhToan, @GhiChu);
SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    var pMaDat = cmd.Parameters.Add("@MaDat", SqlDbType.Int);
                    pMaDat.Value = (hd.MaDat > 0) ? (object)hd.MaDat : DBNull.Value;

                    cmd.Parameters.Add("@NgayLap", SqlDbType.DateTime2).Value = hd.NgayLap;

                    cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value =
                        string.IsNullOrWhiteSpace(hd.LoaiHoaDon) ? (object)DBNull.Value : hd.LoaiHoaDon;

                    var pTong = cmd.Parameters.Add("@TongThanhToan", SqlDbType.Decimal);
                    pTong.Precision = 18; pTong.Scale = 2;
                    pTong.Value = hd.TongThanhToan.HasValue ? (object)hd.TongThanhToan.Value : DBNull.Value;

                    cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                        string.IsNullOrWhiteSpace(hd.GhiChu) ? (object)DBNull.Value : hd.GhiChu;

                    var idObj = cmd.ExecuteScalar();
                    return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
                }
            }
        }

        public int ThemVaTraMa(HoaDon hd)
        {
            try
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
                        var pMaDat = cmd.Parameters.Add("@MaDat", SqlDbType.Int);
                        pMaDat.Value = (hd.MaDat > 0) ? (object)hd.MaDat : DBNull.Value;

                        cmd.Parameters.Add("@NgayLap", SqlDbType.DateTime2).Value = hd.NgayLap;
                        cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value =
                            string.IsNullOrWhiteSpace(hd.LoaiHoaDon) ? (object)DBNull.Value : hd.LoaiHoaDon;

                        var pTong = cmd.Parameters.Add("@TongThanhToan", SqlDbType.Decimal);
                        pTong.Precision = 18; pTong.Scale = 2;
                        pTong.Value = hd.TongThanhToan.HasValue ? (object)hd.TongThanhToan.Value : DBNull.Value;

                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrWhiteSpace(hd.GhiChu) ? (object)DBNull.Value : hd.GhiChu;

                        var id = cmd.ExecuteScalar();
                        if (id == null || id == DBNull.Value)
                        {
                            Console.WriteLine($"Lỗi khi thêm hóa đơn: Không thể lấy MaHD. Kiểm tra MaDat = {hd.MaDat} và cấu trúc bảng.");
                            return 0;
                        }
                        return Convert.ToInt32(id);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL khi thêm hóa đơn: {ex.Message} (MaDat = {hd.MaDat}, LoaiHoaDon = {hd.LoaiHoaDon})");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định khi thêm hóa đơn: {ex.Message}");
                return 0;
            }
        }

        public int Them(HoaDon hd)
        {
            try
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
                        var pMaDat = cmd.Parameters.Add("@MaDat", SqlDbType.Int);
                        pMaDat.Value = (hd.MaDat > 0) ? (object)hd.MaDat : DBNull.Value;

                        cmd.Parameters.Add("@NgayLap", SqlDbType.DateTime).Value = hd.NgayLap;

                        cmd.Parameters.Add("@LoaiHoaDon", SqlDbType.NVarChar, 20).Value =
                            string.IsNullOrWhiteSpace(hd.LoaiHoaDon) ? (object)DBNull.Value : hd.LoaiHoaDon;

                        var pTong = cmd.Parameters.Add("@TongThanhToan", SqlDbType.Decimal);
                        pTong.Precision = 18; pTong.Scale = 2;
                        pTong.Value = hd.TongThanhToan.HasValue ? (object)hd.TongThanhToan.Value : DBNull.Value;

                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value =
                            string.IsNullOrWhiteSpace(hd.GhiChu) ? (object)DBNull.Value : hd.GhiChu;

                        var id = cmd.ExecuteScalar();
                        return (id == null) ? 0 : Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi HoaDonDAO.Them: {ex.Message}");
                return 0;
            }
        }

        public int CapNhatTongTien(int maHD, decimal tong)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
UPDATE HoaDon
SET TongThanhToan = @Tong
WHERE MaHD = @MaHD;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        var pTong = cmd.Parameters.Add("@Tong", SqlDbType.Decimal);
                        pTong.Precision = 18; pTong.Scale = 2; pTong.Value = tong;

                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHD;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine($"Không tìm thấy hóa đơn với mã {maHD} để cập nhật.");
                        }
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi HoaDonDAO.CapNhatTongTien: {ex.Message}");
                return 0;
            }
        }
        public int CapNhatTongTienVaGhiChu(int maHD, decimal tong, string ghiChu)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
UPDATE HoaDon
SET TongThanhToan = @Tong,
    GhiChu = @GhiChu
WHERE MaHD = @MaHD;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        var pTong = cmd.Parameters.Add("@Tong", SqlDbType.Decimal);
                        pTong.Precision = 18; pTong.Scale = 2; pTong.Value = tong;

                        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1) // -1 = NVARCHAR(MAX)
                           .Value = (object)ghiChu ?? DBNull.Value;

                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHD;

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi HoaDonDAO.CapNhatTongTienVaGhiChu: {ex.Message}");
                return 0;
            }
        }

    }
}