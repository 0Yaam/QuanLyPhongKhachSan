// DAL/OL/NhanVienView.cs
using System;

namespace QuanLyPhongKhachSan.DAL.OL
{
    // DataPropertyName bạn nói: Ten, SDT, CCCD, TenTaiKhoan, MatKhau, NgayThamGia
    public class NhanVienView
    {
        public int MaNV { get; set; }          // ẩn trên lưới
        public int? MaTK { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgayThamGia { get; set; }
    }
}
