using ASP.NETCoreIdentityCustom.Models;
using AutheSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ASP.NETCoreIdentityCustom.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }

        public DbSet<Booking> Bookings { get; set; } 
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<ASP.NETCoreIdentityCustom.Models.Booking> Booking { get; set; } = default!;

        public void InitializeAvailableSlots()
        {
           

            DateTime currentDate = DateTime.Today;
            DateTime openTime = currentDate.AddHours(8); // 8 AM
            DateTime closeTime = currentDate.AddHours(17); // 5 PM

            while (openTime < closeTime)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Create and add available slot to the DbSet
                    AvailableSlots.Add(new AvailableSlot
                    {
                        Date = currentDate,
                        Hour = openTime.Hour,
                        AvailableCount = 0 // Initialize available slots count
                    });

                    openTime = openTime.AddMinutes(60); // Move to the next hour
                }
            }

            SaveChanges(); 
        }
    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }
}
