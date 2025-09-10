using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Staff
{
    public partial class frmHoaDon2 : Form
    {
        private readonly HoaDonService _hoaDonService = new HoaDonService();
        private readonly PhongService _phongService = new PhongService();
        private readonly KhachHangService _khachHangService = new KhachHangService();
        private int _maHD;
        private int _maDat;
        private decimal _tongTienLan1;
        private decimal _tienCoc;
        private readonly int _maPhong;

        public frmHoaDon2(int maPhong)
        {
            InitializeComponent();
            _maPhong = maPhong;
            InitGridMapping();
            dgvDichVu.DataSource = new List<DichVuView>(); // Khởi tạo dữ liệu ban đầu
        }

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

            dgvDichVu.AutoGenerateColumns = false;
            MapCol(dgvDichVu, "DichVu", "DichVu", null);
            MapCol(dgvDichVu, "SoTien", "SoTien", "N0");
        }

        private void MapCol(DataGridView dgv, string colName, string dataProperty, string format)
        {
            var col = dgv.Columns.Cast<DataGridViewColumn>()
                         .FirstOrDefault(c => c.Name == colName);
            if (col == null) return;

            col.DataPropertyName = dataProperty;
            if (!string.IsNullOrEmpty(format) && col is DataGridViewTextBoxColumn)
                col.DefaultCellStyle.Format = format;
        }

        public void BindHeader(string loaiHD, DateTime ngayLap, string nhanVien, int maHD, string tenKH, int maDat, decimal tongTienLan1, decimal tienCoc)
        {
            txtLoaiHD.Text = loaiHD;
            txtNgayLapHD.Text = ngayLap.ToString("dd/MM/yyyy HH:mm");
            txtNhanVien.Text = nhanVien;
            txtMaHoaDon.Text = maHD.ToString();
            txtKhachHang.Text = tenKH;
            _maHD = maHD;
            _maDat = maDat;
            _tongTienLan1 = tongTienLan1;
            _tienCoc = tienCoc;
        }

        public void BindChiTietNhieuPhong(IEnumerable<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)> items)
        {
            var rows = new List<CTHDView>();
            DateTime ngayTraThucTe = DateTime.Today;
            var dat = _phongService.LayDatPhongTheoMaPhong(_maDat);
            if (dat?.NgayTraThucTe.HasValue == true) ngayTraThucTe = dat.NgayTraThucTe.Value;

            foreach (var it in items)
            {
                var tu = it.TuNgay.Date;
                var denDuKien = it.DenNgay.Date;
                var denThucTe = ngayTraThucTe > denDuKien ? ngayTraThucTe : denDuKien;
                int soNgayDuKien = it.SoNgay > 0 ? it.SoNgay : (denDuKien - tu).Days;
                int soNgayLo = (denThucTe - denDuKien).Days > 0 ? (denThucTe - denDuKien).Days : 0;

                decimal giaPhong = it.GiaPhong;
                decimal tienPhongDuKien = soNgayDuKien * giaPhong;
                decimal tienPhongLo = soNgayLo * giaPhong;
                decimal tongTien = tienPhongDuKien + tienPhongLo;

                rows.Add(new CTHDView
                {
                    Phong = it.Phong,
                    TuNgay = tu,
                    DenNgay = denThucTe,
                    SoNgay = soNgayDuKien + soNgayLo,
                    TienCoc = 0m,
                    TienPhong = tienPhongDuKien + tienPhongLo,
                    TongTien = tongTien
                });
            }

            dgvCTHD.DataSource = null;
            dgvCTHD.DataSource = rows;

            CapNhatTongTien();
        }

        private void CapNhatTongTien()
        {
            var cthdRows = (List<CTHDView>)dgvCTHD.DataSource ?? new List<CTHDView>();
            var dichVuRows = (List<DichVuView>)dgvDichVu.DataSource ?? new List<DichVuView>();
            decimal tongCTHD = cthdRows.Sum(r => r.TongTien);
            decimal tongDichVu = dichVuRows.Sum(r => r.SoTien);
            decimal tongTien = tongCTHD + tongDichVu;

            var vi = CultureInfo.GetCultureInfo("vi-VN");
            txtTongTien.Text = string.Format(vi, "{0:N0}đ", tongTien);

            decimal soTienConLai = tongTien - (_tongTienLan1 + _tienCoc);
            txtSoTienConLai.Text = string.Format(vi, "{0:N0}đ", soTienConLai);
        }

        // Phương thức public để lấy dữ liệu từ dgvDichVu
        public List<DichVuView> GetDichVuData()
        {
            return (List<DichVuView>)dgvDichVu.DataSource ?? new List<DichVuView>();
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                string tongTienStr = txtTongTien.Text.Replace("đ", "").Replace(",", "");
                if (!decimal.TryParse(tongTienStr, NumberStyles.Any, CultureInfo.GetCultureInfo("vi-VN"), out decimal tongTien))
                {
                    MessageBox.Show("Tổng tiền không hợp lệ! Vui lòng kiểm tra lại.");
                    return;
                }

                if (_maHD <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ!");
                    return;
                }

                var hoaDon = _hoaDonService.LayDanhSach().FirstOrDefault(hd => hd.MaHD == _maHD);
                if (hoaDon == null)
                {
                    MessageBox.Show($"Hóa đơn với mã {_maHD} không tồn tại!");
                    return;
                }

                if (_hoaDonService.CapNhatTongTien(_maHD, tongTien))
                {
                    var dat = _phongService.LayDatPhongTheoMaPhong(_maDat);
                    if (dat != null)
                    {
                        dat.NgayTraThucTe = DateTime.Now;
                        dat.TrangThai = "Trống";
                        if (_phongService.CapNhatDatPhong(dat))
                        {
                            var phong = _phongService.LayPhongTheoMaPhong(_maPhong);
                            if (phong != null)
                            {
                                phong.TrangThai = "Trống";
                                if (_phongService.CapNhat(phong))
                                {
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Lỗi khi reset trạng thái phòng!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi cập nhật trạng thái đặt phòng!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Lỗi khi cập nhật tổng tiền hóa đơn cho mã HD {_maHD}. Vui lòng kiểm tra cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hoàn thành: {ex.Message}");
            }
        }
    }
}
