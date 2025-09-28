using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static QuanLyPhongKhachSan.DAL.Config;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class LoaiPhongDAO
    {
        private readonly string connectionString = Config.ConnectionString;

        public List<LoaiPhong> LayDanhSach()
        {
            var list = new List<LoaiPhong>();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT MaLoaiPhong, TenLoaiPhong, GiaPhong FROM LoaiPhong ORDER BY TenLoaiPhong", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new LoaiPhong
                        {
                            MaLoaiPhong = rd.GetInt32(0),
                            TenLoaiPhong = rd.GetString(1),
                            GiaPhong = rd.GetDecimal(2)
                        });
                    }
                }
            }
            return list;
        }

        public LoaiPhong LayTheoId(int maLoai)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                "SELECT MaLoaiPhong, TenLoaiPhong, GiaPhong FROM LoaiPhong WHERE MaLoaiPhong=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", maLoai);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return new LoaiPhong
                        {
                            MaLoaiPhong = rd.GetInt32(0),
                            TenLoaiPhong = rd.GetString(1),
                            GiaPhong = rd.GetDecimal(2)
                        };
                    }
                }
            }
            return null;
        }

        public int Them(LoaiPhong lp)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                @"INSERT INTO LoaiPhong(TenLoaiPhong, GiaPhong)
                  OUTPUT INSERTED.MaLoaiPhong
                  VALUES(@ten, @gia)", conn))
            {
                cmd.Parameters.AddWithValue("@ten", (object)lp.TenLoaiPhong ?? DBNull.Value);
                cmd.Parameters.Add("@gia", SqlDbType.Money).Value = lp.GiaPhong;

                conn.Open();
                var id = cmd.ExecuteScalar();
                return id != null ? Convert.ToInt32(id) : 0;
            }
        }

        public int CapNhat(LoaiPhong lp)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                @"UPDATE LoaiPhong
                  SET TenLoaiPhong=@ten, GiaPhong=@gia
                  WHERE MaLoaiPhong=@id", conn))
            {
                cmd.Parameters.AddWithValue("@ten", (object)lp.TenLoaiPhong ?? DBNull.Value);
                cmd.Parameters.Add("@gia", SqlDbType.Money).Value = lp.GiaPhong;
                cmd.Parameters.AddWithValue("@id", lp.MaLoaiPhong);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Trả về số phòng đang dùng loại này (để cảnh báo).
        /// </summary>
        public int DemSoPhongDangDung(int maLoai)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Phong WHERE MaLoaiPhong=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", maLoai);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Xoa(int maLoai)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("DELETE FROM LoaiPhong WHERE MaLoaiPhong=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", maLoai);
                conn.Open();
                try
                {
                    return cmd.ExecuteNonQuery(); // 1 nếu xoá OK, 0 nếu không có bản ghi
                }
                catch (SqlException ex) when (ex.Number == 547) // FK violation
                {
                    // Bắn lại để UI hiện thông báo thân thiện
                    throw new InvalidOperationException(
                        "Loại phòng đang được sử dụng bởi một hoặc nhiều phòng. Không thể xóa.", ex);
                }
            }
        }
    }
}
