namespace ConceptOfRandom.Models.Simulation.Timer;

public class TimerObserver : IObserver<TimerStatus> {
    public void OnNext(TimerStatus status) {
        Console.WriteLine("Timer status changed to: " + status.ToString());
    }

    public void OnError(Exception error) {
        Console.WriteLine("Exception occured: " + error.Message);
    }

    public void OnCompleted() {
        Console.WriteLine("Observation completed.");
    }
}