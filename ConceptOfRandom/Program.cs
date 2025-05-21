namespace ConceptOfRandom;
using ConceptOfRandom.view;

class Program
{
    static void Main(string[] args) {
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
                
            }
            program.Tick();
        }
    }
}