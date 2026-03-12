using BookStore;
using System;

namespace Bookstore
{
    // L02_Node concept: Creating a wrapper for our data
    public class Node
    {
        public Order Data;
        public Node Next;
        public Node(Order order) { Data = order; Next = null; }
    }

    public class MyOrderQueue
    {
        private Node _front;
        private Node _rear;
        private int _count;

        public void Enqueue(Order item)
        {
            Node newNode = new Node(item);
            if (_rear == null)
            {
                _front = _rear = newNode;
            }
            else
            {
                _rear.Next = newNode;
                _rear = newNode;
            }
            _count++;
        }

        public Order Dequeue()
        {
            if (_front == null) return null;
            Order data = _front.Data;
            _front = _front.Next;
            if (_front == null) _rear = null;
            _count--;
            return data;
        }
    }
}