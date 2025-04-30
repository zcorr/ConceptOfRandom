using ConceptOfRandom.Models.API;
using Xunit.Abstractions;

namespace ConceptOfRandomTests;

public class RandomUselessFactsTests(ITestOutputHelper output) {
    private readonly RandomUselessFacts RandomUselessFacts = new();

    [Fact]
    public void CanConstructNewUselessFactsObject() {
        RandomUselessFacts newUselessFacts = new RandomUselessFacts();
        Assert.NotNull(newUselessFacts);
    }
    
    [Fact]
    public void RandomUselessFactsIsNamed() {
        bool hasName = RandomUselessFacts.Name.Length > 0;
        Assert.True(hasName);
    }

    [Fact]
    public async void CanGetRandomUselessFact() {
        try {
            string fact = await RandomUselessFacts.Get();
            Assert.NotNull(fact);
            output.WriteLine(fact);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }
}