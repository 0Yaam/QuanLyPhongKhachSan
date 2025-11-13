using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class PhongDAO
    {
        private string connectionString = Config.ConnectionString;

        // DAL/DAO/PhongDAO.cs
        public List<Phong> LayDanhSach()
        {
            var ds = new List<Phong>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                const string sql = @"
SELECT p.MaPhong, p.SoPhong, p.MaLoaiPhong, lp.TenLoaiPhong, lp.GiaPhong, p.TrangThai
FROM   Phong p
JOIN   LoaiPhong lp ON lp.MaLoaiPhong = p.MaLoaiPhong";
                using (var cmd = new SqlCommand(sql, conn))
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        ds.Add(new Phong
                        {
                            MaPhong = r.GetInt32(0),
                            SoPhong = r.GetInt32(1),
                            MaLoaiPhong = r.GetInt32(2),
                            LoaiPhong = r.GetString(3),
                            Gia = r.GetDecimal(4),
                            TrangThai = r.GetString(5)
                        });
                    }
                }
            }
            return ds;
        }

        public int Them(Phong p)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                const string sql = @"
INSERT INTO Phong (SoPhong, MaLoaiPhong, TrangThai)
VALUES (@SoPhong, @MaLoaiPhong, @TrangThai);
SELECT CAST(SCOPE_IDENTITY() AS int)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", p.SoPhong);
                    cmd.Parameters.AddWithValue("@MaLoaiPhong", p.MaLoaiPhong);
                    cmd.Parameters.AddWithValue("@TrangThai", p.TrangThai ?? "Trống");
                    return (int)cmd.ExecuteScalar();
                }
            }
        }


        public void Xoa(int maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Xóa các bản ghi trong ChiTietHoaDon liên quan đến HoaDon của DatPhong
                            string sqlChiTietHoaDon = @"
                    DELETE FROM ChiTietHoaDon 
                    WHERE MaHD IN (
                        SELECT MaHD 
                        FROM HoaDon 
                        WHERE MaDat IN (
                            SELECT MaDat 
                            FROM DatPhong 
                            WHERE MaPhong = @MaPhong
                        )
                    )";
                            using (SqlCommand cmd = new SqlCommand(sqlChiTietHoaDon, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // 2. Xóa các bản ghi trong HoaDon liên quan đến DatPhong
                            string sqlHoaDon = @"
                    DELETE FROM HoaDon 
                    WHERE MaDat IN (
                        SELECT MaDat 
                        FROM DatPhong 
                        WHERE MaPhong = @MaPhong
                    )";
                            using (SqlCommand cmd = new SqlCommand(sqlHoaDon, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // 3. Xóa các bản ghi trong LichSuHoaDon liên quan đến HoaDon
                            string sqlLichSuHoaDon = @"
                    DELETE FROM LichSuHoaDon 
                    WHERE MaHD IN (
                        SELECT MaHD 
                        FROM HoaDon 
                        WHERE MaDat IN (
                            SELECT MaDat 
                            FROM DatPhong 
                            WHERE MaPhong = @MaPhong
                        )
                    )";
                            using (SqlCommand cmd = new SqlCommand(sqlLichSuHoaDon, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // 4. Xóa các bản ghi trong DatPhong liên quan
                            string sqlDatPhong = "DELETE FROM DatPhong WHERE MaPhong = @MaPhong";
                            using (SqlCommand cmd = new SqlCommand(sqlDatPhong, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // 5. Xóa phòng
                            string sqlPhong = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                            using (SqlCommand cmd = new SqlCommand(sqlPhong, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // Commit transaction nếu tất cả thành công
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction nếu có lỗi
                            transaction.Rollback();
                            Console.WriteLine($"Lỗi khi xóa phòng (MaPhong={maPhong}): {ex.Message}");
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa phòng (MaPhong={maPhong}): {ex.Message}");
                throw;
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

        public List<string> LayDanhSachLoaiPhong()
        {
            var loaiPhongList = new List<string>();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string sql = "SELECT DISTINCT LoaiPhong FROM Phong";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loaiPhongList.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LayDanhSachLoaiPhong: {ex.Message}");
            }
            return loaiPhongList;
        }

        public int CapNhat(Phong phong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // ĐÃ CHUẨN HOÁ: bảng Phong giờ chỉ giữ SoPhong, MaLoaiPhong, TrangThai
                    const string sql = @"
UPDATE Phong
SET SoPhong = @SoPhong,
    MaLoaiPhong = @MaLoaiPhong,
    TrangThai = @TrangThai
WHERE MaPhong = @MaPhong";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                        cmd.Parameters.AddWithValue("@SoPhong", phong.SoPhong);
                        cmd.Parameters.AddWithValue("@MaLoaiPhong", phong.MaLoaiPhong);
                        cmd.Parameters.AddWithValue("@TrangThai", (object)phong.TrangThai ?? "Trống");
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật phòng: " + ex.Message);
                return 0;
            }
        }

        public int CapNhatTrangThai(int maPhong, string trangThai)
        {
            try
            {
                using (var conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    const string sql = "UPDATE Phong SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;
                        cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatTrangThai: {ex.Message}");
                return 0;
            }
        }
        public decimal LayGiaTheoLoai(string loaiPhong)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    const string sql = @"
                SELECT TOP 1 Gia 
                FROM Phong 
                WHERE LoaiPhong = @LoaiPhong
                ORDER BY MaPhong DESC";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                        var obj = cmd.ExecuteScalar();
                        return (obj == null || obj == DBNull.Value) ? 0m : Convert.ToDecimal(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LayGiaTheoLoai error: {ex.Message}");
                return 0m;
            }
        }

    }
}