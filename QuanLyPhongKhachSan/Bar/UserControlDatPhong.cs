using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlDatPhong : UserControl
    {
        private readonly PhongService phongService = new PhongService();
        private readonly List<Phong> danhSachPhong = new List<Phong>();

        public UserControlDatPhong()
        {
            InitializeComponent();
            if (txtSoPhong != null) txtSoPhong.PlaceholderText = "Số phòng";
            if (txtGiaPhong != null) txtGiaPhong.PlaceholderText = "Giá";
            if (cbLoaiPhong != null) cbLoaiPhong.Items.AddRange(new string[] {"Phòng đơn", "Phòng đôi", "Tiêu chuẩn", "VIP", "Deluxe" });
        }

        private void UserControlDatPhong_Load(object sender, EventArgs e)
        {
            LoadPhongFromDB();
        }

        private void LoadPhongFromDB()
        {
            danhSachPhong.Clear();
            flpContain.Controls.Clear(); 
            var danhSach = phongService.LayDanhSach();
            foreach (var p in danhSach)
            {
                danhSachPhong.Add(p);
                var pnl = TaoPhongMoi(p);
                flpContain.Controls.Add(pnl);
            }
            // Kiểm tra xem flpContain có hiển thị không
            flpContain.Visible = true;
            flpContain.AutoScroll = true; 
        }

        private Panel TaoPhongMoi(Phong phong)
        {
            var pnl = new Guna2Panel
            {
                Size = new Size(233, 114),
                BorderRadius = 30,
                Margin = new Padding(20),
                FillColor = Color.FromArgb(255, 192, 192),
                Tag = phong,
                Cursor = Cursors.Hand
            };

            var lblSoPhong = new Label
            {
                Location = new Point(14, 10),
                Font = new Font("Microsoft Tai Le", 11, FontStyle.Bold),
                AutoSize = true,
                Text = "Phòng " + phong.SoPhong,
                BackColor = Color.Transparent
            };

            var lblLoaiPhong = new Label
            {
                Location = new Point(14, 35),
                Font = new Font("Microsoft Tai Le", 10, FontStyle.Regular),
                AutoSize = true,
                Text = phong.LoaiPhong,
                BackColor = Color.Transparent
            };

            var lblGia = new Label
            {
                Location = new Point(14, 57),
                Font = new Font("Microsoft Tai Le", 10, FontStyle.Italic),
                AutoSize = true,
                Text = phong.Gia.ToString("N0") + ".000đ",
                BackColor = Color.Transparent
            };

            var lblKhach = new Label
            {
                Name = "lblKhach",
                Location = new Point(14, 80),
                Font = new Font("Microsoft Tai Le", 9, FontStyle.Bold),
                AutoSize = true,
                Text = "",
                BackColor = Color.Transparent
            };

            pnl.Controls.Add(lblSoPhong);
            pnl.Controls.Add(lblLoaiPhong);
            pnl.Controls.Add(lblGia);
            pnl.Controls.Add(lblKhach);

            var menu = new ContextMenuStrip();
            menu.Items.Add("Xóa").Click += (s, e) => XoaPhong(phong, pnl);
            pnl.ContextMenuStrip = menu;
            EventHandler openFormHandler = (s, e) => MoFormKhachHang(pnl);
            pnl.Click += openFormHandler;
            pnl.DoubleClick += openFormHandler;
            foreach (Control child in pnl.Controls)
            {
                child.Click += openFormHandler;
                child.DoubleClick += openFormHandler;
            }

            return pnl;
        }

        private void MoFormKhachHang(Guna2Panel panelPhong)
        {
            if (panelPhong == null) return;
            var phong = panelPhong.Tag as Phong;
            if (phong == null) return;

            try
            {
                using (var frm = new frmThemvaSuaKH(phong))
                {
                    var dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        string ten = frm.TenKhachHang?.Trim() ?? "";
                        string cccd = frm.CCCD?.Trim() ?? "";
                        string sdt = frm.SDT?.Trim() ?? "";

                        int maKh = UpsertKhachHang(ten, cccd, sdt);

                        var lbl = panelPhong.Controls.Find("lblKhach", true).FirstOrDefault() as Label;
                        if (lbl != null)
                        {
                            lbl.Text = (string.IsNullOrEmpty(ten) && string.IsNullOrEmpty(sdt)) ? "" : ("Khách: " + ten + " - " + sdt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form: " + ex.Message);
            }
        }

        private int UpsertKhachHang(string hoTen, string cccd, string sdt)
        {
            hoTen = hoTen?.Trim() ?? "";
            cccd = cccd?.Trim() ?? "";
            sdt = sdt?.Trim() ?? "";
            return -1; // Placeholder
        }

        private void XoaPhong(Phong phong, Guna2Panel pnl)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            phongService.Xoa(phong.MaPhong);
            danhSachPhong.Remove(phong);
            flpContain.Controls.Remove(pnl);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!!");
                return;
            }

            if (!int.TryParse(txtSoPhong.Text, out int soPhong) || soPhong <= 0)
            {
                MessageBox.Show("Vui lòng nhập số phòng hợp lệ!!");
                return;
            }

            if (danhSachPhong.Any(p => p.SoPhong == soPhong))
            {
                MessageBox.Show("Số phòng đã tồn tại!");
                return;
            }

            if (!decimal.TryParse(txtGiaPhong.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!!");
                return;
            }

            string loaiPhong = cbLoaiPhong.SelectedItem.ToString();
            string trangThai = "Trống";

            Phong phongMoi = new Phong(0, soPhong, loaiPhong, gia, trangThai);
            int maPhong = phongService.Them(phongMoi);
            if (maPhong > 0)
            {
                phongMoi.MaPhong = maPhong;
                danhSachPhong.Add(phongMoi);
                var pnl = TaoPhongMoi(phongMoi);
                flpContain.Controls.Add(pnl);
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại! Kiểm tra lại dữ liệu."); // Thông báo lỗi
            }

            txtSoPhong.Clear();
            txtGiaPhong.Clear();
            cbLoaiPhong.SelectedIndex = -1;
        }
    }
}