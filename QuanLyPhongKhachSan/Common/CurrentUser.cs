namespace QuanLyPhongKhachSan.Common
{
    public static class CurrentUser
    {
        public static int MaTK { get; set; }
        public static int? MaNV { get; set; }
        public static string TenDangNhap { get; set; }
        public static int Quyen { get; set; }

        // tiện cho hiển thị/in ấn
        public static string TenHienThi { get; set; }
        public static System.DateTime? NgayThamGia { get; set; }

        public static void Reset()
        {
            MaTK = 0; MaNV = null; TenDangNhap = null; Quyen = 0;
            TenHienThi = null; NgayThamGia = null;
        }
    }
}
