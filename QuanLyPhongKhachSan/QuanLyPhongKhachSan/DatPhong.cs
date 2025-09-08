using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan
{
    public class DatPhong
    {
        public int MaDat { get; set; }
        public int MaKH { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public decimal TienCoc { get; set; }
        public decimal TienThue { get; set; }
        public string TrangThai { get; set; }

        public DatPhong() { }

        public DatPhong(int maDat, int maKH, int maPhong, DateTime ngayNhan, DateTime ngayTraDuKien,
                        DateTime? ngayTraThucTe, decimal tienCoc, decimal tienThue, string trangThai)
        {
            MaDat = maDat;
            MaKH = maKH;
            MaPhong = maPhong;
            NgayNhan = ngayNhan;
            NgayTraDuKien = ngayTraDuKien;
            NgayTraThucTe = ngayTraThucTe;
            TienCoc = tienCoc;
            TienThue = tienThue;
            TrangThai = trangThai;
        }
    }
}
