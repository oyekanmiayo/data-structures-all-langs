"""
Author: Abdulazeez Abdulazeez Adeshina
"""


class Node:
    """
    This represents the template for the node object that is used for the linkedlist.
    """

    def __init__(self, element):
        """
        :param element: This represents the element contained in the Node.
        :type element: This element can be of any type ranging from strings, numbers, arrays etc.
        """
        self.element = element
        self.next = None


class SinglyLinkedList:
    def __init__(self):
        self.head = None
        self.tail = None
        self.size: int = 0


    def __len__(self) -> int:
        """
        :return: Length of the linkedlist
        :rtype: int
        """
        return self.size

    def __str__(self):
        linkedlist = ""
        current = self.head

        while current and current.next is not None:
            linkedlist += str(current.element) + "->"
            current = current.next

        linkedlist += str(current.element)
        return linkedlist

    def addFirst(self, element):
        """
        The `addFirst` method adds a new node to the front of the list making it the head of the linkedlist.

        :param element: the value held in the Node.
        """
        # Create a new node to wrap the element
        newHead = Node(element)
        # Set the new node's next pointer to the current head
        newHead.next = self.head
        # Set the new node as the head
        self.head = newHead

        # Check if the tail node is absent.
        if self.tail == None:
            # If the tail node is absent, assign the head node to the tail node.
            self.tail = self.head

        # Increment linked list size.
        self.size += 1

    def addLast(self, element):
        """
        The `addLast` method adds a new node to the end of the list.
        :param element: the value held in the Node.
        """

        # Starting at the head node, travserse the linkedlist until we get to the tail node
        node = self.head

        while node.next != None:
            node = node.next

        # Set the current tail node's pointer to the new tail node

        node.next = Node(element)
        # Set the new tail node

        self.tail = node.next

        # Increment size
        self.size += 1

    def addLastConstantTime(self, element: Any) -> None:
        """Add a Node object storing `element` to the end of the SinglyLinkedList instance in constant time.

        Parameters
        ----------
        element: Any
            data to be stored in Node object.
        """
        # Create new node to store element.
        newNode = Node(element)

        # If tail pointer and head pointers have not been set.
        # Else set the current tail next pointer to the new node and set new node as tail.
        if self.tail is None:
            self.tail = newNode
            self.head = self.tail
        else:
            self.tail.next = newNode
            self.tail = newNode
        
        # increase size of SLL
        self.size += 1

    def addAtPosition(self, position, element):
        """
        The `addAtPosition` method adds a new node to the specified position in the linkedlist if the position is valid.

        :param position: position of the new element to be added.
        :param element: the value held in the Node.
        :raise
        """

        # Check if position is valid
        if position < 1 or position > self.size:
            raise IndexError("Position is invalid")

        # Check of element to be added will be the new head
        if position == 1:
            self.addFirst(element)
            return

        index = 1
        prev = None
        current = self.head
        newNode = Node(element)

        # Traverse the linkedlist till we get to the position
        # using if (position > self.size)
        while index != position:
            index += 1
            prev = current
            current = current.next

        prev.next = newNode
        newNode.next = current

        self.size += 1

    def removeFirst(self):
        """
        The `removeFirst` method removes the head of the linkedlist and returns it.

        :returns removed element
        """

        # Check if the list is empty
        if self.head is None:
            return "List is Empty!"

        # Check if the head element is the only element in the linkedlist
        if self.head.next is None:
            self.head = self.tail = None
            self.size = 0
            return

        # Removes reference to head element
        nodeToRemove = self.head
        self.head = self.head.next

        # Decrement size
        self.size -= 1

        return nodeToRemove.element

    def removeLast(self):
        """
        The `removeLast` method removes the tail of the linkedlist.

        :returns the removed element
        """

        # Check if linkedlist is empty
        if self.tail is None:
            return "No tail present in linkedlist!"

        # Check if the tail is the only element in the list
        # If `self.head.next` is None, it means `self.head` == `self.tail` :)

        if self.head.next is None:
            self.head = None
            self.tail = None
            self.size = 0
            return

        prev = None
        current = self.head

        # Traverse to the end of the list
        while current.next is not None:
            prev = current
            current = current.next

        # Remove reference to current tail
        prev.next = None

        # Set the new tail
        self.tail = prev

        # Decrement size
        self.size -= 1

        return current.element

    def removeAtPosition(self, position):
        """
        The `removeAtPosition` method removes a node at the specified postion.

        :param position: position to remove element from
        :returns the removed element
        """

        # Check if position is valid
        if position < 1 or position > self.size:
            raise IndexError("Invalid position!")

        # Check if element to be removed is the head
        if position == 1:
            self.removeFirst()
            return

        # Check if element to be removed is the tail
        if position == self.size:
            self.removeLast()
            return

        index = 1
        prev = None
        current = self.head

        # Traverse till we get to position
        while index != position:
            index += 1
            prev = current
            current = current.next

        # Remove reference to node to be deleted
        prev.next = current.next

        # Decrement size
        self.size -= 1

        return current.element

if __name__ == '__main__':
    x = SinglyLinkedList()
    x.addFirst(1) # 1
    x.addLast(2) # 1 -> 2
    x.addFirst(3) # 3 -> 1 -> 2
    print(x) # 3 -> 1 -> 2
    x.addAtPosition(2, 10) # 3 -> 10 -> 1 -> 2
    print(x)
    x.removeAtPosition(2) # 3 -> 1 -> 2
    print(len(x)) # 3
    x.removeAtPosition(0) # Raises "IndexError: Invalid position!" and exit.