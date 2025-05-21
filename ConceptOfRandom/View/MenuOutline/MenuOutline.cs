using System.Reflection;
using ConceptOfRandom.Models.API;

namespace ConceptOfRandom.view;
using ConsoleRenderer;

public class MenuOutline : IConsoleCanvas{
	// Instance variable of our console renderer
	private ConsoleCanvas canvas = new ConsoleCanvas();
	private double _framerate = 30;
	private int _selectedIndex = 0; // Track the currently selected menu item
	
	
	public void Render() {
		canvas.Render();
	}

	public void Tick() {
		canvas.Clear();
		canvas.CreateBorder();
		
		MakeTitle();
		
		canvas.AutoResize = true;
		canvas.Render();
	}

	public void HandleInput(ConsoleKey key) {
		if (key == ConsoleKey.UpArrow) {
			//Console.Beep(1000, 100);
			_selectedIndex = (_selectedIndex - 1 + 5) % 5; // Wrap around to the last item
		} else if (key == ConsoleKey.DownArrow) {
			//Console.Beep(600, 100);
			_selectedIndex = (_selectedIndex + 1) % 5; // Wrap around to the first item
		} else if (key == ConsoleKey.Enter) {
			//Console.Beep(800, 200);
			switch (_selectedIndex) {
				case 0:
					// Start the blackjack game
					new BlackjackIntegration().StartGame();
					break;
				case 1:
					//DiceRoll();
					break;
				case 2:
					new RandomFact().StartFacts();
					break;
				case 3:
					new WaveAnimation().Wave();
					break;
				case 4:
					Environment.Exit(0);
					break;
			}
		}
	}

	public void CreateBorder() {
		canvas.CreateBorder();
	}
	
	public bool AutoResize {
		get => canvas.AutoResize;
		set => canvas.AutoResize = value;
	}
	
	public void Clear() {
		canvas.Clear();
	}

	protected ConsoleCanvas GetCanvas() {
		return canvas;
	}
	
	protected void MakeTitle() {
		Clear();
		CreateBorder();
		
		// Initial Title Screen
		var title = "Concept of Random";
		var titleX = (canvas.Width - title.Length) / 2;
		var titleY = 1;
		canvas.Text(titleX, titleY, title);

		var subtitle = "Up / Down arrows to navigate, Enter to select";
		var subtitleX = (canvas.Width - subtitle.Length) / 2;
		var subtitleY = 3;
		canvas.Text(subtitleX, subtitleY, subtitle);

		// Game List Menu
		Span<string> menuOptions = [
			"1. Blackjack",
			"2. Dice Roll",
			"3. Random Facts",
			"4. Wave Animation",
			"5. Exit"
		];



		var menuX = (canvas.Width - menuOptions[0].Length) / 2;
		var menuY = 5;
		
		for (int i = 0; i < menuOptions.Length; i++) {
			if (i == _selectedIndex) {
				// Draw a rectangle around the selected item
				canvas.CreateRectangle(menuX - 1, menuY + i, menuOptions[i].Length + 2, 1);
			}
			canvas.Text(menuX, menuY + i, menuOptions[i]);
		}
		
	}

}