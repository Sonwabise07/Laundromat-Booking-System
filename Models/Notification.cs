using System;
using System.ComponentModel.DataAnnotations;



    public class Notification
    {
        public int NotificationId { get; set; }

        [Required]
        [Display(Name = "Notification Date")]
        public DateTime NotificationDate { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

    }

