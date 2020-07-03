using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Linq;
using System.Text;

namespace LINQExample
{
    class LINQExampleCollections
    {
        List<Customer> customers = new List<Customer>()
        {
            {new Customer(1, "Michal", "Mustermann", "Musterstrasse", "889221", "Musterstadt", new List<Order>()) },
            {new Customer(1, "Michaela", "Mustermann", "Musterstrasse", "889221", "Musterstadt", new List<Order>()) },
            {new Customer(1, "Daniel", "Düsentrieb", "Erfinderweg 1", "79999", "Entenhausen", new List<Order>()) },
            {new Customer(1, "Donald", "Duck", "Entenstraße 2", "79999", "Entenhausen", new List<Order>()) },
            {new Customer(1, "Torsten", "Tester", "Testweg -1", "D-743398", "Testenhausen", new List<Order>()) },
            {new Customer(1, "Otto", "Walkes", "Friesenstraße 2", "128767", "Emden", new List<Order>()) },
            {new Customer(1, "Tick", "Duck", "Entenstraße 2", "79999", "Entenhausen", new List<Order>()) }
        };
        static void Main(string[] args)
        {
            LINQExampleCollections lec = new LINQExampleCollections();
            lec.startAction();
            Console.ReadLine();
        }

        private void startAction()
        {
            var subset = from c in customers 
                         where c.firstName.StartsWith("D") 
                         orderby c.lastName
                         select c;

            print(subset);

            var subset2 = from c in customers orderby c.lastName select c;
            print(subset2);

            var subset3 = (from c in customers orderby c.lastName select c).Skip(2).Take(3);
            print(subset3);


        }

        private void print(IEnumerable<Customer> c){
            foreach (var cust in c) {
                Console.WriteLine(cust);
            }
        } 
    }

    class Customer
    {
        int customerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        string street { get; set; }
        string postCode { get; set;  }
        string city { get; set; }
        List<Order> orders;

        public Customer(int customerID, string firstName, string lastName, string street, string postCode, string city, List<Order> orders)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.postCode = postCode;
            this.city = city;
            this.orders = orders;
        }

        public override bool Equals(object obj)
        {
            return obj is Customer custumer &&
                   customerID == custumer.customerID &&
                   firstName == custumer.firstName &&
                   lastName == custumer.lastName &&
                   street == custumer.street &&
                   postCode == custumer.postCode &&
                   city == custumer.city &&
                   EqualityComparer<List<Order>>.Default.Equals(orders, custumer.orders);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(customerID, firstName, lastName, street, postCode, city, orders);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append($"ID: {customerID} ");
            sb.Append($"Vorname: {firstName} ");
            sb.Append($"Nachname: {lastName} ");
            sb.Append($"Strasse:  {street} ");
            sb.Append($"PostClode: {postCode} ");
            sb.Append($"City: {city}");
            sb.Append("]");

            return sb.ToString();
        }
    }

    class Order
    {
        public Order(int orderID, DateTime orderDate, bool shipped, DateTime shippingTime, double total)
        {
            this.orderID = orderID;
            this.orderDate = orderDate;
            this.shipped = shipped;
            this.shippingTime = shippingTime;
            this.total = total;
        }

        int orderID { get; set; }
        DateTime orderDate { get; set; }
        bool shipped { get; set; }
        DateTime shippingTime { get; set; }
        double total { get; set; }
    }
}
