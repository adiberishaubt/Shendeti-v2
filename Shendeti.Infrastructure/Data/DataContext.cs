using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Infrastructure.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<BloodRequest> BloodRequests { get; set; }
 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.BloodType)
            .HasName("IND_Users_BloodType");
        
        var passwordHasher = new PasswordHasher<User>();
        
        builder.Entity<User>().HasData(new User
        {
            Id = "123admin",
            UserName = "granit",
            NormalizedUserName = "GRANIT",
            Email = "granit@gmail.com",
            NormalizedEmail = "GRANIT@GMAIL.COM",
            PasswordHash = passwordHasher.HashPassword(null, "Granit.bu2"),
            GivesBlood = false,
            BloodType = BloodType.APositive,
            Gender = "Male"
        });
    
        builder.Entity<Admin>().HasData(new Admin
        {
            UserId = "123admin"
        });
        
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Doctor",
            NormalizedName = "DOCTOR"
        });
        
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Patient",
            NormalizedName = "PATIENT"
        });
        
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "ADMIN21451290fdjskn12389tr12",
            Name = "Admin",
            NormalizedName = "ADMIN"
        });
        
        builder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                UserId = "123admin",
                RoleId = "ADMIN21451290fdjskn12389tr12"
            });
        
        builder.Entity<Doctor>()
            .HasMany(d => d.Specializations)
            .WithMany();
        
        builder.Entity<Doctor>()
            .HasMany(d => d.Services)
            .WithMany();

        builder.Entity<Schedule>()
            .HasMany<Slot>(s => s.Slots)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Slot>()
            .HasOne<Schedule>(s => s.Schedule)
            .WithMany(s => s.Slots)
            .HasForeignKey(s => s.ScheduleId);
        
        builder.Entity<Country>()
            .HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Service>()
            .HasOne(s => s.Specialization)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<User>()
            .HasOne(u => u.City)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BloodRequest>()
            .HasOne<User>(b => b.User)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(builder);
    }
}