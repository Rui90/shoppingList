using ShoppingList.Models.Implementations;

namespace ShoppingList.Models
{
    public class Good : IGood
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Discount Discount { get; set; }

        public Good()
        {
        }
    }
}
