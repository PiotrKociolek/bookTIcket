using BookTicket.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookTicket.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
/*
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().HasOne(x => x.Role);

            modelBuilder.Entity<Ticket>().HasKey(x => x.Id);
            modelBuilder.Entity<Ticket>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Ticket>().HasOne(x => x.Screening).WithMany().HasForeignKey(x => x.ScreeningId);
            modelBuilder.Entity<Ticket>().Property(x => x.BookingTime).IsRequired();

            modelBuilder.Entity<Movie>().HasKey(x => x.Id);
            modelBuilder.Entity<Movie>().Property(x => x.Title).IsRequired();

            modelBuilder.Entity<Screening>().HasKey(x => x.Id);
            modelBuilder.Entity<Screening>().HasOne(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
            modelBuilder.Entity<Screening>().Property(x => x.DateAndTime).IsRequired();
            modelBuilder.Entity<Screening>().Property(x => x.ScreeningRoom).IsRequired();
            modelBuilder.Entity<Screening>().Property(x => x.TotalTickets).IsRequired();
            modelBuilder.Entity<Screening>().Property(x => x.BookedTickets).IsRequired(); */
        }
    }
}