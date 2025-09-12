using QuanLyPhongKhachSan.Admin.UserControlAdmin;
using QuanLyPhongKhachSan.Login.UserControlAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            UserControlChucNang userControlChucNang = new UserControlChucNang();
            addUserControl(userControlChucNang);
        }

        private void addUserControl(UserControl c)
        {
            if (pnlContainer == null)
            {
                MessageBox.Show("panelContainer không tồn tại. Vui lòng kiểm tra Designer!");
                return;
            }
            pnlContainer.Controls.Clear();
            c.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(c);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            UserControlThongKe userControlThongKe = new UserControlThongKe();
            addUserControl(userControlThongKe);
        }

        private void btnChucNang_Click(object sender, EventArgs e)
        {
            UserControlChucNang userControlChucNang = new UserControlChucNang();
            addUserControl(userControlChucNang);

        }

        private void btnLichSuThayDoi_Click(object sender, EventArgs e)
        {
            UserControlLichSuThayDoi userControlLichSuThayDoi = new UserControlLichSuThayDoi();
            addUserControl(userControlLichSuThayDoi);
        }

        private void btnDanhSachKhachHang_Click(object sender, EventArgs e)
        {
            UserControlDanhSachTaiKhoan userControlDanhSachTaiKhoan = new UserControlDanhSachTaiKhoan();
            addUserControl(userControlDanhSachTaiKhoan);
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            UserControlTaiKhoanAdmin userControlAdmin = new UserControlTaiKhoanAdmin();
            addUserControl(userControlAdmin);
        }
    }
}
