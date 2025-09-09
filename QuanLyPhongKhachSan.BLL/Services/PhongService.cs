using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class PhongService
    {
        private readonly PhongDAO _dao = new PhongDAO();
        private readonly DatPhongDAO _repoDatPhong = new DatPhongDAO();

        public List<Phong> LayDanhSach()
        {
            return _dao.LayDanhSach();
        }

        public int Them(Phong p)
        {
            if (p.SoPhong <= 0 || string.IsNullOrEmpty(p.LoaiPhong) || p.Gia <= 0)
            {
                return -1;
            }
            return _dao.Them(p);
        }

        public void Xoa(int maPhong)
        {
            _dao.Xoa(maPhong);
        }

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            return _repoDatPhong.LayDatPhongTheoMaPhong(maPhong);
        }

        public int ThemDatPhong(DatPhong datPhong)
        {
            if (datPhong.MaKH <= 0 || datPhong.MaPhong <= 0
                || datPhong.NgayNhan == DateTime.MinValue
                || datPhong.NgayTraDuKien == DateTime.MinValue)
            {
                return -1;
            }

            // DÙNG ĐÚNG DAO của bảng DatPhong:
            return _repoDatPhong.Them(datPhong);
        }

        public bool KiemTraPhongTrungLichExcept(int maPhong, DateTime nhan, DateTime tra, int excludeMaDat)
        {
            return _repoDatPhong.KiemTraPhongTrungLichExcept(maPhong, nhan, tra, excludeMaDat);
        }

        public bool CapNhatDatPhong(DatPhong dat)
        {
            return _repoDatPhong.Update(dat) > 0;
        }

        public bool KiemTraPhongTrungLich(int maPhong, DateTime nhan, DateTime tra)
        {
            return _repoDatPhong.KiemTraPhongTrungLich(maPhong, nhan, tra);
        }
    }
}