using System.Text.Json;

namespace ConceptOfRandom.Models.API;

public class RandomUselessFacts : APIModel {
    public readonly string Name = "Useless Facts";
    private readonly string URL = "https://uselessfacts.jsph.pl/api/v2/facts/random";
    public readonly string Parameters;

    public class UselessFact() {
        public string id { get; init; }
        public string text { get; init; }
    }
    
    public override async Task<string> Get() {
        string response = await APIClient.Instance.Fetch(URL, Parameters);
        UselessFact fact = JsonSerializer.Deserialize<UselessFact>(response)!;
        return fact.text;
    }
}