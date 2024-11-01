using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public ProductExtend ProductExtend { get; set; }

        public int CategoryId { get; set; } //fk คีย์นอก
        public Category Category { get; set; } //ความสัมพันธ์ 1 M

        //public int xxxId { get; set; } //fk คีย์นอก
        //[ForeignKey("xxxId")]
        //public Category Category { get; set; } //ความสัมพันธ์ 1 M

        public List<ProductDetails> ProductDetails  { get; set; }
    }
}
