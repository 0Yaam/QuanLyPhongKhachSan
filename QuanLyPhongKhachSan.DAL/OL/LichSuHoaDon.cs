using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class LichSuHoaDon
    {
        public int Id { get; set; }
        public int MaHD { get; set; }
        public int? MaDat { get; set; }
        public string TenKH { get; set; }
        public int? SoPhong { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public System.DateTime ThoiGianIn { get; set; }
        public string LoaiHoaDon { get; set; }

        // NEW: SoPhong chỉ để hiển thị, lấy qua JOIN (không cần cột DB)
    }
}

