using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    class SinglyLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }
            public Node NextNode { get; set; }

            //This constructor is called when the list is empty and i will demonstrate how it is called in the Add method
            public Node(T element)
            {
                Element = element;
                this.NextNode = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public SinglyLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public void AddFirst(T item)
        {
            Node newHeadNode = new Node(item);
           
            //I set the new node next pointer to point at the current head 
            newHeadNode.NextNode = this.head;
            //The new node now becomes the new head with the below definitiom
            this.head = newHeadNode;
            
            //If the list has only a single node, then i assign the tail to that single node
            if (this.tail == null)
            {
                this.tail = this.head;
            }

            this.count++;
        }

        public void AddLast(T item)
        {
            Node node = this.head;
            //What i am trying to do here is to get to the tail of the list, so i go through the list starting from the head
            while (node.NextNode != null)
            {
                node = node.NextNode;
            }
            

            Node newNode = new Node(item);
            //Assign the current tail node pointer to the new node 
            node.NextNode = newNode;
            //The new node becomes the tail
            this.tail = newNode;
            
            this.count++;
        }

        public void AddAtPosition(int position, T element)
        {
            Node currentNode = this.head;
            Node previousNode = null;
            Node newNode = new Node(element);
            int index = 1;

            //Checks if the position i want to add to is a valid position
            if (position < index || position > this.Count)
            {
                throw new IndexOutOfRangeException($"{position} is an invalid position");
            }

            //This allows me to traverse the list until i get to the the position i want to add to
            while (position != index)
            {
                previousNode = currentNode;
                currentNode = currentNode.NextNode;

                index++;
            }

            //Incase the position i want to add to is the head node, then i new element becomes the head, and make sure the new head pointer 
            //points to the previous head
            if (currentNode == this.head)
            {
                AddFirst(element);
                return;
            }

            //Incase i am adding the newNode at a position anywhere but the head, then i am updating the previous node pointer to point to the new
            //node and the new node pointer to point to the element that was previously at that position
            else
            {
                previousNode.NextNode = newNode;
                newNode.NextNode = currentNode;
            }
            this.count++;
        }

        public T RemoveFirst()
        {
            //Checks if the list is empty
            if (this.head == null)
            {
                throw new InvalidOperationException("You can not remove from an empty list");
            }

            Node nodeToRemove = this.head;
            //Incase we have just have a single node in the list, then this make sure i set the list to become empty
            if (nodeToRemove.NextNode == null)
            {
                this.head = null;
                this.tail = null;
                this.count = 0;
                return nodeToRemove.Element;
            }
            
            //if we have more than a single node in the list, then i set the head to the current head's succeeding node 
            this.head = nodeToRemove.NextNode;
            this.count--;
            return nodeToRemove.Element;
        }

        public T RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("You can not remove from an empty list");
            }

            //Incase we have just have a single node in the list, then this make sure i set the list to become empty
            if (this.head.NextNode == null)
            {
                Node node = this.head;
                this.head = null;
                this.tail = null;
                this.count = 0;
                return node.Element;
            }

            Node currentNode = this.head;
            Node previousNode = null;

            //This allows me to traverse the list starting from the head to the tail
            while (currentNode.NextNode != null)
            {
                previousNode = currentNode;
                currentNode = currentNode.NextNode;
            }

            //This allows me to make sure that the previous node pointer does not point to the current tail
            previousNode.NextNode = null;
            
            //The previous node becomes the tail
            this.tail = previousNode;
            this.count--;
            return currentNode.Element;
        }

        public T RemoveAtPosition(int index)
        {
            //Checks if the position i want to remove from is valid
            if (index > this.count || index < 1)
            {
                throw new ArgumentOutOfRangeException($"The position {index} does not exist");
            }

            //Checks if the position i want to remove from is the head 
            if (index == 1)
            {
               return RemoveFirst();
            }

            Node currentNode = this.head;
            Node previousNode = null;
            int position = 1;

            //This allows to traverse the list in order to get the position i want to remove from
            while (position != index)
            {
                previousNode = currentNode;
                currentNode = currentNode.NextNode;
            }

            //Incase i am removing the newNode at a position anywhere but the head, then i am updating the previous node pointer to point to the
            //succeeding node of the node i want to remove
            previousNode.NextNode = currentNode.NextNode;
            this.count--;
            return currentNode.Element;
        }

        public int IndexOf(T item)
        {
            int index = 0;
            Node currentNode = this.head;

            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    return index;
                }
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

        public T this[int index]
        {
            get
            {
                if (index >= this.count || index < 1)
                {
                    throw new ArgumentOutOfRangeException($"Index{index} is an invalid index");
                }
                Node currentNode = this.head;
                
                //This loop allows me to get the specified element through its index
                for (int i = 1; i <= index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                return currentNode.Element;
            }
            set
            {
                if (index >= this.count || index < 0)
                {
                    throw new ArgumentOutOfRangeException($"Index{index} is an invalid index");
                }

                Node currentNode = this.head;
                //This loop allows me to get the specified element through its index
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                currentNode.Element = value;
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public override string ToString()
        {
            Node node = this.head;
            if (node == null)
            {
                return "Linkedlist is empty!";
            }

            String linkedList = "";
            while (true)
            {
                if (node.NextNode == null)
                {
                    linkedList += "[" + node.Element + "]";
                    break;
                }

                linkedList += "[" + node.Element + "] => ";
                node = node.NextNode;
            }
            return linkedList;
        }

        static void Main(string[] args)
        {
            SinglyLinkedList<int> numbers = new SinglyLinkedList<int>();
            numbers.AddFirst(30);
            numbers.AddFirst(56);
            numbers.AddFirst(20);
            numbers.AddLast(1);
            numbers.AddLast(37);
            numbers.AddAtPosition(1, 77);
            numbers.RemoveAtPosition(1);
            numbers.RemoveLast();
            numbers.RemoveFirst();

            Console.WriteLine(numbers);//[56] => [30] => [1]
        }
    }
}
