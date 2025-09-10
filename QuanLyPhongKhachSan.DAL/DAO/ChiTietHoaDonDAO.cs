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
INSERT INTO ChiTietHoaDon (MaHD, TenDichVu, SoLuong, Gia)
OUTPUT INSERTED.MaCTHD
VALUES (@MaHD, @TenDichVu, @SoLuong, @Gia);";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", cthd.MaHD);
                        cmd.Parameters.AddWithValue("@TenDichVu", (object)(cthd.TenDichVu ?? "") ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                        cmd.Parameters.AddWithValue("@Gia", cthd.Gia);

                        var id = cmd.ExecuteScalar();
                        return (id == null) ? 0 : Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ChiTietHoaDonDAO.Them: {ex.Message}");
                return 0;
            }
        }

    }
}