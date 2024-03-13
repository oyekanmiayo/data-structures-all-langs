// @author: Lilit

use std::ptr::{NonNull, self};
use std::marker::PhantomData;
use std::mem;
use std::alloc::{self, Layout};
use std::ops::Deref;
use std::ops::DerefMut;

// List accepts elements of same type 
#[derive(PartialEq)]
pub struct List<T> {
    ptr: NonNull<T>, // acts as a pointer to the allocation of the list on the heap
    cap: usize, // the size of the allocation(this is different from the length)
    len: usize, // number of elements that have been initialized in the list
    _marker: PhantomData<T>,
}

// implementing iterations. We need it in order to hold the List's info so that we can free the List
// when IntoIter is `dropped`.
#[allow(dead_code)]
pub struct IntoIter<T> {
    buf: NonNull<T>, // serves as a pointer
    cap: usize, // same as in List
    start: *const T, // the element the List starts with
    end: *const T, // the element it ends with
    _marker: PhantomData<T>,
}


impl<T> List<T> {
    /*
        instantiates a new List, sets the pointer to a dangling, but well aligned pointer to avoid null issues
        returns: the list itself
    */
    fn new() -> Self {
        assert!(mem::size_of::<T>() != 0, "Zero Sized Types Detected");
        List { ptr: NonNull::dangling(), cap: 0, len: 0, _marker: PhantomData }
    }

    /* 
        allocates a space in the heap for the List and reallocates if a new element exceeds the 
        length.
        parameters: a mutable reference to the List struct
    */ 
    fn grow(&mut self) {
        let (new_cap, new_layout) = if self.cap == 0 {
            (1, Layout::array::<T>(1).unwrap())
        } else {
            let new_cap = self.cap * 2;
            let new_layout = Layout::array::<T>(new_cap).unwrap();
            (new_cap, new_layout)
        };

        // make sure that the new allocayion doesn't exceed the maximum sixe of isize::MAX bytes
        assert!(new_layout.size() <= isize::MAX as usize, "Memory allocation too large. Exceeded");

        //set the pointer to point at the new allocation
        let new_ptr = if self.cap == 0 {
            unsafe { alloc::alloc(new_layout) }
        } else {
            let old_layout = Layout::array::<T>(self.cap).unwrap();
            let old_ptr = self.ptr.as_ptr() as *mut u8;
            unsafe { alloc::realloc(old_ptr, old_layout, new_layout.size()) }
        };

        // if the allocation fails, new-ptr will be null which we definitely don't want to happen
        self.ptr = match NonNull::new(new_ptr as *mut T) {
            Some(p) => p,
            None => alloc::handle_alloc_error(new_layout),
        };

        self.cap = new_cap;
    }

    /*
        Appends an element of type T to the end of the list.
        parameters: a mutable reference to the list and the element to be appended
    */
    pub fn push(&mut self, elem: T) {
        // if the capacity of the space of the list allocated on the heap has been filled,
        // copy out into a new List with a bigger size.
        if self.len == self.cap { self.grow() }

        unsafe {
            // overwrites the target address without evaluation
            ptr::write(self.ptr.as_ptr().add(self.len), elem);
        }

        self.len += 1;
    }

    /*
        removes an element at the end of the list
        parameters: a mutable reference to the List
        returns: 
            a `None` if the index is uninitialized or the List has a length of 0
            a `Some` with the element at the last index. 
        Note: it merely copies out the elment without dereferencing it because it would cause a null
        behaviour if we dereferenced it.
    */
    pub fn pop(&mut self) -> Option<T> {
        if self.len == 0 {
            None
        } else {
            self.len -= 1;
            unsafe { Some(ptr::read(self.ptr.as_ptr().add(self.len))) }
        }
    }

