// BLL/Services/ChiTietHoaDonService.cs
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.DAL.DAO;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class ChiTietHoaDonService
    {
        private readonly ChiTietHoaDonDAO _dao = new ChiTietHoaDonDAO();

        public int Them(ChiTietHoaDon ct)
        {
            if (ct == null || ct.MaHD <= 0 || ct.SoLuong <= 0 || ct.Gia < 0) return 0;
            return _dao.Them(ct);
        }
    }
}
