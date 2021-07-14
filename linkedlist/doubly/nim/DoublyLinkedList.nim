## Author: Oghogho Odemwingie

from streams import readAll, close, newStringStream, write, setPosition, getPosition

{.push experimental: "notnil".}
type
  Node*[T] = ref NodeObj[T] ## This represents the template for the node object that is used in the linkedlist
  NodeObj*[T] {.acyclic.} = object
    element* : T ## the element encapsulated in the node
    prev : Node[T]
    next : Node[T]

  DoublyLinkedList*[T] = ref DoublyLinkedListObj[T] ## This represents a doubly linkedlist
  DoublyLinkedListObj*[T] = object
    head : Node[T]
    tail : Node[T]
    size* : int ## the number of nodes in the list

func newNode*[T](e : T) : Node[T] {.inline.} =
  ## Returns a new node of type `T` with element `e`
  result = Node[T](element : e)

{.push inline}
func newDoublyLinkedList*[T] : DoublyLinkedList[T] not nil =
  ## Returns a new doubly linedlist of type `T`
  result = DoublyLinkedList[T](size: 0)
{.pop.}

proc `$`*[T](d : DoublyLinkedList[T] not nil) : string =
  ## stringer for linkedlist type
  result = "Linkedlist is empty!"
  var buffer = newStringStream("")
  defer: buffer.close()
  var node = d.head
  while not node.isNil:
    buffer.write("[", node.element, "]")
    node = node.next
    if not node.isNil: buffer.write(" <=> ")
  if buffer.getPosition() == 0: return
  buffer.setPosition(0)
  result = buffer.readAll()

func addAtPosition*[T](d : DoublyLinkedList[T] not nil, pos : int, e : T) : void {.raises: [].} =
  ## Adds element `e` at position `pos` in the list

  # Checks if position is valid in the list's context
  if (pos < 1) or (pos > (d.size + 1)): raise newException(IndexDefect, "no such position")
  var index : int = 1
  var prev : Node[T]
  var node = d.head
  var nodeToInsert = newNode(e)

  # Traverse list until we get to position of insertion
  while index != pos:
    (prev, node) = (node, node.next)
    inc(index)
  
  # Set next and previous nodes of node to be inserted to the current node at pos and its predecessor
  nodeToInsert.next = node
  nodeToInsert.prev = prev

  # If node currently at pos is not nil, set its previous node to node to be inserted
  if not node.isNil:
    node.prev = nodeToInsert
  
  # If predeesor of node currently at pos is not nil, set its next node to node to be inserted
  if not prev.isNil:
    prev.next = nodeToInsert
  
  # If pos is `1` (head) set list head to node to be inserted
  if pos == 1:
    d.head = nodeToInsert
  
  # If pos is `size + 1` set list tail to node to be inserted
  if pos == (d.size + 1):
    d.tail = nodeToInsert
  
  # Increment list size
  inc(d.size)

func addFirst*[T](d : DoublyLinkedList[T] not nil, e : T) : void {.inline,raises: [].} =
  ## Add element `e` to front (position `1`) of the list, so that it becomes new head node
  ## 
  ## same as `addAtPosition(d, 1, e)`
  d.addAtPosition(1, e)

func addLast*[T](d : DoublyLinkedList[T] not nil, e : T) : void {.inline,raises: [].} =
  ## Add element `e` to back (position `size + 1`) of the list, so that it becomes new tail node
  ## 
  ## same as `addAtPosition(d, d.size + 1, e)`
  d.addAtPosition(d.size + 1, e)

func peek*[T](d : DoublyLinkedList[T] not nil) : T {.raises: [ResourceExhaustedError].} =
  ## Returns the head of the list

  # check if list is empty
  if d.head.isNil: raise newException(ResourceExhaustedError, "list has no more entries")
  result = d.head.element

func removeAtPosition*[T](d : DoublyLinkedList[T] not nil, pos : int) : T {.raises: [].} =
  ## Removes element at position `pos` in the list and returns it

  # Checks if position exists
  if (pos < 1) or (pos > d.size): raise newException(IndexDefect, "no such position")
  
  var index : int = 1
  var node = d.head

  # Traverse list until we get to position of removal
  while index != pos:
    node = node.next
    inc(index)
  
  var prev = node.prev

  # If the predecessor of the node at `pos` is not nil, set its next to node's next
  if not prev.isNil:
    prev.next = node.next
  
  # If the next of the node at `pos` is not nil, set its prev to node's prev
  if not node.next.isNil:
    node.next.prev = prev

  # If pos is `1` (head) set list head to node's next
  if pos == 1:
    d.head = node.next
  
  # If pos is `size` (tail) set list tail to node's prev
  if pos == d.size:
    d.tail = prev

  # Help the GC by assigning node's prev and next to nil
  node.next = nil
  node.prev = nil

  # Decrement list size
  dec(d.size)

  result = node.element

func removeFirst*[T](d : DoublyLinkedList[T] not nil) : T {.inline,raises: [].} =
  ## Removes head of the list and returns it
  ## 
  ## same as `removeAtPosition(d, 1)`
  result = d.removeAtPosition(1)

func removeLast*[T](d : DoublyLinkedList[T] not nil) : T {.inline,raises: [].} =
  ## Removes tail of the list and returns it
  ## 
  ## same as `removeAtPosition(d, d.size)`
  result = d.removeAtPosition(d.size)

runnableExamples:
  var obj = newDoublyLinkedList[int]()
  doAssert ($obj == "Linkedlist is empty!")
  obj.addFirst(2)
  doAssert ($obj == "[2]")
  obj.addLast(3)
  doAssert ($obj == "[2] <=> [3]")
  obj.addAtPosition(2, 9)
  doAssert ($obj == "[2] <=> [9] <=> [3]")
  doAssert (obj.peek() == 2)
  doAssert ($obj == "[2] <=> [9] <=> [3]")
  doAssert (obj.removeAtPosition(1) == 2)
  doAssert ($obj == "[9] <=> [3]")
  doAssert (obj.removeFirst() == 9)
  doAssert ($obj == "[3]")
  doAssert (obj.removeLast() == 3)
  doAssert ($obj == "Linkedlist is empty!")
{.pop.}
