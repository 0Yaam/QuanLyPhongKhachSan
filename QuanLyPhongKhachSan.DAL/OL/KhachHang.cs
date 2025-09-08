using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class KhachHang
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }

        public KhachHang() { }
        public KhachHang(string hoTen, string cccd, string sdt)
        {
            HoTen = hoTen;
            CCCD = cccd;
            SDT = sdt;
        }
    }
}
