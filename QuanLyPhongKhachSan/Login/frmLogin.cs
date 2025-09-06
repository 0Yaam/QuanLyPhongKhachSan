using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongKhachSan.BLL.Services;

namespace QuanLyPhongKhachSan
{
    public partial class frmLogin : Form
    {
        private TaiKhoanService taiKhoanService = new TaiKhoanService();


        public frmLogin()
        {
            InitializeComponent();
            txtUserName.PlaceholderText = "User name";
            txtPassword.PlaceholderText = "Password";
        }
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }else
            {
                txtPassword.UseSystemPasswordChar = true;
            }    
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '●';
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
                    frmAdmin formAdmin = new frmAdmin();
                    formAdmin.Show();
                    this.Hide();
                }
                else if (taiKhoan.Quyen == 2)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
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
