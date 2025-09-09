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
                            MaPhong = reader.GetInt32(0),
                            SoPhong = reader.GetInt32(1),
                            LoaiPhong = reader.GetString(2),
                            Gia = reader.GetDecimal(3),
                            TrangThai = reader.GetString(4)
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
                    // Xóa các bản ghi DatPhong liên quan
                    string sqlDatPhong = "DELETE FROM DatPhong WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(sqlDatPhong, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }

                    // Xóa phòng
                    string sqlPhong = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmd = new SqlCommand(sqlPhong, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa phòng: " + ex.Message);
                throw; // Ném lỗi để xử lý ở tầng trên
            }
        }

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 * FROM DatPhong WHERE MaPhong = @MaPhong AND NgayTraThucTe IS NULL ORDER BY NgayNhan DESC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DatPhong
                            {
                                MaDat = Convert.ToInt32(reader["MaDat"]),
                                MaKH = Convert.ToInt32(reader["MaKH"]),
                                MaPhong = Convert.ToInt32(reader["MaPhong"]),
                                NgayNhan = Convert.ToDateTime(reader["NgayNhan"]),
                                NgayTraDuKien = Convert.ToDateTime(reader["NgayTraDuKien"]),
                                NgayTraThucTe = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                TienCoc = Convert.ToDecimal(reader["TienCoc"]),
                                TienThue = Convert.ToDecimal(reader["TienThue"]),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayDatPhongTheoMaPhong: {ex.Message}");
            }
            return null;
        }


        //                            NgayTraThucTe = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
        public int ThemDatPhong(DatPhong datPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO DatPhong (MaKH, MaPhong, NgayNhan, NgayTraDuKien, TienCoc, TienThue, TrangThai) VALUES (@MaKH, @MaPhong, @NgayNhan, @NgayTraDuKien, @TienCoc, @TienThue, @TrangThai); SELECT CAST(SCOPE_IDENTITY() AS int)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", datPhong.MaKH);
                    cmd.Parameters.AddWithValue("@MaPhong", datPhong.MaPhong);
                    cmd.Parameters.AddWithValue("@NgayNhan", datPhong.NgayNhan);
                    cmd.Parameters.AddWithValue("@NgayTraDuKien", datPhong.NgayTraDuKien);
                    cmd.Parameters.AddWithValue("@TienCoc", datPhong.TienCoc);
                    cmd.Parameters.AddWithValue("@TienThue", datPhong.TienThue);
                    cmd.Parameters.AddWithValue("@TrangThai", datPhong.TrangThai);
                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm đặt phòng: " + ex.Message);
                return -1;
            }
        }
    }
}