using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
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
        int totalWater = 0;
        var pq = new SortedSet<(int, int, int)>(Comparer<(int, int, int)>.Create((a, b) =>
        {
            int comp = a.Item1.CompareTo(b.Item1);
            if (comp != 0) return comp;
            comp = a.Item2.CompareTo(b.Item2);
            if (comp != 0) return comp;
            return a.Item3.CompareTo(b.Item3);
        }));

        int[,] minHeight = new int[n, m];
        bool[,] visited = new bool[n, m];

        for (int i = 0; i < n; i++)
        {
            if (!visited[i, 0]) pq.Add((heights[i, 0], i, 0));
            if (!visited[i, m - 1]) pq.Add((heights[i, m - 1], i, m - 1));
            visited[i, 0] = visited[i, m - 1] = true;
            minHeight[i, 0] = heights[i, 0];
            minHeight[i, m - 1] = heights[i, m - 1];
        }
        for (int j = 0; j < m; j++)
        {
            if (!visited[0, j]) pq.Add((heights[0, j], 0, j));
            if (!visited[n - 1, j]) pq.Add((heights[n - 1, j], n - 1, j));
            visited[0, j] = visited[n - 1, j] = true;
            minHeight[0, j] = heights[0, j];
            minHeight[n - 1, j] = heights[n - 1, j];
        }

        while (pq.Count > 0)
        {
            var (h, x, y) = pq.Min;
            pq.Remove(pq.Min);
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i], ny = y + dy[i];
                if (nx >= 0 && nx < n && ny >= 0 && ny < m && !visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    minHeight[nx, ny] = Math.Max(heights[nx, ny], minHeight[x, y]);
                    pq.Add((minHeight[nx, ny], nx, ny));
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (minHeight[i, j] > heights[i, j])
                    totalWater += minHeight[i, j] - heights[i, j];
            }
        }

        return totalWater;
    }

    static int[] dx = new int[] { -1, 1, 0, 0 };
    static int[] dy = new int[] { 0, 0, -1, 1 };
}
