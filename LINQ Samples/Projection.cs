using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ_Samples
{
    
    public class Projection
    {
        int[] number = {0,1,3,2,5,4,6,8,9,7};
        string[] String = { "zero","one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        //Simple Select Sample: Transformation
        public void SampleTransformation()
        {
            var varNumbers = from n in number
                             select String[n];
            Console.WriteLine("Numbers in the list");
            foreach (var i in varNumbers)
                Console.WriteLine(i);
        }
        //Simple Select Anonymous Type
        public void SelectAnonymosType()
        {
            var varNumbers = from n in number
                             select new { num = n+1 };
            Console.WriteLine("Numbers in the list");
            foreach (var i in varNumbers)
                Console.WriteLine(i);
        }
        //Simple Select Index
        public void SelectIndex()
        { 
            //var varNumber = number.Select((num,index)=> new {Num=num, InPlace=(num==index)});
            var varNumber = number.Select((num, index) => new { Num = num, InPlace = index });
            Console.WriteLine("Numbers in the list");
            foreach (var i in varNumber)
                Console.WriteLine("{0}, {1}", i.Num,i.InPlace);

        }
        //Simple Select Filter
        public void SelectFilter()
        {
            var evenNumber = from n in number
                             where n % 2 == 0
                             select String[n];
            foreach (var i in evenNumber)
                Console.WriteLine(i);
        }
        //select Many: compound From
        public void SelectManySample1()
        { 
            Customer customer = new Customer();
            var list = customer.GetCustomers();
            var customerOrders = from cust in list
                                 from orders in cust.orderList.AsEnumerable<Order>()
                                 select new { CustName = cust.CustomerName, OrderDesc = orders.OrderDescription };
            foreach (var i in customerOrders)
                Console.WriteLine("{0}:{1}",i.CustName,i.OrderDesc);
        }
        //select Many: Compound Form
        public void SelectManySample12()
        {
            int[] number2 = { 0, 1, 3, 2, 5, 4, 6, 8, 9, 7 };
            var numComparer = from num1 in number
                              from num2 in number2
                              where num1 < num2
                              select new { num1, num2 };
            foreach (var pair in numComparer)
                Console.WriteLine("{0} is less than {1}", pair.num1, pair.num2);

        }
        //select Many: Multiple from
        public void SelectManySample2()
        {
            Customer customer = new Customer();
            var list = customer.GetCustomers();
            var customerList = from cust in list
                               where cust.CustomerName.Contains("1")
                               from orders in cust.orderList.AsEnumerable<Order>()
                               where orders.TotalOrderAmt <= 600.00d
                               select new { CustName = cust.CustomerName, OrderDesc = orders.OrderDescription };
            foreach (var i in customerList)
                Console.WriteLine("{0}:{1}", i.CustName, i.OrderDesc);
        }
        //select Many: Indexed
        public void SelectManyIndexed()
        {
            Customer customer = new Customer();
            var list = customer.GetCustomers();
            var orders = list.SelectMany(lst => lst.orderList); 
            //var customerList = list.SelectMany((cust, custIndex) => cust.orderList.Select(order =>"Customer #" + (custIndex + 1) + 
            //                                                                                       " has an order with OrderID " + order.OrderID));

            var custList = list.SelectMany(
                                            (cust, custindex) =>
                                                orders.Where(o => o.OrderID == cust.CustomerID)
                                                .Select(o => new { ordernum = o.OrderID, custnum = custindex + 1, OrderDesc = o.OrderDescription }));
            foreach (var i in custList)
                Console.WriteLine("order number {0} and customer nummber {1} order Description is {2}", i.ordernum, i.custnum, i.OrderDesc);
        
        }
        
    }
    public class Customer
    {
        public int CustomerID;
        public string CustomerName;
        public List<Order> orderList;

        public List<Customer> GetCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            Customer cust;
            Order order;
            cust = new Customer();
            cust.CustomerID = 1;
            cust.CustomerName = "Customer 1";
            cust.orderList = new List<Order>();
            order = new Order();
            order.OrderID = 1;
            order.OrderDescription = "Customer1 Order 1";
            order.OrderDate = new DateTime(2012,1,1);
            order.TotalOrderAmt = 500.00d;
            cust.orderList.Add(order);
            order = new Order();
            order.OrderID = 2;
            order.OrderDescription = "Customer1 Order 2";
            order.OrderDate = new DateTime(2012, 1, 2);
            order.TotalOrderAmt = 600.00d;
            cust.orderList.Add(order);
            order = new Order();
            order.OrderID = 3;
            order.OrderDescription = "Customer1 Order 3";
            order.OrderDate = new DateTime(2012, 1, 2);
            order.TotalOrderAmt = 500.00d;
            cust.orderList.Add(order);
            customerList.Add(cust);
            //-----------------------------------------------------------------
            cust = new Customer();
            cust.CustomerID = 2;
            cust.CustomerName = "Customer 12";
            cust.orderList = new List<Order>();
            order = new Order();
            order.OrderID = 1;
            order.OrderDescription = "Customer2 Order 1";
            order.OrderDate = new DateTime(2011, 1, 1);
            order.TotalOrderAmt = 1500.00d;
            cust.orderList.Add(order);
            order = new Order();
            order.OrderID = 2;
            order.OrderDescription = "Customer2 Order 2";
            order.OrderDate = new DateTime(2012, 1, 2);
            order.TotalOrderAmt = 600.00d;
            cust.orderList.Add(order);
            customerList.Add(cust);
            return customerList;
        }
    }
    public class Order
    {
        public int OrderID;
        public double TotalOrderAmt;
        public string OrderDescription;
        public DateTime OrderDate;
    }
}
