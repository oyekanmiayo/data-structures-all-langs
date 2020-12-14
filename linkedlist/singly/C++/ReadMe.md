#Caution
The code makes use of GoogleTest Library for testing.
It is not required to run the code.
To disable the need for the program to check for GoogleTest Library, follow the steps below:
1. find_package(GTest REQUIRED)
2. include_directories(${GTEST_INCLUDE_DIR})
3. target_link_libraries(LinkedLists ${GTEST_LIBRARY} ${GTEST_MAIN_LIBRARY})
4. Discard the Test.cpp file as it makes use of gtest/gtest.h header file
5. add_executable(LinkedLists Tests.cpp LinkedLists.cpp)
6. Uncomment the commented out add_executable command i.e add_executable(LinkedLists main.cpp LinkedLists.cpp)

#Requirements
1. cmake should be installed on your system for this code to run smoothly
   However, other methods of linking are possible as there are no external libraries except GoogleTest
   
