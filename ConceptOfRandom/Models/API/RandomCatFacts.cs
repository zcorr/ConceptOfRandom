using System.Text.Json;
namespace ConceptOfRandom.Models.API;

public class RandomCatFacts : APIModel {
    public readonly string Name = "Cat Facts";
    private readonly string URL = "https://meowfacts.herokuapp.com/";
    public readonly string Parameters;

    public class CatFact() {
        public List<string> data { get; init; }
    }
    
    public override async Task<string> Get() {
        string response = await APIClient.Instance.Fetch(URL, Parameters);
        CatFact fact = JsonSerializer.Deserialize<CatFact>(response)!;
        return fact.data[0];
    }
}