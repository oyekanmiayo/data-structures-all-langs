__author__ = "Osamudiamen Azamegbe"

from typing import Any


class DoblyLinkedList:

    # check linkedlist/doubly/python/DoublyLinkedList.py for implementation
    def __init__(self):
        pass

    def addLast(self, element):
        pass

    def removeFirst(self):
        pass

    def peek(self):
        pass


class Queue:
    """A QUeue implementation using a Doubly Linked List."""
    def __init__(self) -> None:
        self.list = DoblyLinkedList()

    def enqueue(self, element: Any) -> None:
        """Adds `element` to the end of the Queue.

        Parameters
        ----------
        element: Any
            data to be inserted in back of the Queue.
        """
        self.list.addLast(element)

    def dequeue(self) -> Any:
        """removes element from front of the Queue.

        :return: removed element.
        """

        return self.list.removeFirst()

    def peek(self) -> Any:
        """returns element from front of the Queue.

        :return: element at the front of the Queue.
        """

        return self.list.peek()