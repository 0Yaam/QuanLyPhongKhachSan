using QuanLyPhongKhachSan.DAL.OL;

namespace QuanLyPhongKhachSan.DAL.OL
{
    public class RoomBookingInfo
    {
        public Phong Room { get; set; }
        public DatPhong Booking { get; set; }
        public KhachHang Customer { get; set; }
    }
}
