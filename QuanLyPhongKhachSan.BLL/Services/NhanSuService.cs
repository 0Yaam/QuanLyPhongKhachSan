// BLL/Services/NhanSuService.cs
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

        /// <summary>
        /// Thêm NHÂN VIÊN rồi thêm TÀI KHOẢN trong 1 transaction.
        /// Trả về MaNV vừa tạo.
        /// </summary>
        public int ThemNhanVienVaTaiKhoan(NhanVien nv, TaiKhoan tk, bool ganTaiKhoanVaoNhanVien = true)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) thêm nhân viên
                        int maNV = _nvDAO.Them(nv, conn, tran);
                        if (maNV <= 0) throw new Exception("Không lấy được MaNV.");

                        // 2) nếu cần gắn tài khoản cho NV
                        if (ganTaiKhoanVaoNhanVien)
                        {
                            // check trùng username
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
            if (v == null || v.MaNV <= 0) return false;

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Update NhanVien
                        var nv = new NhanVien
                        {
                            MaNV = v.MaNV,
                            TenNV = v.Ten,
                            SDT = v.SDT,
                            CCCD = v.CCCD,
                            NgayThamGia = (v.NgayThamGia == DateTime.MinValue ? DateTime.Today : v.NgayThamGia)
                        };

                        // dùng cùng connection/transaction cho chắc (overload theo conn/tran nếu bạn có)
                        var cmdNV = new SqlCommand(@"
UPDATE dbo.NhanVien
SET TenNV=@Ten, SDT=@SDT, CCCD=@CCCD, NgayThamGia=@Ngay
WHERE MaNV=@MaNV;", conn, tran);
                        cmdNV.Parameters.AddWithValue("@Ten", (object)nv.TenNV ?? DBNull.Value);
                        cmdNV.Parameters.AddWithValue("@SDT", (object)nv.SDT ?? DBNull.Value);
                        cmdNV.Parameters.AddWithValue("@CCCD", (object)nv.CCCD ?? DBNull.Value);
                        cmdNV.Parameters.AddWithValue("@Ngay", nv.NgayThamGia);
                        cmdNV.Parameters.AddWithValue("@MaNV", nv.MaNV);
                        cmdNV.ExecuteNonQuery();

                        // Update TaiKhoan nếu có
                        if (v.MaTK.HasValue && v.MaTK.Value > 0)
                        {
                            var cmdTK = new SqlCommand(@"
UPDATE dbo.TaiKhoan
SET TenDangNhap = @U, MatKhau = @P
WHERE MaTK = @MaTK;", conn, tran);
                            cmdTK.Parameters.AddWithValue("@U", (object)(v.TenTaiKhoan ?? string.Empty));
                            cmdTK.Parameters.AddWithValue("@P", (object)(v.MatKhau ?? string.Empty));
                            cmdTK.Parameters.AddWithValue("@MaTK", v.MaTK.Value);
                            cmdTK.ExecuteNonQuery();
                        }

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


    }
}
