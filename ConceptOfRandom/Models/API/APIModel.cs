namespace ConceptOfRandom.Models.API;

public abstract class APIModel {
    public readonly string Name;
    private readonly string url;
    public readonly string Parameters;
    
    public abstract Task<string> Get();
}