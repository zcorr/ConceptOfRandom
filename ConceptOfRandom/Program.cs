namespace ConceptOfRandom;

class Program
{
    static void Main(string[] args) {
        Console.CursorVisible = false;
        while (true) {
            var controller = new Controller();
            try {
                controller.Run();
            }
            catch (ReturnToMainMenuException) {
                continue;
            }
        }
    }
}