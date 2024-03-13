using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


/*
AUTHOR: Kehinde Ojewale
*/

class Program
{
    static void Main(string[] args)
    {
        //Tests
        DoublyLinkedList<int> Test = new DoublyLinkedList<int>();
        Test.AddFirst(4);// 4
        Test.AddLast(8);//4 <-> 8
        Test.AddFirst(45);//45 <-> 4 <-> 8
        Test.AddAtPosition(2,67);//45 <-> 4 <-> 67 <-> 8
        //prints the current list
        int i=1;
        foreach (int item in Test)
        {   
            if(i<Test.Count()) {
                Console.Write("{0} <-> ",item);
                i++;
                }//45 <-> 4 <-> 67 <-> 8
            else Console.WriteLine("{0}",item);
        }
        //end print
        Test.Reverse();//8 <-> 67 <-> 4 <-> 45
        //prints the reversed list
        i=1;
        foreach (int item in Test)
        {   
            if(i<Test.Count()) {
                Console.Write("{0} <-> ",item);
                i++;
                }//8 <-> 67 <-> 4 <-> 45
            else Console.WriteLine("{0}",item);
        }
        //end print
        Console.WriteLine(Test.Peek());//8
        Test.RemoveAtPosition(2);//8 <-> 67 <-> 45
        //prints the current list after removal
        i=1;
        foreach (int item in Test)
        {   
            if(i<Test.Count()) {
                Console.Write("{0} <-> ",item);
                i++;
                }//8 <-> 67 <->  45
            else Console.WriteLine("{0}",item);
        }
        //end print
        Test.RemoveFirst();//67 <-> 45
        Test.RemoveLast();//67
        Console.WriteLine(Test.Count());//1
    }
}
class DoublyLinkedList<A>:IEnumerable<A>{
    Node<A> head;
    Node<A> tail;
    int size;
    internal class Node<B>{
        internal B item;
        internal Node<B> next;
        internal Node<B> previous;
        public Node(B item){
            this.item=item;
            next=null;
            previous=null;
        }
    }
    public DoublyLinkedList(){
        head=null;
        tail=null;
        size=0;
    }
    
    public void AddFirst(A item){
        //This method adds a new node to the beginning of the list, making it the new node
        Node<A> newHead = new Node<A>(item);
        newHead.next=head;
        if(head!=null){
            head.previous=newHead;
        }
        head=newHead; 
        if(tail == null){
            tail=newHead;
        }
        size++;
    }

    public void AddLast(A item){
        if(size==0){
            AddFirst(item);
            return;
        }
        Node<A> newTail = new Node<A>(item);
        tail.next=newTail;
        newTail.previous=tail;
        tail=newTail;
        size++; 
    }

    public void AddAtPosition(int index, A item){
        if(index<0 || index>= size){
            throw new IndexOutOfRangeException(String.Format("Index {0:d} is out of the bounds of the list", index));
        }
        if(index==0){
        //adds to the beginning of the list if the insertion index is 0
        AddFirst(item);
        return;
        }

        Node<A> newNode= new Node<A>(item);
        Node<A> currentNode=null;

        //if the index is closer to the tail, traverse the list from the tail until the node just before the insertion index(parameter:index)
        if(index>size/2){
            currentNode=tail;//sets the tail as the current node
            while(index<size-1){
                //traverses the list from the tail
                currentNode=currentNode.previous;//updates the current node
                index++;//increments index as the list is traversed
            }
        }
        
        //if the index is closer to the head, traverse the list from the head until the node just before the insertion index(parameter:index)
        else if(index<=size/2){
            int i=0;
            currentNode=head;//sets the head as the current node
            while(i<index){
                currentNode=currentNode.next;//updates the current node
                i++;//increments i, the traversal step
            }
        }
        currentNode.previous.next=newNode;
        newNode.previous=currentNode.previous;
        newNode.next=currentNode;
        currentNode.previous=newNode;
        size++;//increases the size of the list by 1
    }

    public A RemoveFirst(){
        if(head==null){
            throw new Exception("No such element");
        }
        Node<A> removedHead;
        if(size==1){
            //if the list has just one element,set the new head as null
            removedHead=head;
            head=tail=null;
            size--;
            return removedHead.item;
        }
        removedHead=head;//sets the head to be removed as the current head
        head=head.next;//sets the new head as the next node after the removed head
        head.previous=null;//sets the node before the new head as null
        size--;
        return removedHead.item;
    }
    public A RemoveLast(){
        if(tail==null){
            //throw an exception if the list is empty
            throw new Exception("No such element");
        }
        Node<A> removedTail;
        if(size==1){
            //if the list has just one element, set the new tail as null
            removedTail=tail;
            head=tail=null;
            size--;
            return removedTail.item;
        }
        removedTail=tail;//sets the tail to be removed as the current tail
        tail=removedTail.previous;//sets the new tail as the node before the removed tail
        tail.next=null;//sets the node after the new tail as null
        size--;
        return removedTail.item;
    }
    public A RemoveAtPosition(int index){
        //removes a node from a given index
        if(index<0 || index>= size){
            throw new Exception("No such element");
        }
        if(index==0){
        //removes the head if the removal index is 0
        return RemoveFirst();
        }

        if(index==size-1){
        //removes the tail if the removal index is the last index of the list(index of size -1)
        return RemoveLast();
        }

        Node<A> currentNode=null;

        //if the index is closer to the tail, traverse the list from the tail until the node just before the insertion index(parameter:index)
        if(index>size/2){
            currentNode=tail;//sets the tail as the current node
            while(index<size-1){
                //traverses the list from the tail
                currentNode=currentNode.previous;//updates the current node
                index++;//increments index as the list is traversed
            }
        }
        
        //if the index is closer to the head, traverse the list from the head until the node just before the insertion index(parameter:index)
        else if(index<=size/2){
            int i=0;
            currentNode=head;//sets the head as the current node
            while(i<index){
                currentNode=currentNode.next;//updates the current node
                i++;//increments i, the traversal step
            }
        }

        currentNode.previous.next=currentNode.next;//points the node before the removed node to the node after the removed node
        currentNode.next.previous=currentNode.previous;//points the node after the removed node back to the node before the removed node
        size--;//decreases the size of the list by 1
        return currentNode.item;
    }
    public int Count(){
        return size;
    }

    public A Peek(){
        //The peek method chcks if the list is empty. If it isn't, it returns the head.
        if(size==0){
            throw new Exception("List is empty");
        }
        return head.item;
    }

    public void Reverse(){
        Node<A> node=tail;
        Node<A> currentHead=head;
        head=tail;//sets the new head as the current tail
        tail=currentHead;//sets the new tail as the current head//
        Node<A> prevNode;
        Node<A> nextNode;
        int index = 0;
        while (index<size){
            // Console.WriteLine(node.item);
            prevNode=node.previous;
            nextNode=node.next;
            node.previous=nextNode;
            node.next=prevNode;
            node=prevNode;
            // Console.WriteLine(node.item);
            index++;
        }
    }

    //makes the list iterable
    public IEnumerator<A> GetEnumerator()
    {  
        int index=0;
        Node<A> node= head;
        while (index<size){
            yield return node.item;
            node=node.next;
            index++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}