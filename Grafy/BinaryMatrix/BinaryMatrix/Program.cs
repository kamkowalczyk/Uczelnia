namespace BinaryMatrix
{
    class Program
    {
        static void Main()
        {
            var dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int m = dimensions[0];
            int n = dimensions[1];
   
            var rowSums = Console.ReadLine().Split().Select(int.Parse).ToArray();
          
            var colSums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            long rowSumTotal = rowSums.Sum();
            long colSumTotal = colSums.Sum();

            if (rowSumTotal == colSumTotal)
            {
                Console.WriteLine("TAK");
            }
            else
            {
                Console.WriteLine("NIE");
            }
        }
    }
}
