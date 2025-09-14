using System;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class LichSuHoaDon
    {
        public int Id { get; set; }
        public int MaHD { get; set; }
        public int MaDat { get; set; }
        public DateTime ThoiGianIn { get; set; }
        public int MaNV { get; set; }
        public string SoPhong { get; set; } 
        public string TenKH { get; set; }  
        public string CCCD { get; set; }    
        public string SDT { get; set; }    
        public string LoaiHoaDon { get; set; } 
    }
}