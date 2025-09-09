// DAL/DAO/ChiTietHoaDonDAO.cs
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ChiTietHoaDonDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public int Them(ChiTietHoaDon ct) // dùng model ở namespace .DAL.OL
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
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = ct.MaHD;
                        cmd.Parameters.Add("@TenDichVu", SqlDbType.NVarChar, 255).Value =
                            (object)ct.TenDichVu ?? DBNull.Value;
                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = ct.SoLuong;

                        var pGia = cmd.Parameters.Add("@Gia", SqlDbType.Decimal);
                        pGia.Precision = 18;
                        pGia.Scale = 2;
                        pGia.Value = ct.Gia;

                        var id = cmd.ExecuteScalar();
                        return id == null ? 0 : Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ChiTietHoaDonDAO.Them: " + ex.Message);
                return 0;
            }
        }
    }
}
