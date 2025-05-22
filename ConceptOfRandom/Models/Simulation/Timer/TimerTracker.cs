using System.Timers;

namespace ConceptOfRandom.Models.Simulation.Timer;

public class TimerTracker: IObservable<TimerStatus> {
    private const int DEFAULT_INTERVAL = 2000;
    private List<IObserver<TimerStatus>> observers;
    private readonly System.Timers.Timer timer;
    public TimerTracker(double intervalInMilliseconds = DEFAULT_INTERVAL) {
        observers = new List<IObserver<TimerStatus>>();
        timer = new System.Timers.Timer(intervalInMilliseconds);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = false;
    }

    public void Start() {
        NotifyObservers(TimerStatus.Started);
        timer.Start();
    }

    public void TriggerTimerManually() {
        OnTimedEvent(this, null);
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        timer.Stop();
        NotifyObservers(TimerStatus.Completed);
    }

    public IDisposable Subscribe(IObserver<TimerStatus> observer) {
        if (!observers.Contains(observer)) {
            observers.Add(observer);
        }
        return new Unsubscriber(observers, observer);
    }

    private void NotifyObservers(TimerStatus status) {
        foreach (var observer in observers) {
            observer.OnNext(status);
        }
    }
    
    private class Unsubscriber : IDisposable {
        private List<IObserver<TimerStatus>> observers;
        private IObserver<TimerStatus> observer;

        public Unsubscriber(List<IObserver<TimerStatus>> observers, IObserver<TimerStatus> observer) {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose() {
            if (observer != null && observers.Contains(observer)) {
                observers.Remove(observer);
            }
        }
    }
}

