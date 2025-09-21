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
            using (var frm = new frmThemNhanVien())
            {
                var dr = frm.ShowDialog();      // mở modal
                if (dr == DialogResult.OK)      // nếu thêm thành công
                {
                    LoadGrid();                 // -> reload
                }
            }
        }
        private void UserControlDanhSachTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgvDanhSachTaiKhoan.AutoGenerateColumns = false;
                var data = nhansu.LayDanhSachNhanVien();
                // ép refresh mạnh tay tránh cache binding
                dgvDanhSachTaiKhoan.DataSource = null;
                dgvDanhSachTaiKhoan.DataSource = data;
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
