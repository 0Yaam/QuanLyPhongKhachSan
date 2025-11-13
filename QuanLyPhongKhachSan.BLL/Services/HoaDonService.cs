using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class HoaDonService
    {
        private readonly HoaDonDAO _dao = new HoaDonDAO();

        public int TaoHoaDon(HoaDon hd) => _dao.ThemVaLayMa(hd);

        public List<HoaDon> LayDanhSach() => _dao.LayDanhSach();

        public int ThemVaTraMa(HoaDon hd)
        {
            if (hd == null || hd.MaDat <= 0) return 0;
            return _dao.ThemVaTraMa(hd);
        }
        public int ThemVaLayMa(HoaDon hd)
        {
            if (hd == null) return 0;
            if (hd.NgayLap == default) hd.NgayLap = DateTime.Now;
            if (string.IsNullOrWhiteSpace(hd.LoaiHoaDon)) hd.LoaiHoaDon = "Lần 1";
            return _dao.ThemVaLayMa(hd); // <<< QUAN TRỌNG: gọi đúng hàm
        }

        public bool CapNhatTongTien(int maHD, decimal tong)
        {
            if (maHD <= 0) return false;
            return _dao.CapNhatTongTien(maHD, tong) > 0;
        }
        public bool CapNhatTongTienVaGhiChu(int maHD, decimal tong, string ghiChu)
        {
            if (maHD <= 0) return false;
            return _dao.CapNhatTongTienVaGhiChu(maHD, tong, ghiChu) > 0;
        }

        public HoaDon LayTheoMa(int maHD) => _dao.LayTheoMa(maHD);

        public HoaDon LayLan1TheoMaDat(int maDat)
        {
            var all = _dao.LayDanhSach();
            return all.FirstOrDefault(x => x.MaDat == maDat && string.Equals(x.LoaiHoaDon, "Lần 1", StringComparison.OrdinalIgnoreCase));
        }


    }
}
