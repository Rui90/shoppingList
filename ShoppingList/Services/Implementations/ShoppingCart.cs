using Newtonsoft.Json;
using ShoppingList.Models;
using ShoppingList.Models.Enums;
using ShoppingList.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShoppingList.Services
{
    public class ShoppingCart : IShoppingCart
    {

        private IList<string> _output { get; set; }
        // data set
        private IList<IGood> _goods { get; set; }

        private IList<string> _discountApplied { get; set; }

        private IList<IBasketItem> _items { get; set; }

        public ShoppingCart()
        {
            _output = new List<string>();
            _items = new List<IBasketItem>();
            _discountApplied = new List<string>();
            SetData();
        }

        public IList<string> CalculatePrice(string[] items)
        {
            var basket = new BasketItem();
            foreach(var item in items)
            {
                if (_goods.Any(x => x.Name.Equals(item))) {
                    _goods.Where(x => x.Name.Equals(item)).First().Quantity++;
                } else
                {
                    Console.WriteLine($"Item {item} does not exist");
                }
            }
            // add items
            foreach (IGood good in _goods)
            {
                if (good.Quantity > 0)
                {
                    var basketItem = new BasketItem
                    {
                        Good = good,
                        TotalPrice = good.Quantity * good.Price
                    };

                    _items.Add(basketItem);
                }
            }
            if (_items.Count > 0)
            {
                ApplyPromotions();
                CalculateOutput();
            } else
            {
                _output.Add("0 items added to your shopping cart.");
            }
            return _output;
        }

        #region Auxiliar methods

        private void CalculateOutput()
        {
            double _subTotal = 0;
            double _priceWithDiscount = 0;
            foreach(var item in _items)
            {
                _subTotal += item.TotalPrice;

                if (item.DiscountApplied)
                {

                    var percentage = 100 - item.PriceWithDiscount / item.TotalPrice * 100;
                    var difference = Math.Round(item.TotalPrice - item.PriceWithDiscount, 2);
                    string diff = difference >= 1 ? "£" + difference.ToString("#.00", CultureInfo.InvariantCulture) : difference * 100 + "p";
                    _discountApplied.Add($"{item.Good.Name} {percentage}% off: -{diff}");
                    _priceWithDiscount += item.PriceWithDiscount;
                } else
                {
                    _priceWithDiscount += item.TotalPrice;
                }
            }
            _output.Add($"Subtotal: £{_subTotal.ToString("#.00", CultureInfo.InvariantCulture)}");
            if (!_discountApplied.Any())
            {
                _output.Add("(no offers available)");
            } else
            {
                _output = _output.Concat(_discountApplied).ToList();
            }
            _output.Add($"Total: £{_priceWithDiscount.ToString("#.00", CultureInfo.InvariantCulture)}");
        }

        private void ApplyPromotions()
        {
            // apply discount
            foreach(BasketItem item in _items)
            {
                if (item.Good.Discount != null)
                {
                    switch (item.Good.Discount.Type)
                    {
                        case DiscountEnum.Direct:
                            item.PriceWithDiscount = item.TotalPrice - item.TotalPrice * item.Good.Discount.DiscountPercentange * 0.01;
                            item.DiscountApplied = true;
                            break;
                        case DiscountEnum.Buy2:
                            if (item.Good.Quantity >= 2)
                            {
                                if (string.IsNullOrEmpty(item.Good.Discount.Reference))
                                {
                                    item.PriceWithDiscount = item.TotalPrice - (item.Good.Price * item.Good.Discount.DiscountPercentange * 0.01);
                                    item.DiscountApplied = true;
                                } else
                                {
                                    var referencedItem = _items.FirstOrDefault(x => x.Good.Name.Equals(item.Good.Discount.Reference));
                                    if (referencedItem != null)
                                    {
                                        referencedItem.PriceWithDiscount = referencedItem.TotalPrice > 0 ?
                                            referencedItem.PriceWithDiscount > 0 ?
                                            referencedItem.PriceWithDiscount - (referencedItem.Good.Price * item.Good.Discount.DiscountPercentange * 0.01)
                                            : referencedItem.TotalPrice - (referencedItem.Good.Price * item.Good.Discount.DiscountPercentange * 0.01) : 0;
                                                                                referencedItem.DiscountApplied = true;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void SetData()
        {
            _goods = new List<IGood>
            {
                new Good
                {
                    Name = "Soup",
                    Price = 0.65,
                    Discount = new Discount
                    {
                        DiscountPercentange = 50,
                        Reference = "Bread",
                        Type = DiscountEnum.Buy2
                    }
                },
                new Good
                {
                    Name = "Bread",
                    Price = 0.80
                },
                 new Good
                {
                    Name = "Milk",
                    Price = 1.30
                },
                new Good
                {
                    Name = "Apples",
                    Price = 1.00,
                    Discount = new Discount
                    {
                        DiscountPercentange = 10,
                        Type = DiscountEnum.Direct
                    }
                },
            };
        }

        #endregion
    }
} 