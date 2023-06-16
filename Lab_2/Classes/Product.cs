using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1.Classes
{
    public class Product
    {
        // https://www.delftstack.com/howto/csharp/csharp-private-set/
        public string Name { get; private set; }

        public double Price { get; private set; }
        public string Code { get; private set; }

        public int Quantity { get; set; }

        // Constructor to initialize product properties
        public Product(string name, double price, string code, int quantity)
        {
            Name = name;
            Price = price;
            Code = code;
            Quantity = quantity;
        }
    }
}
