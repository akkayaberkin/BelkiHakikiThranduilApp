namespace BelkiHakiki.Core.DTOs
{
    public class ProductSaveDto
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid? Guid { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
