//
// Created by Simon Akpoveso on 14/12/2020.
//

#ifndef LINKEDLISTS_SINGLYLINKEDLISTS_H
#define LINKEDLISTS_SINGLYLINKEDLISTS_H

template <class T>
struct Node{
    T data;
    Node *next;
};

template <class T>
class SinglyLinkedList{
private:
    Node<T> *head_node;
    int size = 0;
    void delete_nodes();
    void update_list_size(bool increase); //A true flag adds to size and vice versa
public:
    void add_first(T data);
    void add_last(T data);
    Node<T>* get_head_pointer();
    int get_list_size();
    void add_at_position(int position, T data); //0 indexed
    void remove_first();
    void remove_last();
    void remove_at_position(int position); //0 indexed
public:
    SinglyLinkedList();

    virtual ~SinglyLinkedList();
};
/*
 * Have to include this because I am using templates link for more description
 * https://stackoverflow.com/questions/495021/why-can-templates-only-be-implemented-in-the-header-file
 * */
#include "SinglyLinkedLists.tpp"

#endif //LINKEDLISTS_SINGLYLINKEDLISTS_H
