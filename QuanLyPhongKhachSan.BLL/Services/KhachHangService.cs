using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.DAL;
using System;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class KhachHangService
    {
        private KhachHangDAO dao = new KhachHangDAO();

        public int UpsertKhachHang(string hoTen, string cccd, string sdt)
        {
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                return -1;
            }

            KhachHang khachHang = new KhachHang(hoTen, cccd, sdt);
            int maKH = dao.KiemTraTonTai(cccd, sdt);

            if (maKH == -1)
            {
                return dao.ThemKhachHang(khachHang);
            }
            else
            {
                khachHang.MaKH = maKH;
                return dao.CapNhatKhachHang(khachHang);
            }
        }

        public KhachHang LayKhachHangTheoMaKH(int maKH)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaKH, HoTen, CCCD, SDT FROM KhachHang WHERE MaKH = @MaKH";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new KhachHang
                        {
                            MaKH = reader.GetInt32(0),
                            HoTen = reader.GetString(1),
                            CCCD = reader.IsDBNull(2) ? null : reader.GetString(2),
                            SDT = reader.GetString(3)
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy khách hàng: " + ex.Message);
                return null;
            }
        }
    }
}