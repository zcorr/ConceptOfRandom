namespace ConceptOfRandom;
using ConceptOfRandom.view;

class Program
{
    
    static void Main(string[] args) {
        var timer = System.Diagnostics.Stopwatch.StartNew();
        long count = 0;
        
        Console.CursorVisible = true;
        var program = new MenuOutline();
        program.AutoResize = true;
        program.CreateBorder();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                program.HandleInput(key);
                
                if ((key == ConsoleKey.Q) || (key == ConsoleKey.D4))
                {
                    break;
                } 
                if (key == ConsoleKey.D1) {
                    program.WaveAnimation();
                }
                if (key == ConsoleKey.D2) {
                    program.DiceRoll();
                }
                if (key == ConsoleKey.D3) {
                    program.Blackjack();
                }
                
            }
            program.Tick();
            count++;
        }
        Console.WriteLine($"Rendered {count} times in {timer.ElapsedMilliseconds}ms ({count / (timer.ElapsedMilliseconds / 1000f):0.00} fps)");
    }
}