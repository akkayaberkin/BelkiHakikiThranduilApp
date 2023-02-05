
namespace BelkiHakiki.Core
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<CustomerOrder> CustomerOrders { get; set; }

    }
}
