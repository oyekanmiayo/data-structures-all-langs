module doubly_linked_list

fn test_doubly_linked_list() {
	mut obj := DoublyLinkedList{}
	assert obj.str() == 'Linkedlist is empty!'
	obj.add_first(2)
	assert obj.str() == '[2]'
	obj.add_last(3)
	assert obj.str() == '[2] <=> [3]'
	obj.add_at_position(2, 9)
	assert obj.str() == '[2] <=> [9] <=> [3]'
	mut val := obj.peek() or { panic(err) }
	assert val == 2
	assert obj.str() == '[2] <=> [9] <=> [3]'
	val = obj.remove_at_position(1) or { panic(err) }
	assert val == 2
	assert obj.str() == '[9] <=> [3]'
	val = obj.remove_first() or { panic(err) }
	assert val == 9
	assert obj.str() == '[3]'
	val = obj.remove_last() or { panic(err) }
	assert val == 3
	assert obj.str() == 'Linkedlist is empty!'
}
