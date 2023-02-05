namespace BelkiHakiki.Core.DTOs
{
    public class CustomerOrderUpdateDto
    {
        public CustomerUpdateDto Customer { get; set; }
        public List<ProductUpdateDto> ProductList { get; set; }

    }
}
