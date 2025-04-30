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
                break;

            program.Tick();
            count++;
        }
        Console.WriteLine($"Rendered {count} times in {timer.ElapsedMilliseconds}ms ({count / (timer.ElapsedMilliseconds / 1000f):0.00} fps)");
    }
}