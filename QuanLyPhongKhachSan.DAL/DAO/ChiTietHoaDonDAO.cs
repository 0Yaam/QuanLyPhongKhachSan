using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class ChiTietHoaDonDAO
    {
        private string connectionString = Config.ConnectionString;

        public List<ChiTietHoaDon> LayDanhSach()
        {
            List<ChiTietHoaDon> danhSach = new List<ChiTietHoaDon>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT MaCTHD, MaHD, TenDichVu, SoLuong, Gia FROM ChiTietHoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ChiTietHoaDon cthd = new ChiTietHoaDon
                        {
                            MaCTHD = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                            MaHD = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                            TenDichVu = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                            SoLuong = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                            Gia = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0
                        };
                        danhSach.Add(cthd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return danhSach;
        }

        public void Them(ChiTietHoaDon cthd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO ChiTietHoaDon (MaHD, TenDichVu, SoLuong, Gia) VALUES (@MaHD, @TenDichVu, @SoLuong, @Gia)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHD", cthd.MaHD);
                    cmd.Parameters.AddWithValue("@TenDichVu", cthd.TenDichVu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                    cmd.Parameters.AddWithValue("@Gia", cthd.Gia);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm: " + ex.Message);
            }
        }
    }
}