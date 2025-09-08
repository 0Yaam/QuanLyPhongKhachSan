using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class Phong
    {
        public int MaPhong { get; set; }
        public int SoPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal Gia { get; set; }
        public string TrangThai { get; set; }

        public Phong()
        {

        }

        public Phong(int maPhong, int soPhong, string loaiPhong, decimal gia, string trangThai)
        {
            MaPhong = maPhong;
            SoPhong = soPhong;
            LoaiPhong = loaiPhong;
            Gia = gia;
            TrangThai = trangThai;
        }
    }
}
