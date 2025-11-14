using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.Common;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using QuanLyPhongKhachSan.UI.Helpers;
using System.Windows.Forms;
using QuanLyPhongKhachSan.Login;

namespace QuanLyPhongKhachSan
{
    public partial class frmLogin : Form
    {
        private readonly TaiKhoanService taiKhoanService = new TaiKhoanService();

        public frmLogin()
        {
            InitializeComponent();
            txtUserName.PlaceholderText = "User name";
            txtPassword.PlaceholderText = "Password";
            txtPassword.PasswordChar = '●';


            this.KeyPreview = true;
            this.KeyDown += frmLogin_KeyDown;
            this.AcceptButton = btnDangNhap;

        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !cbShowPass.Checked;
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (ModifierKeys == Keys.Control)
                {
                    txtUserName.Clear();
                    txtPassword.Clear();
                }
                else
                {
                    if (this.ActiveControl is TextBoxBase tb)
                        tb.Clear();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtUserName.Text.Trim();
            string matKhau = txtPassword.Text.Trim();

            TaiKhoan taiKhoan = taiKhoanService.KiemTraDangNhap(tenDangNhap, matKhau);

            if (taiKhoan != null)
            {
                CurrentUser.MaTK = taiKhoan.MaTK;
                CurrentUser.MaNV = (taiKhoan.MaNV > 0) ? (int?)taiKhoan.MaNV : null;
                CurrentUser.TenDangNhap = taiKhoan.TenDangNhap;
                CurrentUser.Quyen = taiKhoan.Quyen;
                AuditHelper.SetCurrentUser(
                CurrentUser.MaNV,            // MaNV: để Admin grid map sang TenNV
                CurrentUser.TenDangNhap      // để log vẫn có user text nếu MaNV=null
                );
                // Lấy tên hiển thị + ngày tham gia từ CSDL (nếu có MaNV)
                string tenHienThi = taiKhoan.TenDangNhap;
                System.DateTime? ngayTG = null;

                if (CurrentUser.MaNV.HasValue)
                {
                    var nvDao = new NhanVienDAO();
                    var nv = nvDao.LayTheoMa(CurrentUser.MaNV.Value);
                    if (nv != null)
                    {
                        tenHienThi = string.IsNullOrWhiteSpace(nv.TenNV) ? tenHienThi : nv.TenNV;
                        ngayTG = nv.NgayThamGia;
                    }
                }
                AppSession.TaiKhoanDangNhap = taiKhoan;
                AppSession.MaNVHienTai = taiKhoan.MaNV; 

                string tenNV = null;
                if (taiKhoan.MaNV > 0)
                {
                    // Lấy tên NV nhanh gọn (thêm 1 helper nếu muốn)
                    var nvDao = new NhanVienDAO();
                    // Viết helper trong DAO:
                    // public string LayTenNV(int maNV) { SELECT TenNV FROM NhanVien WHERE MaNV=@id }
                    tenNV = nvDao.LayTenNV(taiKhoan.MaNV); // bạn thêm method này (1–2 dòng SQL)
                }

                AppSession.TenNhanVienHienThi =
                    !string.IsNullOrWhiteSpace(tenNV) ? tenNV
                    : (taiKhoan.Quyen == 1 ? "Admin" : taiKhoan.TenDangNhap);
                if (taiKhoan.Quyen == 1)
                {
                    var formAdmin = new frmAdmin();
                    formAdmin.Show();
                    this.Hide();
                }
                else if (taiKhoan.Quyen == 2)
                {
                    var formNV = new Form1();
                    formNV.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Quyền không hợp lệ!");
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            frmQuenMK f = new frmQuenMK();
            f.ShowDialog();
        }
    }
}
