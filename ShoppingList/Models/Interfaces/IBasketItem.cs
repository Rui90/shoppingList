using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Models
{
    public interface IBasketItem
    {
        IGood Good { get; set; }

        double TotalPrice { get; set; }

        double PriceWithDiscount { get; set; }

        bool DiscountApplied { get; set; }
    }
}
