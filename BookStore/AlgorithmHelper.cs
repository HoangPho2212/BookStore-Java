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

        public static void QuickSortBooks(List<Book> books, int low, int high, string sortBy)
        {
            if (low < high)
            {
                int pivotIndex = PartitionBooks(books, low, high, sortBy);
                QuickSortBooks(books, low, pivotIndex - 1, sortBy);
                QuickSortBooks(books, pivotIndex + 1, high, sortBy);
            }
        }

        private static int PartitionBooks(List<Book> books, int low, int high, string sortBy)
        {
            Book pivot = books[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                bool compare = false;
                if (sortBy.ToLower() == "author")
                {
                    compare = string.Compare(books[j].Author, pivot.Author) < 0;
                }
                else // Default to Title
                {
                    compare = string.Compare(books[j].Title, pivot.Title) < 0;
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

        public static void QuickSortOrders(List<Order> orders, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = PartitionOrders(orders, low, high);
                QuickSortOrders(orders, low, pivotIndex - 1);
                QuickSortOrders(orders, pivotIndex + 1, high);
            }
        }

        private static int PartitionOrders(List<Order> orders, int low, int high)
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
