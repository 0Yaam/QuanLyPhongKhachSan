using QuanLyPhongKhachSan.Bar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UserControlDatPhong phong = new UserControlDatPhong();
            addUserControl(phong);
        }
        private void addUserControl(UserControl c)
        {
            panelContainer.Controls.Clear();
            c.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(c);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UserControlDatPhong phong = new UserControlDatPhong();
            addUserControl(phong);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UserControlDanhSachKhachHang khachHang = new UserControlDanhSachKhachHang();
            addUserControl(khachHang);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UserControlThongKe thongKe = new UserControlThongKe();
            addUserControl(thongKe);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Reset tất cả nút trong panelNav về màu mặc định
            foreach (Control ctrl in pnContainBar.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                {
                    btn.FillColor = Color.LightGray; // màu bình thường
                }
            }

            // Tô đậm cho nút vừa click
            Guna.UI2.WinForms.Guna2Button clickedBtn = (Guna.UI2.WinForms.Guna2Button)sender;
            clickedBtn.FillColor = Color.DodgerBlue;

            // Load user control
            UserControlTaiKhoan phong = new UserControlTaiKhoan();
            addUserControl(phong);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnContainBar.Controls)
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                    btn.FillColor = Color.LightGray; // màu bình thường

            // Tô đậm cho nút vừa click
            Guna.UI2.WinForms.Guna2Button clickedBtn = (Guna.UI2.WinForms.Guna2Button)sender;
            clickedBtn.FillColor = ColorTranslator.FromHtml("#D7E4F2"); // hex

            // Load user control
            UserControlDatPhong datPhong = new UserControlDatPhong();
            addUserControl(datPhong);
        }

        private void btnDanhSachKhachHang_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnContainBar.Controls)
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                    btn.FillColor = Color.LightGray; 

            Guna.UI2.WinForms.Guna2Button clickedBtn = (Guna.UI2.WinForms.Guna2Button)sender;
            clickedBtn.FillColor = ColorTranslator.FromHtml("#D7E4F2"); 

            UserControlDanhSachKhachHang ds  = new UserControlDanhSachKhachHang();
            addUserControl(ds);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnContainBar.Controls)
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                    btn.FillColor = Color.LightGray;

            Guna.UI2.WinForms.Guna2Button clickedBtn = (Guna.UI2.WinForms.Guna2Button)sender;
            clickedBtn.FillColor = ColorTranslator.FromHtml("#D7E4F2");

            UserControlThongKe thongKe = new UserControlThongKe();
            addUserControl(thongKe);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnContainBar.Controls)
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                    btn.FillColor = Color.LightGray;

            Guna.UI2.WinForms.Guna2Button clickedBtn = (Guna.UI2.WinForms.Guna2Button)sender;
            clickedBtn.FillColor = ColorTranslator.FromHtml("#D7E4F2");

            UserControlTaiKhoan tk = new UserControlTaiKhoan();
            addUserControl(tk);
        }
    }
}
