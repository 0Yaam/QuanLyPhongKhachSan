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

        // Multi-select
        private readonly HashSet<int> _selectedRoomIds = new HashSet<int>();

        public UserControlDatPhong()
        {
            InitializeComponent();

            if (dtpNgayHienTai != null) dtpNgayHienTai.Value = DateTime.Now;
            if (txtSoPhong != null) txtSoPhong.PlaceholderText = "Số phòng";
            if (cbLoaiPhong != null)
            {
                cbLoaiPhong.Items.Clear();
                cbLoaiPhong.Items.AddRange(new string[] { "Phòng đơn", "Phòng đôi", "Tiêu chuẩn", "VIP", "Deluxe" });
            }
            if (btnThemKH != null) btnThemKH.Click += btnThemKH_Click;

            // nhận phím Delete (set focusable + handler)
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
            // bắt phím Delete ở level Form để chắc ăn
            var frm = this.FindForm();
            if (frm != null)
            {
                frm.KeyPreview = true;
                frm.KeyDown -= UserControl_KeyDown;
                frm.KeyDown += UserControl_KeyDown;
            }

            LoadPhongFromDB();
        }

        // =============== LOAD PHÒNG ===============
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

        // =============== TẠO PANEL PHÒNG ===============
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

            // Hiện khách (mới nhất chưa trả)
            HienKhachLenPanel(phong, lblKhach, pnl);

            // Click trái 1 lần: chọn/bỏ chọn (và focus để nhận phím)
            pnl.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    ToggleSelect((Guna2Panel)s);
            };

            // DoubleClick: ép đơn chọn panel này rồi mở form đơn phòng
            pnl.DoubleClick += (s, e) => HandleDoubleClickOpen(pnl);

            // Child controls cũng nhận hành vi như panel
            foreach (Control child in pnl.Controls)
            {
                child.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        ToggleSelect(pnl);
                };
                child.DoubleClick += (s, e) => HandleDoubleClickOpen(pnl);
            }

            // Context menu "Xóa" – hành xử giống Delete
            var menu = new ContextMenuStrip();
            menu.Items.Add("Xóa").Click += (s, e) =>
            {
                XoaPhongSelectedOrOne(pnl);
            };
            pnl.ContextMenuStrip = menu;

            SetSelected(pnl, false);
            return pnl;
        }

        // =============== LẤY BOOKING ĐỂ HIỂN THỊ ===============
        private DatPhong GetDatPhongDeHien(int maPhong)
        {
            var dat = phongService.LayDatPhongTheoMaPhong(maPhong); // TOP 1, chưa trả
            if (dat == null) return null;
            if (dat.NgayTraThucTe.HasValue) return null;
            return dat;
        }

        private void HienKhachLenPanel(Phong phong, Label lblKhach, Guna2Panel pnl)
        {
            try
            {
                var dat = GetDatPhongDeHien(phong.MaPhong);
                if (dat == null)
                {
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192); // trống
                    return;
                }

                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                string ten = kh != null ? kh.HoTen : "";
                string sdt = kh != null ? kh.SDT : "";

                lblKhach.Text = (string.IsNullOrEmpty(ten) && string.IsNullOrEmpty(sdt))
                    ? ""
                    : ("Khách: " + ten + " - " + sdt);

                if (dat.TrangThai == "Đang sử dụng")
                    pnl.FillColor = Color.FromArgb(255, 235, 150); // vàng
                else if (dat.TrangThai == "Đã đặt")
                    pnl.FillColor = Color.FromArgb(180, 220, 255); // xanh nhạt
                else
                    pnl.FillColor = Color.FromArgb(255, 192, 192);
            }
            catch
            {
                lblKhach.Text = "";
                pnl.FillColor = Color.FromArgb(255, 192, 192);
            }
        }

        // =============== DOUBLE CLICK ===============
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

        // =============== SELECT UI ===============
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

            // đảm bảo usercontrol nhận phím
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
            var result = new List<Phong>();
            foreach (var r in danhSachPhong)
            {
                if (_selectedRoomIds.Contains(r.MaPhong))
                    result.Add(r);
            }
            return result;
        }

        // =============== MỞ FORM 1 PHÒNG (giữ nguyên logic bạn đang dùng) ===============
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
                        string trangThai = frmthemvasua.TrangThai;

                        int maKh = khachHangService.UpsertKhachHang(ten, cccd, sdt);
                        if (maKh > 0)
                        {
                            var datPhong = new DatPhong(
                                0, maKh, phong.MaPhong,
                                ngayNhan, ngayTraDuKien, null,
                                tienCoc, tienThue, trangThai
                            );

                            int maDatPhong = phongService.ThemDatPhong(datPhong);
                            if (maDatPhong > 0)
                            {
                                var lbl = panelPhong.Controls.Find("lblKhach", true).FirstOrDefault() as Label;
                                if (lbl != null)
                                    lbl.Text = (string.IsNullOrEmpty(ten) && string.IsNullOrEmpty(sdt)) ? "" : ("Khách: " + ten + " - " + sdt);

                                panelPhong.FillColor = (trangThai == "Đang sử dụng")
                                    ? Color.FromArgb(255, 235, 150)
                                    : (trangThai == "Đã đặt")
                                        ? Color.FromArgb(180, 220, 255)
                                        : Color.FromArgb(255, 192, 192);
                            }
                            else
                            {
                                MessageBox.Show("Lưu thông tin đặt phòng thất bại!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu thông tin khách hàng thất bại!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form: " + ex.Message);
            }
        }

        // =============== NÚT THÊM KH (giữ logic hiện tại của bạn) ===============
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
                KhachHang kh = (bk != null) ? khachHangService.LayKhachHangTheoMaKH(bk.MaKH) : null;
                list.Add(new RoomBookingInfo { Room = room, Booking = bk, Customer = kh });
            }

            var bookedItems = list.Where(x => x.Booking != null).ToList();
            var maKhSet = bookedItems.Select(x => x.Booking.MaKH).Distinct().ToList();

            if (maKhSet.Count > 1)
            {
                MessageBox.Show("Chỉ chọn các phòng của CÙNG 1 khách để sửa.");
                return;
            }

            string preTen = "", preCCCD = "", preSDT = "";
            int preMaKH = 0;
            DateTime? preNhan = null, preTra = null;

            if (maKhSet.Count == 1)
            {
                var any = bookedItems.First();
                var kh = any.Customer;
                if (kh != null)
                {
                    preTen = kh.HoTen ?? "";
                    preCCCD = kh.CCCD ?? "";
                    preSDT = kh.SDT ?? "";
                    preMaKH = kh.MaKH;
                }

                preNhan = bookedItems.Min(x => x.Booking.NgayNhan).Date;
                preTra = bookedItems.Max(x => x.Booking.NgayTraDuKien).Date;
                if (preTra <= preNhan) preTra = preNhan.Value.AddDays(1);
            }

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

        // =============== XOÁ HÀNG LOẠT (Delete / ContextMenu) ===============
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
            // 1) Lấy danh sách MaPhong cần xoá
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

            // 2) Confirm
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

            // 3) Thực thi xóa
            foreach (var id in ids)
            {
                // xóa DB
                phongService.Xoa(id);

                // xóa trong list
                var phong = danhSachPhong.FirstOrDefault(x => x.MaPhong == id);
                if (phong != null) danhSachPhong.Remove(phong);

                // xóa panel UI
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

        // =============== THÊM PHÒNG MỚI (giữ nguyên) ===============
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!!");
                return;
            }

            int soPhong;
            if (!int.TryParse(txtSoPhong.Text, out soPhong) || soPhong <= 0)
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
    }
}

// =================== GIÁ PHÒNG CONFIG ===================
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
