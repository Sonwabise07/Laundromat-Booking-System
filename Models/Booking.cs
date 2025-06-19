using AutheSystem.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Booking
    {
        public int Id { get; set; }

        
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please select at least one service.")]
        public bool Washing { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please select at least one service.")]
        public bool StainTreatment { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please select at least one service.")]
        public bool Drying { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please select at least one service.")]
        public bool Ironing { get; set; }

        [Required(ErrorMessage = "Please select a booking date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        [FutureDate(ErrorMessage = "Booking date cannot be in the past.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Please select a booking time.")]
        [Display(Name = "Booking Time")]
        [ValidBookingTime(ErrorMessage = "The selected time slot is not available.")]
        public TimeSpan BookingTime { get; set; }

       
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }
    }
}
