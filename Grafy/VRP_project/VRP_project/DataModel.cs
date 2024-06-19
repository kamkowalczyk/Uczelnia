public class DataModel
{
    public int VehicleNumber { get; set; } = 3;
    public int Depot { get; set; } = 0;

    public double[,] Locations { get; set; } = {
        { 50.0647, 19.9450 }, // Krakow Main Square
        { 50.0678, 19.9123 },
        { 50.0617, 19.9388 },
        { 50.0556, 19.9276 },
        { 50.0712, 19.9038 },
        { 50.0483, 19.9456 },
        { 50.0355, 19.9812 },
        { 50.0645, 19.9491 },
        { 50.0802, 19.9202 },
        { 50.0418, 19.9504 },
        { 50.0562, 19.9736 }
    };
    public int[] Demands { get; set; } = { 0, 1, 1, 2, 4, 2, 4, 8, 8, 1, 2 };
    public int[,] TimeWindows { get; set; } = {
        { 0, 1000 }, 
        { 0, 500 },
        { 100, 600 },
        { 200, 700 },
        { 300, 800 },
        { 400, 900 },
        { 500, 1000 },
        { 600, 1100 },
        { 700, 1200 },
        { 800, 1300 },
        { 900, 1400 }
    };

    public int[,] DistanceMatrix { get; set; }
}
