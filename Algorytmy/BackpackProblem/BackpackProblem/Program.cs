class BackpackProblem
{
    const int BackpackCapacity = 2500;
    const int NumberOfItems = 100;
    const int MinItemVolume = 10;
    const int MaxItemVolume = 90;
    static Random random = new Random();

    static int[] itemsVolume = new int[NumberOfItems];

    static void Main(string[] args)
    {
        for (int i = 0; i < NumberOfItems; i++)
        {
            itemsVolume[i] = random.Next(MinItemVolume, MaxItemVolume + 1);
        }

        var bestSolution = EvolutionaryAlgorithm(300000);
        var bestEvaluation = EvaluateIndividual(bestSolution);

        Console.WriteLine("Lista przedmiotów:");
        for (int i = 0; i < itemsVolume.Length; i++)
        {
            Console.WriteLine($"Przedmiot {i + 1}: Objętość - {itemsVolume[i]}");
        }

        Console.WriteLine("Najlepsze rozwiązanie: " + string.Join("", bestSolution.Select(b => b.ToString())));
        Console.WriteLine($"Wartość najlepszego rozwiązania: {bestEvaluation}");
    }

    static int[] InitializeIndividual()
    {
        return Enumerable.Range(0, NumberOfItems)
            .Select(_ => random.Next(0, 2))
            .ToArray();
    }

    static int[] MutateIndividual(int[] individual)
    {
        var newIndividual = (int[])individual.Clone();
        int mutationIndex = random.Next(NumberOfItems);
        newIndividual[mutationIndex] = 1 - newIndividual[mutationIndex];
        return newIndividual;
    }

    static int EvaluateIndividual(int[] individual)
    {
        int volume = Enumerable.Range(0, NumberOfItems)
            .Where(i => individual[i] == 1)
            .Sum(i => itemsVolume[i]);
        return volume > BackpackCapacity ? 0 : volume;
    }

    static int[] EvolutionaryAlgorithm(int numberOfGenerations)
    {
        var bestSolution = InitializeIndividual();
        int bestEvaluation = EvaluateIndividual(bestSolution);
        for (int generation = 0; generation < numberOfGenerations; generation++)
        {
            var offspring = MutateIndividual(bestSolution);
            int newEvaluation = EvaluateIndividual(offspring);
            if (newEvaluation > bestEvaluation)
            {
                bestSolution = offspring;
                bestEvaluation = newEvaluation;
            }
            Console.WriteLine($"Generation {generation + 1}: Best solution: {string.Join("", bestSolution.Select(b => b.ToString()))}, Value: {bestEvaluation}");
        }
        return bestSolution;
    }
}
