using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store
{
    class ShopBasket : IProduct
    {
        private Dictionary<string, int> products;

        public ShopBasket()
        {
            products = new Dictionary<string, int>();
        }

        public void addProduct(string product, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException();
            }

            products.Add(product, quantity);
        }

        public List<string> getProducts()
        {
            return products.Keys.OrderBy(x=>x).ToList();
        }

        public void removeProduct(string product)
        {
            products.Remove(product);
        }

        public void updateProduct(string product, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException();
            }
            products[product] = quantity;
        }
    }
}
