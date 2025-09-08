using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

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
                Console.WriteLine("Lỗi LayDanhSach DatPhong: " + ex.Message);
            }
            return danhSach;
        }

        public DatPhong LayDatPhongTheoMaPhong(int maPhong)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
SELECT TOP 1 MaDat, MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai
FROM DatPhong
WHERE MaPhong = @MaPhong
ORDER BY NgayNhan DESC;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = maPhong;
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                return new DatPhong
                                {
                                    MaDat = rd.GetInt32(0),
                                    MaKH = rd.GetInt32(1),
                                    MaPhong = rd.GetInt32(2),
                                    NgayNhan = rd.GetDateTime(3),
                                    NgayTraDuKien = rd.GetDateTime(4),
                                    NgayTraThucTe = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                                    TienCoc = rd.GetDecimal(6),
                                    TienThue = rd.GetDecimal(7),
                                    TrangThai = rd.IsDBNull(8) ? null : rd.GetString(8)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LayDatPhongTheoMaPhong: " + ex.Message);
            }
            return null;
        }

        // Check trùng lịch (ngoại trừ chính booking đang sửa)
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
                Console.WriteLine("Lỗi KiemTraPhongTrungLichExcept: " + ex.Message);
                return true; // an toàn: coi như bị trùng để tránh ghi sai
            }
        }

        // Cập nhật đặt phòng
        public int Update(DatPhong dat)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
UPDATE DatPhong
SET MaKH = @MaKH,
    NgayNhan = @NgayNhan,
    NgayTraDuKien = @NgayTraDuKien,
    TienCoc = @TienCoc,
    TienThue = @TienThue,
    TrangThai = @TrangThai
WHERE MaDat = @MaDat;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaDat", SqlDbType.Int).Value = dat.MaDat;
                        cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = dat.MaKH;
                        cmd.Parameters.Add("@NgayNhan", SqlDbType.DateTime2).Value = dat.NgayNhan;
                        cmd.Parameters.Add("@NgayTraDuKien", SqlDbType.DateTime2).Value = dat.NgayTraDuKien;

                        var pCoc = cmd.Parameters.Add("@TienCoc", SqlDbType.Decimal);
                        pCoc.Precision = 18; pCoc.Scale = 2; pCoc.Value = dat.TienCoc;

                        var pThue = cmd.Parameters.Add("@TienThue", SqlDbType.Decimal);
                        pThue.Precision = 18; pThue.Scale = 2; pThue.Value = dat.TienThue;

                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object)(dat.TrangThai ?? "Đã đặt");

                        return cmd.ExecuteNonQuery(); // >0 là ok
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi Update DatPhong: " + ex.Message);
                return 0;
            }
        }

        public int Them(DatPhong dat)
        {
            try
            {
                using (var conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    const string sql = @"
INSERT INTO DatPhong (MaKH, MaPhong, NgayNhan, NgayTraDuKien, NgayTraThucTe, TienCoc, TienThue, TrangThai)
OUTPUT INSERTED.MaDat
VALUES (@MaKH, @MaPhong, @NgayNhan, @NgayTraDuKien, @NgayTraThucTe, @TienCoc, @TienThue, @TrangThai);";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = dat.MaKH;
                        cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = dat.MaPhong;
                        cmd.Parameters.Add("@NgayNhan", SqlDbType.DateTime2).Value = dat.NgayNhan;
                        cmd.Parameters.Add("@NgayTraDuKien", SqlDbType.DateTime2).Value = dat.NgayTraDuKien;
                        cmd.Parameters.Add("@NgayTraThucTe", SqlDbType.DateTime2).Value = (object)dat.NgayTraThucTe ?? DBNull.Value;

                        var pCoc = cmd.Parameters.Add("@TienCoc", SqlDbType.Decimal);
                        pCoc.Precision = 18; pCoc.Scale = 2; pCoc.Value = dat.TienCoc;

                        var pThue = cmd.Parameters.Add("@TienThue", SqlDbType.Decimal);
                        pThue.Precision = 18; pThue.Scale = 2; pThue.Value = dat.TienThue;

                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object)(dat.TrangThai ?? "Đã đặt");

                        var id = cmd.ExecuteScalar();
                        return id == null ? 0 : Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi Them DatPhong: " + ex.Message);
                return 0;
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
                Console.WriteLine("Lỗi KiemTraPhongTrungLich: " + ex.Message);
                return true;
            }
        }
    }
}
