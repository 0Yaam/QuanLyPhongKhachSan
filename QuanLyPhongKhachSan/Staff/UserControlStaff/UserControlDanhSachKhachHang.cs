using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlDanhSachKhachHang : UserControl
    {
        public UserControlDanhSachKhachHang()
        {
            InitializeComponent();
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
        }

    }
}
