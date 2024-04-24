namespace Rysowanie_GRAFU
{

    class Program
    {
        static void Main()
        {
            int t = int.Parse(Console.ReadLine().Trim());

            for (int i = 0; i < t; i++)
            {
                string graphType = Console.ReadLine().Trim();
                int n = int.Parse(Console.ReadLine().Trim()); 

                List<string> edges = new List<string>();

                for (int j = 0; j < n; j++)
                {
                    string line = Console.ReadLine().Trim();
                    string[] parts = line.Split(' ');

                    if (graphType == "g")
                    { 
                        edges.Add($"{parts[0]} -- {parts[1]};");
                    }
                    else if (graphType == "d")
                    { 
                        edges.Add($"{parts[0]} -> {parts[1]};");
                    }
                    else if (graphType == "gw")
                    { 
                        edges.Add($"{parts[0]} -- {parts[1]} [label = {parts[2]}];");
                    }
                    else if (graphType == "dw")
                    { 
                        edges.Add($"{parts[0]} -> {parts[1]} [label = {parts[2]}];");
                    }
                }

                if (graphType == "g" || graphType == "gw")
                {
                    Console.WriteLine("graph {");
                }
                else
                {
                    Console.WriteLine("digraph {");
                }
                foreach (string edge in edges)
                {
                    Console.WriteLine(edge);
                }
                Console.WriteLine("}");
            }
        }
    }
}
