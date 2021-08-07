using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectOrientedProgramming
{
    public interface IOnlineAuction
    {
        void placeProduct(string product, int initialPrice);
        void addBid(string user, string product, int price);
        void removeBid(string user, string product);
        string sellProduct(string product);
    }
    
    public class OnlineAuction : IOnlineAuction
    {
        private readonly Dictionary<string, int> _initialPricesByProducts = new();
        private readonly Dictionary<(string product, string user), int> _productRequests = new();

        public void placeProduct(string product, int initialPrice)
        {
            ValidateProductTitle(product);
            ValidatePrice(initialPrice);
            
            _initialPricesByProducts.Add(product, initialPrice);
        }

        public void addBid(string user, string product, int price)
        {
            ValidateUserName(user);
            ValidateProductTitle(product);
            CheckProductAvailability(product);
            ValidatePrice(price, product);
                
            _productRequests.Add((product, user), price);
        }

        public void removeBid(string user, string product)
        {
            ValidateUserName(user);
            ValidateProductTitle(product);
            CheckProductAvailability(product);
            CheckBidExisting(product, user);

            _productRequests.Remove((product, user));
        }

        public string sellProduct(string product)
        {
            ValidateProductTitle(product);
            CheckProductAvailability(product);

            var productBids = _productRequests
                .Where(productRequest => productRequest.Key.product == product);

            if (!productBids.Any())
                throw new ArgumentException($"There are no bids for product {product}");
            
            var auctionWinner =productBids
                .OrderByDescending(productRequest => productRequest.Value)
                .First();
            
            _productRequests.Remove(auctionWinner.Key);
            _initialPricesByProducts.Remove(product);

            return auctionWinner.Key.user;
        }

        private void ValidatePrice(int price, string product = "")
        {
            if (price < 0) throw new ArgumentException("Price cannot be less then zero", nameof(price));
            if (product == string.Empty) return;
            if (_initialPricesByProducts[product] >= price) 
                throw new ArgumentException("Price cannot be less then initial product price", nameof(price));
        }

        private void ValidateUserName(string userName)
        {
            if (userName == string.Empty)
                throw new ArgumentException("User name cannot be empty");
        }
        
        private void ValidateProductTitle(string productTitle)
        {
            if (productTitle == string.Empty)
                throw new ArgumentException("Title of product cannot be empty");
        }
        
        private void CheckProductAvailability(string productTitle)
        {
            if (!_initialPricesByProducts.ContainsKey(productTitle))
                throw new ArgumentException($"There is no product {productTitle} in auction");
        }

        private void CheckBidExisting(string productTitle, string userName = "")
        {
            if (!_productRequests.ContainsKey((productTitle, userName)))
                throw new ArgumentException($"There is no bid for product {productTitle} by user {userName}");
        }
    }
}