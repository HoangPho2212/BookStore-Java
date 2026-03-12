using BookStore;
using System;
using System.Collections.Generic;

namespace Bookstore
{
    class Program
    {
        static void Main(string[] args)
        {
            MyOrderQueue pendingQueue = new MyOrderQueue(); // FIFO for new orders
            MyStack historyStack = new MyStack();           // LIFO for undo/history
            List<Order> processedOrders = new List<Order>();// Database for searching
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- GREENWICH ONLINE BOOKSTORE ---");
                Console.WriteLine("1. Place New Order (Enqueue)");
                Console.WriteLine("2. Confirm Availability & Sort (Process)");
                Console.WriteLine("3. Undo Last Process (Pop Stack)");
                Console.WriteLine("4. Track Order Status (Binary Search)");
                Console.WriteLine("5. Exit");
                Console.Write("Select: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Order ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Customer Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Shipping Address: ");
                        string address = Console.ReadLine();

                        Order o = new Order { OrderID = id, CustomerName = name, ShippingAddress = address };

                        Console.Write("How many books in this order? ");
                        int bookCount = int.Parse(Console.ReadLine());
                        for (int i = 0; i < bookCount; i++)
                        {
                            Console.WriteLine($"\nBook {i + 1}:");
                            Console.Write("Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Quantity: ");
                            int qty = int.Parse(Console.ReadLine());
                            o.BookList.Add(new Book { Title = title, Author = author, Quantity = qty });
                        }

                        pendingQueue.Enqueue(o);
                        Console.WriteLine(">>> Success: Full order details collected and stored in Queue.");
                        break;

                    case "2":
                        Order toProc = pendingQueue.Dequeue();
                        if (toProc != null)
                        {
                            Console.WriteLine("Sort books by (1) Title or (2) Author?");
                            string sortChoice = Console.ReadLine();
                            string sortBy = (sortChoice == "2") ? "author" : "title";

                            AlgorithmHelper.QuickSortBooks(toProc.BookList, 0, toProc.BookList.Count - 1, sortBy);
                            toProc.Status = $"Sorted by {sortBy} & Confirmed";

                            historyStack.Push(toProc); 
                            processedOrders.Add(toProc);
                            Console.WriteLine($">>> Processed Order {toProc.OrderID}. Books sorted by {sortBy}.");
                        }
                        else
                        {
                            Console.WriteLine(">>> No orders in queue.");
                        }
                        break;

                    case "3":
                        Order undone = historyStack.Pop();
                        if (undone != null)
                        {
                            undone.Status = "Pending (Undone)";
                            pendingQueue.Enqueue(undone);
                            processedOrders.Remove(undone);
                            Console.WriteLine($">>> Reverted Order {undone.OrderID} back to Queue.");
                        }
                        else
                        {
                            Console.WriteLine(">>> No history to undo.");
                        }
                        break;

                    case "4":
                        if (processedOrders.Count == 0)
                        {
                            Console.WriteLine(">>> No processed orders to search.");
                            break;
                        }
                        Console.Write("Enter ID to track: ");
                        int searchId = int.Parse(Console.ReadLine());

                        // Use QuickSort instead of List.Sort for consistency with custom DS requirements
                        AlgorithmHelper.QuickSortOrders(processedOrders, 0, processedOrders.Count - 1);
                        Order[] searchArr = processedOrders.ToArray();

                        int result = AlgorithmHelper.BinarySearchOrders(searchArr, searchArr.Length, searchId);
                        if (result != -1) Console.WriteLine($">>> Status: {searchArr[result].Status}");
                        else Console.WriteLine(">>> Order not found.");
                        break;

                    case "5":
                        running = false;
                        break;
                }
            }
        }
    }
}
