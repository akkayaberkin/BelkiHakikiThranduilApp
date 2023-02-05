namespace BelkiHakiki.Core.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
