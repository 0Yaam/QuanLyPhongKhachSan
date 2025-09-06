using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class KhachHangDAO
    {
       string connection = Config.ConnectionString;

        public List<KhachHang> LayDanhSach()
        {
            List<KhachHang> danhSach = new List<KhachHang>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string sql = "SELECT MaKH, HoTen, CCCD, SDT FROM KhachHang"; 
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        KhachHang kh = new KhachHang
                        {
                            MaKH = reader.GetInt32(0), 
                            HoTen = reader.GetString(1), 
                            CCCD = reader.GetString(2), 
                            SDT = reader.GetString(3) 
                        };
                        danhSach.Add(kh);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return danhSach;
        }

        public void Them(KhachHang kh)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string sql = "INSERT INTO KhachHang (HoTen, CCCD, SDT) VALUES (@HoTen, @CCCD, @SDT)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                    cmd.Parameters.AddWithValue("@CCCD", kh.CCCD);
                    cmd.Parameters.AddWithValue("@SDT", kh.SDT);
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