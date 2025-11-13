// AppEvents.cs
using System;

namespace QuanLyPhongKhachSan
{
    public static class AppEvents
    {
        public static event Action InvoiceLogged;
        public static event Action CustomerChanged;

        public static void RaiseInvoiceLogged()
        {
            try { InvoiceLogged?.Invoke(); } catch { /* ignore */ }
        }

        public static void RaiseCustomerChanged()
        {
            try { CustomerChanged?.Invoke(); } catch { /* ignore */ }
        }
    }
}
