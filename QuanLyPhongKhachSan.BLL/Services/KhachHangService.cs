using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class KhachHangService
    {
        private readonly KhachHangDAO _dao = new KhachHangDAO();

        public int UpsertKhachHang(string hoTen, string cccd, string sdt)
        {
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                return -1;
            }

            KhachHang khachHang = new KhachHang(hoTen, cccd, sdt);
            int maKH = _dao.KiemTraTonTai(cccd, sdt);

            if (maKH == -1)
            {
                return _dao.ThemKhachHang(khachHang);
            }
            else
            {
                khachHang.MaKH = maKH;
                return _dao.CapNhatKhachHang(khachHang);
            }
        }

        public KhachHang LayKhachHangTheoMaKH(int maKH)
        {
            return _dao.LayKhachHangTheoMaKH(maKH);
        }
    }
}