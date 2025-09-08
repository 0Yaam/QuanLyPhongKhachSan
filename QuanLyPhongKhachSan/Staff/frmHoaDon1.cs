using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class frmHoaDon1 : Form
    {
        public frmHoaDon1()
        {
            InitializeComponent();
            InitGridMappingIfNeeded();
        }

        // Model cho 1 dòng CTHD (khớp tên cột)
        private class CTHDRow
        {
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public int SoNgay { get; set; }
            public decimal TienCoc { get; set; }
            public decimal TienPhong { get; set; }
            public decimal TongTien { get; set; }
        }

        // Đảm bảo DataPropertyName khớp tên cột – nếu bạn đã set trong Designer rồi thì đoạn này giữ nguyên
        private void InitGridMappingIfNeeded()
        {
            dgvCTHD.AutoGenerateColumns = false;

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

            col.DataPropertyName = dataProperty; // đảm bảo mapping
            if (!string.IsNullOrEmpty(format) && col is DataGridViewTextBoxColumn)
            {
                col.DefaultCellStyle.Format = format;
            }
        }

        public void BindHeader(
          string soPhong,
          string loaiHD,
          DateTime ngayLap,
          string nhanVien,
          int maHD,
          string tenKH)
        {
            txtPhong.Text = soPhong;
            txtLoaiHD.Text = loaiHD;
            txtNgayLapHD.Text = ngayLap.ToString("dd/MM/yyyy HH:mm");
            txtNhanVien.Text = nhanVien;
            txtMaHoaDon.Text = maHD.ToString();
            txtKhachHang.Text = tenKH;
        }

        // GỌN: chỉ add 1 dòng vào dgvCTHD
        public void BindChiTiet(DateTime tuNgay, DateTime denNgay, int soNgay, decimal tienCoc, decimal tongTien)
        {
            // Nếu cần hiển thị tiền phòng riêng: suy ra từ tổng
            // tongTien = soNgay * giaPhong + tienCoc => TienPhong = tongTien - tienCoc
            decimal tienPhong = tongTien - tienCoc;
            if (tienPhong < 0) tienPhong = 0;

            var rows = new List<CTHDRow>
            {
                new CTHDRow
                {
                    TuNgay   = tuNgay,
                    DenNgay  = denNgay,
                    SoNgay   = soNgay,
                    TienCoc  = tienCoc,
                    TienPhong= tienPhong,
                    TongTien = tongTien
                }
            };

            dgvCTHD.DataSource = null;  // reset an toàn
            dgvCTHD.DataSource = rows;  // bind list -> tên property = DataPropertyName
            decimal tongTatCa = rows.Sum(r => r.TongTien);
            txtTongTien.Text = tongTatCa.ToString("N0");
            txtSoTien.Text = tongTatCa.ToString("N0");
        }
    }
}
