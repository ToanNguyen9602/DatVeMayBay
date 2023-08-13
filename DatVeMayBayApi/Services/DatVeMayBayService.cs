using DatVeMayBayApi.Models;

namespace DatVeMayBayApi.Services
{
    public interface DatVeMayBayService
    {
        public Task<dynamic> FindCB(string date);
        public Task<dynamic> CBDetails(int macb);
        public Task<dynamic> SeatsInfo(int macb);
        public Task<dynamic> FindHanhKhach(string hoten);
        public Task<dynamic> BookingCB(Ve ve);
        public Task<dynamic> UpdateCBDetails(ChuyenbayDto chuyenbay);
        public Task<int> TotalBookingOfAHK(int mahk);
        public Task<int> TotalKindBookingOfAHK(int mahk, int loai);
        public Task<int> TotalSpentBookingOfAHK(int mahk);

        public Task<dynamic> TicketsInfoOfAHK(int mahk);
    }
}
