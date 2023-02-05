namespace BelkiHakiki.Core.DTOs
{
    public class CustomerOrderDto 
    {
        public CustomerDto Customer { get; set; }
        public List<ProductSaveDto> Products { get; set; }
    }
}
