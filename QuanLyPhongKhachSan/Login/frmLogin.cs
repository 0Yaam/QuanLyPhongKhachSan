using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Windows.Forms;
using QuanLyPhongKhachSan.BLL.Services;

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


            // Bắt phím toàn form
            this.KeyPreview = true;
            this.KeyDown += frmLogin_KeyDown;
            this.AcceptButton = btnDangNhap;

        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            // đảo trạng thái ẩn/hiện
            txtPassword.UseSystemPasswordChar = !cbShowPass.Checked;
        }

        // ENTER/DELETE
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Cách 1: perform click
                btnDangNhap.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Delete: xóa ô hiện tại. Ctrl+Delete: xóa cả 2 ô
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
    }
}
