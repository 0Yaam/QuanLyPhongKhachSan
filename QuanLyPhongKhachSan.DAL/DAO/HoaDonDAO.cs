using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class HoaDonDAO
    {
        private string connectionString = Config.ConnectionString;

        public List<HoaDon> LayDanhSach()
        {
            List<HoaDon> danhSach = new List<HoaDon>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

        //                    public int MaHD { get; set; }
        //public int MaDat { get; set; }
        //public DateTime NgayLap { get; set; }
        //public string LoaiHoaDon { get; set; }
        //public decimal? TongThanhToan { get; set; }
        //public string GhiChu { get; set; }
                     conn.Open();
                    string sql = "SELECT MaHD, MaDat, NgayLap, LoaiHoaDon, TongThanhToan, GhiChu FROM HoaDon";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        HoaDon hd = new HoaDon
                        {
                            MaHD = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                            MaDat = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                            NgayLap = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now,
                            LoaiHoaDon = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                            TongThanhToan = !reader.IsDBNull(6) ? reader.GetDecimal(6) : (decimal?)null,
                            GhiChu = !reader.IsDBNull(9) ? reader.GetString(9) : ""
                        };
                        danhSach.Add(hd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return danhSach;
        }

        public void Them(HoaDon hd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO HoaDon (MaDat, NgayLap, LoaiHoaDon, TienThue, TienCoc, TongThanhToan, TienHoanTra, TienBoiThuong, GhiChu) " +
                                 "VALUES (@MaDat, @NgayLap, @LoaiHoaDon, @TienThue, @TienCoc, @TongThanhToan, @TienHoanTra, @TienBoiThuong, @GhiChu)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaDat", hd.MaDat);
                    cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                    cmd.Parameters.AddWithValue("@LoaiHoaDon", hd.LoaiHoaDon ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TongThanhToan", hd.TongThanhToan.HasValue ? (object)hd.TongThanhToan.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", hd.GhiChu ?? (object)DBNull.Value);
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