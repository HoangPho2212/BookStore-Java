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

        public static void QuickSortBooks(List<Book> books, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(books, low, high);
                QuickSortBooks(books, low, pivotIndex - 1);
                QuickSortBooks(books, pivotIndex + 1, high);
            }
        }
        private static int Partition(List<Book> books, int low, int high)
        {
            string pivot = books[high].Title;
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (string.Compare(books[j].Title, pivot) < 0)
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
    }
}
