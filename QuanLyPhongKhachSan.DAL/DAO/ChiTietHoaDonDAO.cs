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

                    // KHÔNG insert ThanhTien vì là computed
                    const string sql = @"
INSERT INTO ChiTietHoaDon (MaHD, DanhMuc, SoLuong, DonGia)
OUTPUT INSERTED.MaCT
VALUES (@MaHD, @DanhMuc, @SoLuong, @DonGia);";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaHD", SqlDbType.Int).Value = cthd.MaHD;

                        cmd.Parameters.Add("@DanhMuc", SqlDbType.NVarChar, 200)
                           .Value = (object)(cthd.TenDichVu ?? "") ?? DBNull.Value;

                        cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = cthd.SoLuong;

                        var pDonGia = cmd.Parameters.Add("@DonGia", SqlDbType.Decimal);
                        pDonGia.Precision = 18;
                        pDonGia.Scale = 2;
                        pDonGia.Value = cthd.Gia;

                        var idObj = cmd.ExecuteScalar();
                        return (idObj == null || idObj == DBNull.Value) ? 0 : Convert.ToInt32(idObj);
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
