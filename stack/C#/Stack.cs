using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    // @author : Babatunde Ibukun
    class Stack<T>
    {
        private int count;
        private int top;
        private T[] stack;
        private int capacity;
        
        public Stack()
        {
            count = 0;
            top = -1;
            capacity = 16;
            stack = new T[capacity];
        }

        public void Push(T element)
        {
            //Before pushing an element into the stack, i check if the stack is not full, if it is not, i go on to add the element
            if (count < capacity)
            {
                stack[count++] = element;
                //The top variable allows me to keep track of the last element i added into the stack
                top++;
            }
            
            //Incase the stack is full, i create a new array with a bigger size and i copy the element within the underlying array into the bigger array and i the go on to assign the underlying array to the new expanded array
            else
            {
                capacity *= 2;
                T[] expandedStack = new T[capacity];
                for (int i = 0; i < stack.Length; i++)
                {
                    expandedStack[i] = stack[i];
                }
                stack = expandedStack;
                stack[count++] = element;
                top++;
            }
        }

        public T Peek()
        {
            //THe top variable allows me to be able to check which element is at the top of the stack
            if (count > 0)
            {
                return stack[top];
            }
            else
            {
                throw new InvalidOperationException("The stack is empty");
            }
        }

        //Before i pop the element at the top of the stack, i decrease the count and top variables by one, this allows me to be able to stop tracking the element that i want to pop. Then in order to have access to that elmenent, the one i want to pop, i added one to the top element 
        public T Pop()
        {
            if (count > 0)
            {
                count--;
                top--;
                return stack[top + 1];
            }
            else
            {
                throw new InvalidOperationException("The stack is empty");
            }
        }

        public int Count
        {
            get { return this.count; }
        }

        public int Capacity
        {
            get { return capacity; }
        }

        static void Main(string[] args)
        {
            Stack<string> text = new Stack<string>();
            text.Push("Ayo");
            text.Push("Timi");
            text.Push("Ibukun");
            text.Push("Olumide");

            Console.WriteLine(text.Pop());//Olumide
            Console.WriteLine(text.Peek());//Ibukun
        }
    }
}
