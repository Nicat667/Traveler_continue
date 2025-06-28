using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModels.Room;

namespace Travel.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _config;
        private readonly IRoomService _roomService;
        private readonly UserManager<AppUser> _userManager;

        public PaymentController(IPaymentService paymentService, IConfiguration config, IRoomService roomService, UserManager<AppUser> userManager)
        {
            _paymentService = paymentService;
            _config = config;
            _roomService = roomService;
            _userManager = userManager;
        }
        public IActionResult Checkout()
        {
            ViewBag.StripeKey = _config["Stripe:PublishableKey"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StartBookingPayment(DateTime startDate, DateTime endDate, int roomCount, int roomId, int hotelId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["BookingError"] = "Please log in to book a room.";
                return RedirectToAction("Detail", "Room", new { id = roomId });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var booking = new BookVM
            {
                StartDate = startDate,
                EndDate = endDate,
                RoomCount = roomCount,
                RoomId = roomId,
                HotelId = hotelId,
                UserId = user.Id
            };
           

            TempData["BookingInfo"] = JsonConvert.SerializeObject(booking); // Requires Newtonsoft.Json

            // Get room info from DB
            var room = await _roomService.GetRoomById(roomId);
            if (room == null) return NotFound();

            var nights = (endDate - startDate).Days;
            if (nights <= 0) return RedirectToAction("Details", "Room", new { id = roomId });

            var amount = room.Price * nights * roomCount * 100; // Stripe amount in cents

            string domain = $"{Request.Scheme}://{Request.Host}";

            var sessionUrl = await _paymentService.CreateCheckoutSessionAsync(
                (long)amount,
                successUrl: domain + "/payment/completebooking",
                cancelUrl: domain + "/payment/cancel",
                userEmail: user.Email
            );

            return Redirect(sessionUrl);
        }

        public IActionResult Success()
        {
           return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }

        public async Task<IActionResult> CompleteBooking()
        {
            if (!TempData.ContainsKey("BookingInfo"))
                return RedirectToAction("Index", "Home");

            var bookingJson = TempData["BookingInfo"]?.ToString();
            if (string.IsNullOrEmpty(bookingJson))
                return RedirectToAction("Index", "Home");

            var booking = JsonConvert.DeserializeObject<BookVM>(bookingJson);

            // Optional: validate again if needed

            

            var result = await _roomService.BookRoom(booking); // You will implement this

            if (!result)
            {
                TempData["BookingError"] = "Failed to book!";
                return RedirectToAction("Details", "Room", new { id = booking.RoomId });
            }

            return RedirectToAction("Success");
        }
    }
}
