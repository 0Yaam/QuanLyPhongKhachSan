//Phongservice
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class PhongService
    {
        private PhongDAO dao = new PhongDAO();
        private readonly DatPhongDAO _repoDatPhong = new DatPhongDAO();
        private readonly PhongDAO _repoPhong = new PhongDAO(); // nếu bạn đã có

        public List<Phong> LayDanhSach()
        {
            return dao.LayDanhSach();
        }

        public int Them(Phong p)
        {
            if (p.SoPhong <= 0)
            {
                Console.WriteLine("Số phòng phải lớn hơn 0!");
                return -1;
            }
            if (string.IsNullOrEmpty(p.LoaiPhong))
            {
                Console.WriteLine("Loại phòng không được để trống!");
                return -1;
            }
            if (p.Gia <= 0)
            {
                Console.WriteLine("Giá phải lớn hơn 0!");
                return -1;
            }
            return dao.Them(p);
        }

        public void Xoa(int maPhong)
        {
            dao.Xoa(maPhong);
        }

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            return dao.LayDatPhongTheoMaPhong(maPhong);
        }

        public int ThemDatPhong(DatPhong datPhong)
        {
            if (datPhong.MaKH <= 0 || datPhong.MaPhong <= 0 || datPhong.NgayNhan == DateTime.MinValue || datPhong.NgayTraDuKien == DateTime.MinValue)
            {
                Console.WriteLine("Thông tin đặt phòng không hợp lệ!");
                return -1;
            }
            return dao.ThemDatPhong(datPhong);
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
            => _repoDatPhong.KiemTraPhongTrungLich(maPhong, nhan, tra);



    }
}