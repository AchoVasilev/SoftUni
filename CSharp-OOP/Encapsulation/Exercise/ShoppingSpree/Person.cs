using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    public class Person
    {
        private List<Product> bag;
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.bag = new List<Product>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Name cannot be empty");
                }

                this.name = value;
            }
        }

        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Money cannot be negative");
                }

                this.money = value;
            }
        }

        public void AddProduct(Product product)
        {
            this.bag.Add(product);
        }

        public List<Product> SeeBag()
        {
            return this.bag;
        }
    }
}
