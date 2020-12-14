//
// Created by Simon Akpoveso on 13/12/2020.
//
#include <gtest/gtest.h>
#include "SinglyLinkedLists.h"

TEST(linkedlist, instantiate_class_to_null_head) {
auto list = new SinglyLinkedList<int>;
ASSERT_EQ(list->get_head_pointer(), nullptr);
delete list;
}

TEST(linkedlist, check_append) {
auto list = new SinglyLinkedList<int>;
list->add_first(5);
ASSERT_NE(list->get_head_pointer(), nullptr);
ASSERT_EQ(5,list->get_head_pointer()->data);
list->add_first(6);
ASSERT_NE(list->get_head_pointer()->next, nullptr);
ASSERT_EQ(list->get_head_pointer()->next->next, nullptr);
ASSERT_EQ(list->get_head_pointer()->next->data, 6);
delete list;
}

TEST(linkedlist, check_prepend_changes_head_to_new_pointer) {
auto list = new SinglyLinkedList<int>;
list->add_first(5);
ASSERT_EQ(list->get_head_pointer()->data, 5);
list->add_last(7);
ASSERT_EQ(list->get_head_pointer()->data, 7);
ASSERT_EQ(list->get_head_pointer()->next->data, 5);
}

TEST(linkedlist, check_list_size) {
auto list = new SinglyLinkedList<int>;
list->add_first(6);
list->add_first(7);
list->add_first(8);
ASSERT_EQ(list->get_list_size(), 3);
delete list;
}

TEST(linkedlist, insert_data_at_position) {
auto list = new SinglyLinkedList<int>;
list->add_first(6);
list->add_first(7);
list->add_first(8);

list->add_at_position(0, 10);
ASSERT_EQ(10,list->get_head_pointer()->data);

int size = list->get_list_size();
list->add_at_position(size + 2, 20);
ASSERT_EQ(size, list->get_list_size());

list->add_at_position(size + 1, 30);
ASSERT_EQ(30,list->get_head_pointer()->next->next->next->next->data);

list->add_at_position(4, 15);
ASSERT_EQ(15,list->get_head_pointer()->next->next->next->next->data);

list->add_at_position(1, 60);
ASSERT_EQ(60,list->get_head_pointer()->next->data);

ASSERT_EQ(list->get_list_size(), 7);

delete list;
}

TEST(linkedlist, remove_first_node) {
auto list = new SinglyLinkedList<int>;
list->add_first(6);
list->add_first(7);
list->add_first(8);
list->remove_first();
ASSERT_EQ(list->get_head_pointer()->data, 7);
list->remove_first();
ASSERT_EQ(list->get_head_pointer()->data, 8);
list->remove_first();
ASSERT_EQ(list->get_head_pointer(), nullptr);
ASSERT_EQ(list->get_list_size(), 0);
delete list;
}

TEST(linkedlist, remove_last_node) {
auto list = new SinglyLinkedList<int>;
list->add_first(6);
list->add_first(7);
list->add_first(8);

list->remove_last();
ASSERT_EQ(list->get_head_pointer()->next->next, nullptr);

list->remove_last();
ASSERT_EQ(list->get_head_pointer()->next, nullptr);

list->remove_last();
ASSERT_EQ(list->get_head_pointer(), nullptr);
ASSERT_EQ(list->get_list_size(), 0);
delete list;
}

TEST(linkedlist, remove_node_at_position) {
auto list = new SinglyLinkedList<int>;
list->add_first(6);
list->add_first(7);
list->add_first(8);
list->add_first(9);
list->add_first(10);

list->remove_at_position(0);
ASSERT_EQ(list->get_list_size(), 4);
ASSERT_EQ(list->get_head_pointer()->data,7);

list->remove_at_position(list->get_list_size() - 1);
ASSERT_EQ(list->get_list_size(),3);
ASSERT_EQ(list->get_head_pointer()->next->next->data, 9);

list->remove_at_position(1);
ASSERT_EQ(list->get_list_size(), 2);
ASSERT_EQ(list->get_head_pointer()->next->data, 9);

delete list;
}

