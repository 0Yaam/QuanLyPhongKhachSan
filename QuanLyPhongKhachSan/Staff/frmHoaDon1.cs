using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class frmHoaDon1 : Form
    {
        public frmHoaDon1()
        {
            InitializeComponent();
            InitGridMapping();
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
            MapCol("Phong", "Phong", null);
            MapCol("TuNgay", "TuNgay", "dd/MM/yyyy");
            MapCol("DenNgay", "DenNgay", "dd/MM/yyyy");
            MapCol("SoNgay", "SoNgay", null);
            MapCol("TienCoc", "TienCoc", "N0");
            MapCol("TienPhong", "TienPhong", "N0");
            MapCol("TongTien", "TongTien", "N0");
        }

        private void MapCol(string colName, string dataProperty, string format)
        {
            var col = dgvCTHD.Columns.Cast<DataGridViewColumn>()
                         .FirstOrDefault(c => c.Name == colName);
            if (col == null) return;

            col.DataPropertyName = dataProperty;
            if (!string.IsNullOrEmpty(format) && col is DataGridViewTextBoxColumn)
                col.DefaultCellStyle.Format = format;
        }

        // Header KHÔNG có tham số soPhong (vì in nhiều phòng)
        public void BindHeader(string loaiHD, DateTime ngayLap, string nhanVien, int maHD, string tenKH)
        {
            txtLoaiHD.Text = loaiHD;
            txtNgayLapHD.Text = ngayLap.ToString("dd/MM/yyyy HH:mm");
            txtNhanVien.Text = nhanVien;
            txtMaHoaDon.Text = maHD.ToString();
            txtKhachHang.Text = tenKH;
        }

        public void BindChiTietNhieuPhong(IEnumerable<(string Phong, DateTime TuNgay, DateTime DenNgay, int SoNgay, decimal TienCoc, decimal GiaPhong)> items)
        {
            var rows = new List<CTHDView>();

            foreach (var it in items)
            {
                var tu = it.TuNgay.Date;
                var den = it.DenNgay.Date <= tu ? tu.AddDays(1) : it.DenNgay.Date;
                int so = it.SoNgay > 0 ? it.SoNgay : (den - tu).Days;

                decimal tienPhong = so * it.GiaPhong;
                decimal tong = tienPhong + it.TienCoc;

                rows.Add(new CTHDView
                {
                    Phong = it.Phong,
                    TuNgay = tu,
                    DenNgay = den,
                    SoNgay = so,
                    TienCoc = it.TienCoc,
                    TienPhong = tienPhong,
                    TongTien = tong
                });
            }

            dgvCTHD.DataSource = null;
            dgvCTHD.DataSource = rows;

            var vi = CultureInfo.GetCultureInfo("vi-VN");
            decimal sum = rows.Sum(r => r.TongTien);
            txtTongTien.Text = string.Format(vi, "{0:N0}", sum);
            txtSoTien.Text = string.Format(vi, "{0:N0}", sum);
        }
    }
}
