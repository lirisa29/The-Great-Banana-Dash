using System;

public class LinkedArray<T> : IList<T>
{
    private T[] array; // Underlying array that holds the data
    private const int initialCapacity = 10;

    public int Size{get; protected set;} // Number of elements currently in the list

    public bool IsEmpty{get{return Size == 0;}} // Returns true if the list has no elements

    // Constructor initializes array with a default size
    public LinkedArray(){
        array = new T[initialCapacity];
        Size = 0;
    }

    // Indexer: allows accessing and assigning elements using array-style syntax
    public T this[int index]{
        get{return FindAt(index);}
        set{Set(index, value);}
    }

    // Returns the index of the first occurrence of the value, or -1 if not found
    public int Find(T value){
        for (int i = 0; i < Size; i++){
            if (array[i].Equals(value)){
                return i;
            }
        }
        return -1;
    }

    // Returns the element at a specific index, or throws an error if out of range
    public T FindAt(int index){
        if (index < 0 || index >= Size){
            throw new IndexOutOfRangeException("invalid index");
        }

        return array[index];
    }

    // Appends a value to the end of the array, increasing capacity if needed
    public void Insert(T value){
        IncreaseCapacity();
        array[Size] = value;
        Size++;
    }

    // Inserts a value at a specified index, shifting subsequent elements
    public void InsertAt(T value, int index){
        if (index < 0 || index > Size){
            throw new IndexOutOfRangeException("Invalid Index");
        }

        IncreaseCapacity();

        // Shift elements to the right to make space
        for (int i = Size; i > index; i--) {
            array[i] = array[i-1];
        }

        array[index] = value;
        Size++;
    }

    // Sets a value at a specified index
    public T Set(int index, T value){
        if (index < 0 || index >= Size){
            throw new IndexOutOfRangeException("Invalid Index");
        }

        T oldValue = array[index];
        array[index] = value;
        return oldValue;
    }

    // Removes the first occurrence of a value and returns true if found
    public bool Remove(T value){
        int index = Find(value);
        if (index == -1){
            return false;
        }

        RemoveAt(index);
        return true;
    }

    // Removes the element at the specified index and returns it
    public T RemoveAt(int index){
        if (index < 0 || index >= Size){
            throw new IndexOutOfRangeException("Invalid Index");
        }

        T value = array[index];

        // Shift elements left to fill the gap
        for (int i = index; i < Size - 1; i++) {
            array[i] = array[i+1];
        }

        Size--;
        return value;
    }

    // Empties the list and resets the underlying array
    public void Empty(){
        array = new T[initialCapacity];
        Size = 0;
    }

    // Doubles the capacity of the array if it's full
    private void IncreaseCapacity(){
        if(Size >= array.Length){
            int newCapacity = array.Length * 2;
            T[] newArray = new T[newCapacity];
            Array.Copy(array, newArray, Size);
            array = newArray;
        }
    }
}