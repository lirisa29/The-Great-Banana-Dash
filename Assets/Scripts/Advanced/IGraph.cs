using System;
using System.Collections.Generic;

public interface IGraph<T>{
   bool AddVertex(T data);
    bool AddEdge(T from, T to);
    List<T> GetConnectedVerticies(T vertex);
    void RemoveVertex(T vertex);
    bool ContainsVertex(T vertex);
}