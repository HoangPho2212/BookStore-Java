using System;
using System.Collections.Generic;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            MyOrderQueue pendingQueue = new MyOrderQueue(); // FIFO for new orders
            MyStack historyStack = new MyStack();           // LIFO for undo/history
            MyArrayList<Order> processedOrders = new MyArrayList<Order>(); // Custom Dynamic Array
            MyArrayList<Book> inventory = new MyArrayList<Book>();
            
            InitializeInventory(inventory);
            
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- GREENWICH ONLINE BOOKSTORE ---");
                Console.WriteLine("1. Place New Order (Enqueue)");
                Console.WriteLine("2. Confirm Availability & Sort (Process)");
                Console.WriteLine("3. Undo Last Process (Pop Stack)");
                Console.WriteLine("4. Track Order Status (Binary Search)");
                Console.WriteLine("5. Manage Inventory (Add/Search)");
                Console.WriteLine("6. Exit");
                Console.Write("Select: ");
                string choice = Console.ReadLine();
                if (choice == null) break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Order ID: ");
                        int id = int.Parse(Console.ReadLine());

                        // Validation: Check for duplicates
                        if (pendingQueue.Exists(id) || processedOrders.Exists(x => x.OrderID == id))
                        {
                            Console.WriteLine($">>> Error: Order ID {id} already exists in the system.");
                            break;
                        }

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
                            
                            // Search inventory for availability
                            Book invBook = inventory.Find(b => AlgorithmHelper.ManualStringCompare(b.Title, title) == 0);
                            if (invBook == null)
                            {
                                Console.WriteLine($">>> Warning: '{title}' is not in inventory. Staff check required.");
                            }
                            else if (invBook.Quantity <= 0)
                            {
                                Console.WriteLine($">>> Warning: '{title}' is out of stock.");
                            }

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
                            processedOrders.Remove(undone); // Uses custom MyArrayList.Remove
                            Console.WriteLine($">>> Reverted Order {undone.OrderID} back to Queue.");
                        }
                        else
                        {
                            Console.WriteLine(">>> No history to undo.");
                        }
                        break;

                    case "4":
                        if (processedOrders.Count == 0 && pendingQueue.Count == 0)
                        {
                            Console.WriteLine(">>> No orders in the system to track.");
                            break;
                        }
                        Console.Write("Enter ID to track: ");
                        int searchId = int.Parse(Console.ReadLine());

                        // 1. Check pendingQueue first (Linear Search since it's a Queue)
                        Order foundInPending = pendingQueue.Find(searchId);
                        if (foundInPending != null)
                        {
                            foundInPending.PrintOrderDetails();
                            break;
                        }

                        // 2. Check processedOrders (Binary Search)
                        if (processedOrders.Count > 0)
                        {
                            AlgorithmHelper.QuickSortOrders(processedOrders, 0, processedOrders.Count - 1);
                            Order[] searchArr = processedOrders.ToArray();
                            int result = AlgorithmHelper.BinarySearchOrders(searchArr, searchArr.Length, searchId);
                            if (result != -1)
                            {
                                searchArr[result].PrintOrderDetails();
                                break;
                            }
                        }

                        Console.WriteLine(">>> Order not found.");
                        break;

                    case "5":
                        Console.WriteLine("\n--- INVENTORY MANAGEMENT ---");
                        Console.WriteLine("1. Add/Update Book Stock");
                        Console.WriteLine("2. Search Book Availability");
                        Console.WriteLine("3. View All Inventory");
                        Console.WriteLine("4. Back");
                        Console.Write("Select: ");
                        string invChoice = Console.ReadLine();
                        if (invChoice == "1")
                        {
                            Console.Write("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Enter Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Enter Quantity: ");
                            int qty = int.Parse(Console.ReadLine());

                            Book existing = inventory.Find(b => 
                                AlgorithmHelper.ManualStringCompare(b.Title, title) == 0 && 
                                AlgorithmHelper.ManualStringCompare(b.Author, author) == 0);

                            if (existing != null)
                            {
                                existing.Quantity += qty;
                                Console.WriteLine($">>> Updated: {title} now has {existing.Quantity} copies.");
                            }
                            else
                            {
                                inventory.Add(new Book { Title = title, Author = author, Quantity = qty });
                                Console.WriteLine($">>> Added: New book '{title}' added to inventory.");
                            }
                        }
                        else if (invChoice == "2")
                        {
                            Console.Write("Enter Title to search: ");
                            string searchTitle = Console.ReadLine();
                            Book found = inventory.Find(b => AlgorithmHelper.ManualStringCompare(b.Title, searchTitle) == 0);
                            if (found != null)
                            {
                                Console.WriteLine($">>> Found: '{found.Title}' by {found.Author} - Stock: {found.Quantity}");
                            }
                            else
                            {
                                Console.WriteLine(">>> Book not found in inventory.");
                            }
                        }
                        else if (invChoice == "3")
                        {
                            Console.WriteLine("\n--- CURRENT INVENTORY ---");
                            for (int i = 0; i < inventory.Count; i++)
                            {
                                Console.WriteLine($"- {inventory[i].Title} by {inventory[i].Author} (Stock: {inventory[i].Quantity})");
                            }
                        }
                        break;

                    case "6":
                        running = false;
                        break;
                }
            }
        }

        static void InitializeInventory(MyArrayList<Book> inventory)
        {
            inventory.Add(new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Quantity = 10 });
            inventory.Add(new Book { Title = "1984", Author = "George Orwell", Quantity = 15 });
            inventory.Add(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Quantity = 8 });
            inventory.Add(new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Quantity = 12 });
            inventory.Add(new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Quantity = 20 });
            inventory.Add(new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Quantity = 5 });
            inventory.Add(new Book { Title = "Moby-Dick", Author = "Herman Melville", Quantity = 7 });
            inventory.Add(new Book { Title = "War and Peace", Author = "Leo Tolstoy", Quantity = 3 });
            inventory.Add(new Book { Title = "Ulysses", Author = "James Joyce", Quantity = 4 });
            inventory.Add(new Book { Title = "The Odyssey", Author = "Homer", Quantity = 6 });
        }
    }
}
