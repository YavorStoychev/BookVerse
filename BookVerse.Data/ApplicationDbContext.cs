using BookVerse.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        { 

        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<UserBook> UsersBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                   .HasOne(b => b.Genre)
                   .WithMany(g => g.Books)
                   .HasForeignKey(b => b.GenreId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Book>()
                .HasQueryFilter(b => b.IsDeleted == false);

            builder.Entity<UserBook>()
                   .HasKey(ub => new { ub.BookId, ub.UserId });

            builder.Entity<UserBook>()
                   .HasOne(ub => ub.Book)
                   .WithMany(b => b.UsersBooks)
                   .HasForeignKey(ub => ub.BookId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserBook>()
                   .HasOne(ub => ub.User)
                   .WithMany()
                   .HasForeignKey(ub => ub.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<UserBook>()
            //    .HasQueryFilter(ub => ub.Book.IsDeleted == false);

            IdentityUser defaultUser = new IdentityUser
            {
                Id = "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
                UserName = "admin@bookverse.com",
                NormalizedUserName = "ADMIN@BOOKVERSE.COM",
                Email = "admin@bookverse.com",
                NormalizedEmail = "ADMIN@BOOKVERSE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>()
                    .HashPassword(new IdentityUser { UserName = "admin@bookverse.com" }, "Admin123!")
            };
            builder.Entity<IdentityUser>().HasData(defaultUser);

            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Thriller" },
                new Genre { Id = 3, Name = "Romance" },
                new Genre { Id = 4, Name = "Sci‑Fi" },
                new Genre { Id = 5, Name = "History" },
                new Genre { Id = 6, Name = "Non‑Fiction" }
            );

            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Isbn = "9780451187949",
                    Title = "Whispers of the Mountain",
                    Description = "Emily Harper (released 2015): A quiet village, a hidden path, and a choice that changes everything.",
                    CoverImageUrl = "https://m.media-amazon.com/images/I/9187Qn8bL6L._UF1000,1000_QL80_.jpg",
                    PublisherId = defaultUser.Id,
                    PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                    GenreId = 1,
                    IsDeleted = false
                },
                new Book
                {
                    Id = 2,
                    Isbn = "9798432591142",
                    Title = "Shadows in the Fog",
                    Description = "Michael Turner (released: 2017): An investigator follows a trail of secrets through a city shrouded in mystery.",
                    CoverImageUrl = "https://m.media-amazon.com/images/I/719g0mh9f2L._UF1000,1000_QL80_.jpg",
                    PublisherId = defaultUser.Id,
                    PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                    GenreId = 2,
                    IsDeleted = false
                },
                new Book
                {
                    Id = 3,
                    Isbn = "9798275420395",
                    Title = "Letters from the Heart",
                    Description = "Sarah Collins (released 2020): A touching story about love, distance, and the power of written words.",
                    CoverImageUrl = "https://m.media-amazon.com/images/I/71zwodP9GzL._UF1000,1000_QL80_.jpg",
                    PublisherId = defaultUser.Id,
                    PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                    GenreId = 3,
                    IsDeleted = false
                }
            );

        }
    }
}
