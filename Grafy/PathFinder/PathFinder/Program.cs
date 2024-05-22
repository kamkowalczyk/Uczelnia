using System;
using System.Collections.Generic;

public class PathFinder
{
    public static void Main()
    {
        string[] lines = new string[]
        {
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "...........###......#####",
            "......###...........#####",
            "...#..###..###......#####",
            "###############.#######..",
            "###############.#######.#",
            "###############.#######..",
            "................########.",
            "................#######..",
            "...#######..##.......##.#",
            "...#######..##.......##..",
            "...##...........########.",
            "#####...........#######..",
            "#####...##..##..#...#...#",
            "#####...##..##....#...#.."
        };

        var path = FindShortestPath(lines);

        if (path == null)
        {
            Console.WriteLine("No path found.");
        }
        else
        {
            foreach (var (x, y) in path)
            {
                lines[x] = lines[x].Substring(0, y) + 'o' + lines[x].Substring(y + 1);
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }

    public static List<(int, int)> FindShortestPath(string[] map)
    {
        int rows = map.Length;
        int cols = map[0].Length;
        var start = (x: 0, y: 0);
        var goal = (x: rows - 1, y: cols - 1);
        var directions = new List<(int, int)> { (1, 0), (0, 1), (-1, 0), (0, -1) };
        var queue = new Queue<(int x, int y, List<(int, int)> path)>();
        var visited = new bool[rows, cols];
        queue.Enqueue((start.x, start.y, new List<(int, int)> { start }));
        visited[start.x, start.y] = true;

        while (queue.Count > 0)
        {
            var (x, y, path) = queue.Dequeue();

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx, newY = y + dy;
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited[newX, newY] && map[newX][newY] != '#')
                {
                    var newPath = new List<(int, int)>(path) { (newX, newY) };
                    if (newX == goal.x && newY == goal.y)
                    {
                        return newPath;
                    }
                    queue.Enqueue((newX, newY, newPath));
                    visited[newX, newY] = true;
                }
            }
        }

        return null;
    }
}
