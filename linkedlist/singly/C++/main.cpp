#include <iostream>
#include "SinglyLinkedLists.h"

int main() {
    auto list = new SinglyLinkedList<int>;
    list->add_first(6);
    list->add_first(7);
    list->add_first(7);
    list->add_first(7);
    list->add_last(8);
    list->add_last(9);
    list->remove_first();
    list->remove_last();
    list->remove_at_position(2);
    std::cout << list->get_list_size() << std::endl;

    delete list;
    return 0;
}
