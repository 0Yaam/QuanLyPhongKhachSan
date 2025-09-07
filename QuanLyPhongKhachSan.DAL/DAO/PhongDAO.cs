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

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaDat, MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai FROM DatPhong WHERE MaPhong = @MaPhong";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new DatPhong
                        {
                            MaDat = reader.GetInt32(0),
                            MaKH = reader.GetInt32(1),
                            MaPhong = reader.GetInt32(2),
                            NgayNhan = reader.GetDateTime(3),
                            NgayTraDuKien = reader.GetDateTime(4),
                            NgayTraThucTe = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            TienCoc = reader.GetDecimal(6),
                            TienThue = reader.GetDecimal(7),
                            TrangThai = reader.GetString(8)
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy đặt phòng: " + ex.Message);
                return null;
            }
        }

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