using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class HoaDon
    {
        public int MaHD { get; set; }
        public int MaDat { get; set; }
        public DateTime NgayLap { get; set; }
        public string LoaiHoaDon { get; set; }
        public decimal? TienThue { get; set; }
        public decimal? TienCoc { get; set; }
        public decimal? TongThanhToan { get; set; }
        public decimal? TienHoanTra { get; set; }
        public decimal? TienBoiThuong { get; set; }
        public string GhiChu { get; set; }

        public HoaDon() { }

        public HoaDon(int maHD, int maDat, DateTime ngayLap, string loaiHoaDon, decimal? tienThue, decimal? tienCoc,
                      decimal? tongThanhToan, decimal? tienHoanTra, decimal? tienBoiThuong, string ghiChu)
        {
            MaHD = maHD;
            MaDat = maDat;
            NgayLap = ngayLap;
            LoaiHoaDon = loaiHoaDon;
            TienThue = tienThue;
            TienCoc = tienCoc;
            TongThanhToan = tongThanhToan;
            TienHoanTra = tienHoanTra;
            TienBoiThuong = tienBoiThuong;
            GhiChu = ghiChu;
        }
    }
}
