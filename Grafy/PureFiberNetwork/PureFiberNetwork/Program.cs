using System;
using System.Collections.Generic;

class PureFiberNetwork
{
    static Dictionary<string, HashSet<string>> connections = new Dictionary<string, HashSet<string>>();

    static void Main()
    {
        string input;
        while ((input = Console.ReadLine()) != null)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];
            string IP1 = parts[1];
            string IP2 = parts[2];

            if (command == "B")
            {
                BuildConnection(IP1, IP2);
            }
            else if (command == "T")
            {
                Console.WriteLine(TestConnection(IP1, IP2) ? "T" : "N");
            }
        }
    }

    static void BuildConnection(string IP1, string IP2)
    {
        if (!connections.ContainsKey(IP1))
            connections[IP1] = new HashSet<string>();
        if (!connections.ContainsKey(IP2))
            connections[IP2] = new HashSet<string>();

        connections[IP1].Add(IP2);
        connections[IP2].Add(IP1);
    }

    static bool TestConnection(string IP1, string IP2)
    {
        if (!connections.ContainsKey(IP1) || !connections.ContainsKey(IP2))
            return false;

        if (IP1 == IP2)
            return true;

        Queue<string> queue = new Queue<string>();
        HashSet<string> visited = new HashSet<string>();
        queue.Enqueue(IP1);
        visited.Add(IP1);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            foreach (string neighbor in connections[current])
            {
                if (neighbor == IP2)
                    return true;

                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }
}
