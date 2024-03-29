﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextFilesProcessing
{
    public record Product(string Name, string Country, double Price, DateTime Date)
    {
        public static Product GetFromLine(string line)
        {
            var splittedLine = line.Split(" ");

            var name = splittedLine[0];
            var country = splittedLine[1];
            var price = double.Parse(splittedLine[2]);
            var date = DateTime.Parse(splittedLine[3]);

            return new (name, country, price, date);
        }
    }

    public static class ProductsProcessor
    {
        public static void ProcessFile(string fullFileName)
        {
            if (!File.Exists(fullFileName))
            {
                Console.WriteLine($"File {fullFileName} not found");
                return;
            }

            var products = File.ReadLines(fullFileName)
                .Select(Product.GetFromLine)
                .ToList();
            
            var productsOrderedByPrices = products.OrderBy(product => -product.Price);
            
            PrintProducts(productsOrderedByPrices);

            var freshProducts = products
                .Where(product => product.Date.CompareTo(DateTime.Now.Subtract(TimeSpan.FromDays(50))) < 0);

            PrintProducts(freshProducts);
        }

        private static void PrintProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} {product.Country} {product.Price} {product.Date}");
            }
        }
    }
}