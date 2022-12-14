using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MMA.API.Data.Entities;


namespace MMA.API.Data
{
  public partial class MMADbContext : IdentityDbContext<ApiUser>
  {
    public MMADbContext()
    {

    }

    public MMADbContext(DbContextOptions<MMADbContext> options) : base(options)
    {

    }

    public virtual DbSet<entityClient> Clients { get; set; } = null!;
    public virtual DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Contact>(entity =>
      {

        entity.HasOne(c => c.Client)
              .WithMany(con => con.Contacts)
              .HasForeignKey(c => c.ClientId);
      });

      modelBuilder.Entity<IdentityRole>().HasData(
          new IdentityRole
          {
            Name = "User",
            NormalizedName = "USER",
            Id = "8343074e-8623-4e1a-b0c1-84fb8678c8f3"
          },
          new IdentityRole
          {
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            Id = "c7ac6cfe-1f10-4baf-b604-cde350db9554"
          }
      );

      var hasher = new PasswordHasher<ApiUser>();

      modelBuilder.Entity<ApiUser>().HasData(
          new ApiUser
          {
            Id = "8e448afa-f008-446e-a52f-13c449803c2e",
            Email = "admin@bookstore.com",
            NormalizedEmail = "ADMIN@BOOKSTORE.COM",
            UserName = "admin@bookstore.com",
            NormalizedUserName = "ADMIN@BOOKSTORE.COM",
            FirstName = "System",
            LastName = "Admin",
            PasswordHash = hasher.HashPassword(null, "P@ssword1")
          },
          new ApiUser
          {
            Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
            Email = "user@bookstore.com",
            NormalizedEmail = "USER@BOOKSTORE.COM",
            UserName = "user@bookstore.com",
            NormalizedUserName = "USER@BOOKSTORE.COM",
            FirstName = "System",
            LastName = "User",
            PasswordHash = hasher.HashPassword(null, "P@ssword1")
          }
      );

      modelBuilder.Entity<IdentityUserRole<string>>().HasData(
          new IdentityUserRole<string>
          {
            RoleId = "8343074e-8623-4e1a-b0c1-84fb8678c8f3",
            UserId = "30a24107-d279-4e37-96fd-01af5b38cb27"
          },
          new IdentityUserRole<string>
          {
            RoleId = "c7ac6cfe-1f10-4baf-b604-cde350db9554",
            UserId = "8e448afa-f008-446e-a52f-13c449803c2e"
          }
      );

      OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

  }


}
