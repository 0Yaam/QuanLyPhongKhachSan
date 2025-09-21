using QuanLyPhongKhachSan.Admin;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlDanhSachTaiKhoan : UserControl
    {
        private readonly NhanSuService nhansu = new NhanSuService();
        private readonly TaiKhoanDAO dao = new TaiKhoanDAO();
        private bool _suppressRowSave = false;

        public UserControlDanhSachTaiKhoan()
        {
            InitializeComponent();
            this.Load += UserControlDanhSachTaiKhoan_Load;
            dgvDanhSachTaiKhoan.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvDanhSachTaiKhoan.RowValidated += dgvDanhSachTaiKhoan_RowValidated;
            dgvDanhSachTaiKhoan.DataError += (s, e) => { /* tránh vỡ khi nhập sai định dạng ngày...*/ };
            txtTimKiem.PlaceholderText = "Tìm kiếm...";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var frm = new frmThemNhanVien())
            {
                var dr = frm.ShowDialog();      // mở modal
                if (dr == DialogResult.OK)      // nếu thêm thành công
                {
                    LoadGrid();                 // -> reload
                }
            }
        }
        private void UserControlDanhSachTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                dgvDanhSachTaiKhoan.AutoGenerateColumns = false;
                var data = nhansu.LayDanhSachNhanVien();
                // ép refresh mạnh tay tránh cache binding
                dgvDanhSachTaiKhoan.DataSource = null;
                dgvDanhSachTaiKhoan.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách nhân viên: " + ex.Message);
            }
        }

        private void dgvDanhSachTaiKhoan_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressRowSave) return; 
            if (e.RowIndex < 0) return;
            var row = dgvDanhSachTaiKhoan.Rows[e.RowIndex];
            var item = row.DataBoundItem as QuanLyPhongKhachSan.DAL.OL.NhanVienView;
            if (item == null) return;

            try
            {
                if (item.MaNV <= 0) return;
                item.TenTaiKhoan = (item.TenTaiKhoan ?? "").Trim();
                item.MatKhau = (item.MatKhau ?? "").Trim();

                var ok = nhansu.CapNhatNhanVienVaTaiKhoan(item);
                // if (!ok) { ... }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dòng: " + ex.Message);
            }
        }





        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void rdSSDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSCCCD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSTen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void rdGiam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdTang_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdSDT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdCCCD_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdTen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private List<NhanVienView> GetSelectedItems()
        {
            return dgvDanhSachTaiKhoan.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as NhanVienView)
                .Where(x => x != null)
                .ToList();
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetSelectedItems();
                if (items.Count == 0)
                {
                    MessageBox.Show("Hãy chọn ít nhất một nhân viên để xóa.");
                    return;
                }

                var tenList = items.Select(x => x.Ten).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var msg = (items.Count == 1)
                    ? $"Xóa nhân viên \"{tenList.FirstOrDefault() ?? ("MaNV=" + items.First().MaNV)}\"?"
                    : $"Xóa {items.Count} nhân viên đã chọn?\n{string.Join(", ", tenList)}";

                if (MessageBox.Show(msg, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                var ids = items.Select(x => x.MaNV).Distinct().ToList();
                var done = nhansu.XoaNhanVienNhieu(ids);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void cmsReset_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetSelectedItems();
                if (items.Count == 0)
                {
                    MessageBox.Show("Hãy chọn ít nhất một dòng để reset mật khẩu.");
                    return;
                }

                var maTKs = items
                    .Where(x => x.MaTK.HasValue && x.MaTK.Value > 0)
                    .Select(x => x.MaTK.Value)
                    .Distinct()
                    .ToList();

                if (maTKs.Count == 0)
                {
                    MessageBox.Show("Các dòng được chọn không có tài khoản để reset.");
                    return;
                }

                if (MessageBox.Show(
                    $"Reset mật khẩu về '123' cho {maTKs.Count} tài khoản?",
                    "Xác nhận reset",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                // CHẶN RowValidated ghi đè
                _suppressRowSave = true;
                // GỌI HÀM BULK mới
                var affected = dao.CapNhatMatKhauByIds(maTKs, "123");

                // Reload để đảm bảo grid hiển thị đúng dữ liệu từ DB
                LoadGrid();

                MessageBox.Show($"Đã reset {affected} tài khoản về '123'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reset: " + ex.Message);
            }
            finally
            {
                _suppressRowSave = false;
            }
        }


        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
