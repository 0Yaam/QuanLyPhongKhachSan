using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<string> LayDanhSachLoaiPhong()
        {
            return _dao.LayDanhSachLoaiPhong();
        }

        public List<Phong> LayDanhSachSapXep(string loaiPhong, string trangThai, bool tangDan)
        {
            var phongList = _dao.LayDanhSach();

            if (!string.IsNullOrEmpty(loaiPhong) && loaiPhong != "None")
            {
                phongList = phongList.Where(p => p.LoaiPhong == loaiPhong).ToList();
            }

            if (!string.IsNullOrEmpty(trangThai))
            {
                phongList = phongList.Where(p => p.TrangThai == trangThai).ToList();
            }

            if (tangDan)
            {
                phongList = phongList.OrderBy(p => p.SoPhong).ToList();
            }
            else
            {
                phongList = phongList.OrderByDescending(p => p.SoPhong).ToList();
            }

            return phongList;
        }

        public bool CapNhat(Phong phong)
        {
            if (phong == null || phong.MaPhong <= 0 || string.IsNullOrEmpty(phong.LoaiPhong) || phong.Gia <= 0)
            {
                return false;
            }
            return _dao.CapNhat(phong) > 0;
        }

        public Phong LayPhongTheoMaPhong(int maPhong)
        {
            return _dao.LayDanhSach().FirstOrDefault(p => p.MaPhong == maPhong);
        }
    }
}