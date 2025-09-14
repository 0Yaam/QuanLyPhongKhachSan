using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.Staff;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class frmThemvaSuaKH : Form
    {
        private readonly Phong _phong;
        private readonly PhongService phongService = new PhongService();
        private readonly KhachHangService khachHangService = new KhachHangService();
        private readonly HoaDonService _hoaDonService = new HoaDonService();
        private readonly ChiTietHoaDonService _cthdService = new ChiTietHoaDonService();

        private static decimal TienCocMacDinh = 200_000m;
        private readonly string _tenNhanVien = Environment.UserName;
        private int _maDatHienTai = 0;

        private int _soDem = 0;
        private decimal _giaPhong = 0m;
        private decimal _tienThue = 0m;
        private decimal _tamTinh = 0m;

        public frmThemvaSuaKH(Phong phong)
        {
            InitializeComponent();
            _phong = phong ?? throw new ArgumentNullException(nameof(phong));

            LoadLoaiPhong();

            _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
            txtSoPhong.Text = _phong.SoPhong.ToString();
            cbLoaiPhong.SelectedItem = _phong.LoaiPhong;
            txtGia.Text = FormatVnd(_giaPhong);

            dtpNgayNhan.Value = DateTime.Now;
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

            dtpNgayNhan.ValueChanged += dtpNgayNhan_ValueChanged;
            dtpNgayTraDuKien.ValueChanged += dtpNgayTraDuKien_ValueChanged;
            cbLoaiPhong.SelectedIndexChanged += cbLoaiPhong_SelectedIndexChanged;

            LoadDatPhongCu();
            CapNhatTamTinh();
        }

        private void LoadLoaiPhong()
        {
            cbLoaiPhong.Items.Clear();
            foreach (var loai in PhongGiaConfig.GiaPhong.Keys)
                cbLoaiPhong.Items.Add(loai);

            if (cbLoaiPhong.Items.Count > 0 && string.IsNullOrEmpty(_phong.LoaiPhong))
                cbLoaiPhong.SelectedIndex = 0;
        }

        private void LoadDatPhongCu()
        {
            var datPhong = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
            System.Diagnostics.Debug.WriteLine($"LoadDatPhongCu: MaPhong={_phong.MaPhong}, DatPhong={(datPhong != null ? $"MaDat={datPhong.MaDat}, NgayTraThucTe={datPhong.NgayTraThucTe}, TrangThai={datPhong.TrangThai}" : "null")}");

            if (datPhong != null && !datPhong.NgayTraThucTe.HasValue && (datPhong.TrangThai == "Đã đặt" || datPhong.TrangThai == "Đang sử dụng"))
            {
                _maDatHienTai = datPhong.MaDat;

                var kh = khachHangService.LayKhachHangTheoMaKH(datPhong.MaKH);
                if (kh != null)
                {
                    txtTenKH.Text = kh.HoTen;
                    txtCCCD.Text = kh.CCCD;
                    txtSDT.Text = kh.SDT;
                    System.Diagnostics.Debug.WriteLine($"LoadDatPhongCu: Đã điền khách hàng - HoTen={kh.HoTen}, CCCD={kh.CCCD}, SDT={kh.SDT}");
                }

                dtpNgayNhan.Value = datPhong.NgayNhan;
                dtpNgayTraDuKien.Value = datPhong.NgayTraDuKien;
                dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

                cbDaThue.Checked = datPhong.TrangThai == "Đã đặt" || datPhong.TrangThai == "Đang sử dụng";
                var tienThue = datPhong.TienThue;
                txtTamTinh.Text = FormatVnd(tienThue + TienCocMacDinh);
            }
            else
            {
                ClearForm();
                System.Diagnostics.Debug.WriteLine("LoadDatPhongCu: Gọi ClearForm vì không có đặt phòng hợp lệ hoặc đã trả phòng");
            }
        }

        private void CapNhatTamTinh()
        {
            var ngayNhan = dtpNgayNhan.Value.Date;
            var ngayTra = dtpNgayTraDuKien.Value.Date;

            if (ngayTra <= ngayNhan)
            {
                ngayTra = ngayNhan.AddDays(1);
                dtpNgayTraDuKien.Value = ngayTra;
            }

            _soDem = (ngayTra - ngayNhan).Days;
            _tienThue = _giaPhong * _soDem;
            _tamTinh = _tienThue + TienCocMacDinh;

            txtTamTinh.Text = FormatVnd(_tamTinh);
        }

        private static string FormatVnd(decimal v)
        {
            var vi = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return string.Format(vi, "{0:N0}đ", v);
        }

        private decimal LayGiaTheoLoaiPhong(string loai)
        {
            if (string.IsNullOrWhiteSpace(loai)) return 0m;
            return PhongGiaConfig.GiaPhong.TryGetValue(loai, out var gia) ? gia : 0m;
        }

        private void dtpNgayNhan_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayTraDuKien.Value <= dtpNgayNhan.Value)
                dtpNgayTraDuKien.Value = dtpNgayNhan.Value.AddDays(1);
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);
            CapNhatTamTinh();
        }

        private void dtpNgayTraDuKien_ValueChanged(object sender, EventArgs e)
        {
            CapNhatTamTinh();
        }

        private void cbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoaiPhong.SelectedItem != null)
            {
                _phong.LoaiPhong = cbLoaiPhong.SelectedItem.ToString();
                _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
                txtGia.Text = FormatVnd(_giaPhong);
                CapNhatTamTinh();
            }
        }

        public string TenKhachHang => txtTenKH.Text;
        public string CCCD => txtCCCD.Text;
        public string SDT => txtSDT.Text;
        public DateTime NgayNhan => dtpNgayNhan.Value.Date;
        public DateTime NgayTraDuKien => dtpNgayTraDuKien.Value.Date;
        public int SoDem => _soDem;
        public decimal GiaPhong => _giaPhong;
        public decimal TienCoc => TienCocMacDinh;
        public decimal TienThue => _tienThue;
        public decimal TamTinh => _tamTinh;
        public string TrangThai
        {
            get
            {
                var today = DateTime.Today;
                var nhan = dtpNgayNhan.Value.Date;
                var tra = dtpNgayTraDuKien.Value.Date;
                return (today >= nhan && today < tra) ? "Đang sử dụng" : "Đã đặt";
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    System.Diagnostics.Debug.WriteLine("btnHoanThanh_Click: Tên khách hàng trống");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    System.Diagnostics.Debug.WriteLine("btnHoanThanh_Click: Số điện thoại trống");
                    return;
                }
                if (NgayNhan >= NgayTraDuKien)
                {
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Ngày nhận ({NgayNhan}) phải trước ngày trả dự kiến ({NgayTraDuKien})");
                    return;
                }
                if (SoDem <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("btnHoanThanh_Click: Số đêm phải lớn hơn 0");
                    return;
                }

                // Cập nhật loại phòng
                _phong.LoaiPhong = cbLoaiPhong.SelectedItem?.ToString() ?? _phong.LoaiPhong;
                _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
                if (!phongService.CapNhat(_phong))
                {
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Cập nhật loại phòng thất bại - MaPhong={_phong.MaPhong}, LoaiPhong={_phong.LoaiPhong}");
                    return;
                }

                int maKh = khachHangService.UpsertKhachHang(txtTenKH.Text, txtCCCD.Text, txtSDT.Text);
                if (maKh <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Lưu khách hàng thất bại - Ten={txtTenKH.Text}, CCCD={txtCCCD.Text}, SDT={txtSDT.Text}");
                    return;
                }

                string trangThai = TrangThai;
                var datPhong = new DatPhong
                {
                    MaDat = _maDatHienTai,
                    MaKH = maKh,
                    MaPhong = _phong.MaPhong,
                    NgayNhan = NgayNhan,
                    NgayTraDuKien = NgayTraDuKien,
                    NgayTraThucTe = null,
                    TienCoc = TienCocMacDinh,
                    TienThue = TienThue,
                    TrangThai = trangThai
                };

                bool result;
                var existingDat = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                if (existingDat != null && !existingDat.NgayTraThucTe.HasValue && (existingDat.TrangThai == "Đã đặt" || existingDat.TrangThai == "Đang sử dụng"))
                {
                    datPhong.MaDat = existingDat.MaDat;
                    result = phongService.CapNhatDatPhong(datPhong);
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Cập nhật DatPhong - MaDat={datPhong.MaDat}, MaKH={maKh}, MaPhong={_phong.MaPhong}, Result={result}");
                }
                else
                {
                    int maDat = phongService.ThemDatPhong(datPhong);
                    result = maDat > 0;
                    _maDatHienTai = maDat;
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Thêm DatPhong - MaDat={maDat}, MaKH={maKh}, MaPhong={_phong.MaPhong}, Result={result}");
                }

                if (!result)
                {
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Lưu DatPhong thất bại - MaKH={maKh}, MaPhong={_phong.MaPhong}");
                    return;
                }

                // Cập nhật trạng thái phòng
                if (!phongService.CapNhatTrangThai(_phong.MaPhong, trangThai))
                {
                    System.Diagnostics.Debug.WriteLine($"btnHoanThanh_Click: Cập nhật trạng thái phòng thất bại - MaPhong={_phong.MaPhong}, TrangThai={trangThai}");
                    return;
                }

                CapNhatTamTinh();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi btnHoanThanh_Click: {ex.Message}");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra thông tin
                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    System.Diagnostics.Debug.WriteLine("btnInHoaDon_Click: Tên khách hàng trống");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    System.Diagnostics.Debug.WriteLine("btnInHoaDon_Click: Số điện thoại trống");
                    return;
                }
                if (NgayNhan >= NgayTraDuKien)
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Ngày nhận ({NgayNhan}) phải trước ngày trả dự kiến ({NgayTraDuKien})");
                    return;
                }
                if (SoDem <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("btnInHoaDon_Click: Số đêm phải lớn hơn 0");
                    return;
                }

                // Cập nhật loại phòng
                _phong.LoaiPhong = cbLoaiPhong.SelectedItem?.ToString() ?? _phong.LoaiPhong;
                _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
                if (!phongService.CapNhat(_phong))
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Cập nhật loại phòng thất bại - MaPhong={_phong.MaPhong}, LoaiPhong={_phong.LoaiPhong}");
                    return;
                }

                // Lưu hoặc cập nhật khách hàng
                int maKh = khachHangService.UpsertKhachHang(txtTenKH.Text, txtCCCD.Text, txtSDT.Text);
                if (maKh <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Lưu khách hàng thất bại - Ten={txtTenKH.Text}, CCCD={txtCCCD.Text}, SDT={txtSDT.Text}");
                    return;
                }

                string trangThai = TrangThai;
                var datPhong = new DatPhong
                {
                    MaDat = _maDatHienTai,
                    MaKH = maKh,
                    MaPhong = _phong.MaPhong,
                    NgayNhan = NgayNhan,
                    NgayTraDuKien = NgayTraDuKien,
                    NgayTraThucTe = null,
                    TienCoc = TienCocMacDinh,
                    TienThue = TienThue,
                    TrangThai = trangThai
                };

                var existingDat = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                bool result;
                if (existingDat != null && !existingDat.NgayTraThucTe.HasValue && (existingDat.TrangThai == "Đã đặt" || existingDat.TrangThai == "Đang sử dụng"))
                {
                    datPhong.MaDat = existingDat.MaDat;
                    result = phongService.CapNhatDatPhong(datPhong);
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Cập nhật DatPhong - MaDat={datPhong.MaDat}, MaKH={maKh}, MaPhong={_phong.MaPhong}, Result={result}");
                }
                else
                {
                    int maDat = phongService.ThemDatPhong(datPhong);
                    result = maDat > 0;
                    _maDatHienTai = maDat;
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Thêm DatPhong - MaDat={maDat}, MaKH={maKh}, MaPhong={_phong.MaPhong}, Result={result}");
                }

                if (!result)
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Lưu DatPhong thất bại - MaKH={maKh}, MaPhong={_phong.MaPhong}");
                    return;
                }

                if (!phongService.CapNhatTrangThai(_phong.MaPhong, trangThai))
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Cập nhật trạng thái phòng thất bại - MaPhong={_phong.MaPhong}, TrangThai={trangThai}");
                    return;
                }

                string loaiHDDb = "Lần 1";
                int soNgay = Math.Max(1, (NgayTraDuKien - NgayNhan).Days);
                decimal giaPhong = _giaPhong > 0 ? _giaPhong : GiaPhong;
                decimal tienCocValue = TienCocMacDinh;
                decimal tienPhong = soNgay * giaPhong;
                decimal tongTien = tienPhong + tienCocValue;

                var hoaDon = new HoaDon
                {
                    MaDat = _maDatHienTai,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = loaiHDDb,
                    TongThanhToan = tongTien,
                    GhiChu = null
                };

                int maHD = _hoaDonService.ThemVaTraMa(hoaDon);
                if (maHD <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: Lưu hóa đơn thất bại - MaDat={_maDatHienTai}");
                    return;
                }

                _cthdService.Them(new ChiTietHoaDon
                {
                    MaHD = maHD,
                    TenDichVu = $"Tiền phòng - Phòng {_phong.SoPhong} ({soNgay} đêm x {giaPhong:N0})",
                    SoLuong = soNgay,
                    Gia = giaPhong
                });

                _cthdService.Them(new ChiTietHoaDon
                {
                    MaHD = maHD,
                    TenDichVu = $"Tiền cọc - Phòng {_phong.SoPhong}",
                    SoLuong = 1,
                    Gia = tienCocValue
                });

                var kh = khachHangService.LayKhachHangTheoMaKH(maKh);
                string tenKH = kh != null ? kh.HoTen : txtTenKH.Text;
                string soPhongStr = _phong.SoPhong.ToString(); // Chuyển int sang string

                // Ghi lịch sử hóa đơn
                var lichSuSvc = new LichSuHoaDonService();
                int logId = lichSuSvc.Them(new LichSuHoaDon
                {
                    MaHD = maHD,
                    MaDat = _maDatHienTai,
                    ThoiGianIn = DateTime.Now,
                    MaNV = 0, // Thay bằng mã nhân viên thực tế nếu có
                    SoPhong = soPhongStr
                });
                System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: LichSuHoaDon saved with ID: {logId}, MaHD: {maHD}, MaDat: {_maDatHienTai}, SoPhong: {soPhongStr}");
                AppEvents.RaiseInvoiceLogged();

                using (var f = new frmHoaDon1())
                {
                    f.BindHeader(
                        loaiHD: loaiHDDb,
                        ngayLap: DateTime.Now,
                        nhanVien: _tenNhanVien,
                        maHD: maHD,
                        tenKH: tenKH
                    );

                    f.BindChiTietNhieuPhong(new[]
                    {
                (
                    Phong: _phong.SoPhong.ToString(),
                    TuNgay: NgayNhan,
                    DenNgay: NgayTraDuKien,
                    SoNgay: soNgay,
                    TienCoc: tienCocValue,
                    GiaPhong: giaPhong
                )
            });

                    f.ShowDialog(this);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi btnInHoaDon_Click: {ex.Message}");
            }
        }
        private void btnHoaDon2_Click(object sender, EventArgs e)
        {
            try
            {
                var dat = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                if (dat == null || dat.MaDat <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi: Không tìm thấy thông tin đặt phòng hợp lệ. MaPhong={_phong.MaPhong}");
                    return;
                }

                _maDatHienTai = dat.MaDat;

                var hoaDonList = _hoaDonService.LayDanhSach();
                var hoaDonLan1 = hoaDonList.FirstOrDefault(hd => hd.MaDat == _maDatHienTai && hd.LoaiHoaDon == "Lần 1");
                if (hoaDonLan1 == null)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi: Chưa có hóa đơn lần 1. MaDat={_maDatHienTai}");
                    return;
                }

                DateTime tuNgay = dat.NgayNhan.Date;
                DateTime denNgay = dat.NgayTraDuKien.Date;
                if (denNgay <= tuNgay) denNgay = tuNgay.AddDays(1);

                int soNgay = Math.Max(1, (denNgay - tuNgay).Days);
                decimal giaPhong = _giaPhong > 0 ? _giaPhong : GiaPhong;
                decimal tienCocValue = dat.TienCoc > 0 ? dat.TienCoc : 200_000m;

                var hoaDon2 = new HoaDon
                {
                    MaDat = dat.MaDat,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = "Lần 2",
                    TongThanhToan = 0m,
                    GhiChu = null
                };

                int maHD2 = _hoaDonService.ThemVaTraMa(hoaDon2);
                if (maHD2 <= 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Lưu hóa đơn lần 2 thất bại! MaHD={maHD2}");
                    return;
                }

                using (var f = new QuanLyPhongKhachSan.Staff.frmHoaDon2(_phong.MaPhong))
                {
                    var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                    string tenKH = kh != null ? kh.HoTen : "";

                    f.BindHeader(
                        loaiHD: "Hóa đơn lần 2",
                        ngayLap: DateTime.Now,
                        nhanVien: _tenNhanVien,
                        maHD: maHD2,
                        tenKH: tenKH,
                        maDat: _maDatHienTai,
                        tongTienLan1: hoaDonLan1.TongThanhToan != null ? hoaDonLan1.TongThanhToan.Value : 0m,
                        tienCoc: tienCocValue
                    );

                    f.BindChiTietNhieuPhong(new[]
                    {
                (
                    Phong: _phong.SoPhong.ToString(),
                    TuNgay: tuNgay,
                    DenNgay: denNgay,
                    SoNgay: soNgay,
                    TienCoc: 0m,
                    GiaPhong: giaPhong
                )
            });

                    if (f.ShowDialog(this) != DialogResult.OK) return;

                    var dvList = f.GetDichVuData();
                    var vi = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    string dvGhiChu = string.Join(Environment.NewLine,
                        dvList.Where(x => x != null && (!string.IsNullOrWhiteSpace(x.DichVu) || x.SoTien > 0))
                              .Select(x => $"{x.DichVu}: {x.SoTien.ToString("N0", vi)}đ"));

                    // Lấy ngày trả thực tế từ dtpNgayHienTai
                    DateTime denThucTe2 = ((Control)this.Parent)?.Controls.Find("dtpNgayHienTai", true)
                        .OfType<DateTimePicker>().FirstOrDefault()?.Value.Date ?? DateTime.Today;
                    DateTime denDuKien2 = dat.NgayTraDuKien.Date;

                    // Tính số ngày thực tế
                    int soNgayTong2 = Math.Max(1, (denThucTe2 - tuNgay).Days);
                    decimal tongCTHD = soNgayTong2 * giaPhong;
                    decimal tongDV = dvList.Sum(x => x != null ? x.SoTien : 0m);
                    decimal tongLan1 = hoaDonLan1.TongThanhToan != null ? hoaDonLan1.TongThanhToan.Value : 0m;

                    // Tính tiền hóa đơn lần 2
                    decimal soTienLan2;
                    if (denThucTe2 < denDuKien2)
                    {
                        soTienLan2 = (tongCTHD - tongLan1) + tongDV - tienCocValue;
                        if (soTienLan2 < 0) soTienLan2 = 0;
                        System.Diagnostics.Debug.WriteLine($"Trả sớm: soNgayTong2={soNgayTong2}, tongCTHD={tongCTHD}, tongDV={tongDV}, tienCocValue={tienCocValue}, soTienLan2={soTienLan2}");
                    }
                    else
                    {
                        soTienLan2 = (tongCTHD - tongLan1) + tongDV - tienCocValue;
                        System.Diagnostics.Debug.WriteLine($"Trả đúng/muộn: soNgayTong2={soNgayTong2}, tongCTHD={tongCTHD}, tongDV={tongDV}, tienCocValue={tienCocValue}, soTienLan2={soTienLan2}");
                    }

                    bool ok = _hoaDonService.CapNhatTongTienVaGhiChu(maHD2, soTienLan2, dvGhiChu);
                    if (!ok)
                    {
                        System.Diagnostics.Debug.WriteLine($"Cập nhật tổng tiền/ghi chú hóa đơn lần 2 thất bại! MaHD={maHD2}");
                        return;
                    }

                    var lichSuSvc = new LichSuHoaDonService();
                    string soPhongStr = _phong.SoPhong.ToString(); // Chuyển int sang string
                    int logId = lichSuSvc.Them(new LichSuHoaDon
                    {
                        MaHD = maHD2,
                        MaDat = dat.MaDat,
                        ThoiGianIn = DateTime.Now,
                        MaNV = 0, // Thay bằng mã nhân viên thực tế nếu có
                        SoPhong = soPhongStr
                    });
                    System.Diagnostics.Debug.WriteLine($"LichSuHoaDon Lần 2 saved with ID: {logId}, MaHD: {maHD2}, MaDat: {dat.MaDat}, SoPhong: {soPhongStr}");

                    // Reset đặt phòng
                    if (dat != null)
                    {
                        dat.NgayTraThucTe = denThucTe2;
                        dat.TrangThai = "Hoàn thành";
                        if (!phongService.CapNhatDatPhong(dat))
                        {
                            System.Diagnostics.Debug.WriteLine($"Lỗi CapNhatDatPhong: MaDat={dat.MaDat}, NgayTraThucTe={dat.NgayTraThucTe}, TrangThai={dat.TrangThai}");
                            return;
                        }
                        System.Diagnostics.Debug.WriteLine($"Đã cập nhật DatPhong: MaDat={dat.MaDat}, NgayTraThucTe={dat.NgayTraThucTe}, TrangThai={dat.TrangThai}");
                    }

                    var phong = phongService.LayPhongTheoMaPhong(_phong.MaPhong);
                    if (phong != null)
                    {
                        phong.TrangThai = "Trống";
                        if (!phongService.CapNhat(phong))
                        {
                            System.Diagnostics.Debug.WriteLine($"Lỗi CapNhat Phong: MaPhong={phong.MaPhong}, TrangThai={phong.TrangThai}");
                            return;
                        }
                        System.Diagnostics.Debug.WriteLine($"Đã cập nhật Phong: MaPhong={phong.MaPhong}, TrangThai={phong.TrangThai}");
                    }

                    ClearForm();
                    LoadDatPhongCu();

                    AppEvents.RaiseInvoiceLogged();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi btnHoaDon2_Click: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            txtTenKH.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtSDT.Text = string.Empty;
            _maDatHienTai = 0;
            cbDaThue.Checked = false;
            txtTamTinh.Text = "0";
            dtpNgayNhan.Value = DateTime.Today;
            dtpNgayTraDuKien.Value = DateTime.Today.AddDays(1);
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);
            _soDem = 0;
            _tienThue = 0m;
            _tamTinh = 0m;
        }
    }
}