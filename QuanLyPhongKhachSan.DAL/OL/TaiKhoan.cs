namespace QuanLyPhongKhachSan.DAL.OL
{
    public class TaiKhoan
    {
        public int MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int Quyen { get; set; }
        public int MaNV { get; set; }

        public TaiKhoan()
        {
            
        }
        public TaiKhoan(string tenDangNhap, string matKhau, int quyen,int maNV)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            Quyen = quyen;
            MaNV = maNV;

        }
    }
}