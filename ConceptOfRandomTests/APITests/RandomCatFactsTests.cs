using ConceptOfRandom.Models.API;
using Xunit.Abstractions;

namespace ConceptOfRandomTests;

public class RandomCatFactsTests(ITestOutputHelper output) {
    private readonly RandomCatFacts randomCatFacts = new();

    [Fact]
    public void CanConstructNewCatFactsObject() {
        RandomCatFacts newCatFacts = new RandomCatFacts();
        Assert.NotNull(newCatFacts);
    }
    
    [Fact]
    public void RandomCatFactsIsNamed() {
        bool hasName = randomCatFacts.Name.Length > 0;
        Assert.True(hasName);
    }

    [Fact]
    public async void CanGetRandomCatFact() {
        try {
            string fact = await randomCatFacts.Get();
            Assert.NotNull(fact);
            output.WriteLine(fact);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }
}