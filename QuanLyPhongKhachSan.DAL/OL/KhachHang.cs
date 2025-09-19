using System;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class KhachHang
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public DateTime? NgayThamGia { get; set; }

        public KhachHang() { }
        public KhachHang(string hoTen, string cccd, string sdt)
        {
            HoTen = hoTen;
            CCCD = cccd;
            SDT = sdt;
            NgayThamGia = DateTime.Now; // Tự động gán thời gian hiện tại
        }
    }
}