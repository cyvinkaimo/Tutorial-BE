namespace Yousource.Services.Identity.Data
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Yousource.Infrastructure.Entities.Identity;

    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //// Customize the ASP.NET Core Identity model and override the defaults if needed.
            //// For example, you can rename the ASP.NET Core Identity table names and more.
            //// Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            builder.Entity<UserClaim>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });

            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });

            builder.Entity<UserLogin>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });

            builder.Entity<UserToken>(entity =>
            {
                entity.ToTable(name: "UserTokens");
            });
        }
    }
}
