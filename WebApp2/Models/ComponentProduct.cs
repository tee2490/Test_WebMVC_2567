namespace WebApp2.Models
{
    public class ComponentProduct
    {
        //public int Id { get; set; } M: Mกำหนด primary key ด้วยคีย์เดียวก็ได้
        public int ComponentId { get; set; }
        public Component Component { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
