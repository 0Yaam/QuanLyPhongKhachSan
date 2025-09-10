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

        private static decimal TienCocMacDinh = 200_000m;
        private readonly string _tenNhanVien = Environment.UserName;
        private int _maDatHienTai = 0;

        private int _soDem = 0;
        private decimal _giaPhong = 0m;
        private decimal _tienThue = 0m;
        private decimal _tamTinh = 0m;

        private readonly ChiTietHoaDonService _cthdService = new ChiTietHoaDonService();

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
            {
                cbLoaiPhong.Items.Add(loai);
            }
            if (cbLoaiPhong.Items.Count > 0 && string.IsNullOrEmpty(_phong.LoaiPhong))
            {
                cbLoaiPhong.SelectedIndex = 0;
            }
        }

        private void LoadDatPhongCu()
        {
            var datPhong = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
            if (datPhong != null)
            {
                _maDatHienTai = datPhong.MaDat;

                var kh = khachHangService.LayKhachHangTheoMaKH(datPhong.MaKH);
                if (kh != null)
                {
                    txtTenKH.Text = kh.HoTen;
                    txtCCCD.Text = kh.CCCD;
                    txtSDT.Text = kh.SDT;
                }

                dtpNgayNhan.Value = datPhong.NgayNhan;
                dtpNgayTraDuKien.Value = datPhong.NgayTraDuKien;
                dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

                cbDaThue.Checked = datPhong.TrangThai == "Đã đặt" || datPhong.TrangThai == "Đang sử dụng";
                txtTamTinh.Text = FormatVnd(datPhong.TienThue + TienCocMacDinh);
            }
            else
            {
                if (dtpNgayNhan.Value.Date < DateTime.Today)
                    dtpNgayNhan.Value = DateTime.Today;

                dtpNgayTraDuKien.Value = dtpNgayNhan.Value.AddDays(1);
                dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

                cbDaThue.Checked = false;
                txtTamTinh.Text = "0";
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
            decimal gia;
            return PhongGiaConfig.GiaPhong != null && PhongGiaConfig.GiaPhong.TryGetValue(loai, out gia) ? gia : 0m;
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
                if (today >= nhan && today < tra) return "Đang sử dụng";
                return "Đã đặt";
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Nhập tên khách hàng");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Nhập số điện thoại");
                return;
            }
            if (NgayNhan >= NgayTraDuKien)
            {
                MessageBox.Show("Ngày nhận phải trước ngày trả dự kiến!");
                return;
            }
            if (SoDem <= 0)
            {
                MessageBox.Show("Số đêm phải lớn hơn 0!");
                return;
            }

            _phong.LoaiPhong = cbLoaiPhong.SelectedItem?.ToString() ?? _phong.LoaiPhong;
            _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
            if (phongService.CapNhat(_phong))
            {
                CapNhatTamTinh();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật loại phòng thất bại!");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                var dat = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                if (dat == null)
                {
                    MessageBox.Show("Chưa có đặt phòng để lập hoá đơn.");
                    return;
                }

                _maDatHienTai = dat.MaDat;
                string loaiHDDb = "Lần 1";

                DateTime tuNgay = dat.NgayNhan.Date;
                DateTime denNgay = dat.NgayTraDuKien.Date;
                if (denNgay <= tuNgay) denNgay = tuNgay.AddDays(1);

                int soNgay = Math.Max(1, (denNgay - tuNgay).Days);
                decimal giaPhong = _giaPhong > 0 ? _giaPhong : GiaPhong;
                decimal? tienCoc = dat.TienCoc;
                decimal tienCocValue = tienCoc.HasValue ? tienCoc.Value : 200_000m;

                decimal tienPhong = soNgay * giaPhong;
                decimal tongTien = tienPhong + tienCocValue;

                var hoaDon = new HoaDon
                {
                    MaDat = dat.MaDat,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = loaiHDDb,
                    TongThanhToan = tongTien,
                    GhiChu = null
                };

                int maHD = _hoaDonService.ThemVaTraMa(hoaDon);
                if (maHD <= 0)
                {
                    MessageBox.Show("Lưu hóa đơn thất bại!");
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

                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                string tenKH = kh?.HoTen ?? "";

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
                            TuNgay: tuNgay,
                            DenNgay: denNgay,
                            SoNgay: soNgay,
                            TienCoc: tienCocValue,
                            GiaPhong: giaPhong
                        )
                    });

                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hoá đơn: " + ex.Message);
            }
        }

        private void btnHoaDon2_Click(object sender, EventArgs e)
        {
            try
            {
                var dat = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                if (dat == null || dat.MaDat <= 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin đặt phòng hợp lệ.");
                    return;
                }

                _maDatHienTai = dat.MaDat;

                // Lấy hóa đơn lần 1 để biết tổng lần 1
                var hoaDonList = _hoaDonService.LayDanhSach();
                var hoaDonLan1 = hoaDonList.FirstOrDefault(hd => hd.MaDat == _maDatHienTai && hd.LoaiHoaDon == "Lần 1");
                if (hoaDonLan1 == null)
                {
                    MessageBox.Show("Chưa có hóa đơn lần 1. Vui lòng in hóa đơn lần 1 trước.");
                    return;
                }

                // Lưu DB bắt buộc dùng "Lần 2" để không vi phạm CHECK constraint
                string loaiHDDb = "Lần 2";

                DateTime tuNgay = dat.NgayNhan.Date;
                DateTime denNgay = dat.NgayTraDuKien.Date;
                if (denNgay <= tuNgay) denNgay = tuNgay.AddDays(1);

                int soNgay = Math.Max(1, (denNgay - tuNgay).Days);
                decimal giaPhong = _giaPhong > 0 ? _giaPhong : GiaPhong;

                // Nếu model DatPhong.TienCoc là decimal?
                decimal tienCocValue;
                {
                    decimal? tienCocNullable = dat.TienCoc; // giả định DatPhong.TienCoc là nullable decimal
                    tienCocValue = tienCocNullable.HasValue ? tienCocNullable.Value : 200_000m;
                }

                // 1) Tạo HĐ2 (tạm thời tổng = 0, ghi chú = null). Service/DAO chịu trách nhiệm insert.
                var hoaDon2 = new HoaDon
                {
                    MaDat = dat.MaDat,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = loaiHDDb,
                    TongThanhToan = 0m,
                    GhiChu = null
                };

                int maHD2 = _hoaDonService.ThemVaTraMa(hoaDon2);
                if (maHD2 <= 0)
                {
                    MessageBox.Show($"Lưu hóa đơn lần 2 thất bại! MaHD = {maHD2}. Vui lòng kiểm tra kết nối hoặc dữ liệu.");
                    return;
                }

                // 2) Mở form Hóa đơn 2 để NV nhập dịch vụ, xem lố ngày...
                using (var f = new QuanLyPhongKhachSan.Staff.frmHoaDon2(_phong.MaPhong))
                {
                    var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                    string tenKH = kh?.HoTen ?? "";

                    f.BindHeader(
                        loaiHD: "Hóa đơn lần 2",               // Text hiển thị, DB vẫn là "Lần 2"
                        ngayLap: DateTime.Now,
                        nhanVien: _tenNhanVien,
                        maHD: maHD2,
                        tenKH: tenKH,
                        maDat: _maDatHienTai,
                        tongTienLan1: hoaDonLan1.TongThanhToan ?? 0m,
                        tienCoc: tienCocValue
                    );

                    f.BindChiTietNhieuPhong(new[]
                    {
                (
                    Phong: _phong.SoPhong.ToString(),
                    TuNgay: tuNgay,
                    DenNgay: denNgay,
                    SoNgay: soNgay,
                    TienCoc: 0m,           // lần 2 không cộng cọc vào CTHD
                    GiaPhong: giaPhong
                )
            });

                    if (f.ShowDialog(this) == DialogResult.OK)
                    {
                        // 3) Ghép GHI CHÚ từ danh sách dịch vụ NV nhập
                        var dvList = f.GetDichVuData();
                        var vi = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                        string dvGhiChu = string.Join(Environment.NewLine,
                            dvList.Where(x => x != null && (!string.IsNullOrWhiteSpace(x.DichVu) || x.SoTien > 0))
                                  .Select(x => $"{x.DichVu}: {x.SoTien.ToString("N0", vi)}đ"));

                        // 4) TÍNH TỔNG CTHD HIỆN TẠI (tiền phòng đã tính lố ngày nếu có)
                        var datNow = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                        DateTime denDuKien2 = datNow?.NgayTraDuKien.Date ?? denNgay;
                        DateTime denThucTe2 = datNow?.NgayTraThucTe ?? DateTime.Today;
                        if (denThucTe2 < denDuKien2) denThucTe2 = denDuKien2;
                        int soNgayTong2 = Math.Max(1, (denThucTe2 - tuNgay).Days);
                        decimal tongCTHD = soNgayTong2 * giaPhong;

                        // 5) Tổng dịch vụ
                        decimal tongDV = dvList.Sum(x => x?.SoTien ?? 0m);

                        // 6) Tổng lần 1
                        decimal tongLan1 = hoaDonLan1.TongThanhToan ?? 0m;

                        // 7) Số tiền LẦN 2 theo công thức của bạn
                        decimal soTienLan2 = (tongCTHD - tongLan1) + tongDV - tienCocValue;

                        // 8) Cập nhật tổng tiền + ghi chú vào HĐ2 qua BLL/DAO (KHÔNG truy vấn trực tiếp ở UI)
                        //    ==> YÊU CẦU: HoaDonService có hàm CapNhatTongTienVaGhiChu(int maHD, decimal tong, string ghiChu)
                        bool ok = _hoaDonService.CapNhatTongTienVaGhiChu(maHD2, soTienLan2, dvGhiChu);
                        if (!ok)
                        {
                            MessageBox.Show("Cập nhật tổng tiền/ghi chú hóa đơn lần 2 thất bại!");
                            return;
                        }

                        // 9) Reset phòng/đặt phòng sau khi hoàn tất HĐ2 (qua service)
                        var datReset = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
                        if (datReset != null)
                        {
                            datReset.NgayTraThucTe = DateTime.Now;
                            datReset.TrangThai = "Trống";
                            phongService.CapNhatDatPhong(datReset);
                        }

                        var phong = phongService.LayPhongTheoMaPhong(_phong.MaPhong);
                        if (phong != null)
                        {
                            phong.TrangThai = "Trống";
                            phongService.CapNhat(phong);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở/lưu hóa đơn lần 2: " + ex.Message);
            }
        }


    }
}