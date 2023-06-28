using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1.Classes
{
    public class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Code { get; private set; }
        public int Quantity { get; set; }

        public Product(string name, double price, string code, int quantity)
        {
            try
            {// simple validation to check if strings are null/empty or if price is less than or 
                // equal to 0, or if any other errors occur, simple validation in catch block.
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Product name cannot be null or empty.");

                if (string.IsNullOrEmpty(code))
                    throw new ArgumentException("Product code cannot be null or empty.");

                if (price <= 0)
                    throw new ArgumentException("Product price must be greater than zero.");

                Name = name;
                Price = price;
                Code = code;
                Quantity = quantity;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
