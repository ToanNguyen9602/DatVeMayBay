using DatVeMayBayApi.Models;
using Lombok.NET;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace DatVeMayBayApi.Services
{
    [NoArgsConstructor]
    [AllArgsConstructor]
    public partial class DatVeMayBayServiceImpl : DatVeMayBayService
    {
        private readonly DatVeMayBayContext db;

        public async Task<dynamic> BookingCB(Ve ve)
        {
            try
            {   
                if (ve.Loaighe == 1)
                {
                    if( ve.MacbNavigation.Gheloai1 - ve.MacbNavigation.Ves.Count(cb => cb.Loaighe == 1) > 0)
                    {
                        ve.Giaghe = ve.MacbNavigation.Giagheloai1;
                        for (var i = 1; i <= ve.MacbNavigation.Gheloai1; i++) 
                        {
                            if (ve.MacbNavigation.Ves.Count(x => x.Soghe == i) == 0)
                            {
                                ve.Soghe = i;
                                break;
                            }
                        }
                        await db.Ves.AddAsync(ve);
                        if (await db.SaveChangesAsync() > 0)
                        {
                            dynamic ticket = await db.Ves.Where(tick => ve.Soghe == tick.Soghe && ve.Macb == tick.Macb).Select(x => new
                            {
                                Mave = x.Mave,
                                TenCB = x.MacbNavigation.Tencb,
                                Soghe = x.Soghe,
                                Loaighe = x.Loaighe,
                                Giaghe = x.Giaghe,
                                SanBayDi = x.MacbNavigation.MasbdiNavigation.Tensanbay,
                                SanBayDen = x.MacbNavigation.MasbdenNavigation.Tensanbay,
                            }).FirstAsync();
                            return ticket;
                        }
                    }
                }
                else 
                {
                    if (ve.MacbNavigation.Gheloai2 - ve.MacbNavigation.Ves.Count(cb => cb.Loaighe == 2) > 0)
                    {   
                        ve.Giaghe = ve.MacbNavigation.Giagheloai2;
                        for (var i = 1; i <= ve.MacbNavigation.Gheloai2; i++)
                        {
                            if (ve.MacbNavigation.Ves.Count(x => x.Soghe == i) == 0)
                            {
                                ve.Soghe = i;
                                break;
                            }
                        }
                        await db.Ves.AddAsync(ve);
                        if (await db.SaveChangesAsync() > 0)
                        {
                            dynamic ticket = await db.Ves.Where(tick => ve.Soghe == tick.Soghe && ve.Macb == tick.Macb).Select(x => new
                            {
                                Mave = x.Mave,
                                TenCB = x.MacbNavigation.Tencb,
                                Soghe = x.Soghe,
                                Loaighe = x.Loaighe,
                                Giaghe = x.Giaghe,
                                SanBayDi = x.MacbNavigation.MasbdiNavigation.Tensanbay,
                                SanBayDen = x.MacbNavigation.MasbdenNavigation.Tensanbay,
                            }) .FirstAsync();
                            return ticket;
                        }
                    }
                }
                return null;
            } catch
            {
                return null;
            }
        }

        public async Task<dynamic> CBDetails(int macb)
        {
           try
            {
                dynamic cdDetails = await db.Chuyenbays.Where(cb => cb.Macb == macb).Select(cb => new
                {
                    Macb = cb.Macb,
                    TenCb = cb.Tencb,
                    SanBayDi = cb.MasbdiNavigation.Tensanbay,
                    SanBayDen = cb.MasbdenNavigation.Tensanbay,
                    Ngaydi = cb.Ngaydi,
                    Gheloai1 = cb.Gheloai1,
                    Gheloai1Empty = cb.Gheloai1 - cb.Ves.Count(ve => ve.Loaighe == 1),
                    Giagheloai1 = cb.Giagheloai1,
                    Gheloai2 = cb.Gheloai2,
                    Gheloai2Empty = cb.Gheloai2 - cb.Ves.Count(ve => ve.Loaighe == 2),
                    Giagheloai2 = cb.Giagheloai2,
                    ThongTinChuyenBay = new
                    {
                        Mota = cb.Chitietchuyenbays.Where(ctb => ctb.Macb == cb.Macb).First().Mota,
                        Thoigiandung = cb.Chitietchuyenbays.Where(ctb => ctb.Macb == cb.Macb).First().Thoigiandung
                    }


                }).ToListAsync();
                if (Enumerable.Any(cdDetails))
                {
                    return cdDetails;
                } else { return null; }
            } catch
            {
                return null;
            }
        }
       
        public async Task<dynamic> FindCB(string date)
        {
           try
            {
                var findDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                dynamic cbs = await db.Chuyenbays.Where(cb =>
                cb.Ngaydi.Year == findDate.Year &&
                cb.Ngaydi.Month == findDate.Month &&
                cb.Ngaydi.Day == findDate.Day
                ).Select(cb => new
                {
                    Macb = cb.Macb,
                    TenCb = cb.Tencb,
                    SanBayDi = cb.MasbdiNavigation.Tensanbay,
                    SanBayDen = cb.MasbdenNavigation.Tensanbay,
                    Ngaydi = cb.Ngaydi,
                    Gheloai1 = cb.Gheloai1,
                    Gheloai1Empty = cb.Gheloai1 - cb.Ves.Count(ve => ve.Loaighe == 1),
                    Giagheloai1 = cb.Giagheloai1,
                    Gheloai2 = cb.Gheloai2,
                    Gheloai2Empty = cb.Gheloai2 - cb.Ves.Count(ve => ve.Loaighe == 2),
                    Giagheloai2 = cb.Giagheloai2,
                    ThongTinChuyenBay = new
                    {
                        Mota = cb.Chitietchuyenbays.Where(ctb => ctb.Macb == cb.Macb).First().Mota,
                        Thoigiandung = cb.Chitietchuyenbays.Where(ctb => ctb.Macb == cb.Macb).First().Thoigiandung
                    }


                }).ToListAsync();
                if (Enumerable.Any(cbs))
                {
                    return cbs;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> FindHanhKhach(string hoten)
        {
            try
            {   
                dynamic hk = await db.Hanhkhaches.Where(hk => hk.Hoten == hoten).Select(hk => new
                {
                    Hoten = hk.Hoten,
                    Mahk = hk.Mahk,
                    Cmnd = hk.Cmnd,
                    NgaySinh = hk.Ngaysinh
                }).FirstAsync();
                if (hk != null)
                {
                    return hk;
                }
                return null;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> SeatsInfo(int macb)
        {
            try
            {   
                dynamic seatsInfo = await db.Chuyenbays.Where(cbi => cbi.Macb == macb).Select(cb => new
                {
                    Loai1 = new
                    {
                        Seats = cb.Gheloai1,
                        SeatAlreadyHave = cb.Ves.Count(ve => ve.Loaighe == 1),
                        EmptySeats = cb.Gheloai1 - cb.Ves.Count(ve => ve.Loaighe == 1)
                    },
                    Loai2 = new
                    {
                        Seats = cb.Gheloai2,
                        SeatAlreadyHave = cb.Ves.Count(ve => ve.Loaighe == 2),
                        EmptySeats = cb.Gheloai1 - cb.Ves.Count(ve => ve.Loaighe == 2)
                    }
                }).FirstAsync();
                if (seatsInfo != null)
                {
                    return seatsInfo;
                }
                return null;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> TicketsInfoOfAHK(int mahk)
        {
            try
            {   
                var hk = await db.Hanhkhaches.FindAsync(mahk);
                if (hk == null)
                {
                    return null;
                }
                dynamic ticketsInfo = hk.Ves.Select(ve => new
                {
                    Mave = ve.Mave,
                    TenCB = ve.MacbNavigation.Tencb,
                    Soghe = ve.Soghe,
                    Loaighe = ve.Loaighe,
                    Giaghe = ve.Giaghe,
                    SanBayDi = ve.MacbNavigation.MasbdiNavigation.Tensanbay,
                    SanBayDen = ve.MacbNavigation.MasbdenNavigation.Tensanbay,
                }).ToList();
                return (Enumerable.Any(ticketsInfo) ? ticketsInfo : 0);
            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> TotalBookingOfAHK(int mahk)
        {
            try
            {
                var hk = await db.Hanhkhaches.FindAsync(mahk);
                return hk.Ves.Count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> TotalKindBookingOfAHK(int mahk, int loai)
        {
            try
            {
                var hk = await db.Hanhkhaches.FindAsync(mahk);
                return hk.Ves.Count(ve => ve.Loaighe == loai);
                
            } catch
            {
                return 0;
            }
        }

        public async Task<int> TotalSpentBookingOfAHK(int mahk)
        {
            var total = 0;
            try
            {
               
                var hk = await db.Hanhkhaches.FindAsync(mahk);
                if (hk != null)
                {
                    
                    foreach (var ve in hk.Ves)
                    {
                        total += ve.Giaghe;
                    }
                }
            } catch {
                return total;
            }
            return total;
        }

        public async Task<dynamic> UpdateCBDetails(ChuyenbayDto chuyenbay)
        {
            var cb = await db.Chuyenbays.FindAsync(chuyenbay.Macb);
            if ((chuyenbay.Gheloai1 < cb.Ves.Count(ve => ve.Loaighe == 1)) ||
                (chuyenbay.Gheloai2 < cb.Ves.Count(ve => ve.Loaighe == 2)))
            {
                return "NotEnoughSeat";
            }
            var fields = cb.GetType().GetProperties();
            foreach (var fieldInfo in chuyenbay.GetType().GetProperties())
            {
                var sameField = Array.Find(fields, f => f.Name == fieldInfo.Name);
                if ( sameField != null && fieldInfo.GetValue(chuyenbay) != null)
                {
                    if (sameField.PropertyType.Name.ToLower() == "int32")
                    {
                        if((int)fieldInfo.GetValue(chuyenbay) == 0)
                        {
                            continue;
                        }
                        if (sameField.Name == "Masbdi")
                        {
                            cb.Chitietchuyenbays.Where(x => x.Macb == cb.Macb).First().Masb = (int)chuyenbay.Masbdi;
                        }
                        if (sameField.Name == "Giagheloai1")
                        {
                            foreach (var v in cb.Ves.Where(ve => ve.Loaighe == 1).ToList())
                            {
                                v.Giaghe = (int)chuyenbay.Giagheloai1;
                            }
                        }

                        if (sameField.Name == "Giagheloai2")
                        {
                            foreach (var v in cb.Ves.Where(ve => ve.Loaighe == 2).ToList())
                            {
                                v.Giaghe = (int)chuyenbay.Giagheloai2;
                            }
                        }
                    }
                    if (sameField.PropertyType.Name.ToLower() == "string")
                    {
                        if (fieldInfo.GetValue(chuyenbay).ToString().Trim() == "")
                        {
                            continue;
                        }
                    }

                    sameField.SetValue(cb, fieldInfo.GetValue(chuyenbay));
                    
                }
            }

            try
            {
                db.Entry(cb).State =  Microsoft.EntityFrameworkCore.EntityState.Modified;
                if (await db.SaveChangesAsync() > 0)
                {   
              
                    dynamic cbDetails = await db.Chuyenbays.Where(x => x.Macb == cb.Macb).Select(b => new
                    {
                        Macb = b.Macb,
                        TenCb = b.Tencb,
                        SanBayDi = b.MasbdiNavigation.Tensanbay,
                        SanBayDen = b.MasbdenNavigation.Tensanbay,
                        Ngaydi = b.Ngaydi,
                        Gheloai1 = b.Gheloai1,
                        Gheloai1Empty = b.Gheloai1 - b.Ves.Count(ve => ve.Loaighe == 1),
                        Giagheloai1 = b.Giagheloai1,
                        Gheloai2 = b.Gheloai2,
                        Gheloai2Empty = b.Gheloai2 - b.Ves.Count(ve => ve.Loaighe == 2),
                        Giagheloai2 = b.Giagheloai2,
                        ThongTinChuyenBay = new
                        {
                            Mota = b.Chitietchuyenbays.Where(ctb => ctb.Macb == b.Macb).First().Mota,
                            Thoigiandung = b.Chitietchuyenbays.Where(ctb => ctb.Macb == b.Macb).First().Thoigiandung
                        }
                    }).FirstAsync();

                    return cbDetails;
                    
                }
                return null;
            } catch
            { 
                return null;
            }
        }
    }
}
