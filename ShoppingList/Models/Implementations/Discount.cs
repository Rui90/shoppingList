using ShoppingList.Models.Enums;

namespace ShoppingList.Models.Implementations
{
    public class Discount
    {
        public DiscountEnum Type { get; set; }
        public string Reference { get; set; }
        public double DiscountPercentange { get; set; }
    }
}