    /*
        inserts an element of type T at index of i.
        parametrs: 
            -) a mutable reference to the List
            -) the index, which is an unsigned integer of the erch size, to insert the element at
            -) the element of type T to be inserted
    */
    pub fn insert(&mut self, index: usize, elem: T) {
        // using <= because if index == self.len that means pushing and it is valid. 
        // meaning we can insert an element after everything in the List
        assert!(index <= self.len, "Index has exceeded the length of the List");
        if self.cap == self.len { self.grow() } // if the capacity eaquals the length reallocate the List on the heap
        
        unsafe {
            //copy rhe whole array from the of the index to another part, but leave a space before 
            // before the beginning of the new pointer.
            // copy takes in -) the src of the data, -) the desrination and -) the count
            ptr::copy(self.ptr.as_ptr().add(index), self.ptr.as_ptr().add(index + 1), self.len - index);
            ptr::write(self.ptr.as_ptr().add(index), elem);
            self.len += 1;
        }
    }

    /*
        does the exact opposite of insert.
        parametrs: 
            -) a mutable reference to the List
            -) the index, which is an unsigned integer of the erch size, to remove the element
        returns: the elemnent removed which is of type T
    */
    pub fn remove(&mut self, index: usize) -> T {
        assert!(index < self.len, "Index has exceeded the length of the List");
        
        unsafe {
            self.len -= 1;
            let result = ptr::read(self.ptr.as_ptr().add(index));
            ptr::copy(self.ptr.as_ptr().add(index + 1), self.ptr.as_ptr().add(index), self.len - index);
            result
        }
    }

    /*
        initializing into_iter for iteration of the List. This method is essential, but not neccessary
        for methods like everse and sort to be called.

        parameters: the List itself
        returns: a struct of type IntoIter
     */
    pub fn into_iter(self) -> IntoIter<T> {
        let ptr = self.ptr;
        let cap = self.cap;
        let len = self.len;

        mem::forget(self);

        unsafe {
            IntoIter {
                buf: ptr,
                cap: cap,
                start: ptr.as_ptr(),
                end: if cap == 0 {
                    // can't offset off this pointer, it's not allocated!
                    ptr.as_ptr()
                } else {
                    ptr.as_ptr().add(len)
                },
                _marker: PhantomData,
            }
        }
    }

    /*
        reverses a list.
        parameters: the List itself
        returns: a new List of revrsed elements
    */
    pub fn reverse(self) -> List<T> {
        let mut reversed_list: List<T> = List::new();
        if self.len == 0 {
            return reversed_list;
        } else {
            for elem in self.into_iter() {
                reversed_list.push(elem);
            }
        }
        reversed_list
    }
}

// Creates an unmutable slice of the List
impl<T> Deref for List<T>  {
    type Target = [T];
    fn deref(&self) -> &[T] {
        unsafe {
            std::slice::from_raw_parts(self.ptr.as_ptr(), self.len)
        }
    }
}

// creates a mutable slice of List
impl<T> DerefMut for List<T> {
    fn deref_mut(&mut self) -> &mut [T] {
        unsafe {
            std::slice::from_raw_parts_mut(self.ptr.as_ptr(), self.len)
        }
    }
}

// for forward iteration. That is moving from the first element to the last one consecutively
impl<T> Iterator for IntoIter<T> {
    type Item = T;
    fn next(&mut self) -> Option<T> {
        // if the element at the beginning and and are the same, that means it's a single element List
        if self.start == self.end {
            None
        } else {
            unsafe {
                let result = ptr::read(self.start);
                // offset the start by one. i.e, set the next element in the list to the start 
                // property of IntoIter struct
                self.start = self.start.offset(1);
                Some(result)
            }
        }
    }

    fn size_hint(&self) -> (usize, Option<usize>) {
        let len = (self.end as usize - self.start as usize)
                  / mem::size_of::<T>();
        (len, Some(len))
    }
}

// iterate backwards form the end to the beginning
impl<T> DoubleEndedIterator for IntoIter<T> {
    fn next_back(&mut self) -> Option<T> {
        if self.start == self.end {
            None
        } else {
            unsafe {
                self.end = self.end.offset(-1);
                Some(ptr::read(self.end))
            }
        }
    }
}


unsafe impl<T: Send> Send for List<T> {}
unsafe impl<T: Sync> Sync for List<T> {}

fn main() {
    let mut my_list: List<i32> = List::new();
    my_list.push(8);
    my_list.push(56);
    my_list.push(48);
}
