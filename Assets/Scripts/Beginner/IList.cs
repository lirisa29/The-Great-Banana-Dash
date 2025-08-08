public interface IList<T>{
    int Size{get;}
    bool IsEmpty{ get; }
    T this[int index] {get; set;}
    T FindAt(int index);
    int Find(T value);
    void Insert(T value);
    void InsertAt(T value, int index);
    T Set(int index, T value);
    bool Remove(T value);
    T RemoveAt(int index);
    void Empty();
}