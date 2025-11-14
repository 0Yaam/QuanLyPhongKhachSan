using QuanLyPhongKhachSan.BLL.Services;
using QuanLyPhongKhachSan.Common;
using QuanLyPhongKhachSan.DAL.DAO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlTaiKhoan : UserControl
    {
        public UserControlTaiKhoan()
        {
            InitializeComponent();
            this.Load += UserControlTaiKhoan_Load;
        }

        private void UserControlTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                string ten = CurrentUser.TenDangNhap;
                DateTime? ngay = null;

                if (CurrentUser.MaNV.HasValue && CurrentUser.MaNV.Value > 0)
                {
                    var nvDao = new NhanVienDAO();
                    var nv = nvDao.LayTheoMa(CurrentUser.MaNV.Value);
                    if (nv != null)
                    {
                        ten = string.IsNullOrWhiteSpace(nv.TenNV) ? ten : nv.TenNV;
                        ngay = nv.NgayThamGia;
                    }
                }

                lblTen.Text = string.IsNullOrWhiteSpace(ten) ? "—" : ten;
                lblNgayThamGia.Text = (ngay ?? CurrentUser.NgayThamGia ?? DateTime.Today)
                    .ToString("dd/MM/yyyy");

                // cache lại hiển thị cho form khác dùng (cập nhật theo thông tin hiện tại)
                CurrentUser.TenHienThi = lblTen.Text;
                CurrentUser.NgayThamGia = (ngay ?? CurrentUser.NgayThamGia ?? DateTime.Today);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin tài khoản: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblTen.Text = ""; lblNgayThamGia.Text = "";
                if (pbAvatar.Image != null) { pbAvatar.Image.Dispose(); pbAvatar.Image = null; }

                CurrentUser.Reset();

                var login = new frmLogin { StartPosition = FormStartPosition.CenterScreen };
                login.Show();
                this.FindForm()?.Hide();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (pbAvatar.Image != null) { pbAvatar.Image.Dispose(); pbAvatar.Image = null; }
                        pbAvatar.Image = new Bitmap(ofd.FileName);
                        pbAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbAvatar.Tag = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải ảnh: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
