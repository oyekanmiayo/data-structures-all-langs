/**
 * @author Ogooluwa Akinola
 */
class BinaryMaxHeap {
    constructor () {
      this._list = [null];
      this.size = 0;
    }
  
    /**
     * Sorts a node at the bottom of the heap to its correct position
     */
    _bottomUpSort () {
      if (this.size <= 1) return;
  
      // start from the bottom of the heap;
      let index = this.size;
      let parentIndex = Math.floor(index/2);
  
      while (index > 1 && 
        this._list[parentIndex] < this._list[index]
      ) {
        // swap parent with node
        [this._list[parentIndex], this._list[index]] = [this._list[index], this._list[parentIndex]];
  
        index = parentIndex;
        parentIndex = Math.floor(index/2);
      }
    }
    
    /**
     * Sorts a node at the top of the heap to its correct position
     */
    _topDownSort () {
      if (this.size <= 1) return;
  
      let index = 1;
  
      while (true) {
        const leftChildIndex = index * 2;
        const rightChildIndex = leftChildIndex + 1;
  
        let swapIndex = null;
        
        if (leftChildIndex > this.size) {
          break;
        } else if (rightChildIndex > this.size) {
          swapIndex = leftChildIndex;
        } else {
          // choose the biggest child
          swapIndex = this._list[leftChildIndex] > this._list[rightChildIndex] ? leftChildIndex : rightChildIndex;
        }
  
        if (this._list[index] > this._list[swapIndex]) break;
  
        // swap node with biggest child;
        [this._list[index], this._list[swapIndex]] = [this._list[swapIndex], this._list[index]];
  
        index = swapIndex;
      }
    }
      
    /**
     * inserts a node to the tree
     * @param {num} value the value to insert
     */
    insert (value) {
      // insert to the end and sort
      this._list[this.size+1] = value;
      this.size += 1;
      this._bottomUpSort();
    }
  
    /**
     * Gets the node at the top of the tree
     * @returns num
     */
    peek () {
      return this._list[1];
    }
  
    /**
     * removes the node at the top of the heap
     * @returns 
     */
    remove () {
      if (this.size == 0) return null;
  
  
      if (this.size == 1) {
        this.size -= 1;
        return this._list.pop();
      }
  
      // get the top
      const top = this._list[1];
      // replace the top with the bottom
      const bottom = this._list.pop();
      this.size -= 1;
      this._list[1] = bottom;
      // sort the tree
      this._topDownSort();
  
      return top;
    }
}

  