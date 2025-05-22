using ConceptOfRandom.Models.Simulation.Timer;
using ConceptOfRandom.view;

namespace ConceptOfRandomTests.SimulationTests.Timer;

public class TimerTests {

    private class MockCanvas : IConsoleCanvas
    {
        public List<string> RenderedText = new List<string>();

        public int Width => 80; // Example width
        public void Clear() { }
        public void CreateBorder() { }
        public void Text(int x, int y, string text) => RenderedText.Add(text);
        public void Render() { }
        public bool AutoResize { get => false; set { } }
    }

    private class TestObserver : IObserver<TimerStatus> {
        public List<TimerStatus> ReceivedStatuses = new List<TimerStatus>();
        public void OnCompleted() => ReceivedStatuses.Add(TimerStatus.Completed);
        public void OnError(Exception error) => ReceivedStatuses.Add(TimerStatus.NotStarted);
        public void OnNext(TimerStatus timerStatusValue) => ReceivedStatuses.Add(timerStatusValue);
    }

    [Fact]
    public void StartNotifiesStartedStatus() {
        var tracker = new TimerTracker(100);
        var observer = new TestObserver();
        tracker.Subscribe(observer);
        tracker.Start();
        Thread.Sleep(150); // Allow time for the timer to start
        Assert.Contains(TimerStatus.Started, observer.ReceivedStatuses);
    }

    [Fact]
    public void ManuallyTriggeringTimerNotifiesCompletedStatus() {
        var tracker = new TimerTracker();
        var observer = new TestObserver();
        tracker.Subscribe(observer);
        tracker.TriggerTimerManually();
        Assert.Contains(TimerStatus.Completed, observer.ReceivedStatuses);
    }

    [Fact]
    public void UnsubscribeWorksForObserver() {
        var tracker = new TimerTracker();
        var observer = new TestObserver();
        var subscription = tracker.Subscribe(observer);
        subscription.Dispose();
        tracker.TriggerTimerManually();
        Assert.Empty(observer.ReceivedStatuses);
    }

    [Fact]
    public void OnNextWorksForTimerObserver() {
        var observer = new TimerObserver();
        var statusList = new List<TimerStatus>();
        observer.TimerStatusChanged += status => statusList.Add(status);

        observer.OnNext(TimerStatus.Started);
        Assert.Contains(TimerStatus.Started, statusList);
    }

    [Fact]
    public void OnErrorWorksForTimerObserver() {
        var observer = new TimerObserver();
        var statusList = new List<TimerStatus>();
        observer.TimerStatusChanged += status => statusList.Add(status);

        observer.OnError(new Exception("Test error"));
        Assert.Contains(TimerStatus.NotStarted, statusList);
    }

    [Fact]
    public void OnCompletedWorksForTimerObserver() {
        var observer = new TimerObserver();
        var statusList = new List<TimerStatus>();
        observer.TimerStatusChanged += status => statusList.Add(status);

        observer.OnCompleted();
        Assert.Contains(TimerStatus.Completed, statusList);
    }
}