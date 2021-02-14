
namespace ShoppingList.Models
{
    public class BasketItem : IBasketItem
    { 
        public IGood Good { get; set; }
        public double TotalPrice { get; set; }
        public double PriceWithDiscount { get; set; }
        public bool DiscountApplied { get; set; }
    }
}
