namespace WebApp2.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        //public int ProductId { get; set; }
        //public Product Product { get; set; }
    }

}
