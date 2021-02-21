using Biblioteca.Core.Models;
using Biblioteca.Core.Models.Auth;
using Biblioteca.Core.Models.Books;
using Biblioteca.Core.Models.Checkouts;
using Biblioteca.Data.Configurations;
using Biblioteca.Data.Configurations.Books;
using Biblioteca.Data.Configurations.Checkouts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data
{
    public class ApiDbContext : IdentityDbContext<User, Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {

        #region Books

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        #endregion

        #region Users
        //public DbSet<User> Users { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<Employee> Employees { get; set; }

        #endregion

        #region Checkouts

        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutBook> CheckoutBooks { get; set; }

        #endregion


        public DbSet<Country> Countries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder
                .ApplyConfiguration(new BookConfiguration());
            builder
                .ApplyConfiguration(new BookAuthorConfiguration());
            builder
                .ApplyConfiguration(new BookCategoryConfiguration());
            builder
                .ApplyConfiguration(new CheckoutConfiguration());
            builder
               .ApplyConfiguration(new CheckoutBookConfiguration());
            builder
               .ApplyConfiguration(new TicketConfiguration());
        }
}
}
