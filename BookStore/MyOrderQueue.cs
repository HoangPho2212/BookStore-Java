using BookStore;
using System;

namespace Bookstore
{
    public class MyOrderQueue
    {
        private Order[] _orders;
        private int _front;
        private int _rear;
        private int _maxSize;
        private int _count;

        public MyOrderQueue(int size)
        {
            _maxSize = size;
            _orders = new Order[_maxSize];
            _front = 0;
            _rear = -1;
            _count = 0;
        }

        
        public void Enqueue(Order item)
        {
            if (_count == _maxSize)
            {
                Console.WriteLine("Queue is full!");
                return;
            }
            _rear = (_rear + 1) % _maxSize;
            _orders[_rear] = item;
            _count++;
        }


        public Order Dequeue()
        {
            if (_count == 0) return null;

            Order item = _orders[_front];
            _orders[_front] = null; // Clear reference
            _front = (_front + 1) % _maxSize; // Circular logic
            _count--;
            return item;
        }

        public int Count => _count;
    }
}