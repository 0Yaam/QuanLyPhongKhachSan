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
        public string LoaiPhong { get; set; }   // lấy ra khi JOIN
        public decimal Gia { get; set; }        // lấy ra khi JOIN
        public string TrangThai { get; set; }
        public int MaLoaiPhong { get; set; }    // 🔹 thêm

        public Phong() { }

        // Dùng khi thêm mới theo schema mới: chỉ cần SoPhong + MaLoaiPhong (+ TrangThai)
        public Phong(int maPhong, int soPhong, int maLoaiPhong, string trangThai)
        {
            MaPhong = maPhong;
            SoPhong = soPhong;
            MaLoaiPhong = maLoaiPhong;
            TrangThai = trangThai;
        }

        // (giữ nguyên overload cũ nếu nơi khác còn dùng)
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
