using ConsoleRenderer;
using ConceptOfRandom.Models.API;
using ConceptOfRandom.View;

namespace ConceptOfRandom.view;

public class RandomFact : MenuOutline
{
    private ConsoleCanvas _canvas;
    public RandomFact()
    {
        _canvas = GetCanvas();
    }

   public void StartFacts()
{
    while (true)
    {
        // Clear and display the menu
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 37) / 2, 2, "Welcome to the Random Fact Generator!");
        _canvas.Text((_canvas.Width - 25) / 2, 3, "Please select a category:");
        
        
        Span<string> menuOptions = new[]
        {
            "1. Cat Facts",
            "2. Useless Facts",
            "3. Commit Messages",
            "4. Quotes",
            "5. Weather",
            "6. Back to Main Menu"
        };

        for (int i = 0; i < menuOptions.Length; i++)
        {
            _canvas.Text((_canvas.Width - menuOptions[i].Length) / 2, 5 + i, menuOptions[i]);
        }

        
        // Get user input
        ConsoleKey key = Console.ReadKey(true).Key;

        // Handle user input
        if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
        {
            DisplayFact("Cat Facts");
        }
        else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
        {
            DisplayFact("Useless Facts");
        }
        else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
        {
            DisplayFact("Commit Messages");
        }
        else if (key == ConsoleKey.D4 || key == ConsoleKey.NumPad4)
        {
            DisplayFact("Quotes");
        }
        else if (key == ConsoleKey.D5 || key == ConsoleKey.NumPad5)
        {
            DisplayFact("Weather");
        }
        else if (key == ConsoleKey.D6 || key == ConsoleKey.NumPad6)
        {
            _canvas.Clear();
            _canvas.CreateBorder();
            RenderMenu();
            _canvas.Render();
            return; // Exit to main menu
        }
        _canvas.Render();
    }
}

private void DisplayFact(string category)
{
    _canvas.Clear();
    _canvas.CreateBorder();
    _canvas.Text((_canvas.Width - category.Length) / 2, 2, $"Category: {category}");

    // Simulate fetching a random fact (replace with actual API call or logic)
    string fact;

    switch (category)
    {
        case "Cat Facts":
            var catFactsApi = new RandomCatFacts();
            fact = catFactsApi.Get().Result; // Call the API synchronously
            break;
        case "Useless Facts":
            var uselessFactsApi = new RandomUselessFacts();
            fact = uselessFactsApi.Get().Result; // Call the API synchronously
            break;
        
        case "Commit Messages":
            var commitMessagesApi = new RandomCommitMessages();
            fact = commitMessagesApi.Get().Result; // Call the API synchronously
            break;
        case "Weather":
            var weatherApi = new RandomWeather();
            fact = weatherApi.Get().Result; // Call the API synchronously
            break;
        case "Quotes":
            var quotesApi = new RandomQuotes();
            fact = quotesApi.Get().Result; // Call the API synchronously
            break;
        default:
            fact = "No API available for this category.";
            break;
    }

    _canvas.Text((_canvas.Width - fact.Length) / 2, 4, fact);
    _canvas.Text((_canvas.Width - 30) / 2, 6, "Press any key to return to the menu...");
    Console.ReadKey(true);
    _canvas.Render();
}
    
}