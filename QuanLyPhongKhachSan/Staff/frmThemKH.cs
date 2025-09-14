using QuanLyPhongKhachSan;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using QuanLyPhongKhachSan.Staff.UserControlStaff;
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

        private void btnHoanThanh_Click(object sender, EventArgs e)
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
                string errorMessages = "";

                foreach (var it in _items)
                {
                    var room = it.Room;
                    if (room == null)
                    {
                        fail++;
                        errorMessages += $"Phòng null; ";
                        continue;
                    }

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
                        errorMessages += $"Phòng {room.SoPhong} bị trùng lịch; ";
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
                        if (!result)
                        {
                            errorMessages += $"Thêm DatPhong cho phòng {room.SoPhong} thất bại (newId={newId}); ";
                        }
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
                    string message = $"Thành công {ok} phòng, thất bại {fail}.";
                    if (!string.IsNullOrEmpty(errorMessages))
                    {
                        message += $"\nChi tiết lỗi: {errorMessages}";
                    }
                    MessageBox.Show(message);
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

                DateTime tu = dtpNgayNhan.Value.Date;
                DateTime den = dtpNgayTraDuKien.Value.Date;
                if (den <= tu) den = tu.AddDays(1);
                int soNgay = (den - tu).Days;

                var kh = _khService.LayKhachHangTheoMaKH(_preMaKH);
                string tenKH = kh?.HoTen ?? "";

                var lines = new List<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)>();
                var bookingIds = new List<int>();
                decimal tongAll = 0m;
                int skipped = 0;

                // Thu thập danh sách số phòng
                var soPhongList = new List<string>();

                foreach (var it in _items)
                {
                    var room = it.Room;
                    if (room == null) { skipped++; continue; }

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

                    decimal gia = PhongGiaConfig.GiaPhong.TryGetValue(room.LoaiPhong, out var g) ? g : room.Gia;
                    decimal coc = (bk.TienCoc > 0 ? bk.TienCoc : 200000m);
                    decimal tienPhong = soNgay * gia;
                    decimal tongTien = tienPhong + coc;

                    tongAll += tongTien;
                    bookingIds.Add(bk.MaDat);
                    soPhongList.Add(room.SoPhong.ToString());

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

                int maDatDaiDien = bookingIds.Min();

                var hoaDon = new HoaDon
                {
                    MaDat = maDatDaiDien,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = "Lần 1",
                    TongThanhToan = 0,
                    GhiChu = (skipped > 0) ? $"Bỏ qua {skipped} phòng không có đặt phòng phù hợp" : ""
                };
                var hdSvc = new HoaDonService();
                int maHD = hdSvc.ThemVaLayMa(hoaDon);
                if (maHD <= 0)
                {
                    MessageBox.Show("Không lưu được hóa đơn.");
                    return;
                }

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

                hdSvc.CapNhatTongTien(maHD, tongAll);

                // Lưu vào LichSuHoaDon với danh sách số phòng trong SoPhong
                var lichSuSvc = new LichSuHoaDonService();
                string soPhongStr = string.Join(" - ", soPhongList);
                int logId = lichSuSvc.Them(new LichSuHoaDon
                {
                    MaHD = maHD,
                    MaDat = maDatDaiDien,
                    ThoiGianIn = DateTime.Now,
                    MaNV = 0,
                    SoPhong = soPhongStr
                });
                System.Diagnostics.Debug.WriteLine($"btnInHoaDon_Click: LichSuHoaDon saved with ID: {logId}, MaHD: {maHD}, MaDat: {maDatDaiDien}, SoPhong: {soPhongStr}");
                AppEvents.RaiseInvoiceLogged();

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

                // Thông báo thành công
                MessageBox.Show(
                    $"Đã lưu Hóa đơn Lần 1 (Mã: {maHD}).\n" +
                    $"Danh sách phòng: {soPhongStr}\n" +
                    $"Tổng tiền: {tongAll:N0}đ\n" +
                    $"Đã ghi lịch sử (Id={logId}).",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }

        private void btnHoaDon2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_preMaKH <= 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng hợp lệ.");
                    return;
                }

                var bookings = new List<(Phong Room, DatPhong Booking)>();
                foreach (var it in _items)
                {
                    var room = it.Room;
                    if (room == null) continue;

                    DatPhong bk = it.Booking ?? _phongService.LayDatPhongTheoMaPhong(room.MaPhong);
                    if (bk == null) continue;
                    if (bk.NgayTraThucTe.HasValue) continue; // đã trả rồi thì bỏ
                    if (bk.MaKH != _preMaKH) continue;       // không phải KH hiện tại

                    bookings.Add((room, bk));
                }

                if (bookings.Count == 0)
                {
                    MessageBox.Show("Không có đặt phòng hợp lệ để in hóa đơn lần 2.");
                    return;
                }

                var maKhSet = bookings.Select(x => x.Booking.MaKH).Distinct().ToList();
                if (maKhSet.Count > 1)
                {
                    MessageBox.Show("Chỉ chọn các phòng của CÙNG 1 khách hàng để in hóa đơn lần 2.");
                    return;
                }

                var kh = _khService.LayKhachHangTheoMaKH(_preMaKH);
                string tenKH = kh?.HoTen ?? "";

                var bookingIds = bookings.Select(x => x.Booking.MaDat).ToList();
                int maDatDaiDien = bookingIds.Min();

                var hdSvc = new HoaDonService();
                var hoaDonList = hdSvc.LayDanhSach();
                var hdLan1 = hoaDonList.FirstOrDefault(hd => hd.MaDat == maDatDaiDien && hd.LoaiHoaDon == "Lần 1");
                if (hdLan1 == null)
                {
                    MessageBox.Show("Chưa có hóa đơn lần 1 (gộp). Vui lòng in hóa đơn lần 1 trước.");
                    return;
                }
                decimal tongLan1 = hdLan1.TongThanhToan ?? 0m;

                decimal tongCoc = 0m;
                var lines = new List<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal GiaPhong)>();
                var soPhongList = new List<string>(); // Thu thập danh sách số phòng

                foreach (var it in bookings)
                {
                    var room = it.Room;
                    var bk = it.Booking;

                    DateTime tu = bk.NgayNhan.Date;
                    DateTime duKien = bk.NgayTraDuKien.Date <= tu ? tu.AddDays(1) : bk.NgayTraDuKien.Date;
                    DateTime thucTe = bk.NgayTraThucTe ?? DateTime.Today;
                    DateTime den = thucTe > duKien ? thucTe.Date : duKien;

                    int soNgay = Math.Max(1, (den - tu).Days);

                    decimal gia = PhongGiaConfig.GiaPhong.TryGetValue(room.LoaiPhong, out var g) ? g : room.Gia;
                    decimal coc = (bk.TienCoc > 0 ? bk.TienCoc : 200000m);
                    tongCoc += coc;

                    soPhongList.Add(room.SoPhong.ToString()); // Thêm số phòng vào danh sách

                    lines.Add((room.SoPhong.ToString(), tu, den, soNgay, gia));
                }

                if (lines.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu phòng hợp lệ để in HĐ lần 2.");
                    return;
                }

                var hd2 = new HoaDon
                {
                    MaDat = maDatDaiDien,
                    NgayLap = DateTime.Now,
                    LoaiHoaDon = "Lần 2",
                    TongThanhToan = 0m,
                    GhiChu = null
                };
                int maHD2 = hdSvc.ThemVaTraMa(hd2);
                if (maHD2 <= 0)
                {
                    MessageBox.Show("Lưu hóa đơn lần 2 thất bại!");
                    return;
                }

                using (var f = new QuanLyPhongKhachSan.Staff.frmHoaDon2(bookings.First().Room.MaPhong))
                {
                    f.BindHeader(
                        loaiHD: "Hóa đơn lần 2",
                        ngayLap: DateTime.Now,
                        nhanVien: Environment.UserName,
                        maHD: maHD2,
                        tenKH: tenKH,
                        maDat: maDatDaiDien,
                        tongTienLan1: tongLan1,
                        tienCoc: tongCoc
                    );

                    f.BindChiTietPrecomputed(lines);

                    if (f.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    var dvList = f.GetDichVuData();
                    var vi = new System.Globalization.CultureInfo("vi-VN");
                    string ghiChuDv = string.Join(Environment.NewLine,
                        dvList.Where(x => x != null && (!string.IsNullOrWhiteSpace(x.DichVu) || x.SoTien > 0))
                              .Select(x => $"{x.DichVu}: {x.SoTien.ToString("N0", vi)}đ"));

                    var datNow = _phongService.LayDatPhongTheoMaPhong(bookings.First().Room.MaPhong);
                    DateTime tuNgay = bookings.First().Booking.NgayNhan.Date;
                    DateTime denDuKien2 = datNow?.NgayTraDuKien.Date ?? lines.Max(l => l.DenNgay);
                    DateTime denThucTe2 = datNow?.NgayTraThucTe ?? DateTime.Today;
                    if (denThucTe2 < denDuKien2) denThucTe2 = denDuKien2;

                    decimal tongCTHD = lines.Sum(l => Math.Max(1, l.SoNgay) * l.GiaPhong);

                    decimal tongDV = dvList.Sum(x => x?.SoTien ?? 0m);

                    // Số tiền lần 2
                    decimal soTienLan2 = (tongCTHD - tongLan1) + tongDV - tongCoc;

                    bool ok = hdSvc.CapNhatTongTienVaGhiChu(maHD2, soTienLan2, ghiChuDv);
                    if (!ok)
                    {
                        MessageBox.Show("Cập nhật tổng tiền/ghi chú hóa đơn lần 2 thất bại!");
                        return;
                    }

                    // Lưu vào LichSuHoaDon với danh sách số phòng trong SoPhong
                    var lichSuSvc = new LichSuHoaDonService();
                    string soPhongStr = string.Join(" - ", soPhongList);
                    int logId2 = lichSuSvc.Them(new LichSuHoaDon
                    {
                        MaHD = maHD2,
                        MaDat = maDatDaiDien,
                        ThoiGianIn = DateTime.Now,
                        SoPhong = soPhongStr,
                        MaNV = 0
                    });
                    System.Diagnostics.Debug.WriteLine($"btnHoaDon2_Click: LichSuHoaDon saved with ID: {logId2}, MaHD: {maHD2}, MaDat: {maDatDaiDien}, SoPhong: {soPhongStr}");
                    AppEvents.RaiseInvoiceLogged();

                    // Thông báo thành công
                    MessageBox.Show(
                        $"Đã lưu Hóa đơn Lần 2 (Mã: {maHD2}).\n" +
                        $"Danh sách phòng: {soPhongStr}\n" +
                        $"Tiền phải thu: {soTienLan2:N0}đ\n" +
                        $"Đã ghi lịch sử (Id={logId2}).",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    foreach (var it in bookings)
                    {
                        var bk = _phongService.LayDatPhongTheoMaPhong(it.Room.MaPhong);
                        if (bk != null)
                        {
                            bk.NgayTraThucTe = DateTime.Now;
                            bk.TrangThai = "Hoàn thành";
                            _phongService.CapNhatDatPhong(bk);
                        }

                        var p = _phongService.LayPhongTheoMaPhong(it.Room.MaPhong);
                        if (p != null)
                        {
                            p.TrangThai = "Trống";
                            _phongService.CapNhat(p);
                        }
                    }


                    if (this.Owner != null && this.Owner is Form parentForm)
                    {
                        foreach (Control ctrl in parentForm.Controls)
                        {
                            if (ctrl is UserControlDatPhong ucDatPhong)
                            {
                                ucDatPhong.RefreshData(); // Gọi phương thức làm mới của UserControl
                                break;
                            }
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in Hóa đơn lần 2: " + ex.Message);
            }
        }
    }
}