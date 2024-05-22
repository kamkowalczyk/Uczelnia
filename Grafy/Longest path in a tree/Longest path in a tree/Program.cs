
class LongestPathInTree
{
    static List<int>[] graph;
    static bool[] visited;
    static int farthestNode = 0;
    static int maxDistance = 0;

    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        graph = new List<int>[N + 1];
        visited = new bool[N + 1];

        for (int i = 0; i <= N; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 1; i < N; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            int u = int.Parse(input[0]);
            int v = int.Parse(input[1]);

            graph[u].Add(v);
            graph[v].Add(u);
        }

        DFS(1, 0);

        Array.Fill(visited, false);
        maxDistance = 0;

        DFS(farthestNode, 0);

        Console.WriteLine(maxDistance);
    }

    static void DFS(int node, int distance)
    {
        visited[node] = true;

        if (distance > maxDistance)
        {
            maxDistance = distance;
            farthestNode = node;
        }

        foreach (int neighbor in graph[node])
        {
            if (!visited[neighbor])
            {
                DFS(neighbor, distance + 1);
            }
        }
    }
}