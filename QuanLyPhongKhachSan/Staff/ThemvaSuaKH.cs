// QuanLyPhongKhachSan.frmThemvaSuaKH.cs
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
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

        public frmThemvaSuaKH(Phong phong)
        {
            InitializeComponent();
            _phong = phong ?? throw new ArgumentNullException(nameof(phong));

            _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);
            txtSoPhong.Text = _phong.SoPhong.ToString();
            cbLoaiPhong.Text = _phong.LoaiPhong;
            txtGia.Text = FormatVnd(_giaPhong);

            dtpNgayNhan.Value = DateTime.Now;
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

            dtpNgayNhan.ValueChanged += dtpNgayNhan_ValueChanged;
            dtpNgayTraDuKien.ValueChanged += dtpNgayTraDuKien_ValueChanged;

            LoadDatPhongCu();
            CapNhatTamTinh();
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

        private static decimal LayGiaTheoLoaiPhong(string loai)
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
        public string TrangThai => cbDaThue.Checked ? "Đã đặt" : _phong.TrangThai;

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

            CapNhatTamTinh();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ====== IN HÓA ĐƠN ======
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

                // Loại hóa đơn: mặc định Lần 1 (sau này bạn có thể tự chọn thêm Lần 2)
                string loaiHDDb = "Lần 1";

                // Tính toán chi tiết
                DateTime tuNgay = dat.NgayNhan.Date;
                DateTime denNgay = dat.NgayTraDuKien.Date;
                if (denNgay <= tuNgay) denNgay = tuNgay.AddDays(1);

                int soNgay = Math.Max(1, (denNgay - tuNgay).Days);
                decimal giaPhong = _giaPhong > 0 ? _giaPhong : GiaPhong;
                decimal tienCoc = dat.TienCoc > 0 ? dat.TienCoc : TienCocMacDinh;
                decimal tongTien = soNgay * giaPhong + tienCoc;

                // Lấy tên khách hàng
                var kh = khachHangService.LayKhachHangTheoMaKH(dat.MaKH);
                string tenKH = kh?.HoTen ?? "";

                using (var f = new frmHoaDon1())
                {
                    // Header hóa đơn
                    f.BindHeader(
                        soPhong: _phong.SoPhong.ToString(),
                        loaiHD: loaiHDDb,
                        ngayLap: DateTime.Now,
                        nhanVien: _tenNhanVien,
                        maHD: dat.MaDat,    // tạm dùng MaDat làm mã hóa đơn hiển thị
                        tenKH: tenKH
                    );

                    // Chi tiết (chỉ 1 dòng: thời gian thuê, cọc, tổng tiền)
                    f.BindChiTiet(
                        tuNgay: tuNgay,
                        denNgay: denNgay,
                        soNgay: soNgay,
                        tienCoc: tienCoc,
                        tongTien: tongTien
                    );

                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hoá đơn: " + ex.Message);
            }
        }


    }
}
