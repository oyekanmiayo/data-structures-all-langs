/*
AUTHOR: Kehinde Ojewale
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //Tests
        List<int> Test = new List<int>();
        Test.Insert(0, 6);//Index 0 is out of the bounds of the list
        Test.Add(9);// {9}
        Test.AddRange(new int[] {2, 700, 90, 67});// {9,2,700,90,67}
        Test.Insert(2, 909);// {9,2,909,700,90,67}
        Test.InsertRange(3, new int[] {23, 16, 45});// {9,2,909,23,16,45,700,90,67}
        Test.Slice(2, 6);// {909,23,16,45,700}
        Test.Slice(5, 5);// {45}
        Test.Get(5);// 45
        Test.Count();// 9
        Test.Remove(4);// 16
        Test.Count();// 8
        Test.Reverse();// {67,90,700,45,23,909,2,9}
        Test.Sort(); //sorted list is {2,9,23,45,67,90,700,909}
        Test.Insert(8, 80);// Raises Index Error: Index 8 is out of the bounds of the list
        Test.Insert(-3, 90);//Raises Index Error: Index -3 is out of the bounds of the list
        Test.Get(16);//Raises Index Error: Index 16 is out of the bounds of the list: no item with index 16
        Test.Remove(24);//Raises Index Error: Index 24 is out of the bounds of the list: no item with index 24
        Test.Get(-4);//Raises Index Error: Index -4 is out of the bounds of the list: no item with index -4
        Test.Get(-1);//Raises Index Error: Index -1 is out of the bounds of the list: no item with index -1
    }
}
class List<A> : IEnumerable<A>
{
    A[] underlyingArray;
    int currentSize;
    double currentThreshold; //this variable tracks 80% of the underlying array's size
    int listSize = 0; //this variable tracks the length of items in the list and tells the index a new item will be added to


    public List()
    {

        underlyingArray = new A[20]; //initializing an unserlying array to store the elements of the list
        currentSize = underlyingArray.Length;
        currentThreshold = 0.8 * currentSize;
    }

    //Method to add an item to the list    
    public void Add(A item)
    {
        if (listSize >= currentThreshold) CreateBiggerArray(1);
        underlyingArray[listSize] = item;
        listSize++;
    }

    //Method to add an array of items to the list
    public void AddRange(A[] items)
    {
        if (listSize + items.Length >= currentThreshold)
        {
            int sizeMultiple;
            sizeMultiple = (listSize + items.Length) / listSize;
            CreateBiggerArray(sizeMultiple);
        }
        int listSizeCopy = listSize;
        for (int i = 0; i < items.Length; i++)
        {
            underlyingArray[listSizeCopy + i] = items[i];
            listSize++;
        }
    }
    //Creates a bigger array once the underlying array's threshhol is reached
    void CreateBiggerArray(int sizeMultiple)
    {
        //Calculates the size of the array to be created
        int newSize = currentSize * 2 * sizeMultiple;
        A[] biggerArray = new A[newSize];
        for (int i = 0; i < listSize; i++)
        {
            biggerArray[i] = underlyingArray[i];
        }
        underlyingArray = biggerArray;
    }

    //Method to add an item at a particular index in the list
    public void Insert(int index, A item)
    {
        if (index >= listSize || index < 0)
        {
            throw new IndexOutOfRangeException(String.Format("Index {0:d} is out of the bounds of the list", index));
        }
        if (listSize >= currentThreshold) CreateBiggerArray(1);
        A itemToBeReplaced = underlyingArray[index];
        underlyingArray[index] = item;
        int replacementIndex = index + 1;
        while (replacementIndex < listSize)
        {
            underlyingArray[replacementIndex + 1] = underlyingArray[replacementIndex];
            replacementIndex++;
        }

        underlyingArray[index + 1] = itemToBeReplaced;
        listSize++;
    }

    //Method to add an array of items starting from a particular index in the list
    public void InsertRange(int index, A[] items)
    {
        if (index >= listSize || index < 0)
        {
            throw new IndexOutOfRangeException(String.Format("Index {0:d} is out of the bounds of the list", index));
        }

        if (listSize + items.Length >= currentThreshold)//checks if the addition of the new items will exceed the Threshold of the underlying array. If it does, a bigger array is craeted
        {
            int sizeMultiple;
            sizeMultiple = (listSize + items.Length) / listSize;//the sizemultiple indicates by how much the size of the underlying array should be incremented to ensure that a much bigger array is created if the addition of the multiple items would exceed the threshold of twice the current array's size
            //NB: The default size of a bigger array is twice the current array's size    
            CreateBiggerArray(sizeMultiple);
        }

        A itemToBeReplaced = underlyingArray[index];
        int replacementIndex = 0;

        replacementIndex = index + items.Length;
        while (replacementIndex < listSize + items.Length)
        {
            underlyingArray[replacementIndex] = underlyingArray[replacementIndex - items.Length];
            replacementIndex++;
        }

        int i = 0;
        while (i < items.Length)
        {
            underlyingArray[index + i] = items[i];
            listSize++;
            i++;
        }


    }
    //Method to retrieve an item from the list using its index
    public A Get(int index)
    {
        if (index >= listSize || index < 0)
        {
            throw new IndexOutOfRangeException(String.Format("Index {0:d} is out of the bounds of the list: no item with index {0:d}", index));
        }
        else
        {
            return underlyingArray[index];
        }
    }
    //Method to remove an item from the list using its index
    public A Remove(int index)
    {
        if (index >= listSize || index < 0)
        {
            throw new IndexOutOfRangeException(String.Format("Index {0:d} is out of the bounds of the list: no item with index {0:d}", index));
        }
        A item = underlyingArray[index];

        for (int i = 1; i < listSize - index; i++)
        {
            underlyingArray[index] = underlyingArray[index + i];
        }
        listSize--;
        return item;
    }

    //Sorts the list
    public void Sort()
    {
        A[] validListItems = ValidListItemsArray();
        Array.Sort(validListItems);
        underlyingArray = new A[currentSize];
        this.InsertRange(0, validListItems);
        listSize -= validListItems.Length;
    }

    //Generates a slice of the list from a start index to an end index(inclusive)
    public List<A> Slice(int startIndex, int endIndex)
    {

        if (startIndex >= listSize || startIndex < 0)
        {
            throw new IndexOutOfRangeException(String.Format("Start index ({0:d}) is out of the bounds of the list", startIndex));
        }
        if (endIndex >= listSize || endIndex < 0)
        {
            throw new IndexOutOfRangeException(String.Format("End index ({0:d}) is out of the bounds of the list", endIndex));

        }
        List<A> slicedList = new List<A>();
        while (startIndex <= endIndex)
        {
            slicedList.Add(underlyingArray[startIndex]);
            startIndex++;
        }
        return slicedList;
    }

    //Reverses the list
    public void Reverse()
    {
        int startIndex = 0;
        int endIndex = listSize - 1;
        while (startIndex < endIndex)
        {
            A itemToBeReplaced = underlyingArray[startIndex];
            underlyingArray[startIndex] = underlyingArray[endIndex];
            underlyingArray[endIndex] = itemToBeReplaced;
            startIndex++;
            endIndex--;
        }
    }

    //returns the number of items in the list
    public int Count()
    {
        return listSize;
    }

    //Returns the array of the actual list items
    A[] ValidListItemsArray()
    {
        A[] validListItemsArray = new A[listSize];
        for (int i = 0; i < listSize; i++)
        {
            validListItemsArray[i] = underlyingArray[i];
        }
        return validListItemsArray;
    }

    //Makes the list object iterable
    public IEnumerator<A> GetEnumerator()
    {
        A[] validListItems = ValidListItemsArray();
        foreach (A item in validListItems)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}