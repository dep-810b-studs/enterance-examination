using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace exam
{
    struct Product
    {
        public string Name;
        public string Country;
        public double Price;
        public DateTime Date;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var fullFileName = @"C:\input.txt";
            
            if (!File.Exists(fullFileName))
            {
                Console.WriteLine($"File {fullFileName} not found");
                return;
            }

            var products = new List<Product>();
            
            foreach (var line in File.ReadLines(fullFileName))
            {
                var splitedLine = line.Split(" ");

                var product = new Product
                {
                    Name = splitedLine[0],
                    Country = splitedLine[1],
                    Price = double.Parse(splitedLine[2]),
                    Date = DateTime.Parse(splitedLine[3])
                };
                
                products.Add(product);
            }

            var productsByPrices = products.OrderBy(product => -product.Price);
            
             foreach (var product in productsByPrices)
             {
                 Console.WriteLine($"{product.Name} {product.Country} {product.Price} {product.Date}");
             }

             Console.WriteLine(DateTime.Parse("04.08.2020").Subtract(TimeSpan.FromDays(50)));
             
             var freshProducts = products.Where(product => product.Date.CompareTo(DateTime.Now.Subtract(TimeSpan.FromDays(50))) < 0);

             foreach (var product in freshProducts)
             {
                 Console.WriteLine($"{product.Name} {product.Country} {product.Price} {product.Date}");
             }
        }
    }
}