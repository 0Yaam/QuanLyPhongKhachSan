using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class PhongDAO
    {
        private string connectionString = Config.ConnectionString;

        public List<Phong> LayDanhSach()
        {
            List<Phong> danhSach = new List<Phong>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaPhong, SoPhong, LoaiPhong, Gia, TrangThai FROM Phong";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Phong p = new Phong
                        {
                            MaPhong = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                            SoPhong = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                            LoaiPhong = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                            Gia = !reader.IsDBNull(3) ? reader.GetDecimal(3) : 0,
                            TrangThai = !reader.IsDBNull(4) ? reader.GetString(4) : ""
                        };
                        danhSach.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return danhSach;
        }

        public int Them(Phong p)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Phong (SoPhong, LoaiPhong, Gia, TrangThai) VALUES (@SoPhong, @LoaiPhong, @Gia, @TrangThai); SELECT CAST(SCOPE_IDENTITY() AS int)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SoPhong", p.SoPhong);
                    cmd.Parameters.AddWithValue("@LoaiPhong", p.LoaiPhong);
                    cmd.Parameters.AddWithValue("@Gia", p.Gia);
                    cmd.Parameters.AddWithValue("@TrangThai", p.TrangThai);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm: " + ex.Message);
                return -1;
            }
        }

        public void Xoa(int maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa: " + ex.Message);
            }
        }
    }
}