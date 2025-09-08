// QuanLyPhongKhachSan.frmThemvaSuaKH.cs
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.Staff;
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
        private readonly string _tenNhanVien = null;   // người lập hóa đơn
        private int _maDatHienTai = 0;                 // mã đặt phòng hiện tại (nếu có)

        private int _soDem = 0;
        private decimal _giaPhong = 0m;
        private decimal _tienThue = 0m;
        private decimal _tamTinh = 0m;

        public frmThemvaSuaKH(Phong phong)
        {
            InitializeComponent();
            _phong = phong ?? throw new ArgumentNullException(nameof(phong));

            // tên NV: có thể lấy từ session người dùng, tạm dùng tên đăng nhập
            _tenNhanVien = Environment.UserName;

            // Giá theo loại phòng
            _giaPhong = LayGiaTheoLoaiPhong(_phong.LoaiPhong);

            // Hiển thị cơ bản
            txtSoPhong.Text = _phong.SoPhong.ToString();
            cbLoaiPhong.Text = _phong.LoaiPhong;
            txtGia.Text = FormatVnd(_giaPhong);

            // Ngày mặc định
            dtpNgayNhan.Value = DateTime.Now;
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

            dtpNgayNhan.ValueChanged += dtpNgayNhan_ValueChanged;
            dtpNgayTraDuKien.ValueChanged += dtpNgayTraDuKien_ValueChanged;

            LoadDatPhongCu();      // nếu có đặt phòng -> fill + lưu _maDatHienTai
            CapNhatTamTinh();      // tính tạm tính
        }

        private void LoadDatPhongCu()
        {
            var datPhong = phongService.LayDatPhongTheoMaPhong(_phong.MaPhong);
            if (datPhong != null)
            {
                _maDatHienTai = datPhong.MaDat; // LƯU để in HĐ

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
            if (PhongGiaConfig.GiaPhong != null && PhongGiaConfig.GiaPhong.TryGetValue(loai, out var gia))
                return gia;
            return 0m;
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

        // ====== NÚT IN HÓA ĐƠN ======
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                var tenKH = (this.TenKhachHang ?? "").Trim();
                if (string.IsNullOrWhiteSpace(tenKH))
                {
                    MessageBox.Show("Chưa có tên khách hàng.");
                    return;
                }
                if (NgayTraDuKien <= NgayNhan)
                {
                    MessageBox.Show("Ngày trả phải sau ngày nhận.");
                    return;
                }

                // Tính toán
                int soDem = (NgayTraDuKien - NgayNhan).Days;
                if (soDem <= 0) soDem = 1;

                decimal tienCoc = this.TienCoc;
                decimal tienThue = this.TienThue;
                decimal tongTien = tienThue + tienCoc;

                // Tạo hóa đơn
                var hd = new HoaDon
                {
                    MaDat = _maDatHienTai,              // 0 nếu chưa có đặt phòng; nếu DB yêu cầu NOT NULL thì cần tạo DatPhong trước
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = "Lần 1",
                    TongThanhToan = tongTien,
                    GhiChu = ""
                };

                int maHD = _hoaDonService.TaoHoaDon(hd);
                if (maHD <= 0)
                {
                    MessageBox.Show("Lưu hóa đơn thất bại.");
                    return;
                }

                // Mở form hóa đơn
                using (var f = new frmHoaDon1())
                {
                    f.BindHeader(
                        soPhong: _phong?.SoPhong.ToString() ?? "",
                        loaiHD: hd.LoaiHoaDon,
                        ngayLap: hd.NgayLap,
                        nhanVien: string.IsNullOrWhiteSpace(_tenNhanVien) ? Environment.UserName : _tenNhanVien,
                        maHD: maHD,
                        tenKH: tenKH
                    );

                    f.BindChiTiet(
                        tuNgay: this.NgayNhan,
                        denNgay: this.NgayTraDuKien,
                        soNgay: soDem,
                        tienCoc: tienCoc,
                        tongTien: tongTien
                    );

                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }
    }
}
