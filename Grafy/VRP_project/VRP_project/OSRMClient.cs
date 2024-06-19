public class OSRMClient
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<int[,]> CalculateDistanceMatrix(double[,] locations)
    {
        int locationCount = locations.GetLength(0);
        int[,] distanceMatrix = new int[locationCount, locationCount];

        for (int i = 0; i < locationCount; i++)
        {
            for (int j = 0; j < locationCount; j++)
            {
                if (i == j)
                {
                    distanceMatrix[i, j] = 0;
                }
                else
                {
                    string url = $"http://router.project-osrm.org/route/v1/driving/{locations[i, 1].ToString().Replace(",", ".")},{locations[i, 0].ToString().Replace(",", ".")};{locations[j, 1].ToString().Replace(",", ".")},{locations[j, 0].ToString().Replace(",", ".")}?overview=false";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var route = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
                        distanceMatrix[i, j] = (int)route.routes[0].legs[0].distance;
                    }
                    else
                    {
                        throw new HttpRequestException($"Request to OSRM failed with status code {response.StatusCode}");
                    }
                }
            }
        }

        return distanceMatrix;
    }
}