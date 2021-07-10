## Introduction
A `Binary Heap` is a tree where its nodes have at most two children. The nodes are arranged in such a way that we can find the highest (Binary-Max-Heap) or the smallest (Binary-Min-Heap) item easily.

The Binary Heap is useful whenever there is the need to frequently access the maximum or minimum item of a group of data. It's also useful in implementing Priority Queues.

A [complete binary tree](https://web.cecs.pdx.edu/~sheard/course/Cs163/Doc/FullvsComplete.html) is usually used to represent the binary heap.

There are two types of Binary Heaps: **Binary-Max-Heap** and **Binary-Min-Heap**.

### Binary-Max-Heap
In a binary max heap, the root node represents the biggest or maximum item on the tree. The property is also true for all subtrees in the tree i.e The parent node must be greater than the child node.

###  Binary-Min-Heap
In a binary min-heap, the root node represents the smallest or minimum item on the tree. The property is also true for all its subtrees in the tree i.e The parent node must be smaller than the child node.

<img src="images/MinHeapAndMaxHeap.png" height="auto" width="400"/>

The rest of this README and the implementations for binary heaps assumes we are using a binary max heap. This will be true except otherwise stated.

Some operations we can perform on a Binary Heap include:
* `insert(value)`
* `peek()`
* `remove()`

## Internals : Parent-Child relationship
Since a [complete binary tree](https://web.cecs.pdx.edu/~sheard/course/Cs163/Doc/FullvsComplete.html) is used to represent the binary heap. We take advantage of the parent-child relationship of a complete binary tree: 

    parent:children relationship can be found as: 
    Given a node at index: i
    its parent can be found at index: (i/2);
    its left child index: i * 2;
    its right child index: i * 2 + 1;

    0 < i < N where N is the number of nodes in the tree

Note: index should start from 1

Due to this relationship, an array is used to implement the complete binary tree.


<img src="images/HeapArrRep.png" height="300" width="500"/>

If you notice from the image above, the index `0` is empty. This is because using the `parent:child` relationship formula; `i * 2` for the left child:

    if nodeA at i = 0;
    left child = i * 2 = 0 = nodeA;

    This doesn't follow the parent:child formula

Therefore, we start at index `1` of the array and leave the index `0` blank or filled with `null`.

#### Helper methods
1. **`TopDownSort`**
    
    This method sorts the heap from top to bottom. It moves a node at the top of the heap to its correct position by comparing the node with its children.
    ```
    Pseudocode:
    * Start at the top node i = 1;
    * Get the bigger node of the left (i * 2) and right (i * 2 + 1) child node
        * swap if the bigger child-node is bigger than node else stop
        * also stop if none of the children is bigger than the node or there is no child node.
    * continue until node doesn't have any bigger child node
    ```
    **Time Complexity**: **O(logN)**, where **N** = is the number of nodes in the heap
    | Worst Case   (Time to move a node from bottom to top)      |
    |------------------------------------------------------------|
    | O(logN)                                                    |

    **Space  Complexity**: Sorting happens in place so no extra space is used in this method. Constant Space or **O(1)**
    | Worst Case                        |
    |-----------------------------------|
    | Constant Space / O(1)             |

2. **`BottomUpSort`**

    This method sorts the heap from bottom to top. It moves a node at the bottom of the heap to its correct position by comparing the node with its parent.
    ```
    Pseudocode:
    * Given a heap of size = 5;
    * Start at the bottom node i = 5;
    * Check if the parent node (floor(i/2)) is smaller than the node
        * swap if true else stop
        *also stop if the node is at the top of the arr (index 1) or when the parent is bigger than the node.
    * continue until the node isn't bigger than its parent or is at the top of the heap
    ```
    **Time Complexity**: **O(logN)**, where **N** = is the number of nodes in the heap
    | Worst Case   (Time to move a node from bottom to top)      |
    |------------------------------------------------------------|
    | O(logN)                                                    |

    **Space  Complexity**: Sorting happens in place so no extra space is used in this method. Constant Space or **O(1)**
    | Worst Case                        |
    |-----------------------------------|
    | Constant Space / O(1)             |

#### Operations
1. **`insert(value)`**
    ```
    Pseudocode:
    * insert the value at the bottom of the array
    * sort the heap with `BottomUpSort` method

    ```

    **Time Complexity**:  **O(logN)**, where **N** = is the number of nodes in the heap
    | Worst Case   for `insert(value)`  |
    |------------------------------------------------------------|
    | O(logN)                                                    |

    **Space  Complexity**: No extra space is used in this method. Constant Space or **O(1)**
    | Worst Case for `insert(value)`    |
    |-----------------------------------|
    | Constant Space / O(1)             |
    

2. **`peek()`**
    ```
    Pseudocode:
    * return the node at index: i

    ```

    **Time Complexity**: Constant Time or **O(1)**
    | Worst Case for `peek()`                         |
    |------------------------------------------------------------|
    | Linear Time / O(1)                                         |

    **Space  Complexity**: No extra space is used in this method. Constant Space or **O(1)**
    | Worst Case for `peek()`           |
    |-----------------------------------|
    | Constant Space / O(1)             |

3. **`remove()`**
    ```
    Pseudocode:
    * Give a heap with size = 5
    * Swap the top node (index 1) with last node (index 5)
    * pop the last node on the array
    * sort the arr with the `topDownSort` method
    ```

    **Time Complexity**:  **O(logN)**, where **N** = is the number of nodes in the heap
    | Worst Case  for `remove()`  |
    |------------------------------------------------------------|
    | O(logN)                                                    |

    **Space  Complexity**: No extra space is used in this method. Constant Space or **O(1)**
    | Worst Case for `remove()`    |
    |-----------------------------------|
    | Constant Space / O(1)             |


#### Additional Notes
* The heap itself has a Space Complexity of O(N) - where N is the number of nodes in the heap

## References
1. [Heap Data structure](https://www.geeksforgeeks.org/heap-data-structure/)
2. [complete binary tree](https://web.cecs.pdx.edu/~sheard/course/Cs163/Doc/FullvsComplete.html) 
3. [Binary Trees](https://opendsa-server.cs.vt.edu/ODSA/Books/Everything/html/CompleteTree.html)
4. [Heap: TutorialHorizon](https://algorithms.tutorialhorizon.com/binary-min-max-heap/)
4. [Heap: Visual Algo](https://visualgo.net/en/heap)

## Author(s)
* [Ogooluwa Akinola](https://github.com/rovilay)
