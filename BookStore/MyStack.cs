
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class MyStack
    {
        private Node _top;
        private int _count;

        public void Push(Order item)
        {
            Node newNode = new Node(item);
            newNode.Next = _top;
            _top = newNode;
            _count++;
        }

        public Order Pop()
        {
            if (_top == null) return null;
            Order item = _top.Data;
            _top = _top.Next;
            _count--;
            return item;
        }

        public int Count => _count;
    }
}