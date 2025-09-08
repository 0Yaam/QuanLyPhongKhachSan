using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan
{
    public class KhachHang
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string SDT { get; set; }

        public KhachHang() { }
        public KhachHang(int maKH, string hoTen, string cmnd, string sdt)
        {
            MaKH = maKH;
            HoTen = hoTen;
            CMND = cmnd;
            SDT = sdt;
        }
    }
}
