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
    public partial class frmThemvaSuaKH : Form
    {
        private Phong phong;
        public frmThemvaSuaKH(Phong phong)
        {
            InitializeComponent();
            txtSoPhong.Text = phong.SoPhong.ToString();
            txtGia .Text = phong.Gia.ToString();
            cbLoaiPhong.Text = phong.LoaiPhong;
        }
    }

}
