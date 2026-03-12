using BookStore;
using System;
using System.Collections.Generic;

namespace Bookstore
{
    class Program
    {
        static void Main(string[] args)
        {

            MyOrderQueue orderQueue = new MyOrderQueue();


            Order order1 = new Order
            {
                OrderID = 101,
                CustomerName = "Hoang Pho",
                ShippingAddress = "123 Greenwich St",
                BookList = new List<Book>
                {
                    new Book { Title = "Data Structures", Author = "Tutor A" },
                    new Book { Title = "Algorithms", Author = "Tutor B" },
                    new Book { Title = "C# Programming", Author = "Tutor C" }
                }
            };


            Console.WriteLine("--- System: Receiving New Order ---");
            orderQueue.Enqueue(order1);
            Console.WriteLine($"Added Order {order1.OrderID} to the system.");


            Console.WriteLine("\n--- System: Sorting Books for Order 101 ---");
            AlgorithmHelper.QuickSortBooks(order1.BookList, 0, order1.BookList.Count - 1);

            foreach (var book in order1.BookList)
            {
                Console.WriteLine($"Sorted: {book.Title} by {book.Author}");
            }


            Console.WriteLine("\n--- System: Processing Next Order in Queue ---");
            Order processedOrder = orderQueue.Dequeue();
            if (processedOrder != null)
            {
                Console.WriteLine($"OrderID {processedOrder.OrderID} for {processedOrder.CustomerName} is now complete.");
            }

            Console.WriteLine("\n--- System: Tracking Order ---");
            int searchID = 101;

            Order[] mockDatabase = { order1 };
            int resultIndex = AlgorithmHelper.BinarySearchOrders(mockDatabase, mockDatabase.Length, searchID);

            if (resultIndex != -1)
            {
                Console.WriteLine($"Order {searchID} found! Status: {mockDatabase[resultIndex].Status}");
            }
            else
            {
                Console.WriteLine($"Order {searchID} not found in system.");
            }

            Console.WriteLine("\nTask finished. Press any key to close.");
            Console.ReadKey();
        }
    }
}