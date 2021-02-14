using ShoppingList.Services;
using System;
using System.Linq;

namespace ShoppingList
{
    class Program
    {
        public static string COMMAND = "PriceBasket";
        static void Main(string[] args)
        {
            Console.WriteLine("Please insert your command:");
            while (true)
            {
                var line = Console.ReadLine();
                var items = line.Split(" ");
                IShoppingCart shoppingCard = new ShoppingCart();
                if (items[0].Equals(COMMAND) && items.Length > 1)
                {
                    var result = shoppingCard.CalculatePrice(items.Skip(1).ToArray());
                    foreach(var r in result)
                    {
                        Console.WriteLine(r);
                    }
                    Console.WriteLine("Please insert another command:");
                } else
                {
                    Console.WriteLine($"Please insert a valid command starting with {COMMAND}");
                }

            }
        }
    }
}
