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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
