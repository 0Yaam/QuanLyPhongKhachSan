using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    public partial class UserControlDatPhong : UserControl
    {
        private readonly PhongService phongService = new PhongService();
        private readonly KhachHangService khachHangService = new KhachHangService();
        private readonly List<Phong> danhSachPhong = new List<Phong>();
        private readonly HashSet<int> _selectedRoomIds = new HashSet<int>();

        public UserControlDatPhong()
        {
            InitializeComponent();
            this.Load += UserControlDatPhong_Load;

            if (dtpNgayHienTai != null) dtpNgayHienTai.Value = DateTime.Now;
            if (txtSoPhong != null) txtSoPhong.PlaceholderText = "Số phòng";
            if (cbLoaiPhong != null)
            {
                cbLoaiPhong.Items.Clear();
                cbLoaiPhong.Items.AddRange(new string[] { "Phòng đơn", "Phòng đôi", "Tiêu chuẩn", "VIP", "Deluxe" });
            }
            if (btnThemKH != null) btnThemKH.Click += btnThemKH_Click;
            this.TabStop = true;
            this.KeyDown += UserControl_KeyDown;
            if (flpContain != null)
            {
                flpContain.TabStop = true;
                flpContain.KeyDown += UserControl_KeyDown;
            }
        }

        private void UserControlDatPhong_Load(object sender, EventArgs e)
        {
            var frm = this.FindForm();
            if (frm != null)
            {
                frm.KeyPreview = true;
                frm.KeyDown -= UserControl_KeyDown;
                frm.KeyDown += UserControl_KeyDown;
            }
            LoadPhongFromDB();
        }

        private void LoadPhongFromDB()
        {
            _selectedRoomIds.Clear();
            danhSachPhong.Clear();
            flpContain.Controls.Clear();

            var danhSach = phongService.LayDanhSach();
            foreach (var p in danhSach)
            {
                danhSachPhong.Add(p);
                var pnl = TaoPhongMoi(p);
                flpContain.Controls.Add(pnl);
            }

            flpContain.Visible = true;
            flpContain.AutoScroll = true;
        }

        private Guna2Panel TaoPhongMoi(Phong phong)
        {
            var pnl = new Guna2Panel
            {
                Size = new Size(233, 114),
                BorderRadius = 30,
                Margin = new Padding(20),
                FillColor = Color.FromArgb(255, 192, 192),
                Tag = phong,
                Cursor = Cursors.Hand,
                BorderThickness = 0,
                BorderColor = Color.Transparent,
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
                Text = PhongGiaConfig.GiaPhong[phong.LoaiPhong].ToString("N0") + "đ",
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

            pnl.Controls.AddRange(new Control[] { lblSoPhong, lblLoaiPhong, lblGia, lblKhach });
            HienKhachLenPanel(phong, lblKhach, pnl);

            pnl.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    ToggleSelect((Guna2Panel)s);
            };

            pnl.DoubleClick += (s, e) => HandleDoubleClickOpen(pnl);

            foreach (Control child in pnl.Controls)
            {
                child.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        ToggleSelect(pnl);
                };
                child.DoubleClick += (s, e) => HandleDoubleClickOpen(pnl);
            }

            var menu = new ContextMenuStrip();
            menu.Items.Add("Xóa").Click += (s, e) =>
            {
                XoaPhongSelectedOrOne(pnl);
            };
            pnl.ContextMenuStrip = menu;

            SetSelected(pnl, false);
            return pnl;
        }

        private DatPhong GetDatPhongDeHien(int maPhong)
        {
            var dat = phongService.LayDatPhongTheoMaPhong(maPhong);
            System.Diagnostics.Debug.WriteLine($"GetDatPhongDeHien(MaPhong={maPhong}): {(dat != null ? $"MaDat={dat.MaDat}, MaKH={dat.MaKH}, TrangThai={dat.TrangThai}, NgayTraThucTe={(dat.NgayTraThucTe.HasValue ? dat.NgayTraThucTe.ToString() : "null")}" : "null")}");
            return dat; // Bỏ điều kiện để kiểm tra nguyên nhân
        }

        private void HienKhachLenPanel(Phong phong, Label lblKhach, Guna2Panel pnl)
        {
            try
            {
                var dat = GetDatPhongDeHien(phong.MaPhong);
                if (dat == null)
                {
                    System.Diagnostics.Debug.WriteLine($"HienKhachLenPanel: No valid DatPhong for MaPhong={phong.MaPhong}");
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192); // trống
                    return;
                }

                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                System.Diagnostics.Debug.WriteLine($"HienKhachLenPanel: LayKhachHangTheoMaKH(MaKH={dat.MaKH}): {(kh != null ? $"HoTen={kh.HoTen}, SDT={kh.SDT}" : "null")}");
                string ten = kh?.HoTen ?? "";
                string sdt = kh?.SDT ?? "";

                lblKhach.Text = (string.IsNullOrEmpty(ten) && string.IsNullOrEmpty(sdt))
                    ? ""
                    : ("Khách: " + ten + " - " + sdt);

                pnl.FillColor = dat.TrangThai == "Đang sử dụng"
                    ? Color.FromArgb(255, 235, 150) // vàng
                    : dat.TrangThai == "Đã đặt"
                        ? Color.FromArgb(180, 220, 255) // xanh nhạt
                        : Color.FromArgb(255, 192, 192); // trống
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"HienKhachLenPanel: Error for MaPhong={phong.MaPhong}: {ex.Message}");
                lblKhach.Text = "";
                pnl.FillColor = Color.FromArgb(255, 192, 192);
                MessageBox.Show($"Lỗi hiển thị khách hàng phòng {phong.SoPhong}: {ex.Message}");
            }
        }

        private void HandleDoubleClickOpen(Guna2Panel pnl)
        {
            if (pnl == null) return;
            var p = pnl.Tag as Phong;
            if (p == null) return;

            if (_selectedRoomIds.Count != 1 || !_selectedRoomIds.Contains(p.MaPhong))
            {
                foreach (Control c in flpContain.Controls)
                {
                    var gp = c as Guna2Panel;
                    if (gp == null) continue;
                    SetSelected(gp, false);
                }
                _selectedRoomIds.Clear();

                _selectedRoomIds.Add(p.MaPhong);
                SetSelected(pnl, true);
            }

            MoFormKhachHang(pnl);
        }

        private void SetSelected(Guna2Panel pnl, bool selected)
        {
            pnl.BorderThickness = selected ? 3 : 0;
            pnl.BorderColor = selected ? Color.DodgerBlue : Color.Transparent;
            pnl.ShadowDecoration.Enabled = selected;
            pnl.ShadowDecoration.Depth = selected ? 8 : 0;
        }

        private void ToggleSelect(Guna2Panel pnl)
        {
            if (pnl == null) return;
            this.Focus();
            var p = pnl.Tag as Phong;
            if (p == null) return;

            if (_selectedRoomIds.Contains(p.MaPhong))
            {
                _selectedRoomIds.Remove(p.MaPhong);
                SetSelected(pnl, false);
            }
            else
            {
                _selectedRoomIds.Add(p.MaPhong);
                SetSelected(pnl, true);
            }
        }

        private List<Phong> GetSelectedRooms()
        {
            return danhSachPhong.Where(p => _selectedRoomIds.Contains(p.MaPhong)).ToList();
        }

        private static string ComputeTrangThai(DateTime ngayNhan, DateTime ngayTraDuKien)
        {
            var today = DateTime.Today;
            return (today >= ngayNhan.Date && today < ngayTraDuKien.Date) ? "Đang sử dụng" : "Đã đặt";
        }

        private void MoFormKhachHang(Guna2Panel panelPhong)
        {
            if (panelPhong == null) return;
            var phong = panelPhong.Tag as Phong;
            if (phong == null) return;

            try
            {
                using (var frmthemvasua = new frmThemvaSuaKH(phong))
                {
                    var dr = frmthemvasua.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        string ten = (frmthemvasua.TenKhachHang ?? "").Trim();
                        string cccd = (frmthemvasua.CCCD ?? "").Trim();
                        string sdt = (frmthemvasua.SDT ?? "").Trim();
                        DateTime ngayNhan = frmthemvasua.NgayNhan;
                        DateTime ngayTraDuKien = frmthemvasua.NgayTraDuKien;
                        decimal tienCoc = frmthemvasua.TienCoc;
                        decimal tienThue = frmthemvasua.TienThue;

                        int maKh = khachHangService.UpsertKhachHang(ten, cccd, sdt);
                        if (maKh <= 0)
                        {
                            MessageBox.Show("Lưu thông tin khách hàng thất bại!");
                            return;
                        }

                        string trangThai = ComputeTrangThai(ngayNhan, ngayTraDuKien);
                        var datPhong = new DatPhong(
                            0, maKh, phong.MaPhong,
                            ngayNhan, ngayTraDuKien, null,
                            tienCoc, tienThue, trangThai
                        );

                        var dat = phongService.LayDatPhongTheoMaPhong(phong.MaPhong);
                        bool result;
                        if (dat != null && (dat.TrangThai == "Đã đặt" || dat.TrangThai == "Đang sử dụng"))
                        {
                            datPhong.MaDat = dat.MaDat;
                            datPhong.TrangThai = trangThai;
                            result = phongService.CapNhatDatPhong(datPhong);
                        }
                        else
                        {
                            result = phongService.ThemDatPhong(datPhong) > 0;
                        }

                        if (result)
                        {
                            RefreshPhongPanel(panelPhong);
                        }
                        else
                        {
                            MessageBox.Show("Lưu thông tin đặt phòng thất bại!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form: " + ex.Message);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                XoaPhongSelectedOrOne(null);
                e.Handled = true;
            }
        }

        private void XoaPhongSelectedOrOne(Guna2Panel contextPanelIfNoneSelected)
        {
            var ids = new List<int>();
            if (_selectedRoomIds.Count > 0)
            {
                ids.AddRange(_selectedRoomIds);
            }
            else if (contextPanelIfNoneSelected != null)
            {
                var p = contextPanelIfNoneSelected.Tag as Phong;
                if (p != null) ids.Add(p.MaPhong);
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("Không có phòng nào được chọn để xóa.");
                return;
            }

            var soPhongs = danhSachPhong
                .Where(x => ids.Contains(x.MaPhong))
                .Select(x => x.SoPhong)
                .OrderBy(x => x)
                .ToList();

            var msg = (ids.Count == 1)
                ? $"Xóa phòng {soPhongs.First()}?"
                : $"Xóa {ids.Count} phòng: {string.Join(", ", soPhongs)} ?";

            if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (var id in ids)
            {
                phongService.Xoa(id);
                var phong = danhSachPhong.FirstOrDefault(x => x.MaPhong == id);
                if (phong != null) danhSachPhong.Remove(phong);

                Guna2Panel pnlToRemove = null;
                foreach (Control c in flpContain.Controls)
                {
                    var gp = c as Guna2Panel;
                    if (gp == null) continue;
                    var tagPhong = gp.Tag as Phong;
                    if (tagPhong != null && tagPhong.MaPhong == id)
                    {
                        pnlToRemove = gp;
                        break;
                    }
                }
                if (pnlToRemove != null)
                    flpContain.Controls.Remove(pnlToRemove);

                _selectedRoomIds.Remove(id);
            }
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

            string loaiPhong = cbLoaiPhong.SelectedItem.ToString();
            decimal giaPhong = PhongGiaConfig.GiaPhong[loaiPhong];
            string trangThai = "Trống";

            Phong phongMoi = new Phong(0, soPhong, loaiPhong, giaPhong, trangThai);
            int maPhong = phongService.Them(phongMoi);
            if (maPhong > 0)
            {
                phongMoi.MaPhong = maPhong;
                danhSachPhong.Add(phongMoi);
                var pnl = TaoPhongMoi(phongMoi);
                flpContain.Controls.Add(pnl);
                txtSoPhong.Clear();
                cbLoaiPhong.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại! Kiểm tra lại dữ liệu.");
            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedRooms();
            if (selected.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng.");
                return;
            }

            var list = new List<RoomBookingInfo>();
            foreach (var room in selected)
            {
                var bk = GetDatPhongDeHien(room.MaPhong);
                KhachHang kh = bk != null ? khachHangService.LayKhachHangTheoMaKH(bk.MaKH) : null;
                list.Add(new RoomBookingInfo { Room = room, Booking = bk, Customer = kh });
            }

            var bookedItems = list.Where(x => x.Booking != null && (x.Booking.TrangThai == "Đã đặt" || x.Booking.TrangThai == "Đang sử dụng")).ToList();
            var maKhSet = bookedItems.Select(x => x.Booking.MaKH).Distinct().ToList();

            if (maKhSet.Count > 1)
            {
                MessageBox.Show("Chỉ chọn các phòng của CÙNG 1 khách để sửa.");
                return;
            }

            string preTen = "", preCCCD = "", preSDT = "";
            int preMaKH = 0;
            DateTime? preNhan = null, preTra = null;

            if (bookedItems.Any())
            {
                var validBooked = bookedItems.FirstOrDefault(x => x.Customer != null);
                if (validBooked == null)
                {
                    MessageBox.Show("Có booking nhưng không lấy được thông tin khách hàng. Kiểm tra bảng KhachHang trong DB.");
                    return;
                }

                var kh = validBooked.Customer;
                preTen = kh.HoTen ?? "";
                preCCCD = kh.CCCD ?? "";
                preSDT = kh.SDT ?? "";
                preMaKH = kh.MaKH;

                preNhan = bookedItems.Min(x => x.Booking.NgayNhan).Date;
                preTra = bookedItems.Max(x => x.Booking.NgayTraDuKien).Date;
                if (preTra <= preNhan) preTra = preNhan.Value.AddDays(1);
            }

            try
            {
                using (var frm = new frmThemKH(list, preTen, preCCCD, preSDT, preMaKH, preNhan, preTra))
                {
                    var dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        LoadPhongFromDB();
                        _selectedRoomIds.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form: {ex.Message}");
            }
        }

        private void RefreshPhongPanel(Guna2Panel panelPhong)
        {
            if (panelPhong == null) return;
            var phong = panelPhong.Tag as Phong;
            if (phong == null) return;

            var lbl = panelPhong.Controls.Find("lblKhach", true)
                                         .OfType<Label>()
                                         .FirstOrDefault();
            if (lbl == null) return;

            HienKhachLenPanel(phong, lbl, panelPhong);
        }

        public void RefreshPhongById(int maPhong)
        {
            foreach (Control c in flpContain.Controls)
            {
                if (c is Guna2Panel pnl && pnl.Tag is Phong p && p.MaPhong == maPhong)
                {
                    RefreshPhongPanel(pnl);
                    break;
                }
            }
        }
    }
}

public static class PhongGiaConfig
{
    public static Dictionary<string, decimal> GiaPhong = new Dictionary<string, decimal>
    {
        { "Phòng đơn", 100000m },
        { "Phòng đôi", 200000m },
        { "Tiêu chuẩn", 300000m },
        { "VIP", 500000m },
        { "Deluxe", 800000m }
    };
}