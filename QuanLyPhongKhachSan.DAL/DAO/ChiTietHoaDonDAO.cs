using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.DAO
{
        public class ChiTietHoaDon
        {
            public int MaCTHD { get; set; }
            public int MaHD { get; set; }
            public string TenDichVu { get; set; }
            public int SoLuong { get; set; }
            public decimal Gia { get; set; }
            public decimal ThanhTien => SoLuong * Gia;

            public ChiTietHoaDon() { }

            public ChiTietHoaDon(int maCTHD, int maHD, string tenDichVu, int soLuong, decimal gia)
            {
                MaCTHD = maCTHD;
                MaHD = maHD;
                TenDichVu = tenDichVu;
                SoLuong = soLuong;
                Gia = gia;
            }
        }
}

