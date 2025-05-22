using ConceptOfRandom.Models.Simulation.Timer;
using ConceptOfRandom.View;
using System.Threading;

namespace ConceptOfRandom.view;

public class TimerIntegration : MenuOutline
{
    bool isRunning = true;
    public void StartTimer()
    {
        while (isRunning)
        {
            Clear();
            CreateBorder();
            Text((Width - 22) / 2, 3, "Select a timer option:");
            Text((Width - 12) / 2, 5, "1. 5 seconds");
            Text((Width - 13) / 2, 6, "2. 10 seconds");
            Text((Width - 14) / 2, 7, "3. 100 seconds");
            Text((Width - 37) / 2, 9, "Press M to return to the main menu.");
            Render();

            var key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                RunTimer(5);
            }
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                RunTimer(10);
            }
            else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
            {
                RunTimer(100);
            }
            else if (key == ConsoleKey.M)
            {
                Clear();
                RenderMenu();
                return; // Exit to the main menu
            }
        }
        RenderMenu();
    }

    private void RunTimer(int seconds)
    {
        var timerTracker = new TimerTracker(seconds * 1000);
        var observer = new TimerObserver();

        timerTracker.Subscribe(observer);
        timerTracker.Start();

        observer.TimerStatusChanged += status =>
        {
            if (status == TimerStatus.Completed)
            {
                RenderMenu();
                isRunning = false;
            }

            Render();
        };
    }
}