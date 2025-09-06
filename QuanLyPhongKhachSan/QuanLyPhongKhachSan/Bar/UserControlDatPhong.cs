using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.Bar
{
    public partial class UserControlDatPhong : UserControl
    {

        string connectString = @"Data Source=DESKTOP-LIASEBH;Initial Catalog=QuanLyPhongKhachSan;Integrated Security=True";
        private List<Phong> danhSachPhong = new List<Phong>();
        
        public UserControlDatPhong()
        {
            InitializeComponent();
            txtSoPhong.PlaceholderText = "Số phòng";
            txtGiaPhong.PlaceholderText = "Giá";
        }


        private void LoadPhongFromDB()
        {
            danhSachPhong.Clear();
            flpContain.Controls.Clear();

            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                string query = "SELECT MaPhong, LoaiPhong, Gia, TrangThai FROM Phong";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maPhong = reader.GetInt32(0);
                            int soPhong = reader.GetInt32(0);
                            string loaiPhong = reader.GetString(1);
                            decimal gia = reader.GetDecimal(2);
                            string trangThai = reader.GetString(3);

                            Phong p = new Phong(maPhong,soPhong, loaiPhong, gia, trangThai);
                            danhSachPhong.Add(p);

                            Panel pnl = TaoPhongMoi(p);
                            flpContain.Controls.Add(pnl);
                        }
                    }
                }
            }
        }





        private Panel TaoPhongMoi(Phong phong)
        {
            

            Guna2Panel pnl = new Guna2Panel();
            pnl.Size = new Size(233, 114);
            pnl.BorderRadius = 30;
            pnl.Margin = new Padding(20);
            pnl.FillColor = Color.FromArgb(255, 192, 192);

            pnl.Tag = phong;

            //form moi
            pnl.Click += (s, e) =>
            {
                frmThemvaSuaKH frm = new frmThemvaSuaKH((Phong)((Guna2Panel)s).Tag);
                frm.ShowDialog();
            };

            // so phong
            Label lblSoPhong = new Label();
            lblSoPhong.Location = new Point(30, 10);
            lblSoPhong.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
            lblSoPhong.AutoSize = true;
            lblSoPhong.Text = "Phòng " + phong.SoPhong;
            lblSoPhong.BackColor = Color.Transparent;

            // loai
            Label lblLoaiPhong = new Label();
            lblLoaiPhong.Location = new Point(10, 40);
            lblLoaiPhong.Font = new Font("Microsoft Tai Le", 11, FontStyle.Bold);
            lblLoaiPhong.AutoSize = true;
            lblLoaiPhong.Text = phong.LoaiPhong;
            lblLoaiPhong.BackColor = Color.Transparent;

            // gia
            Label lblGia = new Label();
            lblGia.Location = new Point(150, 90);
            lblGia.Font = new Font("Microsoft Tai Le", 11, FontStyle.Italic);
            lblGia.AutoSize = true;
            lblGia.Text = phong.Gia.ToString("N0") + ".000đ";
            lblGia.BackColor = Color.Transparent;

            // trang thai
            Label lblTrangThai = new Label();
            lblTrangThai.Location = new Point(10, 90);
            lblTrangThai.Font = new Font("Microsoft Tai Le", 11, FontStyle.Bold);
            lblTrangThai.AutoSize = true;
            lblTrangThai.Text = phong.TrangThai;
            lblTrangThai.BackColor = Color.Transparent;

            // Add controls vào panel
            pnl.Controls.Add(lblSoPhong);
            pnl.Controls.Add(lblLoaiPhong);
            pnl.Controls.Add(lblGia);
            //pnl.Controls.Add(lblTrangThai);

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Xóa").Click += (s, e) =>
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(connectString);
                    con.Open();
                    string query = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                        cmd.ExecuteNonQuery();
                    }
                    danhSachPhong.Remove(phong);
                    flpContain.Controls.Remove(pnl);
                }
            };
            menu.Items.Add("Sửa").Click += (s, e) =>
            {
                
            };
            pnl.ContextMenuStrip = menu;


             


            return pnl;
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbLoaiPhong.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!!");
                return;
            }

            if (!int.TryParse(txtSoPhong.Text, out int soPhong) || soPhong <= 0)
            {
                MessageBox.Show("Vui lòng nhập số phòng hợp lệ!!");
                return;
            }
            if (danhSachPhong.Any(p => p.SoPhong == soPhong))
            {
                MessageBox.Show("Số phòng đã tồn tại!");
                return;
            }

            if (!decimal.TryParse(txtGiaPhong.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!!");
                return;
            }

            string loaiPhong = cbLoaiPhong.SelectedItem.ToString();
            string trangThai = "Phòng trống";

            int maPhong;
            string query = "INSERT INTO Phong (SoPhong, LoaiPhong, Gia, TrangThai) " +
                           "VALUES (@SoPhong, @LoaiPhong, @Gia, @TrangThai); " +
                           "SELECT CAST(SCOPE_IDENTITY() AS int);";
            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                    cmd.Parameters.AddWithValue("@Gia", gia);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    maPhong = (int)cmd.ExecuteScalar(); // Lấy MaPhong vừa thêm
                }
            }


            Phong phongMoi = new Phong(maPhong, soPhong, loaiPhong, gia, trangThai);
            danhSachPhong.Add(phongMoi);
            Panel pnl = TaoPhongMoi(phongMoi);
            flpContain.Controls.Add(pnl);
        }


        private void UserControlDatPhong_Load(object sender, EventArgs e)
        {
            LoadPhongFromDB();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
