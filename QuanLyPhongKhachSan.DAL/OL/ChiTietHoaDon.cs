using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class ChiTietHoaDon // Lớp mới
    {
        public int MaCTHD { get; set; }
        public int MaHD { get; set; }
        public string TenDichVu { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }

        public ChiTietHoaDon()
        {
            
        }

        public ChiTietHoaDon(int maCTHD, int maHD, string tenDichVu, int soLuong, decimal gia)
        {
            maCTHD = MaCTHD;
            MaHD = maHD;
            TenDichVu = tenDichVu;
            SoLuong = soLuong;
            Gia = gia;
        }
    }
    

}
