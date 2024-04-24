namespace grafy_lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var g = new GraphMatrix<char>(new char[] { 'A', 'B', 'C', 'D' });
            g.AddEdge('A', 'B');
            g.AddEdge('A', 'C');
            g.AddEdge('A', 'D');
            g.AddEdge('B', 'C');
            g.AddEdge('B', 'D');

            g.Print();

            Console.WriteLine(string.Join(",", g.Neighbours));

            var wynikPrzegladania = g.BFS('A');
        }
    }
}
