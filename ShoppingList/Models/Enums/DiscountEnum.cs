using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShoppingList.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscountEnum
    {
        // Discount applied directly
        Direct,
        // Discount applied when two are bought
        Buy2
    }
}
