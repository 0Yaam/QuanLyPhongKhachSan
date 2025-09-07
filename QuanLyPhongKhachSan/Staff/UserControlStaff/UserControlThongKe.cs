    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;    
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;



    namespace QuanLyPhongKhachSan.Bar
    {
        public partial class UserControlThongKe : UserControl
        {
            string connectstring = @"Data Source=DESKTOP-LIASEBH;Initial Catalog=QuanLyPhongKhachSan;Integrated Security=True";
            SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adt;
            DataTable dt = new DataTable();

            public UserControlThongKe()
            {
                InitializeComponent();
            }

            private void UserControlThongKe_Load(object sender, EventArgs e)
            {
                con = new SqlConnection(connectstring);

            }

            private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void guna2Button1_Click(object sender, EventArgs e)
            {
                try
                {
                cmd = new SqlCommand("select * from DatPhong", con); // thêm con vào đây
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
