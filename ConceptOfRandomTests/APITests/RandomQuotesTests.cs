using ConceptOfRandom.Models.API;
using Xunit.Abstractions;

namespace ConceptOfRandomTests;

public class RandomQuotesTests(ITestOutputHelper output) {
    private readonly RandomQuotes randomQuotes = new();

    [Fact]
    public void CanConstructNewQuotesObject() {
        RandomQuotes newQuotes = new RandomQuotes();
        Assert.NotNull(newQuotes);
    }
    
    [Fact]
    public void RandomQuotesIsNamed() {
        bool hasName = randomQuotes.Name.Length > 0;
        Assert.True(hasName);
    }

    [Fact]
    public async void CanGetRandomQuote() {
        try {
            string fact = await randomQuotes.Get();
            Assert.NotNull(fact);
            output.WriteLine(fact);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }
}