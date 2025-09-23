using QuanLyPhongKhachSan.Admin;
using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.DAL.DAO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Login.UserControlAdmin
{
    public partial class UserControlLichSuThayDoi : UserControl
    {
        private readonly NhatKyService _logSvc = new NhatKyService();
        private readonly NhanVienDAO _nvDao = new NhanVienDAO();

        // Label thông báo rỗng (tạo runtime để khỏi lệ thuộc Designer)
        private readonly Label labelEmpty = new Label
        {
            AutoSize = false,
            Text = "Không có nhật ký",
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            ForeColor = System.Drawing.Color.DimGray,
            Visible = false
        };

        public UserControlLichSuThayDoi()
        {
            InitializeComponent();
            this.Load += UserControlNhatKy_Load;
        }

        private void UserControlNhatKy_Load(object sender, EventArgs e)
        {
            // CHỈ dùng dgvLog. Nếu trong Designer control đang tên khác (vd dgvLogLog)
            // hãy đổi Name của control đó thành "dgvLog".
            if (dgvLog == null)
            {
                MessageBox.Show("Không tìm thấy DataGridView có tên 'dgvLog'. Hãy mở Designer và đổi Name thành 'dgvLog'.",
                                "Thiếu control", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gắn labelEmpty để hiện khi rỗng
            if (labelEmpty.Parent == null) this.Controls.Add(labelEmpty);
            labelEmpty.BringToFront();

            // Thiết lập grid
            dgvLog.AutoGenerateColumns = false;
            dgvLog.ReadOnly = true;
            dgvLog.AllowUserToAddRows = false;
            dgvLog.AllowUserToDeleteRows = false;
            dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLog.MultiSelect = false;

            // Clear cột cũ để tránh trùng/ẩn
            dgvLog.Columns.Clear();

            // Cột ẩn Id để mở chi tiết
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Visible = false
            };
            dgvLog.Columns.Add(colId);

            // Cột Tên nhân viên
            var colTenNV = new DataGridViewTextBoxColumn
            {
                Name = "TenNV",
                HeaderText = "Tên nhân viên",
                DataPropertyName = "TenNV",
                FillWeight = 45
            };
            dgvLog.Columns.Add(colTenNV);

            // Cột Loại thay đổi
            var colLoai = new DataGridViewTextBoxColumn
            {
                Name = "Loai",
                HeaderText = "Loại thay đổi",
                DataPropertyName = "Loai",
                FillWeight = 55
            };
            dgvLog.Columns.Add(colLoai);

            dgvLog.CellDoubleClick -= dgvLog_CellDoubleClick;
            dgvLog.CellDoubleClick += dgvLog_CellDoubleClick;

            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                var logs = _logSvc.LayDanhSach() ?? new System.Collections.Generic.List<QuanLyPhongKhachSan.DAL.OL.NhatKyHeThong>();
                Debug.WriteLine($"[UserControlLichSuThayDoi] Tổng log: {logs.Count}");

                // Map MaNV -> Tên NV
                var dictNV = _nvDao.LayDanhSachNhanVien()
                                   .GroupBy(x => x.MaNV)
                                   .ToDictionary(g => g.Key, g => g.First().Ten ?? "");

                // View: 3 field (Id ẩn, TenNV, Loai)
                var view = logs.Select(l => new
                {
                    Id = l.Id,
                    TenNV = (l.MaNV.HasValue && dictNV.ContainsKey(l.MaNV.Value))
                                ? dictNV[l.MaNV.Value]
                                : (l.TenDangNhap ?? ""),
                    Loai = $"{(l.HanhDong ?? "").Trim()} / {(l.DoiTuong ?? "").Trim()}"
                }).ToList();

                dgvLog.DataSource = null;
                dgvLog.DataSource = view;
                dgvLog.Refresh();

                labelEmpty.Visible = view.Count == 0;
                if (view.Count > 0)
                {
                    // chọn hàng đầu tiên cho thân thiện
                    dgvLog.ClearSelection();
                    dgvLog.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[UserControlLichSuThayDoi] Lỗi RefreshData: {ex.Message}");
                MessageBox.Show("Lỗi tải nhật ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLog.CurrentRow == null) return;
            var row = dgvLog.Rows[e.RowIndex];

            int id = 0;
            try
            {
                // Lấy từ cột Id ẩn
                id = Convert.ToInt32(row.Cells["Id"].Value ?? 0);
            }
            catch { id = 0; }

            if (id <= 0)
            {
                MessageBox.Show("Không xác định được Id nhật ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var frm = new frmChiTietNhatKy(id))
            {
                frm.ShowDialog(this);
            }
        }

        private void dgvLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbbTenNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
