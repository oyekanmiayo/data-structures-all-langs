/*
AUTHOR: Kehinde Ojewale
*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //Tests
        SinglyLinkedList<int> Test = new SinglyLinkedList<int>();
        Test.AddFirst(4);// 4
        Test.AddLast(8);//4 -> 8
        Test.AddFirst(45);//45 -> 4 -> 8
        Test.AddAtPosition(2,67);//45 -> 4 -> 67 -> 8
        //prints the current list
        int i=1;
        foreach (int item in Test)
        {   
            if(i<Test.Count()) {
                Console.Write("{0} -> ",item);
                i++;
                }//45 -> 4 -> 67 -> 8
            else Console.WriteLine("{0}",item);
        }
        //end of print
        Test.RemoveAtPosition(2);//45 -> 4 -> 8
        //prints the current list
        int i=1;
        foreach (int item in Test)
        {   
            if(i<Test.Count()) {
                Console.Write("{0} -> ",item);
                i++;
                }//45 -> 4 -> 8
            else Console.WriteLine("{0}",item);
        }
        //end of print
        Test.RemoveFirst();//4 -> 8
        Test.RemoveLast();//4
        Console.WriteLine(Test.Count());//1 
    }
}
class SinglyLinkedList<A>:IEnumerable<A>{
    Node<A> head;
    Node<A> tail;
    int size;
    internal class Node<B>{
        internal B item;
        internal Node<B> next;
        public Node(B item){
            this.item=item;
            next=null;
        }
    }
    public SinglyLinkedList(){
        head=null;
        tail=null;
        size=0;
    }
    
    public void AddFirst(A item){
        //This method adds a new node to the beginning of the list, making it the new node
        Node<A> newHead = new Node<A>(item);
        newHead.next=head;
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

        Node<A> currentNode=head;//sets the head as the current node
        Node<A> newNode= new Node<A>(item);
        Node<A> prevNode=null;
        int i=0;
        //traverse the list until the node just before the insertion index(parameter:index)
        while(i<index){
            prevNode= currentNode; //assigns the present current node as the new previous node
            currentNode=currentNode.next;//updates the current node
            i++;//increments i, the traversal step
        }
        prevNode.next=newNode;
        newNode.next=currentNode;
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
        removedHead=head;
        head=head.next;
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
        // Node<A> removedTail=Tail;
        // head=head.next;
        // size--;
        Node<A> currentNode= head;//set the head node as the current node
        //traverse the list until the node before the tail is reached 
        while(currentNode.next.next != null){
            currentNode=currentNode.next;
        }
        removedTail=currentNode.next;
        currentNode=tail;
        currentNode.next=null;
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

        Node<A> currentNode=head;//sets the head as the current node
        Node<A> prevNode=null;
        int i=0;
        //traverse the list until the node just before the removal index(parameter:index)
        while(i<index){
            prevNode= currentNode; //assigns the present current node as the new previous node
            currentNode=currentNode.next;//updates the current node
            i++;//increments i, the traversal step
        }
        Node<A> nodeToBeRemoved = currentNode;
        prevNode.next=nodeToBeRemoved.next;
        size--;
        return nodeToBeRemoved.item;
    }
    public int Count(){
        return size;
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