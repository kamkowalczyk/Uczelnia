
class TaskAllocation
{
    static void Main(string[] args)
    {
        int[] taskDurations = { 5, 3, 8, 6, 2 };
        int numberOfResources = 3;

        var allocation = AllocateTasks(taskDurations, numberOfResources);

        for (int i = 0; i < allocation.Length; i++)
        {
            Console.WriteLine($"Zasób {i + 1}:");
            foreach (var task in allocation[i])
            {
                Console.WriteLine($"  Zadanie o czasie trwania: {task} godzin");
            }
        }
    }

    static List<int>[] AllocateTasks(int[] tasks, int resources)
    {
        List<int>[] allocation = new List<int>[resources];
        for (int i = 0; i < resources; i++)
        {
            allocation[i] = new List<int>();
        }

        int[] sortedTasks = tasks.OrderByDescending(t => t).ToArray();

        int[] resourceTimes = new int[resources];

        foreach (var task in sortedTasks)
        {
            int minResourceIndex = Array.IndexOf(resourceTimes, resourceTimes.Min());
            allocation[minResourceIndex].Add(task);
            resourceTimes[minResourceIndex] += task;
        }

        return allocation;
    }
}