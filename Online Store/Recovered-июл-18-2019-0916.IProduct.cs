using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store
{
    interface IProduct
    {
        void addProduct(String product, int quantuty);
        void removeProduct(String product);
        void updateProduct(String product, int quantuty);
        List<String> getProducts();
    }
}
