namespace ConceptOfRandom.Models.API;

public class RandomCommitMessages : APIModel {
    public new readonly string Name = "Random Commit Message";
    private const string url = "https://whatthecommit.com/index.txt";
    private new readonly string Parameters = "";
    
    public override async Task<string> Get() {
        // implementation note: not using JSON here because quotes use apostrophes, which break JSON parsing.
        string response = await APIClient.Instance.Fetch(url, Parameters);
        return response;
    }
}