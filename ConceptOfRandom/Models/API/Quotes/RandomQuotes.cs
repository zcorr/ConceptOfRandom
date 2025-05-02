namespace ConceptOfRandom.Models.API;

public class RandomQuotes : APIModel {
    public new readonly string Name = "Random Quotes";
    private const string url = "https://api.forismatic.com/api/1.0/";
    private new readonly string Parameters = "method=getQuote&lang=en&format=text";
    
    public override async Task<string> Get() {
        // implementation note: not using JSON here because quotes use apostrophes, which break JSON parsing.
        string response = await APIClient.Instance.Fetch(url, Parameters);
        return response;
    }
}