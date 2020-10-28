import java.util.NoSuchElementException;

/**
 * @author Ayomide Oyekanmi
 */
class SinglyLinkedList<E> {

    /** 
     * This represents the template for the node object that is used in the linkedlist
     */
    class Node<E> {
        E element;
        Node<E> next;

        public Node(E element){
            this.element = element;
            next = null;
        }
    }

    private Node<E> head;
    private Node<E> tail;
    private int size;

    public SinglyLinkedList(){ 
        head = null;
        tail = null;
        size = 0;
    }

    public static void main(String[] args) {
        SinglyLinkedList<Integer> obj = new SinglyLinkedList<>();
        obj.addFirst(2);
        System.out.println(obj); // [2]
        obj.addLast(3);
        System.out.println(obj); // [2] => [3]
        obj.addAtPosition(2, 9);
        System.out.println(obj); // [2] => [9] => [3]
        System.out.println(obj.removeAtPosition(1)); // 2
        System.out.println(obj); // [9] => [3]
        System.out.println(obj.removeFirst()); // 9
        System.out.println(obj); // [3]
        System.out.println(obj.removeLast()); // 3
        System.out.println(obj); // Linkedlist is empty!
    }

    /**
     * Add element to front of the list, so that it becomes new head node
     * 
     * @param element element that the new node created wraps around
     */
    public void addFirst(E element){
        // Create new node to wrap element
        Node<E> newHead = new Node(element);
        // Set the new node's pointer to the current head
        newHead.next = head;
        // Set new node as head
        head = newHead;

        // If tail hasn't be set, tail is equal to head
        if(tail == null){
            tail = head;
        }

        // Increment size
        size++;
    }

    /**
     * Add element to back of the list, so that it becomes new tail node
     * 
     * @param element element that the new node created wraps around
     */
    public void addLast(E element){
        // Starting at head node, traverse the linkedlist until tail node is reached
        Node<E> node = head;
        while(node.next != null){
           node = node.next; 
        }

        // Set the current tail node's pointer to the new node that wraps around element
        node.next = new Node(element);
        // Set new node as new tail node
        tail = node.next;

        // Increment size
        size++;
    }

    /**
     * At element at any position in the list
     * 
     * @param n represents position to insert element into.
     * @param element represents element to be inserted into position n
     * @throws IllegalArgumentException if the position given does not exist for list
     */
    public void addAtPosition(int n, E element){
        // Checks if position exists
        if (n < 1 || n > size){
            throw new IllegalArgumentException("No such position");
        }

        // Checks if element to be added will be the new head
        if (n == 1){
            addFirst(element);
            return;
        }

        int index = 1;
        Node<E> prev = null;
        Node<E> node = head;
        Node<E> newNode = new Node(element);

        // Traverse till we get to position
        // PS: node != null check is not necessary because we already check if given index is valid
        // using if (n > size) 
        while(index != n){
            prev = node;
            node = node.next;
            index++;
        }

        // Update pointers to reference new node
        prev.next =  newNode;
        newNode.next = node;

        // Increment size
        size++;
    }

    /**
     * Removes head of the list and returns it
     * 
     * @return returns removed element
     * @throws NoSuchElementException if list has no head i.e. the list is empty
     */
    public E removeFirst(){
        // Checks if list is empty
        if(head == null){
            throw new NoSuchElementException();
        }

        // Removes reference to head element
        Node<E> nodeToRemove = head;

        // Checks if head element is the only element in the list
        if(head.next == null){
            head = null;
            tail = null;
            size = 0;
            return nodeToRemove.element;
        }

        head = head.next;

        // Decrements size
        size--;

        return nodeToRemove.element;
    }

    /**
     * Removes tail of the list and returns it
     * 
     * @return returns removed element
     * @throws NoSuchElementException if list has no tail i.e. the list is empty
     */
    public E removeLast(){
        // Checks if list is empty
        if (tail == null){
            throw new NoSuchElementException();
        }

        // Checks if tail is the only element in the list
        // If head.next is null, it means head == tail :)
        if(head.next == null){
            Node<E> nodeToRemove = tail;
            head = null;
            tail = null;
            size = 0;
            return nodeToRemove.element;
        }

        // Traverses to the end of the list
        Node<E> prev = null;
        Node<E> node = head;
        while(node.next != null){
            prev = node;
            node = node.next;
        }

        // Removes reference to current tail
        prev.next = null;
        // Set the new tail
        tail = prev;

        // Decrements size
        size--;

        return node.element;
    }

    /**
     * Removes element at position n in the list and returns it
     * 
     * @param n position to remove element from
     * @return returns removed element
     * @throws IllegalArgumentException if the position given does not exist for list
     */
    public E removeAtPosition(int n){
        // Checks if position exists
        if (n < 1 || n > size){
            throw new IllegalArgumentException("No such position");
        }

        // Checks if element to be removed is head
        if(n == 1){
            return removeFirst();
        }

        // Checks if element to be removed is tail
        if(n == size){
            return removeLast();
        }

        int index = 1;
        Node<E> prev = null;
        Node<E> node = head;

        // Traverse till we get to position
        // PS: node != null check is not necessary because we already check if given index is valid
        // using if (n > size) 
        while(index != n){
            prev = node;
            node = node.next;
        }

        // Remove reference to node to be deleted
        prev.next = node.next;

        // Decrement size
        size--;

        return node.element;
    }

    @Override
    public String toString() {
        Node<E> node = head;
        if (node == null) {
            return "Linkedlist is empty!";
        }

        String linkedList = "";
        while (true) {
            if (node.next == null) {
                linkedList += "[" + node.element + "]";
                break;
            }

            linkedList += "[" + node.element + "] => ";
            node = node.next;
        }
        return linkedList;
    }
}