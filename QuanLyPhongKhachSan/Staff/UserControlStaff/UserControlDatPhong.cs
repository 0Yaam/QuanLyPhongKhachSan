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
        private readonly HoaDonService hoaDonService = new HoaDonService();

        private readonly List<Phong> _allRooms = new List<Phong>();
        private readonly HashSet<int> _selectedRoomIds = new HashSet<int>();
        private HashSet<int> _maDatDaInLan1 = new HashSet<int>();

        public UserControlDatPhong()
        {
            InitializeComponent();
            KhoiTaoComboBox();
            this.Load += UserControlDatPhong_Load;

            // Nghe event toàn cục: in xong HĐ → reload
            AppEvents.InvoiceLogged -= AppEvents_InvoiceLogged;
            AppEvents.InvoiceLogged += AppEvents_InvoiceLogged;

            if (dtpNgayHienTai != null) dtpNgayHienTai.Value = DateTime.Now;
            if (txtSoPhong != null) txtSoPhong.PlaceholderText = "Số phòng";
            if (btnThemKH != null) btnThemKH.Click += btnThemKH_Click;
            this.TabStop = true;
            this.KeyDown += UserControl_KeyDown;
            if (flpContain != null)
            {
                flpContain.TabStop = true;
                flpContain.KeyDown += UserControl_KeyDown;
            }
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            rdSoPhong.Checked = true;

            if (dtpNgayHienTai != null)
                dtpNgayHienTai.ValueChanged += (s, e) => LoadDanhSachPhong();
            txtSoPhong.PlaceholderText = "Nhập số phòng";
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            AppEvents.InvoiceLogged -= AppEvents_InvoiceLogged;
            base.OnHandleDestroyed(e);
        }

        private void AppEvents_InvoiceLogged()
        {
            try
            {
                if (IsHandleCreated) BeginInvoke((Action)(() => LoadPhongFromDB()));
                else LoadPhongFromDB();
            }
            catch { }
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

        // Cache các MaDat đã có HĐ Lần 1 (để tô màu nhanh)
        private void BuildHoaDonLan1Cache()
        {
            try
            {
                var list = hoaDonService.LayDanhSach();
                _maDatDaInLan1 = new HashSet<int>(
                    list.Where(h => string.Equals(h.LoaiHoaDon, "Lần 1", StringComparison.OrdinalIgnoreCase) && h.MaDat > 0)
                        .Select(h => h.MaDat)
                );
            }
            catch
            {
                _maDatDaInLan1.Clear();
            }
        }

        private void LoadPhongFromDB()
        {
            _selectedRoomIds.Clear();
            _allRooms.Clear();
            flpContain.Controls.Clear();

            // Xây cache HĐ1
            BuildHoaDonLan1Cache();

            var danhSach = phongService.LayDanhSach();
            foreach (var p in danhSach)
            {
                _allRooms.Add(p);
                var pnl = TaoPhongMoi(p);
                flpContain.Controls.Add(pnl);
            }

            flpContain.Visible = true;
            flpContain.AutoScroll = true;
            LoadDanhSachPhong();
            KhoiTaoComboBox();
        }

        public void RefreshData()
        {
            LoadPhongFromDB();
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
                Text = phong.Gia.ToString("N0") + "đ",
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

            // Menu chuột phải
            var menu = new ContextMenuStrip();
            menu.Items.Add("Xóa").Click += (s, e) => XoaPhongSelectedOrOne(pnl);

            // Đổi loại phòng
            menu.Items.Add("Đổi loại phòng...").Click += (s, e) => DoiLoaiPhong(pnl);

            // Đặt trạng thái
            var mSetStatus = new ToolStripMenuItem("Đặt trạng thái");
            mSetStatus.DropDownItems.Add("Trống", null, (s, e) => DatTrangThaiPhong(pnl, "Trống"));
            mSetStatus.DropDownItems.Add("Đã đặt", null, (s, e) => DatTrangThaiPhong(pnl, "Đã đặt"));
            mSetStatus.DropDownItems.Add("Đang sử dụng", null, (s, e) => DatTrangThaiPhong(pnl, "Đang sử dụng"));
            menu.Items.Add(mSetStatus);

            pnl.ContextMenuStrip = menu;

            SetSelected(pnl, false);
            return pnl;
        }

        // Hiển thị KH & tô màu panel theo trạng thái + HĐ Lần 1
        private void HienKhachLenPanel(Phong phong, Label lblKhach, Guna2Panel pnl)
        {
            try
            {
                var dat = phongService.LayDatPhongTheoMaPhong(phong.MaPhong);
                if (dat == null || dat.NgayTraThucTe.HasValue)
                {
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192); // Trống
                    return;
                }

                var today = DateTime.Today;
                bool trangThaiOK = dat.TrangThai.Equals("Đã đặt", StringComparison.OrdinalIgnoreCase)
                                   || dat.TrangThai.Equals("Đang sử dụng", StringComparison.OrdinalIgnoreCase);

                bool hieuLuc =
                    (today >= dat.NgayNhan.Date && today <= dat.NgayTraDuKien.Date) // gồm cả ngày trả
                    || (today < dat.NgayNhan.Date); // đặt tương lai

                if (!trangThaiOK || !hieuLuc)
                {
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192);
                    return;
                }

                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                lblKhach.Text = string.Format("{0} - {1}", kh != null ? kh.HoTen : "", kh != null ? kh.SDT : "");

                bool daInLan1 = _maDatDaInLan1.Contains(dat.MaDat);

                // Chưa in Lần 1: vàng nhạt; Đã in Lần 1: xanh nhạt
                pnl.FillColor = daInLan1
                    ? Color.FromArgb(187, 222, 251)   // xanh nhạt
                    : Color.FromArgb(255, 245, 157);  // vàng nhạt
            }
            catch
            {
                lblKhach.Text = "";
                pnl.FillColor = Color.FromArgb(255, 192, 192);
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
                    if (c is Guna2Panel gp) SetSelected(gp, false);
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
            return _allRooms.Where(p => _selectedRoomIds.Contains(p.MaPhong)).ToList();
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
                    if (dr != DialogResult.OK)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("MoFormKhachHang: Hủy hoặc đóng form - MaPhong={0}", phong.MaPhong));
                        return;
                    }

                    string ten = (frmthemvasua.TenKhachHang ?? "").Trim();
                    string cccd = (frmthemvasua.CCCD ?? "").Trim();
                    string sdt = (frmthemvasua.SDT ?? "").Trim();
                    DateTime ngayNhan = frmthemvasua.NgayNhan;
                    DateTime ngayTraDuKien = frmthemvasua.NgayTraDuKien;
                    decimal tienCoc = frmthemvasua.TienCoc;
                    decimal tienThue = frmthemvasua.TienThue;

                    if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(sdt))
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Lỗi MoFormKhachHang: Tên hoặc SDT trống - Ten={0}, SDT={1}", ten, sdt));
                        return;
                    }

                    int maKh = khachHangService.UpsertKhachHang(ten, cccd, sdt);
                    if (maKh <= 0)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Lỗi UpsertKhachHang: Ten={0}, CCCD={1}, SDT={2}", ten, cccd, sdt));
                        return;
                    }

                    string trangThai = ComputeTrangThai(ngayNhan, ngayTraDuKien);
                    var datPhong = new DatPhong(
                        0, maKh, phong.MaPhong,
                        ngayNhan, ngayTraDuKien, null,
                        tienCoc, tienThue, trangThai
                    );

                    var dat = phongService.LayDatPhongTheoMaPhong(phong.MaPhong);
                    System.Diagnostics.Debug.WriteLine(string.Format("MoFormKhachHang: Kiểm tra DatPhong - MaPhong={0}, Dat={1}",
                        phong.MaPhong,
                        (dat != null ? string.Format("MaDat={0}, NgayTraThucTe={1}, TrangThai={2}", dat.MaDat, dat.NgayTraThucTe, dat.TrangThai) : "null")));

                    bool result;
                    if (dat != null && !dat.NgayTraThucTe.HasValue && (dat.TrangThai == "Đã đặt" || dat.TrangThai == "Đang sử dụng"))
                    {
                        datPhong.MaDat = dat.MaDat;
                        datPhong.TrangThai = trangThai;
                        result = phongService.CapNhatDatPhong(datPhong);
                        System.Diagnostics.Debug.WriteLine(string.Format("Cập nhật DatPhong: MaDat={0}, MaKH={1}, MaPhong={2}, Result={3}",
                            datPhong.MaDat, maKh, phong.MaPhong, result));
                    }
                    else
                    {
                        int maDat = phongService.ThemDatPhong(datPhong);
                        result = maDat > 0;
                        System.Diagnostics.Debug.WriteLine(string.Format("Thêm DatPhong: MaDat={0}, MaKH={1}, MaPhong={2}, Result={3}",
                            maDat, maKh, phong.MaPhong, result));
                    }

                    if (result)
                    {
                        if (!phongService.CapNhatTrangThai(phong.MaPhong, trangThai))
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Lỗi CapNhatTrangThai: MaPhong={0}, TrangThai={1}", phong.MaPhong, trangThai));
                            return;
                        }

                        LoadPhongFromDB();
                        _selectedRoomIds.Clear();
                        System.Diagnostics.Debug.WriteLine(string.Format("Lưu đặt phòng thành công: MaKH={0}, MaPhong={1}", maKh, phong.MaPhong));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Lỗi lưu DatPhong: MaKH={0}, MaPhong={1}, NgayNhan={2}, NgayTraDuKien={3}",
                            maKh, phong.MaPhong, ngayNhan, ngayTraDuKien));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Lỗi MoFormKhachHang: MaPhong={0}, Exception={1}", phong.MaPhong, ex.Message));
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
            if (_selectedRoomIds.Count > 0) ids.AddRange(_selectedRoomIds);
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

            var soPhongs = _allRooms
                .Where(x => ids.Contains(x.MaPhong))
                .Select(x => x.SoPhong)
                .OrderBy(x => x)
                .ToList();

            var msg = (ids.Count == 1)
                ? string.Format("Xóa phòng {0}?", soPhongs.First())
                : string.Format("Xóa {0} phòng: {1} ?", ids.Count, string.Join(", ", soPhongs));

            if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (var id in ids)
            {
                phongService.Xoa(id);
                var phong = _allRooms.FirstOrDefault(x => x.MaPhong == id);
                if (phong != null) _allRooms.Remove(phong);
            }
            LoadPhongFromDB();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhong == null || cbLoaiPhong.SelectedIndex == -1)
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
            if (_allRooms.Any(p => p.SoPhong == soPhong))
            {
                MessageBox.Show("Số phòng đã tồn tại!");
                return;
            }

            var tenLoai = cbLoaiPhong.SelectedItem.ToString();
            int maLoai = phongService.LayMaLoaiTheoTen(tenLoai);
            if (maLoai <= 0)
            {
                MessageBox.Show("Không tìm thấy Mã loại phòng. Hãy kiểm tra bảng LoaiPhong.");
                return;
            }

            var phongMoi = new Phong(maPhong: 0, soPhong: soPhong, maLoaiPhong: maLoai, trangThai: "Trống");
            int maPhong = phongService.Them(phongMoi);

            if (maPhong > 0)
            {
                LoadPhongFromDB();
                txtSoPhong.Clear();
                if (cbLoaiPhong.Items.Count > 0) cbLoaiPhong.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại! Vui lòng kiểm tra lại LoaiPhong và dữ liệu đầu vào.");
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
                var bk = phongService.LayDatPhongTheoMaPhong(room.MaPhong);
                KhachHang kh = (bk != null && !bk.NgayTraThucTe.HasValue)
                    ? khachHangService.LayKhachHangTheoMaKH(bk.MaKH)
                    : null;
                list.Add(new RoomBookingInfo { Room = room, Booking = bk, Customer = kh });
            }

            var bookedItems = list.Where(x => x.Booking != null && !x.Booking.NgayTraThucTe.HasValue).ToList();
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
                    MessageBox.Show("Có booking nhưng không lấy được thông tin khách hàng.");
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
                        RefreshData();
                        _selectedRoomIds.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Lỗi khi mở form: {0}", ex.Message));
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
                var pnl = c as Guna2Panel;
                if (pnl != null)
                {
                    var p = pnl.Tag as Phong;
                    if (p != null && p.MaPhong == maPhong)
                    {
                        RefreshPhongPanel(pnl);
                        break;
                    }
                }
            }
        }

        private void KhoiTaoComboBox()
        {
            var loaiPhongList = phongService.LayDanhSachLoaiPhong().Distinct().ToList();

            // combobox lọc danh sách
            cbLoai.Items.Clear();
            cbLoai.Items.Add("None");
            cbLoai.Items.AddRange(loaiPhongList.ToArray());
            if (cbLoai.Items.Count > 0) cbLoai.SelectedIndex = 0;

            // combobox loại phòng khi thêm mới
            if (cbLoaiPhong != null)
            {
                cbLoaiPhong.Items.Clear();
                cbLoaiPhong.Items.AddRange(loaiPhongList.ToArray());
                cbLoaiPhong.SelectedIndex = loaiPhongList.Count > 0 ? 0 : -1;
            }
        }

        private void LoadDanhSachPhong()
        {
            try
            {
                string loaiPhong = cbLoai.SelectedItem != null ? cbLoai.SelectedItem.ToString() : null;
                string trangThai = rdPhongTrong.Checked ? "Trống" : (rdPhongDaDat.Checked ? "Đã có khách" : null);
                bool tangDan = rdTang.Checked;

                var filteredRooms = new List<Phong>(_allRooms);

                if (!string.IsNullOrEmpty(loaiPhong) && loaiPhong != "None")
                    filteredRooms = filteredRooms.Where(p => p.LoaiPhong == loaiPhong).ToList();

                if (trangThai == "Trống")
                    filteredRooms = filteredRooms.Where(p =>
                    {
                        var dp = phongService.LayDatPhongTheoMaPhong(p.MaPhong);
                        return dp == null || dp.NgayTraThucTe.HasValue == true;
                    }).ToList();
                else if (trangThai == "Đã có khách")
                    filteredRooms = filteredRooms.Where(p =>
                    {
                        var dp = phongService.LayDatPhongTheoMaPhong(p.MaPhong);
                        if (dp == null || dp.NgayTraThucTe.HasValue) return false;
                        var today = DateTime.Today;
                        bool okStatus =
                            string.Equals(dp.TrangThai, "Đã đặt", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(dp.TrangThai, "Đang sử dụng", StringComparison.OrdinalIgnoreCase);
                        bool hieuLuc =
                            (today >= dp.NgayNhan.Date && today < dp.NgayTraDuKien.Date) ||
                            (today < dp.NgayNhan.Date);
                        return okStatus && hieuLuc;
                    }).ToList();

                filteredRooms = tangDan
                    ? filteredRooms.OrderBy(p => p.SoPhong).ToList()
                    : filteredRooms.OrderByDescending(p => p.SoPhong).ToList();

                flpContain.Controls.Clear();
                foreach (var phong in filteredRooms)
                {
                    var pnl = TaoPhongMoi(phong);
                    flpContain.Controls.Add(pnl);
                }

                flpContain.Visible = true;
                flpContain.AutoScroll = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Lỗi khi tải danh sách phòng: {0}", ex.Message));
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                LoadDanhSachPhong();
                return;
            }

            var datPhongList = _allRooms
                .Select(p => phongService.LayDatPhongTheoMaPhong(p.MaPhong))
                .Where(dp => dp != null && !dp.NgayTraThucTe.HasValue)
                .ToList();

            var khachHangList = khachHangService.LayDanhSach();
            var filteredRooms = new List<Phong>();

            if (rdTen.Checked)
            {
                filteredRooms = _allRooms
                    .Join(datPhongList, p => p.MaPhong, dp => dp.MaPhong, (p, dp) => new { Phong = p, DatPhong = dp })
                    .Join(khachHangList, x => x.DatPhong.MaKH, kh => kh.MaKH, (x, kh) => new { x.Phong, KhachHang = kh })
                    .Where(x => ((x.KhachHang.HoTen ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0))
                    .Select(x => x.Phong).ToList();
            }
            else if (rdCCCD.Checked)
            {
                filteredRooms = _allRooms
                    .Join(datPhongList, p => p.MaPhong, dp => dp.MaPhong, (p, dp) => new { Phong = p, DatPhong = dp })
                    .Join(khachHangList, x => x.DatPhong.MaKH, kh => kh.MaKH, (x, kh) => new { x.Phong, KhachHang = kh })
                    .Where(x => ((x.KhachHang.CCCD ?? "").Contains(tuKhoa)))
                    .Select(x => x.Phong).ToList();
            }
            else if (rdSDT.Checked)
            {
                filteredRooms = _allRooms
                    .Join(datPhongList, p => p.MaPhong, dp => dp.MaPhong, (p, dp) => new { Phong = p, DatPhong = dp })
                    .Join(khachHangList, x => x.DatPhong.MaKH, kh => kh.MaKH, (x, kh) => new { x.Phong, KhachHang = kh })
                    .Where(x => ((x.KhachHang.SDT ?? "").Contains(tuKhoa)))
                    .Select(x => x.Phong).ToList();
            }
            else if (rdSoPhong.Checked)
            {
                filteredRooms = _allRooms.Where(p => p.SoPhong.ToString().Contains(tuKhoa)).ToList();
            }

            flpContain.Controls.Clear();
            foreach (var phong in filteredRooms)
            {
                var pnl = TaoPhongMoi(phong);
                flpContain.Controls.Add(pnl);
            }
            flpContain.Visible = true;
            flpContain.AutoScroll = true;
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e) { LoadDanhSachPhong(); }
        private void rdPhongTrong_CheckedChanged(object sender, EventArgs e) { LoadDanhSachPhong(); }
        private void rdPhongDaDat_CheckedChanged(object sender, EventArgs e) { LoadDanhSachPhong(); }
        private void rdTang_CheckedChanged(object sender, EventArgs e) { LoadDanhSachPhong(); }
        private void rdGiam_CheckedChanged(object sender, EventArgs e) { LoadDanhSachPhong(); }

        private void rdTen_CheckedChanged(object sender, EventArgs e) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        private void rdCCCD_CheckedChanged(object sender, EventArgs e) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        private void rdSoPhong_CheckedChanged(object sender, EventArgs e) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        private void rdSDT_CheckedChanged(object sender, EventArgs e) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }

        private void btnReset_Click(object sender, EventArgs e)
        {
            rdPhongDaDat.Checked = false;
            rdPhongTrong.Checked = false;
            rdTang.Checked = false;
            rdGiam.Checked = false;
        }

        //helperss

        private void DoiLoaiPhong(Guna2Panel pnl)
        {
            if (pnl == null) return;
            var p = pnl.Tag as Phong;
            if (p == null) return;

            var loais = phongService.LayDanhSachLoaiPhong();
            if (loais.Count == 0)
            {
                MessageBox.Show("Chưa có loại phòng trong hệ thống.");
                return;
            }

            using (var f = new Form())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MinimizeBox = false;
                f.MaximizeBox = false;
                f.Width = 360; f.Height = 140;
                f.Text = string.Format("Đổi loại phòng {0}", p.SoPhong);

                var cb = new ComboBox { Left = 15, Top = 15, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };
                cb.Items.AddRange(loais.ToArray());
                if (!string.IsNullOrEmpty(p.LoaiPhong) && loais.Contains(p.LoaiPhong)) cb.SelectedItem = p.LoaiPhong;
                else cb.SelectedIndex = 0;

                var btnOK = new Button { Text = "OK", Left = 170, Top = 55, Width = 75, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Hủy", Left = 260, Top = 55, Width = 75, DialogResult = DialogResult.Cancel };
                f.Controls.Add(cb); f.Controls.Add(btnOK); f.Controls.Add(btnCancel);
                f.AcceptButton = btnOK; f.CancelButton = btnCancel;

                if (f.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    var tenLoai = cb.SelectedItem != null ? cb.SelectedItem.ToString() : null;
                    int maLoai = phongService.LayMaLoaiTheoTen(tenLoai);
                    if (maLoai <= 0)
                    {
                        MessageBox.Show("Không tìm thấy mã loại phòng.");
                        return;
                    }

                    p.MaLoaiPhong = maLoai;
                    if (!phongService.CapNhat(p))
                    {
                        MessageBox.Show("Cập nhật loại phòng thất bại.");
                        return;
                    }

                    LoadPhongFromDB();
                }
            }
        }

        // Đặt trạng thái phòng thủ công (Trống/Đã đặt/Đang sử dụng)
        private void DatTrangThaiPhong(Guna2Panel pnl, string trangThai)
        {
            if (pnl == null) return;
            var p = pnl.Tag as Phong;
            if (p == null) return;

            if (!phongService.CapNhatTrangThai(p.MaPhong, trangThai))
            {
                MessageBox.Show("Cập nhật trạng thái phòng thất bại.");
                return;
            }

            LoadPhongFromDB();
        }
    }
}
