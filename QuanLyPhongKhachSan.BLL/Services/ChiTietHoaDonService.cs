using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System.Collections.Generic;

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
        public List<ChiTietHoaDon> LayTheoMaHD(int maHD) => _dao.LayTheoMaHD(maHD);

    }
}
