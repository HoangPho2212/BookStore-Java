using System;

namespace BookStore
{
    public class TestRunner
    {
        private static int _pass = 0;
        private static int _fail = 0;

        public static void RunAllTests()
        {
            _pass = 0;
            _fail = 0;
            Console.WriteLine("\n--- STARTING SYSTEM DIAGNOSTICS ---");

            TestMyArrayList();
            TestMyOrderQueue();
            TestMyStack();
            TestAlgorithmHelper();

            Console.WriteLine($"\n--- DIAGNOSTICS COMPLETE ---");
            Console.WriteLine($"Passed: {_pass} | Failed: {_fail}");
            if (_fail == 0) Console.WriteLine("✅ SYSTEM HEALTHY");
            else Console.WriteLine("❌ SYSTEM ERRORS DETECTED");
        }

        private static void Assert(bool condition, string message)
        {
            if (condition)
            {
                Console.WriteLine($"  ✅ PASS: {message}");
                _pass++;
            }
            else
            {
                Console.WriteLine($"  ❌ FAIL: {message}");
                _fail++;
            }
        }

        private static void TestMyArrayList()
        {
            Console.WriteLine("\n[Testing MyArrayList]");
            MyArrayList<int> list = new MyArrayList<int>();
            
            list.Add(10);
            list.Add(20);
            Assert(list.Count == 2, "Count should be 2 after adding 2 items");
            Assert(list[0] == 10, "First item should be 10");

            // Test Resize
            list.Add(30); list.Add(40); list.Add(50);
            Assert(list.Count == 5, "Count should be 5 (triggering resize)");

            // Test RemoveAt
            list.RemoveAt(1); // Remove 20
            Assert(list.Count == 4, "Count should be 4 after RemoveAt");
            Assert(list[1] == 30, "Item at index 1 should now be 30");

            // Test ToArray
            int[] arr = list.ToArray();
            Assert(arr.Length == 4, "Array length should match count");
        }

        private static void TestMyOrderQueue()
        {
            Console.WriteLine("\n[Testing MyOrderQueue]");
            MyOrderQueue q = new MyOrderQueue();
            Order o1 = new Order { OrderID = 1 };
            Order o2 = new Order { OrderID = 2 };

            q.Enqueue(o1);
            q.Enqueue(o2);
            Assert(q.Count == 2, "Queue count should be 2");
            Assert(q.Exists(1), "Order 1 should exist");
            Assert(q.Find(2) == o2, "Find should return correct order object");

            Order deq = q.Dequeue();
            Assert(deq.OrderID == 1, "First dequeued should be Order 1 (FIFO)");
            Assert(q.Count == 1, "Count should be 1 after dequeue");
        }

        private static void TestMyStack()
        {
            Console.WriteLine("\n[Testing MyStack]");
            MyStack s = new MyStack();
            Order o1 = new Order { OrderID = 101 };
            Order o2 = new Order { OrderID = 102 };

            s.Push(o1);
            s.Push(o2);
            Assert(s.Count == 2, "Stack count should be 2");

            Order popped = s.Pop();
            Assert(popped.OrderID == 102, "First popped should be Order 102 (LIFO)");
            Assert(s.Count == 1, "Count should be 1 after pop");
        }

        private static void TestAlgorithmHelper()
        {
            Console.WriteLine("\n[Testing AlgorithmHelper]");

            // 1. ManualStringCompare
            Assert(AlgorithmHelper.ManualStringCompare("Apple", "Banana") < 0, "Apple < Banana");
            Assert(AlgorithmHelper.ManualStringCompare("Zebra", "Apple") > 0, "Zebra > Apple");
            Assert(AlgorithmHelper.ManualStringCompare("Test", "Test") == 0, "Equal strings return 0");

            // 2. BinarySearch
            Order[] orders = new Order[] { 
                new Order { OrderID = 10 }, 
                new Order { OrderID = 20 }, 
                new Order { OrderID = 30 } 
            };
            int index = AlgorithmHelper.BinarySearchOrders(orders, 3, 20);
            Assert(index == 1, "BinarySearch should find ID 20 at index 1");

            // 3. QuickSort (Books by Title)
            MyArrayList<Book> books = new MyArrayList<Book>();
            books.Add(new Book { Title = "C++" });
            books.Add(new Book { Title = "Algorithms" });
            books.Add(new Book { Title = "Basic" });

            AlgorithmHelper.QuickSortBooks(books, 0, 2, "title");
            Assert(AlgorithmHelper.ManualStringCompare(books[0].Title, "Algorithms") == 0, "Sorted first item should be Algorithms");
            Assert(AlgorithmHelper.ManualStringCompare(books[2].Title, "C++") == 0, "Sorted last item should be C++");
        }
    }
}
