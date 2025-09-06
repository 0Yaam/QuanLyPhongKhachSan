using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class PhongService
    {
        private PhongDAO dao = new PhongDAO();

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
            return dao.Them(p); // Kiểm tra dao.Them có lỗi không
        }

        public void Xoa(int maPhong)
        {
            dao.Xoa(maPhong);
        }
    }
}