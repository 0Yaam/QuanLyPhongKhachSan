using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ChiTietHoaDonDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public int Them(ChiTietHoaDon cthd)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
INSERT INTO ChiTietHoaDon (MaHD, SoLuong)
OUTPUT INSERTED.MaCTHD
VALUES (@MaHD, @SoLuong);";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = cthd.MaHD;
                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = cthd.SoLuong;

                        var id = cmd.ExecuteScalar();
                        return (id == null) ? 0 : Convert.ToInt32(id);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi ChiTietHoaDonDAO.Them: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định ChiTietHoaDonDAO.Them: {ex.Message}");
                return 0;
            }
        }
    }
}