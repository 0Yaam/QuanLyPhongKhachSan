using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel; // BindingList
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff
{
    public partial class frmHoaDon2 : Form
    {
        private readonly HoaDonService _hoaDonService = new HoaDonService();
        private readonly PhongService _phongService = new PhongService();
        private readonly KhachHangService _khachHangService = new KhachHangService();

        private int _maHD;                  // MaHD của Hóa đơn lần 2
        private int _maDat;                 // MaDat (đặt phòng) đại diện
        private decimal _tongTienLan1;      // Tổng thanh toán của Hóa đơn lần 1
        private decimal _tienCoc;           // Tổng tiền cọc (của HĐ1)
        private readonly int _maPhong;      // MaPhong đang xử lý

        // Nguồn dữ liệu cho dgvDichVu (cho phép nhập trực tiếp)
        private readonly BindingList<DichVuView> _dvBinding = new BindingList<DichVuView>();

        public frmHoaDon2(int maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;

            InitGridMapping();

            // Cấu hình dgvDichVu để nhập trực tiếp dịch vụ
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.AllowUserToAddRows = true;
            dgvDichVu.AllowUserToDeleteRows = true;
            dgvDichVu.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvDichVu.DataSource = _dvBinding;

            // Recalc khi thay đổi dịch vụ
            dgvDichVu.CellValueChanged += (s, e) => CapNhatTongTien();
            dgvDichVu.RowsRemoved += (s, e) => CapNhatTongTien();
            dgvDichVu.UserAddedRow += (s, e) => CapNhatTongTien();
            dgvDichVu.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvDichVu.IsCurrentCellDirty)
                    dgvDichVu.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            // Nếu muốn 3 textbox chỉ để hiển thị, nên ReadOnly = true ở Designer.
        }

        // View model cho dgvCTHD
        private class CTHDView
        {
            public string Phong { get; set; }
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public int SoNgay { get; set; }
            public decimal TienCoc { get; set; }
            public decimal TienPhong { get; set; }
            public decimal TongTien { get; set; }
        }

        // Ánh xạ cột 2 grid
        private void InitGridMapping()
        {
            dgvCTHD.AutoGenerateColumns = false;
            MapCol(dgvCTHD, "Phong", "Phong", null);
            MapCol(dgvCTHD, "TuNgay", "TuNgay", "dd/MM/yyyy");
            MapCol(dgvCTHD, "DenNgay", "DenNgay", "dd/MM/yyyy");
            MapCol(dgvCTHD, "SoNgay", "SoNgay", null);
            MapCol(dgvCTHD, "TienCoc", "TienCoc", "N0");
            MapCol(dgvCTHD, "TienPhong", "TienPhong", "N0");
            MapCol(dgvCTHD, "TongTien", "TongTien", "N0");

            // dgvDichVu phải có 2 cột Name đúng "DichVu" và "SoTien"
            dgvDichVu.AutoGenerateColumns = false;
            MapCol(dgvDichVu, "DichVu", "DichVu", null);
            MapCol(dgvDichVu, "SoTien", "SoTien", "N0");
        }

        private void MapCol(DataGridView dgv, string colName, string dataProperty, string format)
        {
            var col = dgv.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.Name == colName);
            if (col == null) return;

            col.DataPropertyName = dataProperty;
            if (!string.IsNullOrEmpty(format) && col is DataGridViewTextBoxColumn)
                col.DefaultCellStyle.Format = format;
        }

        /// <summary>
        /// Set header và thông số nội bộ: _maHD (HD2), _maDat, _tongTienLan1, _tienCoc
        /// </summary>
        public void BindHeader(string loaiHD, DateTime ngayLap, string nhanVien, int maHD, string tenKH,
                               int maDat, decimal tongTienLan1, decimal tienCoc)
        {
            txtLoaiHD.Text = loaiHD;                      // "Hóa đơn lần 2"
            txtNgayLapHD.Text = ngayLap.ToString("dd/MM/yyyy HH:mm");
            txtNhanVien.Text = nhanVien;
            txtMaHoaDon.Text = maHD.ToString();
            txtKhachHang.Text = tenKH;

            _maHD = maHD;
            _maDat = maDat;
            _tongTienLan1 = tongTienLan1;
            _tienCoc = tienCoc;

            // Hiển thị ngay tổng cọc vào txtTongTienCoc
            var vi = CultureInfo.GetCultureInfo("vi-VN");
            txtTongTienCoc.Text = string.Format(vi, "{0:N0}đ", _tienCoc);
        }

        /// <summary>
        /// Bind chi tiết phòng vào dgvCTHD (tự tính lố ngày nếu có)
        /// </summary>
        public void BindChiTietNhieuPhong(IEnumerable<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)> items)
        {
            // Lấy đặt phòng hiện tại bằng MaPhong (tránh nhầm MaDat)
            var dat = _phongService.LayDatPhongTheoMaPhong(_maPhong);
            DateTime ngayTraThucTe = dat?.NgayTraThucTe ?? DateTime.Today;

            var rows = new List<CTHDView>();
            foreach (var it in items)
            {
                var tu = it.TuNgay.Date;
                var denDuKien = it.DenNgay.Date <= tu ? tu.AddDays(1) : it.DenNgay.Date;
                var denThucTe = (ngayTraThucTe > denDuKien) ? ngayTraThucTe : denDuKien;

                int soNgayDuKien = (denDuKien - tu).Days;
                int soNgayLo = Math.Max(0, (denThucTe - denDuKien).Days);
                int soNgayTong = soNgayDuKien + soNgayLo;
                if (soNgayTong <= 0) soNgayTong = 1; // an toàn

                decimal gia = it.GiaPhong;
                decimal tienPhong = soNgayTong * gia;

                rows.Add(new CTHDView
                {
                    Phong = it.Phong,
                    TuNgay = tu,
                    DenNgay = denThucTe,
                    SoNgay = soNgayTong,
                    TienCoc = 0m,                 // Lần 2 không cộng cọc vào dòng CTHD
                    TienPhong = tienPhong,
                    TongTien = tienPhong
                });
            }

            dgvCTHD.DataSource = rows;
            CapNhatTongTien();
        }

        /// <summary>
        /// Tính và hiển thị:
        /// - txtTongTien = tổng tiền từ dgvCTHD
        /// - txtTongTienCoc = tổng tiền cọc (HĐ1) = _tienCoc
        /// - txtTienSauDichVu = _tienCoc - TổngDịchVụ
        /// </summary>
        private void CapNhatTongTien()
        {
            var vi = CultureInfo.GetCultureInfo("vi-VN");

            // 1) Tổng tiền CTHD (tiền phòng đã tính cả lố ngày nếu có)
            var cthdRows = dgvCTHD.DataSource as List<CTHDView> ?? new List<CTHDView>();
            decimal tongCTHD = cthdRows.Sum(r => r.TongTien);
            txtTongTien.Text = string.Format(vi, "{0:N0}đ", tongCTHD);

            // 2) Tổng dịch vụ
            decimal tongDichVu = 0m;
            foreach (var r in _dvBinding)
            {
                if (r == null) continue;
                if (r.SoTien < 0) r.SoTien = 0; // không âm
                tongDichVu += r.SoTien;
            }

            // 3) Tổng tiền cọc hiển thị (từ HĐ1)
            txtTongTienCoc.Text = string.Format(vi, "{0:N0}đ", _tienCoc);

            // 4) Tiền sau dịch vụ = tiền cọc - tổng dịch vụ (âm/dương đều có thể)
            decimal tienSauDichVu = _tienCoc - tongDichVu;
            txtTienSauDichVu.Text = string.Format(vi, "{0:N0}đ", tienSauDichVu);
        }

        /// <summary>
        /// Lấy dữ liệu dịch vụ NV vừa nhập để caller ghi vào bảng ChiTietHoaDon
        /// </summary>
        public List<DichVuView> GetDichVuData() => _dvBinding
            .Where(x => x != null && (!string.IsNullOrWhiteSpace(x.DichVu) || x.SoTien > 0))
            .ToList();

        private decimal ParseVnd(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0m;
            s = s.Replace("đ", "").Replace(".", "").Replace(",", "").Trim();
            decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v);
            return v;
        }

        /// <summary>
        /// Hoàn thành HĐ2:
        ///   Số tiền LẦN 2 = (Tổng CTHD hiện tại - Tổng HĐ1) + Tổng Dịch Vụ - Tiền Cọc
        ///   -> cập nhật vào HoaDon (HĐ2). Caller sẽ:
        ///     - chèn các dòng ChiTietHoaDon (dịch vụ)
        ///     - reset phòng / đặt phòng
        /// </summary>
        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                var cthdRows = dgvCTHD.DataSource as List<CTHDView> ?? new List<CTHDView>();
                decimal tongCTHD = cthdRows.Sum(r => r.TongTien);
                decimal tongDV = _dvBinding.Sum(r => r?.SoTien ?? 0m);

                decimal soTienLan2 = (tongCTHD - _tongTienLan1) + tongDV - _tienCoc;

                if (_maHD <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ!");
                    return;
                }

                if (!_hoaDonService.CapNhatTongTien(_maHD, soTienLan2))
                {
                    MessageBox.Show("Cập nhật tổng tiền lần 2 thất bại!");
                    return;
                }

                // Thành công -> đóng form, trả OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hoàn thành: {ex.Message}");
            }
        }

        // THÊM vào frmHoaDon2
        public void BindChiTietPrecomputed(IEnumerable<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal GiaPhong)> items)
        {
            var rows = new List<CTHDView>();
            foreach (var it in items)
            {
                int soNgay = Math.Max(1, it.SoNgay);
                decimal tienPhong = soNgay * it.GiaPhong;

                rows.Add(new CTHDView
                {
                    Phong = it.Phong,
                    TuNgay = it.TuNgay.Date,
                    DenNgay = it.DenNgay.Date <= it.TuNgay.Date ? it.TuNgay.Date.AddDays(1) : it.DenNgay.Date,
                    SoNgay = soNgay,
                    TienCoc = 0m,           // Lần 2 không cộng cọc vào dòng chi tiết
                    TienPhong = tienPhong,
                    TongTien = tienPhong
                });
            }

            dgvCTHD.DataSource = rows;
            // Cập nhật lại các textbox tổng/tạm tính
            var vi = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            txtTongTien.Text = string.Format(vi, "{0:N0}đ", rows.Sum(r => r.TongTien));
            // txtTongTienCoc / txtTienSauDichVu sẽ tự cập nhật khi NV nhập dịch vụ (đã gắn event trong ctor)
        }




    }
}
