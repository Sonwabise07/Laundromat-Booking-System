// Review.cs
using System;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int ReviewId { get; set; }

    [Required]
    public string UserId { get; set; } 

    public int? LaundryServiceId { get; set; } 

    [Required]
    [Range(1, 5)] 
    public int Rating { get; set; }

    [Required]
    [MaxLength(500)] 
    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; } 

 
}
