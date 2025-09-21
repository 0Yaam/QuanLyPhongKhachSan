using QuanLyPhongKhachSan.DAL;
using QuanLyPhongKhachSan.DAL.DAO;
using QuanLyPhongKhachSan.DAL.OL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static QuanLyPhongKhachSan.DAL.Config;

namespace QuanLyPhongKhachSan.BLL.Services
{
    public class NhanSuService
    {
        private readonly NhanVienDAO _nvDAO = new NhanVienDAO();
        private readonly TaiKhoanDAO _tkDAO = new TaiKhoanDAO();
        private readonly string _cs = Config.ConnectionString;

        public int ThemNhanVienVaTaiKhoan(NhanVien nv, TaiKhoan tk, bool ganTaiKhoanVaoNhanVien = true)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int maNV = _nvDAO.Them(nv, conn, tran);
                        if (maNV <= 0) throw new Exception("Không lấy được MaNV.");

                        if (ganTaiKhoanVaoNhanVien)
                        {
                            if (_tkDAO.DemTheoTenDangNhap(tk.TenDangNhap, conn, tran) > 0)
                                throw new Exception("Tên đăng nhập đã tồn tại.");

                            tk.MaNV = maNV;
                            _tkDAO.Them(tk, conn, tran);
                        }

                        tran.Commit();
                        return maNV;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<NhanVienView> LayDanhSachNhanVien()
        {
            return _nvDAO.LayDanhSachNhanVien();
        }

        public bool CapNhatNhanVienVaTaiKhoan(NhanVienView v)
        {
            if (v == null || !v.MaTK.HasValue || v.MaTK.Value <= 0) return false;

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Chỉ cập nhật NhanVien nếu MaNV > 0
                        if (v.MaNV > 0)
                        {
                            var nv = new NhanVien
                            {
                                MaNV = v.MaNV,
                                TenNV = v.Ten,
                                SDT = v.SDT,
                                CCCD = v.CCCD,
                                ChucVu = v.Quyen == 1 ? "Admin" : (v.ChucVu ?? "Nhân viên"),
                                NgayThamGia = (v.NgayThamGia == DateTime.MinValue ? DateTime.Today : v.NgayThamGia)
                            };

                            var cmdNV = new SqlCommand(@"
UPDATE dbo.NhanVien
SET TenNV=@Ten, SDT=@SDT, CCCD=@CCCD, ChucVu=@ChucVu, NgayThamGia=@Ngay
WHERE MaNV=@MaNV;", conn, tran);
                            cmdNV.Parameters.AddWithValue("@Ten", (object)nv.TenNV ?? DBNull.Value);
                            cmdNV.Parameters.AddWithValue("@SDT", (object)nv.SDT ?? DBNull.Value);
                            cmdNV.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                            cmdNV.Parameters.AddWithValue("@ChucVu", (object)nv.ChucVu ?? DBNull.Value);
                            cmdNV.Parameters.AddWithValue("@Ngay", nv.NgayThamGia);
                            cmdNV.Parameters.AddWithValue("@MaNV", nv.MaNV);
                            cmdNV.ExecuteNonQuery();
                        }

                        // Cập nhật TaiKhoan, bao gồm Quyen
                        var cmdTK = new SqlCommand(@"
UPDATE dbo.TaiKhoan
SET TenDangNhap = @U, MatKhau = @P, Quyen = @Quyen
WHERE MaTK = @MaTK;", conn, tran);
                        cmdTK.Parameters.AddWithValue("@U", (object)(v.TenTaiKhoan ?? string.Empty));
                        cmdTK.Parameters.AddWithValue("@P", (object)(v.MatKhau ?? string.Empty));
                        cmdTK.Parameters.AddWithValue("@Quyen", v.Quyen ?? 2); // Mặc định là Nhân viên nếu null
                        cmdTK.Parameters.AddWithValue("@MaTK", v.MaTK.Value);
                        cmdTK.ExecuteNonQuery();

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public int XoaNhanVien(int maNV)
        {
            return _nvDAO.Xoa(maNV);
        }

        public int XoaNhanVienNhieu(IEnumerable<int> maNVs)
        {
            int total = 0;
            foreach (var id in maNVs) total += _nvDAO.Xoa(id);
            return total;
        }

        public int ResetMatKhauNhieu(IEnumerable<int> maTKs, string newPass = "123")
        {
            int total = 0;
            foreach (var id in maTKs) total += _tkDAO.ResetMatKhau(id, newPass);
            return total;
        }
    }
}