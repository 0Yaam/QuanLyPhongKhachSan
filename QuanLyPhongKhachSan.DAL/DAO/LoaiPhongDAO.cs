using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyPhongKhachSan.DAL.Config;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    // DAL/DAO/LoaiPhongDAO.cs
    public class LoaiPhongDAO
    {
        private readonly string _cs = Config.ConnectionString;

        public List<(int MaLoaiPhong, string TenLoaiPhong, decimal GiaPhong)> LayDanhSach()
        {
            var list = new List<(int, string, decimal)>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                const string sql = "SELECT MaLoaiPhong, TenLoaiPhong, GiaPhong FROM LoaiPhong";
                using (var cmd = new SqlCommand(sql, conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                        list.Add((r.GetInt32(0), r.GetString(1), r.GetDecimal(2)));
                }
            }
            return list;
        }
    }

}
