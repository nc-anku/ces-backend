using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("/api")]
    public class RouteController : ControllerBase
    {
        private static List<Booking> savedBookings = new List<Booking>();
        private static int bookingId = 1;

        [HttpGet(Name = "GetRoute")]
        public List<Route> Get(int From, int To)
        {
            return TotalRouteSearch.FindTotalRoute(From, To, true);
        }

        [HttpPost]
        [Route("search")]
        public IActionResult search(SearchCriteria criteria)
        {
            //System.Diagnostics.Debug.Write(criteria.FromCity + " to " + criteria.ToCity);
            List<Booking> resultList = new List<Booking>();
            Booking cheap = new Booking();
            cheap.FromCity = Int32.Parse(criteria.FromCity);
            cheap.ToCity = Int32.Parse(criteria.ToCity);
            List<Route> totalRoutesCheap = TotalRouteSearch.FindTotalRoute(cheap.FromCity, cheap.ToCity, true);
            cheap.BookingId = bookingId;
            bookingId++;
            cheap.ParcelTypeId = criteria.ParcelType;
            cheap.Price = totalRoutesCheap.Select(x => x.Price).Sum();
            cheap.ShippingTime = totalRoutesCheap.Select(x => x.Time).Sum();
            cheap.Date = criteria.Date;
            cheap.Height = criteria.Height;
            cheap.Width = criteria.Width;
            cheap.Length = criteria.Length;
            cheap.Weight = criteria.Weight;
            cheap.TotalRoute = totalRoutesCheap;

            Booking fast = new Booking();

            fast.FromCity = Int32.Parse(criteria.FromCity);
            fast.ToCity = Int32.Parse(criteria.ToCity);
            List<Route> totalRoutesFast = TotalRouteSearch.FindTotalRoute(cheap.FromCity, cheap.ToCity, false);
            fast.BookingId = bookingId;
            bookingId++;
            fast.ParcelTypeId = criteria.ParcelType;
            fast.Price = totalRoutesFast.Select(x => x.Price).Sum();
            fast.ShippingTime = totalRoutesFast.Select(x => x.Time).Sum();
            
            fast.Date = criteria.Date;
            fast.Height = criteria.Height;
            fast.Width = criteria.Width;
            fast.Length = criteria.Length;
            fast.Weight = criteria.Weight;
            fast.TotalRoute = totalRoutesFast;

            resultList.Add(cheap);
            resultList.Add(fast);
            savedBookings.Add(cheap);
            savedBookings.Add(fast);
            return Ok(resultList);
        }

        [HttpGet]
        [Route("book")]
        public Booking Get(int id)
        {
            Booking b = savedBookings[id - 1];
            b.isBooked = true;
            //List<Booking> trueBookings = savedBookings.Where(x => x.isBooked).ToList();
            //string fileName = "Bookings.json";
            //string jsonString = JsonSerializer.Serialize(trueBookings);
            //System.IO.File.WriteAllText(fileName, jsonString);
            return b;
        }


    }
}
