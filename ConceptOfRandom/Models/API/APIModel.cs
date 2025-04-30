namespace ConceptOfRandom.Models.API;

public abstract class APIModel {
    public string Name;
    private string URL;
    private string parameters;
    
    public abstract Task<string> Get();
}