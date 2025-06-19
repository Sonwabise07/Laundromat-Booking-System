using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

       
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Washing,StainTreatment,Drying,Ironing,BookingDate,BookingTime")] Booking booking)
        {
            // Debugging statement to indicate that the action has started
            Console.WriteLine("Create action started.");

            if (ModelState.IsValid)
            {
                Console.WriteLine("Model state is valid.");

                if (booking.BookingDate < DateTime.Today)
                {
                   
                    Console.WriteLine("Booking date is in the past.");
                    ModelState.AddModelError("BookingDate", "Booking date cannot be in the past.");
                    return View(booking);
                }

               
                var openTime = TimeSpan.FromHours(8); // 8 AM
                var closeTime = TimeSpan.FromHours(17); // 5 PM

                if (booking.BookingTime < openTime || booking.BookingTime >= closeTime)
                {
                   
                    Console.WriteLine("Booking time is outside operating hours.");
                    ModelState.AddModelError("BookingTime", "Booking time must be between 8 AM and 5 PM.");
                    return View(booking);
                }

              
                var availableSlots = CalculateAvailableSlots(booking.BookingDate, booking.BookingTime);

                if (availableSlots > 0)
                {
                    // Reduce available slots
                    var updatedAvailableSlots = ReduceAvailableSlots(booking.BookingDate, booking.BookingTime);

                    if (updatedAvailableSlots >= 0)
                    {
                        try
                        {
                            _context.Add(booking);
                            await _context.SaveChangesAsync();

                           
                            Console.WriteLine("Booking created successfully.");

                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            
                            Console.WriteLine("An error occurred while saving the booking.");
                            ModelState.AddModelError(string.Empty, "An error occurred while saving the booking.");
                            return View(booking);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("BookingDate", "No available slots for the selected date and time.");

                        
                        Console.WriteLine("No available slots for the selected date and time.");

                        var availableSlotsForErrorMessage = CalculateAvailableSlots(booking.BookingDate, booking.BookingTime);
                        ViewBag.AvailableSlotsForErrorMessage = availableSlotsForErrorMessage;
                        return View(booking);
                    }
                }
                else
                {
                    ModelState.AddModelError("BookingDate", "No available slots for the selected date and time.");

                   
                    Console.WriteLine("No available slots for the selected date and time.");

                    var availableSlotsForErrorMessage = CalculateAvailableSlots(booking.BookingDate, booking.BookingTime);
                    ViewBag.AvailableSlotsForErrorMessage = availableSlotsForErrorMessage;
                    return View(booking);
                }
            }
            else
            {
                
                Console.WriteLine("Model state is invalid.");
            }

            return View(booking);
        }


       
        [HttpGet]
        public IActionResult GetAvailableSlots(string bookingDate, string bookingTime)
        {
            if (string.IsNullOrWhiteSpace(bookingDate) || string.IsNullOrWhiteSpace(bookingTime))
            {
                return Json("Invalid input");
            }

            try
            {
                var availableSlots = CalculateAvailableSlots(DateTime.Parse(bookingDate), TimeSpan.Parse(bookingTime));
                return Json(availableSlots);
            }
            catch (FormatException ex)
            {
                return Json("Invalid date or time format");
            }
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Washing,StainTreatment,Drying,Ironing,BookingDate,BookingTime")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private int ReduceAvailableSlots(DateTime bookingDate, TimeSpan bookingTime)
        {
            
            var updatedAvailableSlots = _context.AvailableSlots
                .Where(slot => slot.Date == bookingDate && slot.Hour == bookingTime.Hours)
                .FirstOrDefault();

            return updatedAvailableSlots.AvailableCount;
        }

        private int CalculateAvailableSlots(DateTime bookingDate, TimeSpan bookingTime)
        {
            
            var availableSlots = _context.AvailableSlots
                .Where(slot => slot.Date == bookingDate && slot.Hour == bookingTime.Hours)
                .Select(slot => slot.AvailableCount)
                .FirstOrDefault();

            return availableSlots;
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
