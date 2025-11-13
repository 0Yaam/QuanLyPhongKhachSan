using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login
{
    public partial class frmQuenMK : Form
    {
        public frmQuenMK()
        {
            InitializeComponent();

            txtCCCD.PlaceholderText = "Nhập số CCCD";
            txtMatKhau.PlaceholderText = "Mật khẩu được cấp";
            txtSDT.PlaceholderText = "Nhập số điện thoại";
        }
    }
}
