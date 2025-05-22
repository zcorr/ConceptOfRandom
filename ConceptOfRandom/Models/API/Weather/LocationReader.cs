namespace ConceptOfRandom.Models.API;

public static class LocationReader {
    public static string GetRandomLocationFromFile() {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Models/API/Weather/WeatherLocations.txt");
        string[] lines = File.ReadAllLines(path);
        return lines[Random.Shared.Next(0, lines.Length)];
    }
}