using ConceptOfRandom.view;
using ConceptOfRandom.View;
using ConceptOfRandom.View.Games;

namespace ConceptOfRandom;


public class Controller {
	private MenuOutline menuView = new MenuOutline();
	private int _selectedIndex = 0;

	private string[] menuOptions = {
		"1. Blackjack",
		"2. Dice Roll",
		"3. Random Facts",
		"4. Wave Animation",
		"5. Timer",
		"6. Complete Randomness",
		"7. Exit"
	};

	public void Run() {
		while (true)
		{
			Console.CursorVisible = false;
			menuView.AutoResize = true;
			menuView.Clear();
			menuView.CreateBorder();
			menuView.RenderMenu("Concept of Random", "Up / Down arrows to navigate, Enter to select", menuOptions);
			menuView.Render();
			while (true) {
				var key = Console.ReadKey(true).Key;

				if (key == ConsoleKey.UpArrow) {
					_selectedIndex = (menuView.GetSelectedIndex() - 1 + menuOptions.Length) % menuOptions.Length;
					menuView.UpdateSelectedIndex(_selectedIndex);
				}
				else if (key == ConsoleKey.DownArrow) {
					_selectedIndex = (menuView.GetSelectedIndex() + 1) % menuOptions.Length;
					menuView.UpdateSelectedIndex(_selectedIndex);
				}
				else if (key == ConsoleKey.Enter) {
					ExecuteMenuOption(_selectedIndex);
					break;
				}

				menuView.RenderMenu("Concept of Random", "Up / Down arrows to navigate, Enter to select",
					menuOptions);
			}
		}
	}
	

	// private void HandleInput(ConsoleKey key) {
	// 	if (key == ConsoleKey.UpArrow) {
	// 		_selectedIndex = (_selectedIndex - 1 + menuOptions.Length) % menuOptions.Length;
	// 	} else if (key == ConsoleKey.DownArrow) {
	// 		_selectedIndex = (_selectedIndex + 1) % menuOptions.Length;
	// 	} else if (key == ConsoleKey.Enter) {
	// 		
	// 	}
	// 	menuView.UpdateSelectedIndex(_selectedIndex);
	// }

	private void ExecuteMenuOption(int index) {
		switch (index) {
			case 0:
				new BlackjackIntegration().StartGame();
				break;
			case 1:
				new DiceIntegration().StartDice();
				break;
			case 2:
				new RandomFactIntegration().StartFacts();
				break;
			case 3:
				new WaveIntegration().Wave();
				break;
			case 4:
				new TimerIntegration().StartTimer();
				break;
			case 5:
				new FullRandomIntegration().ExecuteRandomGame();
				break;
			case 6:
				Environment.Exit(0);
				break;
		}
	}
	
	
	
}