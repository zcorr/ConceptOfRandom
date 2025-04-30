using System.Text.Json;

namespace ConceptOfRandom.Models.API;

public class RandomUselessFacts : APIModel {
    public new readonly string Name = "Useless Facts";
    private readonly string url = "https://uselessfacts.jsph.pl/api/v2/facts/random";
    public new readonly string Parameters;

    public class UselessFact {
        public string text { get; init; }
    }
    
    public override async Task<string> Get() {
        string response = await APIClient.Instance.Fetch(url, Parameters);
        UselessFact fact = JsonSerializer.Deserialize<UselessFact>(response)!;
        return fact.text;
    }
}