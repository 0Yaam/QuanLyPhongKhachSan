using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyPhongKhachSan.DAL.DAO
{
    public class DatPhongDAO
    {
        private readonly string _connStr = Config.ConnectionString;

        public List<DatPhong> LayDanhSach()
        {
            var danhSach = new List<DatPhong>();
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"SELECT MaDat, MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai 
                                         FROM DatPhong";
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dp = new DatPhong
                            {
                                MaDat = reader.GetInt32(0),
                                MaKH = reader.GetInt32(1),
                                MaPhong = reader.GetInt32(2),
                                NgayNhan = reader.GetDateTime(3),
                                NgayTraDuKien = reader.GetDateTime(4),
                                NgayTraThucTe = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                TienCoc = reader.GetDecimal(6),
                                TienThue = reader.GetDecimal(7),
                                TrangThai = reader.IsDBNull(8) ? null : reader.GetString(8)
                            };
                            danhSach.Add(dp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi LayDanhSach DatPhong: {ex.Message}");
            }
            return danhSach;
        }

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    string sql = @"
                SELECT TOP 1 MaDat, MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai
                FROM DatPhong
                WHERE MaPhong = @MaPhong
                  AND NgayTraThucTe IS NULL
                ORDER BY NgayNhan DESC";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read()) return null;

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
                                TrangThai = reader.IsDBNull(8) ? null : reader.GetString(8)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LayDatPhongTheoMaPhong(MaPhong={maPhong}): Error: {ex.Message}");
                throw;
            }
        }

        public bool KiemTraPhongTrungLichExcept(int maPhong, DateTime nhan, DateTime tra, int excludeMaDat)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
SELECT CASE WHEN EXISTS (
    SELECT 1
    FROM DatPhong
    WHERE MaPhong = @MaPhong
      AND MaDat <> @ExcludeMaDat
      AND NgayTraThucTe IS NULL
      AND NOT (@Tra <= NgayNhan OR @Nhan >= NgayTraDuKien)
) THEN 1 ELSE 0 END;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                        cmd.Parameters.Add("@ExcludeMaDat", SqlDbType.Int).Value = excludeMaDat;
                        cmd.Parameters.Add("@Nhan", SqlDbType.DateTime2).Value = nhan;
                        cmd.Parameters.Add("@Tra", SqlDbType.DateTime2).Value = tra;

                        var result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"KiemTraPhongTrungLichExcept(MaPhong={maPhong}, ExcludeMaDat={excludeMaDat}): Error: {ex.Message}");
                return true;
            }
        }

        public int Update(DatPhong dat)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = @"UPDATE DatPhong 
                              SET MaKH = @MaKH, MaPhong = @MaPhong, NgayNhan = @NgayNhan, 
                                  NgayTraDuKien = @NgayTraDuKien, NgayTraThucTe = @NgayTraThucTe, 
                                  TienCoc = @TienCoc, TienThue = @TienThue, TrangThai = @TrangThai 
                              WHERE MaDat = @MaDat";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDat", dat.MaDat);
                        cmd.Parameters.AddWithValue("@MaKH", dat.MaKH);
                        cmd.Parameters.AddWithValue("@MaPhong", dat.MaPhong);
                        cmd.Parameters.AddWithValue("@NgayNhan", dat.NgayNhan);
                        cmd.Parameters.AddWithValue("@NgayTraDuKien", dat.NgayTraDuKien);
                        cmd.Parameters.AddWithValue("@NgayTraThucTe", (object)dat.NgayTraThucTe ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TienCoc", dat.TienCoc);
                        cmd.Parameters.AddWithValue("@TienThue", dat.TienThue);
                        cmd.Parameters.AddWithValue("@TrangThai", dat.TrangThai);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine($"DatPhongDAO.Update: MaDat={dat.MaDat}, RowsAffected={rowsAffected}");
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi DatPhongDAO.Update: MaDat={dat.MaDat}, Exception={ex.Message}");
                return 0;
            }
        }

        public int Them(DatPhong datPhong)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO DatPhong (MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai)
                              VALUES (@MaKH, @MaPhong, @NgayNhan, @NgayTraDuKien, @NgayTraThucTe, @TienCoc, @TienThue, @TrangThai);
                              SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", datPhong.MaKH);
                        cmd.Parameters.AddWithValue("@MaPhong", datPhong.MaPhong);
                        cmd.Parameters.AddWithValue("@NgayNhan", datPhong.NgayNhan);
                        cmd.Parameters.AddWithValue("@NgayTraDuKien", datPhong.NgayTraDuKien);
                        cmd.Parameters.AddWithValue("@NgayTraThucTe", (object)datPhong.NgayTraThucTe ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TienCoc", datPhong.TienCoc);
                        cmd.Parameters.AddWithValue("@TienThue", datPhong.TienThue);
                        cmd.Parameters.AddWithValue("@TrangThai", datPhong.TrangThai);
                        object result = cmd.ExecuteScalar();
                        int maDat = result != null ? Convert.ToInt32(result) : -1;
                        System.Diagnostics.Debug.WriteLine($"DatPhongDAO.Them: MaDat={maDat}, MaKH={datPhong.MaKH}, MaPhong={datPhong.MaPhong}");
                        return maDat;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi DatPhongDAO.Them: MaKH={datPhong.MaKH}, MaPhong={datPhong.MaPhong}, Exception={ex.Message}");
                return -1;
            }
        }

        public bool KiemTraPhongTrungLich(int maPhong, DateTime nhan, DateTime tra)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
SELECT CASE WHEN EXISTS (
    SELECT 1
    FROM DatPhong
    WHERE MaPhong = @MaPhong
      AND NgayTraThucTe IS NULL
      AND NOT (@Tra <= NgayNhan OR @Nhan >= NgayTraDuKien)
) THEN 1 ELSE 0 END;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                        cmd.Parameters.Add("@Nhan", SqlDbType.DateTime2).Value = nhan;
                        cmd.Parameters.Add("@Tra", SqlDbType.DateTime2).Value = tra;

                        var result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"KiemTraPhongTrungLich(MaPhong={maPhong}): Error: {ex.Message}");
                return true;
            }
        }

        public int CapNhatNgayTraThucTe(int maDat, DateTime ngayTra)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
UPDATE DatPhong
SET NgayTraThucTe = @NgayTra, TrangThai = 'Đã trả'
WHERE MaDat = @MaDat;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = maDat;
                        cmd.Parameters.Add("@NgayTra", SqlDbType.DateTime2).Value = ngayTra;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatNgayTraThucTe: {ex.Message}");
                return 0;
            }
        }
    }
}