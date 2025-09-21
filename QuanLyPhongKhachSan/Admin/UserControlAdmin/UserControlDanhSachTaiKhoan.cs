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
using QuanLyPhongKhachSan.BLL.Services;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlDanhSachTaiKhoan : UserControl
    {
        private readonly NhanSuService nhansu = new NhanSuService();
        public UserControlDanhSachTaiKhoan()
        {
            InitializeComponent();
            this.Load += UserControlDanhSachTaiKhoan_Load;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmThemNhanVien frm = new frmThemNhanVien();
            frm.ShowDialog();
        }

        private void UserControlDanhSachTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDanhSachTaiKhoan.AutoGenerateColumns = false; // bạn đã set DataPropertyName trong Designer
                dgvDanhSachTaiKhoan.DataSource = nhansu.LayDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách nhân viên: " + ex.Message);
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rdSSDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSCCCD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSTen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void rdGiam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdTang_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmsReset_Click(object sender, EventArgs e)
        {

        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {

        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
