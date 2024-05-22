using System;
using System.Collections.Generic;

public class Program
{
    static int[] dx = new int[] { -1, 1, 0, 0 };
    static int[] dy = new int[] { 0, 0, -1, 1 };

    public static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());
        while (t-- > 0)
        {
            string[] dimensions = Console.ReadLine().Split();
            int n = int.Parse(dimensions[0]);
            int m = int.Parse(dimensions[1]);
            int[,] heights = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine().Split();
                for (int j = 0; j < m; j++)
                {
                    heights[i, j] = int.Parse(row[j]);
                }
            }

            Console.WriteLine(CalculateWaterVolume(heights, n, m));

            if (t > 0)
                Console.ReadLine();
        }
    }

    private static int CalculateWaterVolume(int[,] heights, int n, int m)
    {
        var pq = new SortedSet<(int height, int x, int y)>(Comparer<(int, int, int)>.Create((a, b) =>
        {
            int comparison = a.height.CompareTo(b.height);
            if (comparison != 0) return comparison;
            comparison = a.x.CompareTo(b.x);
            if (comparison != 0) return comparison;
            return a.y.CompareTo(b.y);
        }));

        int[,] minHeight = new int[n, m];
        bool[,] visited = new bool[n, m];

        for (int i = 0; i < n; i++)
        {
            pq.Add((heights[i, 0], i, 0));
            pq.Add((heights[i, m - 1], i, m - 1));
            visited[i, 0] = true;
            visited[i, m - 1] = true;
        }
        for (int j = 0; j < m; j++)
        {
            pq.Add((heights[0, j], 0, j));
            pq.Add((heights[n - 1, j], n - 1, j));
            visited[0, j] = true;
            visited[n - 1, j] = true;
        }

        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            int curHeight = current.height;
            int x = current.x;
            int y = current.y;

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (nx >= 0 && nx < n && ny >= 0 && ny < m && !visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    int nextHeight = Math.Max(curHeight, heights[nx, ny]);
                    minHeight[nx, ny] = nextHeight;
                    pq.Add((nextHeight, nx, ny));
                }
            }
        }

        int totalWater = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                totalWater += minHeight[i, j] - heights[i, j];
            }
        }

        return totalWater;
    }
}
