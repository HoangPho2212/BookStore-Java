using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookStore
{
    public class AlgorithmHelper {
        public static int BinarySearchOrders(Order[] orders, int count, int targetID)
        {
            int low = 0;
            int high = count - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (orders[mid].OrderID == targetID)
                    return mid;

                if (orders[mid].OrderID < targetID)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return -1; // Not found
        }

        public static int ManualStringCompare(string s1, string s2)
        {
            if (s1 == null && s2 == null) return 0;
            if (s1 == null) return -1;
            if (s2 == null) return 1;

            int len1 = s1.Length;
            int len2 = s2.Length;
            int minLen = len1 < len2 ? len1 : len2;

            for (int i = 0; i < minLen; i++)
            {
                if (s1[i] < s2[i]) return -1;
                if (s1[i] > s2[i]) return 1;
            }

            if (len1 < len2) return -1;
            if (len1 > len2) return 1;
            return 0;
        }

        public static void QuickSortBooks(MyArrayList<Book> books, int low, int high, string sortBy)
        {
            if (low < high)
            {
                int pivotIndex = PartitionBooks(books, low, high, sortBy);
                QuickSortBooks(books, low, pivotIndex - 1, sortBy);
                QuickSortBooks(books, pivotIndex + 1, high, sortBy);
            }
        }

        private static int PartitionBooks(MyArrayList<Book> books, int low, int high, string sortBy)
        {
            Book pivot = books[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                bool compare = false;
                // Manual check instead of ToLower()
                if (sortBy == "author" || sortBy == "Author")
                {
                    compare = ManualStringCompare(books[j].Author, pivot.Author) < 0;
                }
                else // Default to Title
                {
                    compare = ManualStringCompare(books[j].Title, pivot.Title) < 0;
                }

                if (compare)
                {
                    i++;
                    Book temp = books[i];
                    books[i] = books[j];
                    books[j] = temp;
                }
            }
            Book temp2 = books[i + 1];
            books[i + 1] = books[high];
            books[high] = temp2;
            return i + 1;
        }

        public static void QuickSortOrders(MyArrayList<Order> orders, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = PartitionOrders(orders, low, high);
                QuickSortOrders(orders, low, pivotIndex - 1);
                QuickSortOrders(orders, pivotIndex + 1, high);
            }
        }

        private static int PartitionOrders(MyArrayList<Order> orders, int low, int high)
        {
            int pivot = orders[high].OrderID;
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (orders[j].OrderID < pivot)
                {
                    i++;
                    Order temp = orders[i];
                    orders[i] = orders[j];
                    orders[j] = temp;
                }
            }
            Order temp2 = orders[i + 1];
            orders[i + 1] = orders[high];
            orders[high] = temp2;
            return i + 1;
        }
    }
}
