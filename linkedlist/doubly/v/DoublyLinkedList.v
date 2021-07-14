// Author: Oghogho Odemwingie

module doubly_linked_list

// This represents the template for the node object that is used in the linkedlist
// using `ref_only` attribute because it is only always used as a pointer
[ref_only]
struct Node {
	pub:
	element int
	pub mut:
	prev &Node = 0
	next &Node = 0
}

// This represents a doubly linkedlist
struct DoublyLinkedList {
	pub mut:
	head &Node = 0
	tail &Node = 0
	size int = 0
}

// str is the stringer for the DoublyLinkedList struct type
pub fn (d DoublyLinkedList) str() string {
	mut b := ''
	mut node := d.head
	for node != 0 {
		b += '[$node.element]'
		node = node.next
		if node != 0 {
			b += ' <=> '
		}
	}
	if b == '' { b = 'Linkedlist is empty!' }
	return b
}

// add_at_position Adds element `e` at position `pos` in the list
pub fn (mut d DoublyLinkedList) add_at_position(pos int, e int) {
	// Check if position is valid in the list's context
	if pos < 1 || pos > d.size + 1 { panic("no such position") }

	mut index := 1
	mut prev := &Node(0) 
	mut node := d.head
	mut node_to_insert := &Node{element: e}

	// Traverse list until we get to position of insertion
	for index != pos {
		prev, node = node, node.next
		index++
	}

	// Set next and previous nodes of node to be inserted to the current node at pos and its predecessor
	node_to_insert.next = node
	node_to_insert.prev = prev
	
	// If node currently at pos is not nil, set its previous node to node to be inserted
	if node != 0 { node.prev = node_to_insert }
	// If predeesor of node currently at pos is not nil, set its next node to node to be inserted
	if prev != 0 { prev.next = node_to_insert }
	// If pos is `1` (head) set list head to node to be inserted
	if pos == 1 { d.head = node_to_insert }
	// If pos is `size + 1` set list tail to node to be inserted
	if pos == d.size+1 { d.tail = node_to_insert }

	// Increment list size
	d.size++
}

// add_first Add element `e` to front (position `1`) of the list, so that it becomes new head node
// same as `d.add_at_position(1, e)`
[inline]
pub fn (mut d DoublyLinkedList) add_first(e int) {
	d.add_at_position(1, e)
}

// add_last Add element `e` to back (position `size + 1`) of the list, so that it becomes new tail node
// same as `d.add_at_position(d.size+1, e)`
[inline]
pub fn (mut d DoublyLinkedList) add_last(e int) {
	d.add_at_position(d.size + 1, e)
}

// peek Returns the head of the list
pub fn (d DoublyLinkedList) peek() ?int {
	if d.head != 0 {
		return d.head.element
	}
	return error("list has no more entries")
}

// remove_at_position Removes element at position `pos` in the list and returns it
pub fn (mut d DoublyLinkedList) remove_at_position(pos int) ?int {
	// Checks if position exists
	if pos < 1 || pos > d.size { return error("no such position") }
	mut index := 1
	mut node := d.head
	// Traverse list until we get to position of removal
	for index != pos {
		node = node.next
		index++
	}

	mut prev := node.prev

	// If the predecessor of the node at `pos` is not nil, set its next to node's next
	if prev != 0 { prev.next = node.next }
	// If the next of the node at `pos` is not nil, set its prev to node's prev
	if node.next != 0 { node.next.prev = prev }
	// If pos is `1` (head) set list head to node's next
	if pos == 1 {	d.head = node.next }
	// If pos is `size` (tail) set list tail to node's prev
	if pos == d.size { d.tail = prev }

	// Help the GC by dereferencing node's prev and next
	node.next = 0
	node.prev = 0

	// Decrement list size
	d.size--

	return node.element
}

// remove_first Removes head of the list and returns it
// same as `d.remove_at_position(1)`
[inline]
pub fn (mut d DoublyLinkedList) remove_first() ?int {
	return d.remove_at_position(1)
}

// remove_last Removes tail of the list and returns it
// same as `d.remove_at_position(d.size)`
[inline]
pub fn (mut d DoublyLinkedList) remove_last() ?int {
	return d.remove_at_position(d.size)
}
