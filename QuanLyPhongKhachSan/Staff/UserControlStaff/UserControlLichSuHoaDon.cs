using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;           // <<< cần để gọi NhanVienDAO
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff.UserControlStaff
{
    public partial class UserControlLichSuHoaDon : UserControl
    {
        private readonly LichSuHoaDonService _svc = new LichSuHoaDonService();

        public UserControlLichSuHoaDon()
        {
            InitializeComponent();
            this.Load += UserControlLichSuHoaDon_Load;

            AppEvents.InvoiceLogged += OnInvoiceLogged;
            this.Disposed += (s, e) => AppEvents.InvoiceLogged -= OnInvoiceLogged;

            txtTimKiem.PlaceholderText = "Tìm kiếm...";
        }

        private void OnInvoiceLogged()
        {
            System.Diagnostics.Debug.WriteLine("OnInvoiceLogged triggered at " + DateTime.Now);
            if (IsHandleCreated && InvokeRequired) BeginInvoke(new Action(RefreshData));
            else RefreshData();
        }

        // =========================
        //  LOAD: tạo cột + map DP
        // =========================
        private void UserControlLichSuHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                dgvLichSu.AutoGenerateColumns = false;

                // Thêm cột nếu chưa có
                if (dgvLichSu.Columns["MaHD"] == null) dgvLichSu.Columns.Add("MaHD", "Mã hóa đơn");
                if (dgvLichSu.Columns["TenNV"] == null) dgvLichSu.Columns.Add("TenNV", "Nhân viên"); // <<< thêm cột NV
                if (dgvLichSu.Columns["TenKH"] == null) dgvLichSu.Columns.Add("TenKH", "Tên KH");
                if (dgvLichSu.Columns["CCCD"] == null) dgvLichSu.Columns.Add("CCCD", "CCCD");
                if (dgvLichSu.Columns["SDT"] == null) dgvLichSu.Columns.Add("SDT", "SĐT");
                if (dgvLichSu.Columns["LoaiHoaDon"] == null) dgvLichSu.Columns.Add("LoaiHoaDon", "Loại hóa đơn");
                if (dgvLichSu.Columns["SoPhong"] == null) dgvLichSu.Columns.Add("SoPhong", "Số phòng");
                if (dgvLichSu.Columns["ThoiGianIn"] == null) dgvLichSu.Columns.Add("ThoiGianIn", "Thời gian");

                // Map DataPropertyName
                dgvLichSu.Columns["MaHD"].DataPropertyName = "MaHD";
                dgvLichSu.Columns["TenNV"].DataPropertyName = "TenNV"; // <<< map DP
                dgvLichSu.Columns["TenKH"].DataPropertyName = "TenKH";
                dgvLichSu.Columns["CCCD"].DataPropertyName = "CCCD";
                dgvLichSu.Columns["SDT"].DataPropertyName = "SDT";
                dgvLichSu.Columns["LoaiHoaDon"].DataPropertyName = "LoaiHoaDon";
                dgvLichSu.Columns["SoPhong"].DataPropertyName = "SoPhong";
                dgvLichSu.Columns["ThoiGianIn"].DataPropertyName = "ThoiGianIn";

                // Order: MaHD (0) -> TenNV (1) -> ...
                dgvLichSu.Columns["MaHD"].DisplayIndex = 0;
                dgvLichSu.Columns["TenNV"].DisplayIndex = 1;    // <<< ngay sau MaHD
                dgvLichSu.Columns["TenKH"].DisplayIndex = 2;
                dgvLichSu.Columns["CCCD"].DisplayIndex = 3;
                dgvLichSu.Columns["SDT"].DisplayIndex = 4;
                dgvLichSu.Columns["LoaiHoaDon"].DisplayIndex = 5;
                dgvLichSu.Columns["SoPhong"].DisplayIndex = 6;
                dgvLichSu.Columns["ThoiGianIn"].DisplayIndex = 7;
                // đăng ký sự kiện trong UserControlLichSuHoaDon_Load:
                dgvLichSu.CellDoubleClick += dgvLichSu_CellDoubleClick;

                dtpTuNgay.Value = DateTime.Today.AddDays(-7);
                dtpDenNgay.Value = DateTime.Today;
                chkLocTheoNgay.Checked = false;
                rdSoPhong.Checked = true;

                dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
                dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;
                chkLocTheoNgay.CheckedChanged += chkLocTheoNgay_CheckedChanged;

                RefreshData();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi UserControlLichSuHoaDon_Load: {ex.Message}");
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================
        //  REFRESH: nạp list + gán TenNV đầy đủ
        // =====================================
        public void RefreshData()
        {
            try
            {
                var list = _svc.LayDanhSach(); // list LichSuHoaDon (có: MaNV, MaHD, TenKH, ...)
                System.Diagnostics.Debug.WriteLine($"RefreshData: Loaded {list?.Count ?? 0} records");

                if (chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                    list = list.Where(x => x.ThoiGianIn >= tuNgay && x.ThoiGianIn <= denNgay).ToList();
                }

                // Build dict MaNV -> TenNV  (1 lần/refresh)
                var nvDao = new NhanVienDAO();
                var nvDict = nvDao.LayDanhSachNhanVien()
                                  .GroupBy(v => v.MaNV)
                                  .ToDictionary(g => g.Key, g => g.First().Ten ?? ""); // NhanVienView.Ten

                var view = list.Select(x => new
                {
                    MaHD = x.MaHD,
                    TenNV = (x.MaNV > 0 && nvDict.ContainsKey(x.MaNV)) ? nvDict[x.MaNV] : "", // <<< gán TenNV
                    TenKH = x.TenKH,
                    CCCD = x.CCCD,
                    SDT = x.SDT,
                    LoaiHoaDon = x.LoaiHoaDon,
                    SoPhong = x.SoPhong,
                    ThoiGianIn = x.ThoiGianIn.ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                dgvLichSu.DataSource = null;
                dgvLichSu.DataSource = view;

                // (Giữ lại logic dọn cột dư SoPhong, nếu Designer có sẵn cột phụ trùng tên)
                int tenKhIndex = dgvLichSu.Columns.Contains("TenKH") ? dgvLichSu.Columns["TenKH"].Index : -1;
                for (int i = dgvLichSu.Columns.Count - 1; i >= 0; i--)
                {
                    string colName = dgvLichSu.Columns[i].Name;
                    if ((colName == "SoPhong" || colName == "SoPhong1" || colName == "RoomNumber") && i == tenKhIndex + 1)
                        dgvLichSu.Columns.RemoveAt(i);
                }

                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================
        //  TÌM KIẾM: filter + vẫn phải gán TenNV cho view kết quả
        // =====================================================
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                var list = _svc.LayDanhSach();

                if (chkLocTheoNgay.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                    list = list.Where(x => x.ThoiGianIn >= tuNgay && x.ThoiGianIn <= denNgay).ToList();
                }

                var filteredList = list;
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    if (rdSoPhong.Checked)
                        filteredList = list.Where(x => (x.SoPhong ?? "").Contains(tuKhoa)).ToList();
                    else if (rdTen.Checked)
                        filteredList = list.Where(x => (x.TenKH ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    else if (rdCCCD.Checked)
                        filteredList = list.Where(x => (x.CCCD ?? "").Contains(tuKhoa)).ToList();
                    else if (rdSDT.Checked)
                        filteredList = list.Where(x => (x.SDT ?? "").Contains(tuKhoa)).ToList();
                }

                // Map TenNV cho tập đã lọc
                var nvDao = new NhanVienDAO();
                var nvDict = nvDao.LayDanhSachNhanVien()
                                  .GroupBy(v => v.MaNV)
                                  .ToDictionary(g => g.Key, g => g.First().Ten ?? "");

                var view = filteredList.Select(x => new
                {
                    MaHD = x.MaHD,
                    TenNV = (x.MaNV > 0 && nvDict.ContainsKey(x.MaNV)) ? nvDict[x.MaNV] : "", // <<< gán TenNV
                    TenKH = x.TenKH,
                    CCCD = x.CCCD,
                    SDT = x.SDT,
                    LoaiHoaDon = x.LoaiHoaDon,
                    SoPhong = x.SoPhong,
                    ThoiGianIn = x.ThoiGianIn.ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                dgvLichSu.DataSource = null;
                dgvLichSu.DataSource = view;

                // cleanup cột SoPhong dư
                int tenKhIndex = dgvLichSu.Columns.Contains("TenKH") ? dgvLichSu.Columns["TenKH"].Index : -1;
                for (int i = dgvLichSu.Columns.Count - 1; i >= 0; i--)
                {
                    string colName = dgvLichSu.Columns[i].Name;
                    if ((colName == "SoPhong" || colName == "SoPhong1" || colName == "RoomNumber") && i == tenKhIndex + 1)
                        dgvLichSu.Columns.RemoveAt(i);
                }

                dgvLichSu.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi txtTimKiem_TextChanged: {ex.Message}");
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                // lấy dữ liệu từ row (view ẩn danh đã bind)
                var row = dgvLichSu.Rows[e.RowIndex];
                int maHD = Convert.ToInt32(row.Cells["MaHD"].Value ?? 0);
                string loai = row.Cells["LoaiHoaDon"]?.Value?.ToString() ?? "";
                string tenKH = row.Cells["TenKH"]?.Value?.ToString() ?? "";
                string tenNV = row.Cells["TenNV"]?.Value?.ToString() ?? ""; // có thể rỗng

                if (maHD <= 0) return;

                var hdSvc = new HoaDonService();
                var cthdSvc = new ChiTietHoaDonService();
                var nvDAO = new NhanVienDAO();

                var hd = hdSvc.LayTheoMa(maHD);
                if (hd == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // fallback tên nhân viên nếu chưa có ở grid
                if (string.IsNullOrWhiteSpace(tenNV))
                {
                    // tìm trong lịch sử (dòng hiện tại) có MaNV không? -> nếu bạn muốn chính xác hơn, mở rộng LichSuHoaDonDAO join TenNV
                    // Ở đây ta chấp nhận để trống nếu không có.
                }

                // Lấy chi tiết để dựng lại view
                var cts = cthdSvc.LayTheoMaHD(maHD);

                if (string.Equals(hd.LoaiHoaDon, "Lần 1", StringComparison.OrdinalIgnoreCase))
                {
                    OpenHoaDonLan1(hd, cts, tenNV, tenKH);
                }
                else
                {
                    OpenHoaDonLan2(hd, cts, tenNV, tenKH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenHoaDonLan1(HoaDon hd, List<ChiTietHoaDon> cts, string tenNV, string tenKH)
        {
            // regex khớp pattern đã in lần 1: "Phòng {Phong} ({SoNgay} đêm x {GiaPhong}) + cọc"
            var re = new Regex(@"Phòng\s+(?<p>\d+)\s*\((?<d>\d+)\s*đêm\s*x\s*(?<g>[\d\.]+)", RegexOptions.IgnoreCase);

            var items = new System.Collections.Generic.List<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)>();

            foreach (var ct in cts)
            {
                var m = re.Match(ct.TenDichVu ?? "");
                if (!m.Success) continue;

                string phong = m.Groups["p"].Value;
                int soNgay = int.TryParse(m.Groups["d"].Value, out var d) ? d : 1;

                // giá hiển thị dạng "1.000.000" -> bỏ dấu chấm
                var giaStr = (m.Groups["g"].Value ?? "").Replace(".", "").Replace(",", "");
                decimal giaPhong = decimal.TryParse(giaStr, out var g) ? g : 0m;

                // Tính tạm TienCoc = Gia (dòng) - soNgay*giaPhong
                decimal tienDong = ct.Gia;
                decimal tienPhong = soNgay * giaPhong;
                decimal coc = Math.Max(0, tienDong - tienPhong);

                // ngày không còn trong DB -> ước lượng: kết thúc = NgayLap.Date, bắt đầu lùi soNgay ngày
                DateTime den = hd.NgayLap.Date;
                DateTime tu = den.AddDays(-Math.Max(1, soNgay));

                items.Add((phong, tu, den, soNgay, coc, giaPhong));
            }

            using (var f = new frmHoaDon1())
            {
                f.BindHeader(
                    loaiHD: hd.LoaiHoaDon ?? "Lần 1",
                    ngayLap: hd.NgayLap,
                    nhanVien: string.IsNullOrWhiteSpace(tenNV) ? Environment.UserName : tenNV,
                    maHD: hd.MaHD,
                    tenKH: tenKH ?? string.Empty
                );

                // nếu parse được thì bind; nếu không, bind rỗng vẫn ok (hiện tổng tiền trong textbox SoTien/TongTien)
                if (items.Count > 0) f.BindChiTietNhieuPhong(items);

                f.ShowDialog(this);
            }
        }

        private void OpenHoaDonLan2(HoaDon hd2, List<ChiTietHoaDon> cts2, string tenNV, string tenKH)
        {
            var hdSvc = new HoaDonService();
            var cthdSvc = new ChiTietHoaDonService();

            // Tìm HĐ lần 1 cùng MaDat
            decimal tongLan1 = 0m;
            decimal tongCoc = 0m;

            if (hd2.MaDat > 0)
            {
                var hd1 = hdSvc.LayLan1TheoMaDat(hd2.MaDat);
                if (hd1 != null)
                {
                    tongLan1 = hd1.TongThanhToan ?? 0m;

                    // lấy tổng cọc từ chi tiết lần 1 (parse như ở trên)
                    var cts1 = cthdSvc.LayTheoMaHD(hd1.MaHD);
                    tongCoc = TinhTongCocTuCTHDLan1(cts1);
                }
            }

            // Parse chi tiết phòng từ TenDichVu (nếu là dòng phòng). 
            // Lần 2 bạn đã bind trước bằng BindChiTietPrecomputed(Phong, Tu, Den, SoNgay, GiaPhong)
            var re = new Regex(@"Phòng\s+(?<p>\d+)\s*\((?<d>\d+)\s*đêm\s*x\s*(?<g>[\d\.]+)", RegexOptions.IgnoreCase);
            var lines = new System.Collections.Generic.List<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal GiaPhong)>();

            foreach (var ct in cts2)
            {
                var m = re.Match(ct.TenDichVu ?? "");
                if (!m.Success) continue;

                string phong = m.Groups["p"].Value;
                int soNgay = int.TryParse(m.Groups["d"].Value, out var d) ? d : 1;
                var giaStr = (m.Groups["g"].Value ?? "").Replace(".", "").Replace(",", "");
                decimal giaPhong = decimal.TryParse(giaStr, out var g) ? g : 0m;

                // ngày không còn trong DB -> ước lượng cho hiển thị
                DateTime den = hd2.NgayLap.Date;
                DateTime tu = den.AddDays(-Math.Max(1, soNgay));

                lines.Add((phong, tu, den, soNgay, giaPhong));
            }

            using (var f = new frmHoaDon2(maPhong: 0)) // maPhong chỉ dùng để tính lố ngày khi đang ở trạng thái active; mở lại chỉ xem
            {
                f.BindHeader(
                    loaiHD: hd2.LoaiHoaDon ?? "Hóa đơn lần 2",
                    ngayLap: hd2.NgayLap,
                    nhanVien: string.IsNullOrWhiteSpace(tenNV) ? Environment.UserName : tenNV,
                    maHD: hd2.MaHD,
                    tenKH: tenKH ?? string.Empty,
                    maDat: hd2.MaDat,
                    tongTienLan1: tongLan1,
                    tienCoc: tongCoc
                );

                if (lines.Count > 0) f.BindChiTietPrecomputed(lines);

                // Form này mặc định có nút Hoàn Thành (ghi lại tổng). Khi xem lại nên vô hiệu để tránh sửa:
                f.Controls.OfType<Button>().Where(b => b.Name == "btnHoanThanh").ToList().ForEach(b => b.Enabled = false);

                f.ShowDialog(this);
            }
        }

        private decimal TinhTongCocTuCTHDLan1(System.Collections.Generic.List<ChiTietHoaDon> cts)
        {
            var re = new Regex(@"Phòng\s+(?<p>\d+)\s*\((?<d>\d+)\s*đêm\s*x\s*(?<g>[\d\.]+)", RegexOptions.IgnoreCase);
            decimal tongCoc = 0m;

            foreach (var ct in cts)
            {
                var m = re.Match(ct.TenDichVu ?? "");
                if (!m.Success) continue;

                int soNgay = int.TryParse(m.Groups["d"].Value, out var d) ? d : 1;
                var giaStr = (m.Groups["g"].Value ?? "").Replace(".", "").Replace(",", "");
                decimal giaPhong = decimal.TryParse(giaStr, out var g) ? g : 0m;

                decimal tienPhong = soNgay * giaPhong;
                decimal coc = Math.Max(0, ct.Gia - tienPhong);
                tongCoc += coc;
            }

            return tongCoc;
        }

        // ====== mấy handler filter ngày/switch ======
        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked) txtTimKiem_TextChanged(sender, e);
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (chkLocTheoNgay.Checked) txtTimKiem_TextChanged(sender, e);
        }

        private void chkLocTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            txtTimKiem_TextChanged(sender, e);
        }

        private void rdSoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSoPhong.Checked) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        }

        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTen.Checked) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        }

        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCCCD.Checked) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSDT.Checked) { txtTimKiem.Clear(); txtTimKiem_TextChanged(sender, e); }
        }
    }
}
