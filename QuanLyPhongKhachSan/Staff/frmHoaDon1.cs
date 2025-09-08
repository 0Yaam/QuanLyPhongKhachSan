using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan
{
    public partial class frmHoaDon1 : Form
    {
        public frmHoaDon1()
        {
            InitializeComponent();
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

        public void BindChiTiet(DateTime tuNgay, DateTime denNgay, int soNgay, decimal tienCoc, decimal tongTien)
        {
            dgvCTHD.AutoGenerateColumns = false;
            if (dgvCTHD.Columns.Count == 0)
            {
                dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { Name = "TuNgay", HeaderText = "Từ ngày", DataPropertyName = "TuNgay", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { Name = "DenNgay", HeaderText = "Đến ngày", DataPropertyName = "DenNgay", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoNgay", HeaderText = "Số ngày", DataPropertyName = "SoNgay", Width = 80 });
                dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { Name = "TienCoc", HeaderText = "Tiền cọc", DataPropertyName = "TienCoc", Width = 120, DefaultCellStyle = { Format = "N0" } });
                dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongTien", HeaderText = "Tổng tiền", DataPropertyName = "TongTien", Width = 120, DefaultCellStyle = { Format = "N0" } });
            }

            var dt = new DataTable();
            dt.Columns.Add("TuNgay", typeof(string));
            dt.Columns.Add("DenNgay", typeof(string));
            dt.Columns.Add("SoNgay", typeof(int));
            dt.Columns.Add("TienCoc", typeof(decimal));
            dt.Columns.Add("TongTien", typeof(decimal));

            dt.Rows.Add(
                tuNgay.ToString("dd/MM/yyyy"),
                denNgay.ToString("dd/MM/yyyy"),
                soNgay,
                tienCoc,
                tongTien
            );

            dgvCTHD.DataSource = dt;
        }
    }
}
