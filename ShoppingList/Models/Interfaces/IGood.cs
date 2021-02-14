using ShoppingList.Models.Implementations;

namespace ShoppingList.Models
{
    public interface IGood
    {
        /// <summary>
        /// Product Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        double Price { get; set; }

        /// <summary>
        /// Product Quantity
        /// </summary>
        int Quantity { get; set; }

        /// <summary>
        /// Product Discount
        /// </summary>
        Discount Discount { get; set; }
    }
}
