using System;

public class StackArray<T>{
    private T[] stack; // The array that stores the stack elements
    private int count; // The current count of elements in the stack
    private const int initialCapacity = 10; // Initial capacity of the stack
    
    // Constructor: initialises the stack with default capacity
    public StackArray()
    {
        count = 0;
        stack = new T[initialCapacity]; // Default size of 10
    }

    public void Push(T value)
    {
        // Resize the array if necessary
        if (count == stack.Length){
            Resize();
        }
        
        stack[count++] = value; // Adds an item and increases count
    }
 
    public T Pop()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        T item = stack[--count]; // Decreases count and returns the item
        stack[count] = default(T); // Clears the element
        return item;
    }

    public T Peek(){
        if (count == 0){
            throw new InvalidOperationException("Stack is empty");
        }

        return stack[count - 1];
    }

    // Returns the number of elements in the stack
    public int Count(){
        return count;
    }

    // Resizes array to double size
    private void Resize(){
        int newSize = stack.Length * 2;
        T[] newArray = new T[newSize];
        Array.Copy(stack, newArray, count);
        stack = newArray;
    }
}