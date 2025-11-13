using System;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class NhanVienView
    {
        public int MaNV { get; set; } // Cho phép MaNV=0 cho admin
        public int? MaTK { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string ChucVu { get; set; } // Hiển thị "Admin" hoặc "Nhân viên" dựa trên Quyen
        public int? Quyen { get; set; } // Lưu quyền từ TaiKhoan
        public DateTime NgayThamGia { get; set; }
    }
}