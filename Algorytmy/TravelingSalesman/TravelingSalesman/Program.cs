class TravelingSalesman
{
    private int[,] distances;
    private int numberOfCities;

    public TravelingSalesman(int[,] distances)
    {
        this.distances = distances;
        numberOfCities = distances.GetLength(0);
    }

    public int[] FindShortestPath()
    {
        // Start with a random tour
        int[] currentPath = Enumerable.Range(0, numberOfCities).ToArray();
        Random rng = new Random();
        currentPath = currentPath.OrderBy(x => rng.Next()).ToArray();

        int currentDistance = CalculatePathDistance(currentPath);

        // Perform 2-opt search
        bool improvement = true;
        while (improvement)
        {
            improvement = false;
            for (int i = 0; i < numberOfCities - 1; i++)
            {
                for (int k = i + 1; k < numberOfCities; k++)
                {
                    int[] newPath = TwoOptSwap(currentPath, i, k);
                    int newDistance = CalculatePathDistance(newPath);
                    if (newDistance < currentDistance)
                    {
                        currentDistance = newDistance;
                        currentPath = newPath;
                        improvement = true;
                    }
                }
            }
        }

        return currentPath;
    }

    private int[] TwoOptSwap(int[] path, int i, int k)
    {
        int[] newPath = new int[numberOfCities];
        Array.Copy(path, 0, newPath, 0, numberOfCities);

        // Perform 2-opt swap
        while (i < k)
        {
            int temp = newPath[i];
            newPath[i] = newPath[k];
            newPath[k] = temp;
            i++;
            k--;
        }

        return newPath;
    }

    private int CalculatePathDistance(int[] path)
    {
        int totalDistance = 0;
        for (int i = 0; i < numberOfCities - 1; i++)
        {
            totalDistance += distances[path[i], path[i + 1]];
        }
        totalDistance += distances[path[numberOfCities - 1], path[0]];
        return totalDistance;
    }
}

class Program
{
    static void Main()
    {
        int[,] distances = {
            { 0, 2, 9, 10 },
            { 1, 0, 6, 4 },
            { 15, 7, 0, 8 },
            { 6, 3, 12, 0 }
        };

        TravelingSalesman tsp = new TravelingSalesman(distances);
        int[] path = tsp.FindShortestPath();
        Console.WriteLine("Shortest path: " + string.Join(" -> ", path));
    }
}
