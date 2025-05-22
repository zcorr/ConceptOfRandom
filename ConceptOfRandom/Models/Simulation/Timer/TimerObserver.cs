namespace ConceptOfRandom.Models.Simulation.Timer;

public class TimerObserver : IObserver<TimerStatus> {
    
    public event Action<TimerStatus> TimerStatusChanged;
    public void OnNext(TimerStatus status)
    {
        TimerStatusChanged?.Invoke(status);    
    }

    public void OnError(Exception error) {
        TimerStatusChanged?.Invoke(TimerStatus.NotStarted);
    }

    public void OnCompleted() {
        TimerStatusChanged?.Invoke(TimerStatus.Completed);
    }
}