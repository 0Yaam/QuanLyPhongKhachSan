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

    }
}
