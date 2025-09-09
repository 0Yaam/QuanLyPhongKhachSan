using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff
{
    public partial class frmThemKH : Form
    {
        private readonly List<RoomBookingInfo> _items;
        private readonly string _preTen;
        private readonly string _preCCCD;
        private readonly string _preSDT;
        private readonly int _preMaKH;
        private readonly DateTime? _preNgayNhan;
        private readonly DateTime? _preNgayTraDuKien;

        private readonly PhongService _phongService = new PhongService();
        private readonly KhachHangService _khService = new KhachHangService();

        private decimal _tongTamTinh = 0m;

        public frmThemKH(
            List<RoomBookingInfo> items,
            string tenPrefill,
            string cccdPrefill,
            string sdtPrefill,
            int preMaKh,
            DateTime? ngayNhanPrefill,
            DateTime? ngayTraPrefill
        )
        {
            InitializeComponent();
            _items = items ?? new List<RoomBookingInfo>();
            _preTen = tenPrefill ?? "";
            _preCCCD = cccdPrefill ?? "";
            _preSDT = sdtPrefill ?? "";
            _preMaKH = preMaKh;
            _preNgayNhan = ngayNhanPrefill;
            _preNgayTraDuKien = ngayTraPrefill;
        }

        private void frmThemKH_Load(object sender, EventArgs e)
        {
            try
            {
                txtTamTinh.ReadOnly = true;

                // Prefill thông tin khách hàng
                txtTenKH.Text = _preTen;
                txtCCCD.Text = _preCCCD;
                txtSDT.Text = _preSDT;

                DateTime nhan = _preNgayNhan.HasValue ? _preNgayNhan.Value.Date : DateTime.Today;
                DateTime tra = _preNgayTraDuKien.HasValue ? _preNgayTraDuKien.Value.Date : DateTime.Today.AddDays(1);
                if (tra <= nhan) tra = nhan.AddDays(1);

                dtpNgayNhan.Value = nhan;
                dtpNgayTraDuKien.Value = tra;
                dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);

                dtpNgayNhan.ValueChanged += (s, ev) => OnDateChanged();
                dtpNgayTraDuKien.ValueChanged += (s, ev) => OnDateChanged();

                CapNhatTamTinh();

                if (_preMaKH > 0)
                {
                    this.Text = "Sửa Thông Tin Khách Hàng và Đặt Phòng";
                    btnInHoaDon.Enabled = true;
                }
                else
                {
                    btnInHoaDon.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load form: {ex.Message}");
            }
        }

        private void OnDateChanged()
        {
            if (dtpNgayTraDuKien.Value <= dtpNgayNhan.Value)
                dtpNgayTraDuKien.Value = dtpNgayNhan.Value.AddDays(1);
            dtpNgayTraDuKien.MinDate = dtpNgayNhan.Value.AddDays(1);
            CapNhatTamTinh();
        }

        private void CapNhatTamTinh()
        {
            var nhan = dtpNgayNhan.Value.Date;
            var tra = dtpNgayTraDuKien.Value.Date;
            var soDem = (tra - nhan).Days;
            if (soDem <= 0)
            {
                _tongTamTinh = 0m;
                txtTamTinh.Text = "0";
                return;
            }

            decimal total = 0m;
            foreach (var it in _items)
            {
                var room = it.Room;
                if (room == null) continue;

                decimal gia = PhongGiaConfig.GiaPhong.TryGetValue(room.LoaiPhong, out decimal g) ? g : room.Gia;
                decimal coc = (it.Booking?.TienCoc > 0 ? it.Booking.TienCoc : 200000m);
                total += gia * soDem + coc;
            }

            _tongTamTinh = total;
            txtTamTinh.Text = string.Format(new System.Globalization.CultureInfo("vi-VN"), "{0:N0}đ", total);
        }

        private void btnHoanThanh_Click_1(object sender, EventArgs e)
        {
            try
            {
                var ten = (txtTenKH.Text ?? "").Trim();
                var cccd = (txtCCCD.Text ?? "").Trim();
                var sdt = (txtSDT.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(ten))
                {
                    MessageBox.Show("Nhập tên khách hàng");
                    return;
                }
                if (string.IsNullOrWhiteSpace(sdt))
                {
                    MessageBox.Show("Nhập số điện thoại");
                    return;
                }

                var nhan = dtpNgayNhan.Value.Date;
                var tra = dtpNgayTraDuKien.Value.Date;
                if (tra <= nhan)
                {
                    MessageBox.Show("Ngày trả phải sau ngày nhận");
                    return;
                }

                int maKh = _khService.UpsertKhachHang(ten, cccd, sdt);
                if (maKh <= 0)
                {
                    MessageBox.Show("Lưu khách hàng thất bại");
                    return;
                }

                int ok = 0, fail = 0;
                int soDem = (tra - nhan).Days;

                foreach (var it in _items)
                {
                    var room = it.Room;
                    if (room == null) { fail++; continue; }

                    decimal gia = PhongGiaConfig.GiaPhong.TryGetValue(room.LoaiPhong, out decimal g) ? g : room.Gia;
                    decimal tienThue = gia * soDem;
                    decimal coc = (it.Booking?.TienCoc > 0 ? it.Booking.TienCoc : 200000m);

                    bool conflict;
                    if (it.Booking != null)
                        conflict = _phongService.KiemTraPhongTrungLichExcept(room.MaPhong, nhan, tra, it.Booking.MaDat);
                    else
                        conflict = _phongService.KiemTraPhongTrungLich(room.MaPhong, nhan, tra);

                    if (conflict)
                    {
                        MessageBox.Show($"Phòng {room.SoPhong} bị trùng lịch trong khoảng đã chọn.");
                        fail++;
                        continue;
                    }

                    bool result;
                    if (it.Booking != null)
                    {
                        var b = it.Booking;
                        b.MaKH = maKh;
                        b.NgayNhan = nhan;
                        b.NgayTraDuKien = tra;
                        b.TienCoc = coc;
                        b.TienThue = tienThue;
                        b.TrangThai = "Đã đặt";
                        result = _phongService.CapNhatDatPhong(b);
                    }
                    else
                    {
                        var bnew = new DatPhong(0, maKh, room.MaPhong, nhan, tra, null, coc, tienThue, "Đã đặt");
                        int newId = _phongService.ThemDatPhong(bnew);
                        result = newId > 0;
                    }

                    if (result) ok++; else fail++;
                }

                if (ok > 0 && fail == 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Thành công {ok} phòng, thất bại {fail}.");
                    if (ok > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}");
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                if (_preMaKH <= 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng hợp lệ.");
                    return;
                }

                // Khoảng ngày in
                DateTime tu = dtpNgayNhan.Value.Date;
                DateTime den = dtpNgayTraDuKien.Value.Date;
                if (den <= tu) den = tu.AddDays(1);
                int soNgay = (den - tu).Days;

                // Thông tin KH
                var kh = _khService.LayKhachHangTheoMaKH(_preMaKH);
                string tenKH = kh?.HoTen ?? "";

                // Build các dòng in + gom MaDat
                var lines = new List<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)>();
                var bookingIds = new List<int>();
                decimal tongAll = 0m;
                int skipped = 0;

                foreach (var it in _items)
                {
                    var room = it.Room;
                    if (room == null) { skipped++; continue; }

                    // Lấy booking: ưu tiên it.Booking, nếu null thì hỏi DB
                    DatPhong bk = it.Booking;
                    if (bk == null)
                    {
                        bk = _phongService.LayDatPhongTheoMaPhong(room.MaPhong);
                    }
                    if (bk == null || bk.MaKH != _preMaKH)
                    {
                        skipped++;
                        continue;
                    }

                    // Tính tiền theo DTP form
                    decimal gia = PhongGiaConfig.GiaPhong.TryGetValue(room.LoaiPhong, out var g) ? g : room.Gia;
                    decimal coc = (bk.TienCoc > 0 ? bk.TienCoc : 200000m);
                    decimal tienPhong = soNgay * gia;
                    decimal tongTien = tienPhong + coc;

                    tongAll += tongTien;
                    bookingIds.Add(bk.MaDat);

                    lines.Add((
                        Phong: room.SoPhong.ToString(),
                        TuNgay: tu,
                        DenNgay: den,
                        SoNgay: soNgay,
                        TienCoc: coc,
                        GiaPhong: gia
                    ));
                }

                if (bookingIds.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy đặt phòng (MaDat) cho các phòng đã chọn. Hãy lưu đặt phòng trước khi in.");
                    return;
                }

                // Dùng 1 MaDat đại diện (vì cột MaDat đang NOT NULL)
                int maDatDaiDien = bookingIds.Min();

                // 1) Lưu hóa đơn
                var hoaDon = new HoaDon
                {
                    MaDat = maDatDaiDien,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = "Lần 1",      // khớp constraint
                    TongThanhToan = 0,         // cập nhật sau
                    GhiChu = (skipped > 0) ? $"Bỏ qua {skipped} phòng không có đặt phòng phù hợp" : ""
                };
                var hdSvc = new HoaDonService();
                int maHD = hdSvc.ThemVaLayMa(hoaDon);
                if (maHD <= 0)
                {
                    MessageBox.Show("Không lưu được hóa đơn.");
                    return;
                }

                // 2) Lưu chi tiết
                var ctSvc = new ChiTietHoaDonService();
                foreach (var ln in lines)
                {
                    var ct = new QuanLyPhongKhachSan.DAL.OL.ChiTietHoaDon
                    {
                        MaHD = maHD,
                        TenDichVu = $"Phòng {ln.Phong} ({ln.SoNgay} đêm x {ln.GiaPhong:N0}) + cọc",
                        SoLuong = 1,
                        Gia = ln.SoNgay * ln.GiaPhong + ln.TienCoc
                    };
                    ctSvc.Them(ct);
                }

                // 3) Cập nhật tổng tiền hóa đơn
                hdSvc.CapNhatTongTien(maHD, tongAll);

                // 4) Mở form in
                using (var f = new frmHoaDon1())
                {
                    f.BindHeader(
                        loaiHD: "Lần 1",
                        ngayLap: DateTime.Now,
                        nhanVien: Environment.UserName,
                        maHD: maHD,
                        tenKH: tenKH
                    );
                    f.BindChiTietNhieuPhong(lines);
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