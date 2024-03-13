cmake_minimum_required(VERSION 3.17)
project(LinkedLists)

set(CMAKE_CXX_STANDARD 14)

find_package(GTest REQUIRED)

add_executable(LinkedLists main.cpp)
#add_executable(LinkedLists Test.cpp SinglyLinkedLists.tpp)

include_directories(${GTEST_INCLUDE_DIR})
target_link_libraries(LinkedLists ${GTEST_LIBRARY} ${GTEST_MAIN_LIBRARY})