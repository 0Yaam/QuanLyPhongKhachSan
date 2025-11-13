// Admin/frmThemNhanVien.cs
using System;
using System.Windows.Forms;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.Admin
{
    public partial class frmThemNhanVien : Form
    {
        private readonly NhanSuService _svc = new NhanSuService();

        public frmThemNhanVien()
        {
            InitializeComponent();

            // Nếu bạn chưa set Items trong Designer:
            if (cbbChucVu.Items.Count == 0)
            {
                cbbChucVu.Items.Add("Admin");      // quyền = 1
                cbbChucVu.Items.Add("Nhân viên");  // quyền = 2
                cbbChucVu.SelectedIndex = 1;       // mặc định
            }
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                string tenNV = (txtTemHienThi.Text ?? "").Trim();
                string cccd = (txtCCCD.Text ?? "").Trim();
                string sdt = (txtSDT.Text ?? "").Trim();
                DateTime ngayTG = dtpNgayThamGia.Value.Date;

                string username = (txtUserName.Text ?? "").Trim();
                string password = txtPass.Text ?? "";

                int quyen = (cbbChucVu.SelectedIndex == 0) ? 1 : 2; // 0=Admin->1, 1=Nhân viên->2
                string chucVu = (quyen == 1) ? "Admin" : "Nhân viên";

                if (string.IsNullOrWhiteSpace(tenNV))
                {
                    MessageBox.Show("Vui lòng nhập Tên hiển thị.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu.");
                    return;
                }

                var nv = new NhanVien
                {
                    TenNV = tenNV,
                    CCCD = string.IsNullOrWhiteSpace(cccd) ? null : cccd,
                    SDT = string.IsNullOrWhiteSpace(sdt) ? null : sdt,
                    ChucVu = chucVu,
                    NgayThamGia = ngayTG
                };

                var tk = new TaiKhoan
                {
                    TenDangNhap = username,
                    MatKhau = password, // gợi ý: lưu hash
                    Quyen = quyen
                };

                int maNV = _svc.ThemNhanVienVaTaiKhoan(nv, tk, ganTaiKhoanVaoNhanVien: true);

                MessageBox.Show($"Thêm nhân viên thành công (MaNV={maNV}).");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
