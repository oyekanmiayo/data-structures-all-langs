
//
// Created by Simon Akpoveso on 13/12/2020.
//

#include "SinglyLinkedLists.h"
#include <iostream>

using namespace std;

//Instantiate the class and set head_node pointer to null
template <class T>
SinglyLinkedList<T>::SinglyLinkedList() {
    head_node = nullptr;
}

//For every element added or removed from the list update the list size
template <class T>
void SinglyLinkedList<T>::update_list_size(bool increase) {
    if (increase){
        size+=1;
    }else {
        size-=1;
    }
}

//Add element too the head/beginning of the list
template <class T>
void SinglyLinkedList<T>::add_first(T data) {
    if (head_node == nullptr) {
        head_node = new Node<T>;
        head_node->data = data;
        head_node->next = nullptr;
        update_list_size(true);
        return;
    }
    Node<T> *current = head_node;
    Node<T> *temp;
    while (current != nullptr){
        temp = current;
        current = current->next;
    }
    current = new Node<T>;
    temp->next = current;
    current->data = data;
    current->next = nullptr;
    update_list_size(true);
}

//Add element to the tail/end of the list
template <class T>
void SinglyLinkedList<T>::add_last(T data) {
    if (head_node == nullptr) {
        head_node = new Node<T>;
        head_node->data = data;
        head_node->next = nullptr;
        update_list_size(true);
        return;
    }
    Node<T> *temp = head_node;
    head_node = new Node<T>;
    head_node->data = data;
    head_node->next = temp;
    update_list_size(true);
}

//Add element to any valid specified position in the list
template <class T>
void SinglyLinkedList<T>::add_at_position(int position, T data) {
    if (size+1 < position) {
        std::cout << "List not large enough" << std::endl;
    }else if (position == size+1) {
        add_first(data);
    }else if (position == 0){
        add_last(data);
    }else {
        Node<T> *temp = head_node;
        for (int i = 0; i < position - 1; ++i) {
            //Subtract position from 1 so we don't point to the data we want to substitute
            temp = temp->next;
        }
        auto new_node = new Node<T>;
        new_node->data = data;
        new_node->next = temp->next;
        temp->next = new_node;
        update_list_size(true);
    }
}

//Remove element for head/beginning of the list
template <class T>
void SinglyLinkedList<T>::remove_first() {
    if (size == 1){
        Node<T> *temp = head_node;
        head_node = nullptr;
        update_list_size(false);
        delete temp;
    }else {
        Node<T> *temp = head_node;
        head_node = head_node->next;
        update_list_size(false);
        delete temp;
    }
}

//Remove element from the end/tail of the list
template <class T>
void SinglyLinkedList<T>::remove_last() {
    if (size == 1){
        Node<T> *temp = head_node;
        head_node = nullptr;
        update_list_size(false);
        delete temp;
    }else {
        Node<T> *temp;
        Node<T> *current = head_node;
        for (int i = 0; i < size - 1; ++i) {
            temp = current;
            current = current->next;
        }
        temp->next = nullptr;
        update_list_size(false);
        delete current;
    }

}

//Remove element from specified valid position
template <class T>
void SinglyLinkedList<T>::remove_at_position(int position) {
    if (size-1 < position) {
        std::cout << "List not large enough" << std::endl;
    }else if (position == size-1) {
        remove_last();
    }else if (position == 0){
        remove_first();
    }else {
        Node<T> *temp = head_node;
        Node<T> *current;
        for (int i = 0; i < position; ++i) {
            current = temp;
            temp = temp->next;
        }
        current->next = temp->next;
        delete temp;
        update_list_size(false);
    }
}

//Helper function to get head pointer
template <class T>
Node<T> *SinglyLinkedList<T>::get_head_pointer() {
    return head_node;
};

//Helper function to get the size of the list at any time
template <class T>
int SinglyLinkedList<T>::get_list_size() {
    return size;
}

//Clean up all memory allocated for the lists
template <class T>
void SinglyLinkedList<T>::delete_nodes() { //Free all the internally allocated memory for the lists
    Node<T> *temp = head_node;
    while (temp != nullptr) {
        temp = head_node->next;
        delete head_node;
        head_node = temp;
    }
}

//Destructor for class. Cleans up memory allocated for the class
template <class T>
SinglyLinkedList<T>::~SinglyLinkedList() {
    delete_nodes();
}