namespace ConceptOfRandom.Models.API;

public class APIClient {
    private static readonly Lazy<APIClient> LazyInstance = new(() => new APIClient());
    private static HttpClient client;

    public static APIClient Instance => LazyInstance.Value;

    private APIClient() {
        client = new HttpClient();
    }
    
    public async Task<string> Fetch(string url, string parameters) {
        string parameterString = string.IsNullOrWhiteSpace(parameters) ? "" : "?"+parameters;
        using HttpResponseMessage response = await client.GetAsync(url + parameterString);
        response.EnsureSuccessStatusCode();
        var returnResponse = await response.Content.ReadAsStringAsync();
        return returnResponse;
    }
    
    
}