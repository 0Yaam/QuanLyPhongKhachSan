using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;

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

            KhachHang khachHang = new KhachHang(hoTen, cccd, sdt); // NgayThamGia tự động gán DateTime.Now trong constructor
            int maKH = _dao.KiemTraTonTai(cccd, sdt);

            if (maKH == -1)
            {
                return _dao.ThemKhachHang(khachHang);
            }
            else
            {
                khachHang.MaKH = maKH;
                khachHang.NgayThamGia = DateTime.Now; // Cập nhật NgayThamGia khi sửa
                return _dao.CapNhatKhachHang(khachHang);
            }
        }

        public KhachHang LayKhachHangTheoMaKH(int maKH)
        {
            return _dao.LayKhachHangTheoMaKH(maKH);
        }

        public List<KhachHang> LayDanhSach()
        {
            return _dao.LayDanhSach();
        }
    }
}