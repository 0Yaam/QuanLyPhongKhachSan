// Common/AppSession.cs
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.Common
{
    public static class AppSession
    {
        public static TaiKhoan TaiKhoanDangNhap { get; set; }   // giữ MaTK, TenDangNhap, Quyen, MaNV
        public static int MaNVHienTai { get; set; }             // 0 nếu admin không map NV
        public static string TenNhanVienHienThi { get; set; }   // Ví dụ: "Nguyễn A" hoặc "Admin"
    }
}
