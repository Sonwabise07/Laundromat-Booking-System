using System;
using System.ComponentModel.DataAnnotations;



    public class Inventory
    {
        public int InventoryId { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        
    }

