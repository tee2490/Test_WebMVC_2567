using Microsoft.EntityFrameworkCore;

namespace WebApp2.Models
{
    [Owned]
    public class ProductExtend
    {
        public string Color { get; set; }
        public double Weight { get; set; }
    }

}
