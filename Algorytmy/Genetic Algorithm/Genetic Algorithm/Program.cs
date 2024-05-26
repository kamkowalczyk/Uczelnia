namespace Genetic_Algorithm
{
    class Program
    {
        public static Random random = new Random();
        static int populationSize = 200;
        static int numberOfGenerations = 1000;
        static double crossoverRate = 0.8;
        static double mutationRate = 0.01;
        static int chromosomeLength = 10;
        static int elitismCount = 2;

        static void Main()
        {
            List<Chromosome> population = InitializePopulation();

            for (int generation = 0; generation < numberOfGenerations; generation++)
            {
                population = EvolvePopulation(population);

                Chromosome best = population.OrderByDescending(c => c.Fitness).First();
                Console.WriteLine($"Generation {generation + 1}: Best Fitness = {best.Fitness}");
            }
        }

        static List<Chromosome> InitializePopulation()
        {
            List<Chromosome> population = new List<Chromosome>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Chromosome(chromosomeLength));
            }
            return population;
        }

        static List<Chromosome> EvolvePopulation(List<Chromosome> population)
        {
            List<Chromosome> newPopulation = new List<Chromosome>();

            List<Chromosome> sortedPopulation = population.OrderByDescending(c => c.Fitness).ToList();
            newPopulation.AddRange(sortedPopulation.Take(elitismCount));

            while (newPopulation.Count < populationSize)
            {
                Chromosome parent1 = SelectParent(population);
                Chromosome parent2 = SelectParent(population);

                Chromosome offspring1, offspring2;
                if (random.NextDouble() < crossoverRate)
                {
                    (offspring1, offspring2) = Crossover(parent1, parent2);
                }
                else
                {
                    offspring1 = new Chromosome(parent1.Genes.ToArray());
                    offspring2 = new Chromosome(parent2.Genes.ToArray());
                }

                Mutate(offspring1);
                Mutate(offspring2);

                newPopulation.Add(offspring1);
                newPopulation.Add(offspring2);
            }

            return newPopulation;
        }

        static Chromosome SelectParent(List<Chromosome> population)
        {
            int tournamentSize = 5;
            List<Chromosome> tournament = new List<Chromosome>();
            for (int i = 0; i < tournamentSize; i++)
            {
                tournament.Add(population[random.Next(populationSize)]);
            }
            return tournament.OrderByDescending(c => c.Fitness).First();
        }

        static (Chromosome, Chromosome) Crossover(Chromosome parent1, Chromosome parent2)
        {
            int crossoverPoint = random.Next(chromosomeLength);
            Chromosome offspring1 = new Chromosome(parent1.Genes.Take(crossoverPoint).Concat(parent2.Genes.Skip(crossoverPoint)).ToArray());
            Chromosome offspring2 = new Chromosome(parent2.Genes.Take(crossoverPoint).Concat(parent1.Genes.Skip(crossoverPoint)).ToArray());
            return (offspring1, offspring2);
        }

        static void Mutate(Chromosome chromosome)
        {
            for (int i = 0; i < chromosomeLength; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    chromosome.Genes[i] = chromosome.Genes[i] == 0 ? 1 : 0;
                }
            }
        }
    }

    class Chromosome
    {
        public int[] Genes { get; private set; }
        public double Fitness => EvaluateFitness();

        public Chromosome(int length)
        {
            Genes = new int[length];
            for (int i = 0; i < length; i++)
            {
                Genes[i] = Program.random.Next(2);
            }
        }

        public Chromosome(int[] genes)
        {
            Genes = genes;
        }

        private double EvaluateFitness()
        {
            return Genes.Sum();
        }
    }
}
