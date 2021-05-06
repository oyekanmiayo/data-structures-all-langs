using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    // @author : Babatunde Ibukun
    //@notes This implementation uses a Doubly Linked List as the underlying data structure

    public class Queue<T>
    {
        private Node front;
        private Node rear;
        private int count;

        private class Node
        {
            public T Element { get; set; }
            public Node NextNode { get; set; }

            public Node(T element)
            {
                Element = element;
                NextNode = null;
            }

            public Node(T element, Node previousNode)
            {
                Element = element;
                previousNode.NextNode = this;
            }
        }

        public Queue()
        {
            front = null;
            rear = null;
            count = 0;
        }


        public void Enqueue(T item)
        {
            //Before adding the element to the queue, i check if it is empty, if it is empty, i set the element to the front of the queue, i also
            //make the element the rear of the queue, since we have only a single element within the queue
            if (this.front == null)
            {
                this.front = new Node(item);
                this.rear = this.front;
            }

            //If the queue is not empty then i add the new item to the end of the queue 
            else
            {
                //I create the new node i want to add and i pass the item i want to add and the current rear as parameters to the node's constructor
                Node newNode = new Node(item, this.rear);
                this.rear = newNode;
            }
            count++;
        }

        public T Dequeue()
        {
            //Checks if the queue i want to remove from is empty or not
            if (this.count <= 0)
            {
                throw new InvalidOperationException("The Queue is empty");
            }
            
            //I get the front i want to dequeue and i put it into a variable so that i can return it 
            Node originalFront = this.front;

            //The front's succeeding node is set as the new front
            this.front = this.front.NextNode;
            
            //Checks if the new front is null, if it is, i make the queue empty
            if (this.front == null)
            {
                this.rear = null;
            }
            count--;
            return originalFront.Element;
        }

        public T Peek()
        {
            if (this.count <= 0)
            {
                throw new InvalidOperationException("The Queue is empty");
            }
            return this.front.Element;
        }

        private int IndexOf(T item)
        {
            int index = 0;
            Node currentNode = this.front;
            Node previousNode = null;

            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    return index;
                }
                previousNode = currentNode;
                currentNode = currentNode.NextNode;
                index++;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        static void Main(string[] args)
        {
            Queue<int> numbers = new Queue<int>();
            numbers.Enqueue(10);
            numbers.Enqueue(8);
            numbers.Enqueue(12);
            numbers.Enqueue(3);
            Console.WriteLine(numbers.Dequeue());//10;
            Console.WriteLine(numbers.Peek());//8;
        }
    }
}
