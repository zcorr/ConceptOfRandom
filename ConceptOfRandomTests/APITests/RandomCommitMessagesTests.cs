using ConceptOfRandom.Models.API;
using Xunit.Abstractions;

namespace ConceptOfRandomTests;

public class RandomCommitMessagesTests(ITestOutputHelper output) {
    private readonly RandomCommitMessages RandomCommitMessages = new();

    [Fact]
    public void CanConstructNewCommitMessagesObject() {
        RandomCommitMessages newCommitMessages = new RandomCommitMessages();
        Assert.NotNull(newCommitMessages);
    }
    
    [Fact]
    public void RandomCommitMessagesIsNamed() {
        bool hasName = RandomCommitMessages.Name.Length > 0;
        Assert.True(hasName);
    }

    [Fact]
    public async void CanGetRandomCommitMessage() {
        try {
            string fact = await RandomCommitMessages.Get();
            Assert.NotNull(fact);
            output.WriteLine(fact);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }
}