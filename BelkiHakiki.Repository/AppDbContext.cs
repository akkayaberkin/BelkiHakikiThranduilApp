using Microsoft.EntityFrameworkCore;
using BelkiHakiki.Core;
using System.Reflection;

namespace BelkiHakiki.Repository
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<CustomerOrder> CostumerOrders { get; set; }
        public DbSet<Customer> Costumer { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;

                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region UserCreate
            modelBuilder.Entity<AppUsers>().HasData(new AppUsers
            {
                Id = 1,
                Username = "testUser",
                Password = "testPassword",
                AppRole = "Admin",
                InUse = true,
                Guid = Guid.NewGuid(),

            },
            new AppUsers
            {
                Id = 2,
                Username = "ber",
                Password = "123",
                AppRole = "Admin",
                InUse = true,
                Guid = Guid.NewGuid(),
            }
            );
            #endregion

            #region RoleCreate
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 1,
                Definition = "Admin",
            },

            new AppRole
            {
                Id = 2,
                Definition = "Member",
            });

            #endregion

            base.OnModelCreating(modelBuilder);

        }
    }
}
