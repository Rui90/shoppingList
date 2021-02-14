using ShoppingList.Services;
using System;
using Xunit;

namespace ShoppingList.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void ShoppingCartTestsAssingmentOutput()
        {
            var expectedOutput = new string[]{"Subtotal: £3.10", "Apples 10% off: -10p", "Total: £3.00" };
            var shoppingCard = new ShoppingCart();
            var output = shoppingCard.CalculatePrice(new string[]{ "Apples", "Milk", "Bread"});
            Assert.Equal(output.Count, expectedOutput.Length);
            for(int i = 0; i < output.Count; i++)
            {
                Assert.Equal(expectedOutput[i], output[i]);
            }
        }

        [Fact]
        public void ShoppingCartTestsAssingmentWithInvalidInput()
        {
            var expectedOutput = "0 items added to your shopping cart.";
            var shoppingCard = new ShoppingCart();
            var output = shoppingCard.CalculatePrice(new string[] { "Item1" });
            Assert.Equal(1, output.Count);
            Assert.Equal(output[0], expectedOutput);
        }

        [Fact]
        public void ItemWithoutDiscount()
        {
            var expectedOutput = new string[] { "Subtotal: £1.30", "(no offers available)", "Total: £1.30" };
            var shoppingCard = new ShoppingCart();
            var output = shoppingCard.CalculatePrice(new string[] { "Milk" });
            Assert.Equal(3, output.Count);
            for (int i = 0; i < output.Count; i++)
            {
                Assert.Equal(expectedOutput[i], output[i]);
            }
        }

        [Fact]
        public void MultiDiscountItem()
        {
            var expectedOutput = new string[] { "Subtotal: £4.10", "Bread 50% off: -40p", "Apples 10% off: -20p", "Total: £3.50" };
            var shoppingCard = new ShoppingCart();
            var output = shoppingCard.CalculatePrice(new string[] { "Apples", "Apples", "Bread", "Soup", "Soup" });
            Assert.Equal(4, output.Count);
            for (int i = 0; i < output.Count; i++)
            {
                Assert.Equal(expectedOutput[i], output[i]);
            }
        }
    }
}
