using ConceptOfRandom.view;
using ConsoleRenderer;

namespace ConceptOfRandom.View;

public class MenuOutline : IConsoleCanvas {
	private ConsoleCanvas canvas = new ConsoleCanvas();
	private int _selectedIndex = 0;

	public void RenderMenu(string title, string subtitle, string[] menuOptions) {
		canvas.Clear();
		canvas.CreateBorder();

		var height = Console.WindowHeight;

		// Render title and subtitle
		var titleX = Math.Max((canvas.Width - title.Length) / 2, 0);
		canvas.Text(titleX, 1, TruncateText(title, canvas.Width));

		var subtitleX = Math.Max((canvas.Width - subtitle.Length) / 2, 0);
		canvas.Text(subtitleX, 3, TruncateText(subtitle, canvas.Width));

		// Render menu options
		for (var i = 0; i < menuOptions.Length; i++) {
			var menuX = Math.Max((canvas.Width - menuOptions[i].Length) / 2, 0);
			var menuY = 5 + i;
			if (menuY >= height - 1) break;
			if (i == _selectedIndex) {
				canvas.Text(menuX - 2, 5 + i, ">"); // Highlight selected option
			}
			canvas.Text(menuX, 5 + i, TruncateText(menuOptions[i], canvas.Width - menuX));
		}

		canvas.Render();
	}
	
	public void RenderMenu() {
		RenderMenu("Concept of Random", "Up / Down arrows to navigate, Enter to select", new[] {
			"1. Blackjack",
			"2. Dice Roll",
			"3. Random Facts",
			"4. Wave Animation",
			"5. Exit"
		});
	}

	public void UpdateSelectedIndex(int selectedIndex) {
		_selectedIndex = selectedIndex;
	}
	
	public int GetSelectedIndex() {
		return _selectedIndex;
	}

	public void Clear() => canvas.Clear();
	public void CreateBorder() => canvas.CreateBorder();
	public bool AutoResize { get => canvas.AutoResize; set => canvas.AutoResize = value; }
	public void Render() => canvas.Render();
	public void Text(int x, int y, string text) => canvas.Text(x, y, text);
	public int Width => Console.WindowWidth;
	public ConsoleCanvas GetCanvas() => canvas;

	private string TruncateText(string text, int maxWidth) {
		return text.Length > maxWidth ? text.Substring(0, maxWidth - 3) + "..." : text;
	}
}