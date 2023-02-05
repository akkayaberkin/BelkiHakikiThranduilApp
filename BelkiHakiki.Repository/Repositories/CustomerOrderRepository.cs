using Microsoft.EntityFrameworkCore;
using BelkiHakiki.Core;
using BelkiHakiki.Core.Repositories;

namespace BelkiHakiki.Repository.Repositories
{
    public class CustomerOrderRepository : GenericRepository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
