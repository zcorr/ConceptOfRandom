namespace ConceptOfRandom.Models.API;

public class APIClient {
    private static APIClient instance;
    private static HttpClient client;

    public static APIClient Instance
    {
        get
        {
            if(instance == null) instance = new APIClient();
            return instance;
        }
    }

    private APIClient() {
        client = new HttpClient();
    }
    
    public async Task<string> Fetch(string url, string parameters) {
        string parameterString = String.IsNullOrWhiteSpace(parameters) ? "" : "?"+parameters;
        client.BaseAddress = new Uri(url);
        using HttpResponseMessage response = await client.GetAsync(parameterString);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }
    
}