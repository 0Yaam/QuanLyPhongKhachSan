using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.Admin
{
    public partial class frmThemNhanVien : Form
    {
        private readonly NhanVienDAO _nvDAO = new NhanVienDAO();
        private readonly TaiKhoanDAO _tkDAO = new TaiKhoanDAO();

        public frmThemNhanVien()
        {
            InitializeComponent();
            this.Load -= frmThemNhanVien_Load;
            this.Load += frmThemNhanVien_Load;

            btnHoanThanh.Click -= btnHoanThanh_Click;
            btnHoanThanh.Click += btnHoanThanh_Click;

            cbbChucVu.SelectedIndexChanged -= cbbChucVu_SelectedIndexChanged;
            cbbChucVu.SelectedIndexChanged += cbbChucVu_SelectedIndexChanged;
        }

        private void frmThemNhanVien_Load(object sender, EventArgs e)
        {
            // Đổ chức vụ
            cbbChucVu.Items.Clear();
            cbbChucVu.Items.Add("Admin");       // -> quyền 1
            cbbChucVu.Items.Add("Nhân viên");   // -> quyền 2
            cbbChucVu.SelectedIndex = 1;        // mặc định "Nhân viên"

            // Clear form
            txtTemHienThi.Clear();   // TenNV
            txtCCCD.Clear();
            txtSDT.Clear();
            txtUserName.Clear();
            txtPass.Clear();
        }

        private void cbbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu cần xử lý gì thêm khi đổi chức vụ thì làm ở đây
        }

        private int MapQuyenFromChucVu(string chucVu)
        {
            if (string.Equals(chucVu, "Admin", StringComparison.OrdinalIgnoreCase)) return 1;
            return 2; // Nhân viên
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            try
            {
                // ==== 1) Validate cơ bản ====
                string tenNV = (txtTemHienThi.Text ?? "").Trim();
                string cccd = string.IsNullOrWhiteSpace(txtCCCD.Text) ? null : txtCCCD.Text.Trim();
                string sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
                string chucVu = (cbbChucVu.SelectedItem?.ToString() ?? "Nhân viên").Trim();

                string username = (txtUserName.Text ?? "").Trim();
                string password = (txtPass.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(tenNV))
                {
                    MessageBox.Show("Vui lòng nhập Tên nhân viên.");
                    txtTemHienThi.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập.");
                    txtUserName.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu.");
                    txtPass.Focus();
                    return;
                }

                // Char(50) trong DB -> cắt bớt nếu dài
                if (username.Length > 50) username = username.Substring(0, 50);
                if (password.Length > 50) password = password.Substring(0, 50);

                // Check trùng username
                if (_tkDAO.TenDangNhapDaTonTai(username))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.");
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                    return;
                }

                // ==== 2) Thêm NhanVien (lấy MaNV) ====
                var nv = new NhanVien
                {
                    TenNV = tenNV,
                    CCCD = cccd,
                    SDT = sdt,
                    ChucVu = chucVu
                };

                int maNV = _nvDAO.ThemTraMa(nv);
                if (maNV <= 0)
                {
                    MessageBox.Show("Thêm nhân viên thất bại.");
                    return;
                }

                // ==== 3) Thêm TaiKhoan ====
                int quyen = MapQuyenFromChucVu(chucVu);
                bool ok = _tkDAO.Them(username, password, quyen, maNV);
                if (!ok)
                {
                    MessageBox.Show("Thêm tài khoản thất bại. (Nhân viên đã được tạo)");
                    return;
                }

                MessageBox.Show("Thêm nhân viên & tài khoản thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // ==== Các handler trống sẵn trong Designer (giữ cho khỏi lỗi) ====
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void txtTemHienThi_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel9_Click(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }
        private void txtCCCD_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel2_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel3_Click(object sender, EventArgs e) { }
        private void txtPass_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel4_Click(object sender, EventArgs e) { }
        private void txtUserName_TextChanged(object sender, EventArgs e) { }
        private void guna2HtmlLabel5_Click(object sender, EventArgs e) { }
    }
}
