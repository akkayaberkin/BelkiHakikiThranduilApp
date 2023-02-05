using Microsoft.EntityFrameworkCore;
using BelkiHakiki.Core;
using BelkiHakiki.Core.Repositories;

namespace BelkiHakiki.Repository.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

       
    }
}
