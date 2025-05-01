using System.Text.Json;
namespace ConceptOfRandom.Models.API;

public class RandomCatFacts : APIModel {
    public new readonly string Name = "Cat Facts";
    private const string url = "https://meowfacts.herokuapp.com/";
    public new readonly string Parameters;

    public class CatFact {
        public List<string> data { get; init; }
    }
    
    public override async Task<string> Get() {
        string response = await APIClient.Instance.Fetch(url, Parameters);
        CatFact fact = JsonSerializer.Deserialize<CatFact>(response)!;
        return fact.data[0];
    }
}