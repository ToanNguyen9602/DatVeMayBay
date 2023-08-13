using DatVeMayBayApi.Models;
using DatVeMayBayApi.Services;
using Lombok.NET;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatVeMayBayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllArgsConstructor]
    public partial class DatVeController : ControllerBase
    {
        private readonly DatVeMayBayContext db;
        private readonly DatVeMayBayService dvService;

        /* public DatVeController(DatVeMayBayService _dvService)
         {
             this.dvService = _dvService;
         }*/



        [HttpGet("listCB/{date}")]
        public async Task<IActionResult> ListChuyenBay(string date)
        {
            dynamic listCB = await dvService.FindCB(date);
            if (listCB == null) 
            {
                return NotFound("Khong co chuyen Bay nao");
             } else
            {
                return Ok( listCB);
            }
        }

        [HttpGet("cbDetails/{macb}")]
        public async Task<IActionResult> ChiTietChuyenBay(int macb)
        {
            dynamic cb = await dvService.CBDetails(macb);
            if (cb == null)
            {
                return NotFound("Khong Tim Thay Chuyen Bay");
            } else
            {
                return Ok( cb);
            }
        }

        [HttpGet("seatsInfo/{macb}")]
        public async Task<IActionResult> SeatsInfo(int macb)
        {
            dynamic seats = await dvService.SeatsInfo(macb);
            if (seats == null)
            {
                return NotFound("Khong Tim Thay Chuyen Bay");
            } else 
            { 
                return Ok( seats); 
            }
        }

        [HttpGet("findCustomer/{hoten}")]
        public async Task<IActionResult> FindCustomer(string hoten)
        {
            dynamic hkInfo = await dvService.FindHanhKhach(hoten);
            if (hkInfo == null)
            {
                return NotFound("Khong Tim Thay Hanh Khach");

            } else 
            { 
                return Ok( hkInfo); 
            }
        }

        [HttpGet("ticketsInfo/{mahk}")]
        public async Task<IActionResult> TicketsInfo(int mahk)
        {
            dynamic ticketsInfo = await dvService.TicketsInfoOfAHK(mahk);
            if (ticketsInfo == null)
            {
                return NotFound("Khong Tim Thay Hanh Khach");
            } else
            {
                if (!(Enumerable.Any(ticketsInfo)))
                {
                    return Ok("Hanh Khach Chua Dat Ve Nao!");
                } else
                {
                    return Ok(ticketsInfo);
                }
            }
        }

        [HttpPost("orderTicket/{macb}")]      
        public async Task<IActionResult> OrderTicket(int macb, int loaighe, [FromBody] HanhkhachDto hanhkhach)
        {

            var ticket = new Ve();
            var hk = new Hanhkhach() 
            {
                Hoten = hanhkhach.Hoten,
                Cmnd = hanhkhach.Cmnd,
                Ngaysinh = hanhkhach.Ngaysinh
            };
            ticket.MacbNavigation = await db.Chuyenbays.FindAsync(macb);

            if (ticket.MacbNavigation == null)
            {
                return NotFound("Khong Tim Thay Chuyen Bay!");
            }

            ticket.Macb = macb;
            ticket.Loaighe = loaighe;
            if (db.Hanhkhaches.Count(x => x.Cmnd == hk.Cmnd) > 0)
            {
                var hkcu = await db.Hanhkhaches.Where(x => x.Cmnd == hk.Cmnd).FirstAsync();

                hk.Mahk = hkcu.Mahk;

                if (hk.Hoten == null || hk.Hoten.Trim() == "")
                {
                    hk.Hoten = hkcu.Hoten;
                }
                if (hk.Ngaysinh == null)
                {
                    hk.Ngaysinh = hkcu.Ngaysinh;
                }
                ticket.Mahk = hk.Mahk;
                hk.Ves = hkcu.Ves;
                
            } else
            {
                if (hk.Ngaysinh == null)
                {
                    return BadRequest("Ngay Sinh Khong Dung Dinh Dang dd-MM-yyyy");
                }
            }

            ticket.MahkNavigation = hk;
            dynamic ve = await dvService.BookingCB(ticket);
            if ( ve != null)
            {   
                
                return Ok(JsonConvert.SerializeObject(ve));
            } else
            {
                return BadRequest("Khong The Dat Ve Cho Chuyen Bay Nay!");
            }
            
        }

        [HttpPut("updateChuyenBay")]
        public async Task<IActionResult> UpdateChuyenBay([FromBody] ChuyenbayDto chuyenbayDto)
        {
            if (chuyenbayDto.Macb == 0 || chuyenbayDto == null)
            {
                return BadRequest("Khong Tim Thay Chuyen Bay");
            }

            dynamic updateResult = await dvService.UpdateCBDetails(chuyenbayDto);
            if (updateResult is string)
            {
                return BadRequest("So Ghe Khong Du");
            }

            if (updateResult != null) 
            {
                return Ok(JsonConvert.SerializeObject(updateResult));
            } else
            {
                return BadRequest("Khong The Cap Nhat Cho Chuyen Bay");
            }
        }

        [HttpGet("countTotalTicket/{mahk}")]
        public async Task<IActionResult> CountTotalTicket(int mahk)
        {
            if (mahk == 0 || mahk == null)
            {
                return BadRequest("Khong Tim Thay Hanh Khach");
            }
            else
            {
                var result = (await dvService.TotalBookingOfAHK(mahk));


                if (result != 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Khong Tim Thay Hanh Khach");
                }
            }
        }

        [HttpGet("countTicketFllKind/{mahk}")]
        public async Task<IActionResult> CountTicketFllKind(int mahk, int loai)
        {
            if (mahk == 0 || mahk == null)
            {
                return BadRequest("Khong Tim Thay Hanh Khach");
            }
            else
            {
                var result = await dvService.TotalKindBookingOfAHK(mahk, loai);

                if (result != 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Khong Tim Thay Hanh Khach Hoac Loai Ghe Khong Hop Le");
                }
            }
        }

        [HttpGet("totalSpentTickets/{mahk}")]
        public async Task<IActionResult> TotalSpentTickets(int mahk)
        {
            if (mahk == 0 || mahk == null)
            {
                return BadRequest("Khong Tim Thay Hanh Khach");
            }
            else
            {
                var result = await dvService.TotalSpentBookingOfAHK(mahk);
                if (result != 0 )
                {
                    return Ok(result);
                } else
                {
                    return NotFound("Khong Tim Thay Hanh Khach");
                }
               
            }
        }
    }
}
