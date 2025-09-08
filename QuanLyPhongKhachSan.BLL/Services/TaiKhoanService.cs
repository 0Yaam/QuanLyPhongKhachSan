using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System.Collections.Generic;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class TaiKhoanService
    {
        private TaiKhoanDAO dao = new TaiKhoanDAO();

        public TaiKhoan KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                return null;
            }
            return dao.KiemTraDangNhap(tenDangNhap, matKhau);
        }

        public List<TaiKhoan> LayDanhSachTaiKhoan()
        {
            return dao.LayDanhSachTaiKhoan();
        }

        public bool CapNhatMatKhau(int maTK, string matKhauMoi)
        {
            if (string.IsNullOrEmpty(matKhauMoi))
            {
                return false;
            }
            return dao.CapNhatMatKhau(maTK, matKhauMoi);
        }

        public bool ThemTaiKhoan(TaiKhoan taiKhoan)
        {
            if (string.IsNullOrEmpty(taiKhoan.TenDangNhap) || string.IsNullOrEmpty(taiKhoan.MatKhau) || taiKhoan.Quyen == 0)
            {
                return false;
            }
            return dao.ThemTaiKhoan(taiKhoan);
        }

        public bool XoaTaiKhoan(int maTK)
        {
            return dao.XoaTaiKhoan(maTK);
        }
    }
}