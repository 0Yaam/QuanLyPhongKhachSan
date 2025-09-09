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
            MessageBox.Show("Chức năng in hóa đơn đang phát triển.");
        }
    }
}