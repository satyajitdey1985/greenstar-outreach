using GreenStar.Entity.Holiday;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using GreenStar.Entity.Address;
using GreenStar.Entity.Academy;
using GreenStar.Entity.User;

namespace GreenStar.Service
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<State> state { get; set; }
        public DbSet<City> city { get; set; }
        public DbSet<School> school { get; set; }
        public DbSet<HolidayCalendar> HolidayCalendar { get; set; }
        public DbSet<HolidayTracker> HolidayTracker { get; set; }
        public DbSet<ClassDetails> ClassDetails { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<ParameterAttribute> ParameterAttribute { get; set; }
        public DbSet<GroupDetails> GroupDetails { get; set; }
        public DbSet<GroupStudentMapping> GroupStudentMapping { get; set; }
        public DbSet<PerformanceCount> PerformCount { get; set; }
        public DbSet<UserDetails> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HolidayCalendar>()
                .Property(m => m.HolidayName)
                .IsRequired();

            //modelBuilder.Entity<HolidayTracker>()
            //.HasRequired(hk => hk.holidayCalendar)
            //.WithMany()
            //.HasForeignKey(hk => hk.HolidayID)
            //.WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupStudentMapping>()
                .HasRequired(c => c.groupDetails)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupStudentMapping>()
                .HasRequired(s => s.student)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}