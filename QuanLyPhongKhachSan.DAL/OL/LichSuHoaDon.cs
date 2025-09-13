using System;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class LichSuHoaDon
    {
        public int Id { get; set; }
        public int MaHD { get; set; } // Sử dụng 0 thay vì null
        public int MaDat { get; set; } // Sử dụng 0 thay vì null
        public DateTime ThoiGianIn { get; set; }
        public int MaNV { get; set; } // Sử dụng 0 thay vì null

        // Thuộc tính bổ sung để hiển thị, không lưu vào CSDL
        public string TenKH { get; set; } // Sử dụng string.Empty thay vì null
        public string CCCD { get; set; } // Sử dụng string.Empty thay vì null
        public string SDT { get; set; } // Sử dụng string.Empty thay vì null
        public int SoPhong { get; set; } // Sử dụng 0 thay vì null
        public string LoaiHoaDon { get; set; } // Sử dụng string.Empty thay vì null
    }
}