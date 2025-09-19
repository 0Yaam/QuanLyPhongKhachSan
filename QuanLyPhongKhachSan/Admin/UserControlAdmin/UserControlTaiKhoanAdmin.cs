using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongKhachSan.Admin.UserControlAdmin
{
    public partial class UserControlTaiKhoanAdmin : UserControl
    {
        public UserControlTaiKhoanAdmin()
        {
            InitializeComponent();
        }


        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // txtTenNhanVien.Text = ""; txtNgayThamGia.Text = ""; pbAvatar.Image = null; ...

                var login = new frmLogin();
                login.StartPosition = FormStartPosition.CenterScreen;
                login.Show();

                var currentForm = this.FindForm();
                currentForm?.Hide();
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
                        if (pbAvatar.Image != null)
                        {
                            pbAvatar.Image.Dispose();
                            pbAvatar.Image = null;
                        }

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
