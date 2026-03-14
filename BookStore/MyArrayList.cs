using System;

namespace BookStore
{
    public class MyArrayList<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 4;

        public MyArrayList()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        public int Count => _count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                Resize();
            }
            _items[_count++] = item;
        }

        public bool Exists(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i])) return true;
            }
            return false;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i])) return _items[i];
            }
            return default(T);
        }

        public void Remove(T item)
        {
            int index = -1;
            for (int i = 0; i < _count; i++)
            {
                // Manual reference comparison for objects
                if (object.ReferenceEquals(_items[i], item))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1) RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
            
            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
            _items[_count] = default(T); 
        }

        private void Resize()
        {
            T[] newArray = new T[_items.Length * 2];

            for (int i = 0; i < _items.Length; i++)
            {
                newArray[i] = _items[i];
            }
            _items = newArray;
        }

        public T[] ToArray()
        {
            T[] result = new T[_count];

            for (int i = 0; i < _count; i++)
            {
                result[i] = _items[i];
            }
            return result;
        }
    }
}
