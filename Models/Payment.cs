using System;
using System.ComponentModel.DataAnnotations;


    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

       
    }

