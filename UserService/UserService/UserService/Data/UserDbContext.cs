
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserService.Model;

namespace UserService.Data;
   public class UserDbContext : IdentityDbContext<UserModel>
    {
      public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserModel>()
            .Property(u => u.Role)
            .HasConversion<string>();         
    }
}

