using BelkiHakiki.Core.DTOs;

namespace BelkiHakiki.Core.Services
{
    public interface ICustomerOrderService : IService<CustomerOrder>
    {
        Task<CustomerOrder> SaveAsync(CustomerOrder customerOrder);
    }
}
