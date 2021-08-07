using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectOrientedProgramming
{
    public interface IOnlineShop
    {
        void addProduct(string product, int quantuty);
        void removeProduct(string product);
        void updateProductQuantity(string product, int quantuty);
        List<string> getProducts();
    }

    public class OnlineShop : IOnlineShop
    {
        private readonly Dictionary<string, int> _products = new ();

        public void addProduct(string product, int quantity)
        {
            ValidateProductTitle(product);
            ValidateProductQuantity(quantity);

            _products.Add(product, quantity);
        }

        public List<string> getProducts()
        {
            var productsSortedByPrices = _products
                .Select(product => (product.Key, product.Value))
                .OrderByDescending(product=> product.Value)
                .Select(product => product.Key)
                .ToList();

            return productsSortedByPrices;
        }

        public void removeProduct(string product)
        {
            ValidateProductTitle(product);
            CheckProductAvailability(product);
            
            _products.Remove(product);
        }

        public void updateProductQuantity(string product, int quantity)
        {
            ValidateProductTitle(product);
            CheckProductAvailability(product);            
            
            _products[product] = quantity;
        }
        
        private void ValidateProductTitle(string productTitle)
        {
            if (productTitle == string.Empty)
                throw new ArgumentException("Title of product cannot be empty");
        }

        private void ValidateProductQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Product quantity should be greater then zero");
        }
        
        private void CheckProductAvailability(string productTitle)
        {
            if (!_products.ContainsKey(productTitle))
                throw new ArgumentException($"There is no product {productTitle} in shop");
        }
    }
}