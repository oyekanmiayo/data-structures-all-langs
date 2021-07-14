"""
  Author: Oghogho Odemwingie
"""

"""
  DoublyLinkedList implements a double linked list

# Examples
```jldoctest
julia> using DoublyLinkedList

julia> obj = LinkedList{Int}()
Linkedlist is empty!

julia> @assert string(obj) == "Linkedlist is empty!"

julia> addFirst!(obj, 2)

julia> @assert string(obj) == "[2]"

julia> addLast!(obj, 3)

julia> @assert string(obj) == "[2] <=> [3]"

julia> addAtPosition!(obj, 2, 9)

julia> @assert string(obj) == "[2] <=> [9] <=> [3]"

julia> @assert peek(obj) == 2

julia> @assert removeAtPosition!(obj, 1) == 2

julia> @assert string(obj) == "[9] <=> [3]"

julia> @assert removeFirst!(obj) == 9

julia> @assert string(obj) == "[3]"

julia> @assert removeLast!(obj) == 3

julia> @assert string(obj) == "Linkedlist is empty!"

```
"""
module DoublyLinkedList

export LinkedList, Node
export addAtPosition!, addFirst!, addLast!
export removeAtPosition!, removeFirst!, removeLast!

"""
This represents the template for the node object that is used in the linkedlist
"""
mutable struct Node{T}
  """  
  the element encapsulated in the node
  """
  element::T
  prev::Union{Node{T}, Nothing}
  next::Union{Node{T}, Nothing}
  Node{T}(e :: T) where T = new(e, nothing, nothing)
end

"""
This represents a doubly linkedlist
"""
mutable struct LinkedList{T}
  head::Union{Node{T}, Nothing}
  tail::Union{Node{T}, Nothing}
  """  
  the number of nodes in the list
  """
  size::Int
  LinkedList{T}() where T = new(nothing, nothing, 0)
end

"""
Adds element `e` at position `pos` in the list

# Arguments
- `d::LinkedList{T}`: the linked list
- `pos::Int`: the position at which a node should be removed
- `e::T`: the element to be added to the list
"""
function addAtPosition!(d :: LinkedList{T}, pos :: Int, e :: T) :: Nothing where T
  (pos < 1 || pos > d.size + 1) && throw(BoundsError("no such position"))

  index::Int = 1
  prev::Union{Node{T}, Nothing} = nothing
  node::Union{Node{T}, Nothing} = d.head
  nodeToInsert::Node{T} = Node{T}(e)

  while index != pos
    prev, node = node, node.next
    index += 1
  end

  nodeToInsert.next = node
  nodeToInsert.prev = prev

  if !isnothing(node) node.prev = nodeToInsert end
  if !isnothing(prev) prev.next = nodeToInsert end

  if pos == 1 d.head = nodeToInsert end
  if pos == (d.size + 1) d.tail = nodeToInsert end

  d.size += 1
  nothing
end

"""
Add element `e` to front (position `1`) of the list, so that it becomes new head node

same as `addAtPosition!(d, 1, e)`
"""
@inline function addFirst!(d :: LinkedList{T}, e :: T) :: Nothing where T addAtPosition!(d, 1, e) end

"""
Add element `e` to back (position `size + 1`) of the list, so that it becomes new tail node

same as `addAtPosition!(d, d.size + 1, e)`
"""
@inline function addLast!(d :: LinkedList{T}, e :: T) :: Nothing where T addAtPosition!(d, d.size + 1, e) end

function peek(d :: LinkedList{T}) :: T where T
  isnothing(d.head) && throw(BoundsError("list has no more entries"))
  d.head.element
end

"""
Removes element at position `pos` in the list and returns it

# Arguments
- `d::LinkedList{T}`: the linked list
- `pos::Int`: the position at which a node should be removed
"""
function removeAtPosition!(d :: LinkedList{T}, pos :: Int) :: T where T
  (pos < 1 || pos > d.size) && throw(BoundsError("no such position"))

  index::Int = 1
  node::Node{T} = d.head

  while index != pos
    node = node.next
    index += 1
  end

  prev::Union{Node{T}, Nothing} = node.prev

  if !isnothing(prev) prev.next = node.next end
  if !isnothing(node.next) node.next.prev = prev end
  
  if pos == 1 d.head = node.next end
  if pos == d.size d.tail = prev end

  node.next = nothing
  node.prev = nothing

  d.size -= 1

  node.element
end

"""
Removes head of the list and returns it

same as `removeAtPosition!(d, 1)`
"""
@inline function removeFirst!(d :: LinkedList{T}) :: T where T removeAtPosition!(d, 1) end

"""
Removes tail of the list and returns it

same as `removeAtPosition!(d, d.size)`
"""
@inline function removeLast!(d :: LinkedList{T}) :: T where T removeAtPosition!(d, d.size) end

Base.convert(::Type{String}, d :: LinkedList{T}) where T = begin
  buffer = IOBuffer()
  node::Union{Node{T}, Nothing} = d.head
  isnothing(node) && return "Linkedlist is empty!"
  while !isnothing(node)
    write(buffer, "[$(node.element)]")
    node = node.next
    if !isnothing(node) write(buffer, " <=> ") end
  end
  String(take!(buffer))
end
Base.string(d :: LinkedList{T}) where T = convert(String, d)
Base.show(io::IO, d::LinkedList{T}) where T = print(io, string(d))
Base.peek(d :: LinkedList{T}, ::Type{T}) where T = peek(d)
Base.peek(d:: LinkedList{T}) where T = Base.peek(d, T)

end