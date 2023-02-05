using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BelkiHakiki.Core;

namespace BelkiHakiki.Repository.Configurations
{
    internal class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            //builder.HasOne(co => co.Customer).WithMany(c => c.CustomerOrders).HasForeignKey(co => co.CustomerId);
            //builder.HasMany(co => co.Products).WithOne().HasForeignKey(p => p.CustomerOrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
