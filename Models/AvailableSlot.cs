using System;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class AvailableSlot
    {
        public int Id { get; set; }

        
        public DateTime Date { get; set; }

       
        public int Hour { get; set; }

        
        public int AvailableCount { get; set; }

    }
}
