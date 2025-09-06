using System;
using System.Windows.Forms;
using QuanLyPhongKhachSan.Bar; // để thấy class Phong
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan
{
    public partial class frmThemvaSuaKH : Form
    {
        private readonly Phong _phong;

        // Constructor NHẬN PHÒNG để hiển thị Số phòng/Loại/Giá
        public frmThemvaSuaKH(Phong phong)
        {
            InitializeComponent();
            _phong = phong;

            // Hiển thị thông tin phòng ngay khi mở form
            if (txtSoPhong != null) txtSoPhong.Text = _phong.SoPhong.ToString();
            if (cbLoaiPhong != null) cbLoaiPhong.Text =_phong.LoaiPhong;
            if (txtGia != null) txtGia.Text =_phong.Gia.ToString("N0") + "đ";
        }

        // 3 property public để UserControl lấy dữ liệu
        public string TenKhachHang { get { return txtTenKH.Text; } }
        public string CCCD { get { return txtCCCD.Text; } }
        public string SDT { get { return txtSDT.Text; } }

        // Nút HOÀN THÀNH (btnThem) – đặt DialogResult = OK

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            { MessageBox.Show("Nhập tên khách hàng"); return; }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            { MessageBox.Show("Nhập số điện thoại"); return; }

            // OK để UserControlDatPhong nhận và lưu
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
