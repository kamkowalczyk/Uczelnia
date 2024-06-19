using Google.OrTools.ConstraintSolver;

class VRP
{
    static void Main(string[] args)
    {
        var data = new DataModel();

        var distanceMatrixTask = OSRMClient.CalculateDistanceMatrix(data.Locations);
        distanceMatrixTask.Wait();
        data.DistanceMatrix = distanceMatrixTask.Result;

        RoutingIndexManager manager = new RoutingIndexManager(data.DistanceMatrix.GetLength(0), data.VehicleNumber, data.Depot);

        RoutingModel routing = new RoutingModel(manager);

        int transitCallbackIndex = routing.RegisterTransitCallback((long fromIndex, long toIndex) => {
            var fromNode = manager.IndexToNode(fromIndex);
            var toNode = manager.IndexToNode(toIndex);
            return data.DistanceMatrix[fromNode, toNode];
        });
        routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

        routing.AddDimension(
            transitCallbackIndex,
            0,
            100,
            true,
            "Capacity");

        for (int i = 1; i < data.TimeWindows.GetLength(0); ++i)
        {
            routing.AddDimension(
                transitCallbackIndex,
                data.TimeWindows[i, 0],
                data.TimeWindows[i, 1],
                false,
                "Time");
        }

        RoutingSearchParameters searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
        searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

        Assignment solution = routing.SolveWithParameters(searchParameters);

        if (solution != null)
        {
            PrintSolution(routing, manager, solution);
        }
        else
        {
            Console.WriteLine("No solution found.");
        }
    }

    static void PrintSolution(RoutingModel routing, RoutingIndexManager manager, Assignment solution)
    {
        Console.WriteLine("Optimal routes:");
        long totalDistance = 0;
        for (int i = 0; i < routing.Vehicles(); ++i)
        {
            Console.WriteLine($"Vehicle route {i + 1}:");
            long routeDistance = 0;
            var index = routing.Start(i);
            while (routing.IsEnd(index) == false)
            {
                Console.Write($"{manager.IndexToNode(index)} -> ");
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
            Console.WriteLine($"{manager.IndexToNode(index)}");
            totalDistance += routeDistance;
            Console.WriteLine($"Route distance: {routeDistance} m");
        }
        Console.WriteLine($"Total distance: {totalDistance} m");
    }
}
