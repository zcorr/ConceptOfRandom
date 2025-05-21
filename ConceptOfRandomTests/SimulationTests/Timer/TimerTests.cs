using System.Runtime.InteropServices.JavaScript;
using ConceptOfRandom.Models.Simulation.Timer;

namespace ConceptOfRandomTests.SimulationTests.Timer;

public class TimerTests {
    private class TestObserver : IObserver<TimerStatus> {
        public List<TimerStatus> ReceivedStatuses = new List<TimerStatus>();
        public void OnCompleted() {}

        public void OnError(Exception error) {}

        public void OnNext(TimerStatus timerStatusValue) {
            ReceivedStatuses.Add(timerStatusValue);
        }
    }

    [Fact]
    public void StartNotifiesStartedStatus() {
        TimerTracker tracker = new TimerTracker(100);
        TestObserver observer = new TestObserver();
        tracker.Subscribe(observer);
        tracker.Start();
        System.Threading.Thread.Sleep(150);
        Assert.Contains(TimerStatus.Started, observer.ReceivedStatuses);
    }

    [Fact]
    public void ManuallyTriggeringTimerNotifiesCompletedStatus() {
        TimerTracker tracker = new TimerTracker();
        TestObserver observer = new TestObserver();
        tracker.Subscribe(observer);
        tracker.TriggerTimerManually();
        Assert.Contains(TimerStatus.Completed, observer.ReceivedStatuses);
    }

    [Fact]
    public void UnsubscribeWorksForObserver() {
        TimerTracker tracker = new TimerTracker();
        TestObserver observer = new TestObserver();
        IDisposable subscription = tracker.Subscribe(observer);
        subscription.Dispose();
        tracker.TriggerTimerManually();
        Assert.Empty(observer.ReceivedStatuses);
    }

    [Fact]
    public void OnNextWorksForTimerObserver() {
        TimerObserver observer = new TimerObserver();
        observer.OnNext(TimerStatus.Started);
    }

    [Fact]
    public void OnErrorWorksForTimerObserver() {
        TimerObserver observer = new TimerObserver();
        observer.OnError(new Exception());
    }

    [Fact]
    public void OnCompletedWorksForTimerObserver() {
        TimerObserver observer = new TimerObserver();
        observer.OnCompleted();
    }
}