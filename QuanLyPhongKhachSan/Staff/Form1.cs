using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.Bar;
using QuanLyPhongKhachSan.Staff.UserControlStaff;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class Form1 : Form
    {
        // Khởi tạo instance cho các UserControl, tái sử dụng thay vì tạo mới
        private UserControlDatPhong datPhongInstance;
        private UserControlDanhSachKhachHang danhSachKhachHangInstance;
        private UserControlThongKe thongKeInstance;
        private UserControlTaiKhoan taiKhoanInstance;

        public Form1()
        {
            InitializeComponent();
            datPhongInstance = new UserControlDatPhong();
            danhSachKhachHangInstance = new UserControlDanhSachKhachHang();
            thongKeInstance = new UserControlThongKe();
            taiKhoanInstance = new UserControlTaiKhoan();

            // Load UserControl mặc định khi mở form
            addUserControl(datPhongInstance);
        }

        private void addUserControl(UserControl c)
        {
            if (panelContainer == null)
            {
                MessageBox.Show("panelContainer không tồn tại. Vui lòng kiểm tra Designer!");
                return;
            }
            panelContainer.Controls.Clear();
            c.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(c);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            addUserControl(datPhongInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            addUserControl(danhSachKhachHangInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            addUserControl(thongKeInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            addUserControl(taiKhoanInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Chưa có UserControl tương ứng, bạn có thể thêm sau
            CapNhatMauNut(sender);
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            addUserControl(datPhongInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void btnDanhSachKhachHang_Click(object sender, EventArgs e)
        {
            addUserControl(danhSachKhachHangInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            addUserControl(thongKeInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            addUserControl(taiKhoanInstance); // Tái sử dụng instance
            CapNhatMauNut(sender);
        }

        private void CapNhatMauNut(object sender)
        {
            if (pnContainBar == null)
            {
                MessageBox.Show("pnContainBar không tồn tại. Vui lòng kiểm tra Designer!");
                return;
            }
            // Reset tất cả nút trong pnContainBar về màu mặc định
            foreach (Control ctrl in pnContainBar.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.FillColor = Color.LightGray; // Màu mặc định
                }
            }

            // Tô đậm cho nút vừa click
            if (sender is Guna2Button clickedBtn)
            {
                clickedBtn.FillColor = ColorTranslator.FromHtml("#D7E4F2"); // Màu chọn
            }
        }
    }
}