namespace BelkiHakiki.Core
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}
