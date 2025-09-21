using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlChucNang : UserControl
    {
        public UserControlChucNang()
        {
            InitializeComponent();
            txtTrangThai.PlaceholderText = "Thêm trạng thái phòng";
            txtLoaiPhong.PlaceholderText = "Thêm loại phòng";
        }


        private void txtLoaiPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTrangThai_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nudGiaTien_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {

        }

        private void xóaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvTrangThai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLoaiPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
