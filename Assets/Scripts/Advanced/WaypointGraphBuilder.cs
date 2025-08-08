using UnityEngine;
using System.Collections.Generic;

public class WaypointGraphBuilder : MonoBehaviour
{
    // List of vertex transforms (waypoints in the scene)
    public List<Transform> vertices = new List<Transform>();
    // List of edges that define connections between waypoints
    public List<EdgeData> edges = new List<EdgeData>();

    // The graph data structure built from the vertices and edges
    public AdjacencyList<Transform> Graph { get; private set; }

    private void Awake()
    {
        BuildGraph();
    }

    public void BuildGraph()
    {
        // Initialize a new adjacency list graph
        Graph = new AdjacencyList<Transform>();

        // Add each valid vertex (waypoint) to the graph
        foreach (var vertex in vertices)
        {
            if (vertex != null)
                Graph.AddVertex(vertex);
        }

        // Add each valid edge (connection) to the graph
        foreach (var edge in edges)
        {
            if (edge.from != null && edge.to != null)
                Graph.AddEdge(edge.from, edge.to);
        }
    }
}
