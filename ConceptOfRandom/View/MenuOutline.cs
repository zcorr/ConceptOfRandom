namespace ConceptOfRandom.view;
using ConsoleRenderer;

public class MenuOutline : IConsoleCanvas{
	// Instance variable of our console renderer
	private ConsoleCanvas _canvas = new ConsoleCanvas();
	private double _framerate = 30;
	private int _selectedIndex = 0; // Track the currently selected menu item
	
	
	public void Render() {
		_canvas.Render();
	}

	public void Tick() {
		_canvas.Clear();
		_canvas.CreateBorder();

		// Initial Title Screen
		var title = "Concept of Random";
		var titleX = (_canvas.Width - title.Length) / 2;
		var titleY = 1;
		_canvas.Text(titleX, titleY, title);

		var subtitle = "Up / Down arrows to navigate, Enter to select";
		var subtitleX = (_canvas.Width - subtitle.Length) / 2;
		var subtitleY = 3;
		_canvas.Text(subtitleX, subtitleY, subtitle);

		// Game List Menu
		var menuOptions = new string[] {
			"1. Wave Animation",
			"2. Dice Roll",
			"3. Blackjack",
			"4. Exit"
		};
		var menuX = (_canvas.Width - menuOptions[0].Length) / 2;
		var menuY = 5;

		for (int i = 0; i < menuOptions.Length; i++) {
			if (i == _selectedIndex) {
				// Draw a rectangle around the selected item
				_canvas.CreateRectangle(menuX - 1, menuY + i, menuOptions[i].Length + 2, 1);
			}
			_canvas.Text(menuX, menuY + i, menuOptions[i]);
		}

		_canvas.AutoResize = true;
		_canvas.Render();
	}

	public void HandleInput(ConsoleKey key) {
		if (key == ConsoleKey.UpArrow) {
			_selectedIndex = (_selectedIndex - 1 + 4) % 4; // Wrap around to the last item
		} else if (key == ConsoleKey.DownArrow) {
			_selectedIndex = (_selectedIndex + 1) % 4; // Wrap around to the first item
		} else if (key == ConsoleKey.Enter) {
			switch (_selectedIndex) {
				case 0:
					WaveAnimation();
					break;
				case 1:
					DiceRoll();
					break;
				case 2:
					Blackjack();
					break;
				case 3:
					Environment.Exit(0);
					break;
			}
		}
	}

	public void CreateBorder() {
		_canvas.CreateBorder();
	}
	
	public bool AutoResize {
		get => _canvas.AutoResize;
		set => _canvas.AutoResize = value;
	}
	
	public void Clear() {
		_canvas.Clear();
	}

	public void WaveAnimation() {
		// Initial Text string and position (centered and at 1 for title)
		var waveText = "Wave Animation!";
		var titleText = "Concept of Random";
		var waveTextX = (_canvas.Width - waveText.Length) / 2;
		var titleTextX = (_canvas.Width - titleText.Length) / 2;
		var titleTextY = 1;
		
		var startTime = DateTime.Now;
		
		
		var amplitude = 1.5; // Height of the oscillation
		var frequency = 0.2; // Speed of the oscillation
		var wavePeriod = (2 * Math.PI) / frequency; // Duration of one full wave cycle
		var waveDuration = TimeSpan.FromSeconds(wavePeriod);

		while ((DateTime.Now - startTime) < waveDuration) {
			var currentTime = DateTime.Now;
			_canvas.Clear();
			_canvas.CreateBorder();
			_canvas.Text(titleTextX, titleTextY, titleText);

			// Render each letter at its calculated vertical position
			for (int i = 0; i < waveText.Length; i++) {
				int x = waveTextX + i;
				int y = (int)(Math.Sin((i * frequency) + (currentTime.Ticks / 10000000.0)) * amplitude + 4); // Single wave
				_canvas.Set(x, y, waveText[i], ConsoleColor.White, ConsoleColor.Black);
			}

			_canvas.AutoResize = true;
			_canvas.Render();
			System.Threading.Thread.Sleep(100); // Adjust delay for smoother animation
		}
	}

	/**
	 * THESE TWO METHODS BELOW WILL BE THE GAME METHODS THAT WILL BE INTEGRATED ONCE WE HAVE THE CODE MERGED
	 */
	public void DiceRoll() {
	}
	
	public void Blackjack() {
	}
}