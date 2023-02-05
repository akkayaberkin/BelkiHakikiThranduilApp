using Microsoft.EntityFrameworkCore;
using BelkiHakiki.Core;
using BelkiHakiki.Core.Repositories;

namespace BelkiHakiki.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

    }
}
