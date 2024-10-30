namespace WebApp2.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public int ComponentId { get; set; } 1 to 1 ห้ามใส่คีย์อีก จะเกิด loop
        public Component Component { get; set; }

    }
}
