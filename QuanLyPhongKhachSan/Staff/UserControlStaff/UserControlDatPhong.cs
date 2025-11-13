using Guna.UI2.WinForms;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.UI.Helpers;
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

            AppEvents.InvoiceLogged -= AppEvents_InvoiceLogged;
            AppEvents.InvoiceLogged += AppEvents_InvoiceLogged;

            if (dtpNgayHienTai != null) dtpNgayHienTai.Value = DateTime.Now;
            if (txtSoPhong != null) txtSoPhong.PlaceholderText = "Số phòng";
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
                Name = "lblLoaiPhong",
                Location = new Point(14, 35),
                Font = new Font("Microsoft Tai Le", 10, FontStyle.Regular),
                AutoSize = true,
                Text = phong.LoaiPhong,
                BackColor = Color.Transparent
            };

            var lblGia = new Label
            {
                Name = "lblGia",
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
            menu.Items.Add("Đổi loại phòng...").Click += (s, e) => DoiLoaiPhong(pnl);

            var mSetStatus = new ToolStripMenuItem("Đặt trạng thái");
            mSetStatus.DropDownItems.Add("Trống", null, (s, e) => DatTrangThaiPhong(pnl, "Trống"));
            mSetStatus.DropDownItems.Add("Đã đặt", null, (s, e) => DatTrangThaiPhong(pnl, "Đã đặt"));
            mSetStatus.DropDownItems.Add("Đang sử dụng", null, (s, e) => DatTrangThaiPhong(pnl, "Đang sử dụng"));
            menu.Items.Add(mSetStatus);

            pnl.ContextMenuStrip = menu;

            SetSelected(pnl, false);
            return pnl;
        }

        private void HienKhachLenPanel(Phong phong, Label lblKhach, Guna2Panel pnl)
        {
            try
            {
                var dat = phongService.LayDatPhongTheoMaPhong(phong.MaPhong);
                if (dat == null || dat.NgayTraThucTe.HasValue)
                {
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192);
                    return;
                }

                var today = DateTime.Today;
                bool trangThaiOK = dat.TrangThai.Equals("Đã đặt", StringComparison.OrdinalIgnoreCase)
                                   || dat.TrangThai.Equals("Đang sử dụng", StringComparison.OrdinalIgnoreCase);

                bool hieuLuc =
                    (today >= dat.NgayNhan.Date && today <= dat.NgayTraDuKien.Date)
                    || (today < dat.NgayNhan.Date);

                if (!trangThaiOK || !hieuLuc)
                {
                    lblKhach.Text = "";
                    pnl.FillColor = Color.FromArgb(255, 192, 192);
                    return;
                }

                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                lblKhach.Text = $"{(kh != null ? kh.HoTen : "")} - {(kh != null ? kh.SDT : "")}";

                bool daInLan1 = _maDatDaInLan1.Contains(dat.MaDat);
                pnl.FillColor = daInLan1
                    ? Color.FromArgb(187, 222, 251)
                    : Color.FromArgb(255, 245, 157);
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

        private List<Phong> GetSelectedRooms() =>
            _allRooms.Where(p => _selectedRoomIds.Contains(p.MaPhong)).ToList();

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
                        System.Diagnostics.Debug.WriteLine($"MoFormKhachHang: Hủy hoặc đóng form - MaPhong={phong.MaPhong}");
                        return;
                    }

                    LoadPhongFromDB();
                    _selectedRoomIds.Clear();

                    string ten = (frmthemvasua.TenKhachHang ?? "").Trim();
                    string cccd = (frmthemvasua.CCCD ?? "").Trim();
                    string sdt = (frmthemvasua.SDT ?? "").Trim();
                    DateTime ngayNhan = frmthemvasua.NgayNhan;
                    DateTime ngayTraDuKien = frmthemvasua.NgayTraDuKien;
                    decimal tienCoc = frmthemvasua.TienCoc;
                    decimal tienThue = frmthemvasua.TienThue;

                    if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(sdt))
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi MoFormKhachHang: Tên hoặc SDT trống - Ten={ten}, SDT={sdt}");
                        return;
                    }

                    int maKh = khachHangService.UpsertKhachHang(ten, cccd, sdt);
                    AuditHelper.Log("Upsert", "KhachHang", maKh.ToString(),
                        moTa: $"KH: {ten} - {sdt}",
                        duLieuMoi: $"Ten={ten}; CCCD={cccd}; SDT={sdt}",
                        ketQua: maKh > 0);

                    if (maKh <= 0) return;

                    string trangThai = ComputeTrangThai(ngayNhan, ngayTraDuKien);
                    var datPhong = new DatPhong(
                        0, maKh, phong.MaPhong,
                        ngayNhan, ngayTraDuKien, null,
                        tienCoc, tienThue, trangThai
                    );

                    var dat = phongService.LayDatPhongTheoMaPhong(phong.MaPhong);
                    bool result;

                    if (dat != null && !dat.NgayTraThucTe.HasValue && (dat.TrangThai == "Đã đặt" || dat.TrangThai == "Đang sử dụng"))
                    {
                        datPhong.MaDat = dat.MaDat;
                        datPhong.TrangThai = trangThai;
                        result = phongService.CapNhatDatPhong(datPhong);

                        AuditHelper.Log("Sửa", "DatPhong",
                            datPhong.MaDat.ToString(),
                            moTa: $"Cập nhật đặt phòng | Phòng={phong.SoPhong} | {ngayNhan:dd/MM}→{ngayTraDuKien:dd/MM} | Cọc={tienCoc:N0} | Thuế={tienThue:N0} | Trạng thái={trangThai}",
                            ketQua: result);
                    }
                    else
                    {
                        int maDat = phongService.ThemDatPhong(datPhong);
                        result = maDat > 0;

                        AuditHelper.Log("Thêm", "DatPhong",
                            (maDat > 0 ? maDat.ToString() : null),
                            moTa: $"Thêm đặt phòng | Phòng={phong.SoPhong} | {ngayNhan:dd/MM}→{ngayTraDuKien:dd/MM} | Cọc={tienCoc:N0} | Thuế={tienThue:N0} | Trạng thái={trangThai}",
                            ketQua: result);
                    }

                    if (result)
                    {
                        bool okTrangThai = phongService.CapNhatTrangThai(phong.MaPhong, trangThai);
                        AuditHelper.Log("Sửa", "Phong",
                            phong.MaPhong.ToString(),
                            moTa: $"Cập nhật trạng thái phòng {phong.SoPhong} = {trangThai}",
                            ketQua: okTrangThai);

                        LoadPhongFromDB();
                        _selectedRoomIds.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi MoFormKhachHang: MaPhong={phong.MaPhong}, Exception={ex.Message}");
                AuditHelper.Log("Sửa", "DatPhong",
                    phong.MaPhong.ToString(),
                    ketQua: false,
                    loi: ex.Message,
                    moTa: "Exception khi lưu đặt phòng");
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
                if (contextPanelIfNoneSelected.Tag is Phong p) ids.Add(p.MaPhong);
            }

            if (ids.Count == 0)
            {
                MessageBox.Show("Không có phòng nào được chọn để xóa.");
                return;
            }

            var soPhongs = _allRooms.Where(x => ids.Contains(x.MaPhong))
                                    .Select(x => x.SoPhong)
                                    .OrderBy(x => x)
                                    .ToList();

            var msg = (ids.Count == 1)
                ? $"Xóa phòng {soPhongs.First()}?"
                : $"Xóa {ids.Count} phòng: {string.Join(", ", soPhongs)} ?";

            if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return; // chỉ return ở đây, KHÔNG đặt log phía sau return

            try
            {
                foreach (var id in ids)
                {
                    phongService.Xoa(id);
                    var phong = _allRooms.FirstOrDefault(x => x.MaPhong == id);
                    if (phong != null) _allRooms.Remove(phong);
                }

                // LOG OK (ghi 1 log cho thao tác hàng loạt)
                QuanLyPhongKhachSan.UI.Helpers.AuditHelper.LogSuccess(
                    hanhDong: "Xoá",
                    doiTuong: "Phong",
                    khoaChinh: string.Join(",", ids),
                    moTa: $"Xóa phòng số {string.Join(", ", soPhongs)}"
                );

                LoadPhongFromDB();
            }
            catch (Exception ex)
            {
                QuanLyPhongKhachSan.UI.Helpers.AuditHelper.LogFail(
                    hanhDong: "Xoá",
                    doiTuong: "Phong",
                    khoaChinh: string.Join(",", ids),
                    moTa: $"Xóa phòng số {string.Join(", ", soPhongs)}",
                    loi: ex.Message
                );
                MessageBox.Show("Lỗi xóa phòng: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLoaiPhong == null || cbLoaiPhong.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng!!");
                    return;
                }
                if (!int.TryParse(txtSoPhong.Text, out var soPhong) || soPhong <= 0)
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
                    // LOG OK
                    QuanLyPhongKhachSan.UI.Helpers.AuditHelper.LogSuccess(
                        hanhDong: "Thêm",
                        doiTuong: "Phong",
                        khoaChinh: maPhong.ToString(),
                        moTa: $"Thêm phòng số {soPhong}, loại {tenLoai}"
                    );

                    LoadPhongFromDB();
                    txtSoPhong.Clear();
                    if (cbLoaiPhong.Items.Count > 0) cbLoaiPhong.SelectedIndex = 0;
                }
                else
                {
                    // LOG FAIL
                    QuanLyPhongKhachSan.UI.Helpers.AuditHelper.LogFail(
                        hanhDong: "Thêm",
                        doiTuong: "Phong",
                        khoaChinh: "(chưa có)",
                        moTa: $"Thêm phòng số {soPhong}, loại {tenLoai}",
                        loi: "Them() trả về 0"
                    );

                    MessageBox.Show("Thêm phòng thất bại! Vui lòng kiểm tra lại LoaiPhong và dữ liệu đầu vào.");
                }
            }
            catch (Exception ex)
            {
                QuanLyPhongKhachSan.UI.Helpers.AuditHelper.LogFail(
                    hanhDong: "Thêm",
                    doiTuong: "Phong",
                    khoaChinh: "(exception)",
                    moTa: "Lỗi khi thêm phòng",
                    loi: ex.Message
                );
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message);
            }
        }


        private void RefreshPhongPanel(Guna2Panel panelPhong)
        {
            if (panelPhong == null) return;
            var phongTag = panelPhong.Tag as Phong;
            if (phongTag == null) return;

            var phongMoi = phongService.LayPhongTheoMaPhong(phongTag.MaPhong);
            if (phongMoi != null)
            {
                panelPhong.Tag = phongMoi;

                var lblLoai = panelPhong.Controls.Find("lblLoaiPhong", true).OfType<Label>().FirstOrDefault();
                var lblGia = panelPhong.Controls.Find("lblGia", true).OfType<Label>().FirstOrDefault();
                if (lblLoai != null) lblLoai.Text = phongMoi.LoaiPhong ?? "";
                if (lblGia != null) lblGia.Text = phongMoi.Gia.ToString("N0") + "đ";
            }

            var lblKhach = panelPhong.Controls.Find("lblKhach", true).OfType<Label>().FirstOrDefault();
            if (lblKhach != null)
            {
                HienKhachLenPanel((Phong)panelPhong.Tag, lblKhach, panelPhong);
            }
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

            cbLoai.Items.Clear();
            cbLoai.Items.Add("None");
            cbLoai.Items.AddRange(loaiPhongList.ToArray());
            if (cbLoai.Items.Count > 0) cbLoai.SelectedIndex = 0;

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
                MessageBox.Show($"Lỗi khi tải danh sách phòng: {ex.Message}");
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
                f.Text = $"Đổi loại phòng {p.SoPhong}";

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
                    string old = p.LoaiPhong;
                    var tenLoai = cb.SelectedItem?.ToString();
                    int maLoai = phongService.LayMaLoaiTheoTen(tenLoai);
                    if (maLoai <= 0)
                    {
                        MessageBox.Show("Không tìm thấy mã loại phòng.");
                        return;
                    }

                    p.MaLoaiPhong = maLoai;
                    bool ok = phongService.CapNhat(p);

                    AuditHelper.Log("Sửa", "Phong",
                        p.MaPhong.ToString(),
                        moTa: $"Đổi loại phòng {p.SoPhong}: {old} -> {tenLoai}",
                        duLieuCu: old, duLieuMoi: tenLoai,
                        ketQua: ok);

                    if (!ok)
                    {
                        MessageBox.Show("Cập nhật loại phòng thất bại.");
                        return;
                    }

                    LoadPhongFromDB();
                }
            }
        }

        private void DatTrangThaiPhong(Guna2Panel pnl, string trangThai)
        {
            if (pnl == null) return;
            var p = pnl.Tag as Phong;
            if (p == null) return;

            bool ok = phongService.CapNhatTrangThai(p.MaPhong, trangThai);

            AuditHelper.Log("Sửa", "Phong",
                p.MaPhong.ToString(),
                moTa: $"Đặt trạng thái phòng {p.SoPhong} = {trangThai}",
                ketQua: ok);

            if (!ok)
            {
                MessageBox.Show("Cập nhật trạng thái phòng thất bại.");
                return;
            }

            LoadPhongFromDB();
        }

        private void btnThemNhieuKhachHang_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedRooms();
            if (selected.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng.");
                return;
            }

            var items = new List<RoomBookingInfo>();
            foreach (var room in selected)
            {
                var bk = phongService.LayDatPhongTheoMaPhong(room.MaPhong);
                KhachHang kh = (bk != null && !bk.NgayTraThucTe.HasValue)
                    ? khachHangService.LayKhachHangTheoMaKH(bk.MaKH)
                    : null;
                items.Add(new RoomBookingInfo { Room = room, Booking = bk, Customer = kh });
            }

            var emptyRooms = items.Where(x => x.Booking == null || x.Booking.NgayTraThucTe.HasValue).ToList();
            var bookedRooms = items.Where(x => x.Booking != null && !x.Booking.NgayTraThucTe.HasValue).ToList();

            if (emptyRooms.Count > 0 && bookedRooms.Count > 0)
            {
                MessageBox.Show("Vui lòng chỉ chọn các phòng TRỐNG hoặc các phòng của CÙNG 1 khách hàng.");
                return;
            }

            if (bookedRooms.Count > 0)
            {
                var khSet = bookedRooms.Select(x => x.Booking.MaKH).Distinct().ToList();
                if (khSet.Count > 1)
                {
                    MessageBox.Show("Chỉ chọn các phòng của CÙNG 1 khách để sửa.");
                    return;
                }
            }

            if (items.Count == 1)
            {
                var one = items[0];
                try
                {
                    using (var frm = new frmThemvaSuaKH(one.Room))
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
                    MessageBox.Show("Lỗi khi mở form: " + ex.Message);
                }
                return;
            }

            string preTen = "", preCCCD = "", preSDT = "";
            int preMaKH = 0;
            DateTime? preNhan = null, preTra = null;

            if (bookedRooms.Count > 0)
            {
                var anyHasCustomer = bookedRooms.FirstOrDefault(x => x.Customer != null);
                if (anyHasCustomer == null)
                {
                    MessageBox.Show("Có booking nhưng không lấy được thông tin khách hàng.");
                    return;
                }

                var kh = anyHasCustomer.Customer;
                preTen = kh.HoTen ?? "";
                preCCCD = kh.CCCD ?? "";
                preSDT = kh.SDT ?? "";
                preMaKH = kh.MaKH;

                preNhan = bookedRooms.Min(x => x.Booking.NgayNhan).Date;
                preTra = bookedRooms.Max(x => x.Booking.NgayTraDuKien).Date;
                if (preTra <= preNhan) preTra = preNhan.Value.AddDays(1);
            }
            else
            {
                preTen = ""; preCCCD = ""; preSDT = ""; preMaKH = 0;
                preNhan = DateTime.Today;
                preTra = DateTime.Today.AddDays(1);
            }

            try
            {
                using (var frm = new frmThemKH(items, preTen, preCCCD, preSDT, preMaKH, preNhan, preTra))
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
                MessageBox.Show("Lỗi khi mở form: " + ex.Message);
            }
        }
    }
}
