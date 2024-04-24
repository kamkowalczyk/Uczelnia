using System.Xml.Linq;
public class Node
{
    public int val;
    public IList<Node> neighbors;

    public Node()
    {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val)
    {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }
}
public class Solution
{
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        var visited = new Dictionary<Node, Node>();
        var queue = new Queue<Node>();
        queue.Enqueue(node);
        visited[node] = new Node(node.val);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in current.neighbors)
            {

                if (!visited.ContainsKey(neighbor))
                {
                    visited[neighbor] = new Node(neighbor.val);
                    queue.Enqueue(neighbor);
                }

                visited[current].neighbors.Add(visited[neighbor]);
            }
        }

        return visited[node];
    }
}
