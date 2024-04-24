#nullable disable

namespace grafy_lab3
{
    //Implementacja grafu prostego, nieskierowanego, nieważonego, etykietowanego
    public class GraphMatrix<T>
    {
        //wenetrza reprezentacja grafu jako macierz sasiedztwa
        private int[,] matrix;
        private Dictionary<T, int> labelToIndex;
        private Dictionary<int, T> indexToLabel;

        //konstruktor
        public GraphMatrix(IEnumerable<T> vertices)
        {
            int n = vertices.Count();
            matrix = new int[n, n];
            labelToIndex = new Dictionary<T, int>();
            indexToLabel = new Dictionary<int, T>();
            int i = 0;
            foreach (var v in vertices)
            {
                labelToIndex[v] = i;
                indexToLabel[i] = v;
                i++;
            }
        }

        // liczba wierzchołków
        public int VerticesCount => matrix.GetLength(0);

        // liczba krawędzi
        public int EdgesCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < VerticesCount; i++)
                {
                    for (int j = 0; j < VerticesCount; j++)
                    {
                        if (matrix[i, j] == 1)
                        {
                            count++;
                        }
                    }
                }
                return count / 2;
            }
        }

        // dodanie krawędzi
        public void AddEdge(T v1, T v2)
        {
            int i1 = labelToIndex[v1];
            int i2 = labelToIndex[v2];
            matrix[i1, i2] = 1;
            matrix[i2, i1] = 1;
        }

        // usunięcie krawędzi
        public void RemoveEdge(T v1, T v2)
        {
            if (!ContainsVertex(v1) || !ContainsVertex(v2))
            {
                throw new ArgumentException("Vertex not found");
            }
            int i1 = labelToIndex[v1];
            int i2 = labelToIndex[v2];
            matrix[i1, i2] = 0;
            matrix[i2, i1] = 0;
        }

        // sprawdzenie czy istnieje węzeł
        public bool ContainsVertex(T v) => labelToIndex.ContainsKey(v);

        //drukowanie grafu
        public void Print()
        {   //lista wezłów
            Console.WriteLine("Graph matrix:");
            Console.WriteLine(string.Join(", ", labelToIndex.Keys));

            for (int i = 0; i < VerticesCount; i++)
            {
                Console.Write(indexToLabel[i] + ": ");
                for (int j = 0; j < VerticesCount; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Console.Write(indexToLabel[j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        // lista sąsiadów
        public IEnumerable<T> Neighbours(T v)
        {
            if (!ContainsVertex(v))
            {
                throw new ArgumentException("Vertex not found");
            }
            int i = labelToIndex[v];
            for (int j = 0; j < VerticesCount; j++)
            {
                if (matrix[i, j] == 1)
                {
                    yield return indexToLabel[j];
                }
            }
        }

        public IEnumerable<T> BFS(T start)
        {
            if (!ContainsVertex(start))
            {
                throw new ArgumentException("Vertex not found");
            }
            var visited = new HashSet<T>();
            var stack = new Stack<T>();
            stack.Push(start);
            while (stack.Count > 0)
            {
                var v = stack.Pop();
                if (visited.Contains(v))
                {
                    continue;
                }
                visited.Add(v);
                yield return v;
                foreach (var n in Neighbours(v))
                {
                    stack.Push(n);
                }
            }
        }   
    }
}
