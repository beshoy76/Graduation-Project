using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HospitalManegement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Patient patient { get; set; }
        public virtual Doctor doctor { get; set; }

    }
    public partial class HositalContext :IdentityDbContext
    {
    
        public HositalContext(DbContextOptions<HositalContext> options)
            : base(options)
        {
        }

       
     
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<bookingBed> BookingBeds { get; set; }
        public virtual DbSet<Hospitals> Hospitals { get; set; }
        public virtual DbSet<BookingDoctor> BookingDoctors { get; set; }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }

        // vertiual 2 
        public virtual DbSet<prescrption> Prescrptions { get; set; }
        public virtual DbSet<medicine> Medicines { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
               .HasMany(x => x.doctors)
               .WithMany(x => x.patients)
               .UsingEntity<BookingDoctor>(
                x=>x
                .HasOne(x=>x.doctor)
                .WithMany(x=>x.bookingDoctors)
                .HasForeignKey(x=>x.doctorid)
                ,i=>i.HasOne(x=>x.patient)
                .WithMany(x=>x.bookingDoctors)
                .HasForeignKey(x=>x.patientid),
                i =>
                {
                    i.Property(x => x.reservedate).HasDefaultValueSql("GETDATE()");
                    i.HasKey(x => new { x.patientid, x.doctorid });
                }
                );


           
            modelBuilder.Entity<ApplicationUser>()


               .HasOne(x => x.patient)
                .WithOne(i => i.AppUser)
               .HasForeignKey<Patient>(b => b.UserxId);

            modelBuilder.Entity<ApplicationUser>()


              .HasOne(x => x.doctor)
               .WithOne(i => i.AppUser)
              .HasForeignKey<Doctor>(b => b.UseryId);


            modelBuilder.Entity<bookingBed>()
                .HasKey(b => new { b.bedid, b.patientid });

        }




    }
}
