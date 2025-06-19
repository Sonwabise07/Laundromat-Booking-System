using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ValidBookingTimeAttribute : ValidationAttribute
{
    private readonly TimeSpan[] _validTimeSlots = {
        new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0),
        new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0),
        new TimeSpan(14, 0, 0), new TimeSpan(15, 0, 0), new TimeSpan(16, 0, 0)
    };

    public override bool IsValid(object value)
    {
        if (value is TimeSpan selectedTime)
        {
            return _validTimeSlots.Contains(selectedTime);
        }
        return false;
    }
}
