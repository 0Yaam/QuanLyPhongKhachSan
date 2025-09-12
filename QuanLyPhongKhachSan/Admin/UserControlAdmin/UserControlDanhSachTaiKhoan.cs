using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyPhongKhachSan.Admin;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlDanhSachTaiKhoan : UserControl
    {
        public UserControlDanhSachTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemNhanVien frm = new frmThemNhanVien();
            frm.ShowDialog();
        }
    }
}
