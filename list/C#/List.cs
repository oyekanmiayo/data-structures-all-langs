using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    // @author : Babatunde Ibukun

    class List<T> : IEnumerable<T>
    {
        private T[] array;
        private int count;

        public List()
        {
            this.array = new T[16];
            this.count = 0;
        }

        public List(int capacity)
        {
            this.array = new T[capacity];
            this.count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public void ExpandArrayIfFull()
        {
            if (this.Count + 1 > this.array.Length)
            {
                int newLength = this.array.Length * 2; 
                T[] expandedArray = new T[newLength];

                //What happens here is that, i am copying the old array(full one) into a new one that has an increased size, the last argument 
                //specifies the amount of the old array i want to copy into the new, and i specified the whole length
                Array.Copy(this.array, expandedArray, this.Count);
                this.array = expandedArray;
            }
        }

        public void Add(T item)
        {
            ExpandArrayIfFull();
            this.array[this.Count] = item;
            this.count++;
        }

        public void Add(int index, T item)
        {
            if (index > this.Count || index < 0)
            {
                throw new IndexOutOfRangeException($"The index {index} you specified is not within the range of the list, please specify " +
                    $"another index");
            }
            ExpandArrayIfFull();
            //What i am trying to do here is, i am moving the original element at the specified position where we want to put our new element
            //one place to the right
            Array.Copy(this.array, index, this.array, index + 1, this.count - index);
            this.array[index] = item;
            this.count++;
        }

        public void Clear()
        {
            this.array = new T[16];
            this.count = 0;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.array.Length; i++)
            {
                if (item.Equals(this.array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Contains(T item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }

        public T Get(int index)
        {
            if (index > this.count || index < 0)
            {
                throw new IndexOutOfRangeException($"{index} is invalid");
            }
           return this.array[index];
        }

        public T this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0 )
                {
                    throw new ArgumentOutOfRangeException($"Index {index} is invalid");
                }
                return this.array[index];
            }
            set
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException($"Index {index} is invalid");
                }
                this.array[index] = value;
            }
        }

        public T Remove(int index)
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException($"Index {index} is invalid");
            }
            
            T item = this.array[index];

            //What i am trying do here is, i am moving every element on right side of the specified element we want to remove one place backward
            //This allows me to overwrite the element i want to remove
            Array.Copy(this.array, index + 1, this.array, index, this.count - index - 1);
            //I assign a value (default of whatever type we working with) to the empty index after moving elements within the array
            this.array[this.count - 1] = default(T);
            this.count--;
            return item;
        }

        public void Sort()
        {
            Array.Sort(this.array);
        }

        public List<T> Slice(int start, int end)
        {
            if (start >= this.count || start < 0)
            {
                throw new IndexOutOfRangeException($"{start} is an invalid index");
            }
            if (end >= this.count || end < 0)
            {
                throw new IndexOutOfRangeException($"{end} is an invalid index");
            }
            
            List<T> slicedItems = new List<T>();
             
            for (int i = start; i <= end; i++)
            {
                slicedItems.Add(this.array[i]);
            }
            return slicedItems;
        }

        public void Reverse()
        {
            int start = 0;
            int end = this.count - 1;
            
            for (int i = 0; i < this.count / 2; i++)
            {
                T temp = array[start];
                array[start] = array[end];
                array[end] = temp;
                start++;
                end--;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in this.array)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            numbers.Add(34);
            numbers.Add(4);
            numbers.Add(20);
            numbers.Add(2, 28);
            numbers.Add(3, 30);

            Console.WriteLine(numbers.Get(4)); //20
            Console.WriteLine(numbers.Remove(2));
            List<int> sliced = numbers.Slice(1, 3); // 4, 28, 30
            numbers.Sort(); // 4, 20, 28, 30, 34
            numbers.Reverse(); //20, 30, 28, 4, 34
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
