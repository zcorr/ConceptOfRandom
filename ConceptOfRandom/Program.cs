using System.Reflection.Metadata.Ecma335;
using ConceptOfRandom.View;

namespace ConceptOfRandom;
using ConceptOfRandom.View;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

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