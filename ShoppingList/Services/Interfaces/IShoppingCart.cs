using System.Collections.Generic;

namespace ShoppingList.Services
{
    public interface IShoppingCart
    {
        IList<string> CalculatePrice(string[] items);
    }
}
