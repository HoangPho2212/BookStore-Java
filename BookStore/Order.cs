using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore
{
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public MyArrayList<Book> BookList { get; set; } = new MyArrayList<Book>();
        public string Status { get; set; } = "Pending";

        public void PrintOrderDetails()
        {
            Console.WriteLine($">>> Status: {Status}");
            Console.WriteLine($"Order ID: {OrderID}");
            Console.WriteLine($"Customer: {CustomerName}");
            Console.WriteLine($"Address: {ShippingAddress}");
            Console.WriteLine("Books in Order:");
            if (BookList.Count == 0)
            {
                Console.WriteLine("  (No books added)");
            }
            else
            {
                for (int i = 0; i < BookList.Count; i++)
                {
                    var b = BookList[i];
                    Console.WriteLine($"  - {b.Title} (Author: {b.Author}, Qty: {b.Quantity})");
                }
            }
        }
    }
}
