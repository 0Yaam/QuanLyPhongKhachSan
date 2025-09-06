using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class DatPhongDAO
    {
        string connection = Config.ConnectionString;
        public List<DatPhong> LayDanhSach()
        {
            List<DatPhong> danhSach = new List<DatPhong>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    string sql = "SELECT MaDat, MaKH, MaPhong, NgayNhan, NgayTraDuKien,NgayTraThucTe,TienCoc,TienThue,TrangThai FROM DatPhong"; 
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DatPhong dp = new DatPhong
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
                        danhSach.Add(dp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return danhSach;
        }
    }
}
