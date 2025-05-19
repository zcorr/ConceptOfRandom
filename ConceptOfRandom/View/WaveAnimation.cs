using ConsoleRenderer;

namespace ConceptOfRandom.view;

public class WaveAnimation : MenuOutline {
	private ConsoleCanvas _canvas;

	public WaveAnimation() {
		_canvas = GetCanvas(); // Initialize the canvas using the inherited method
	}

	public void Wave() {
		var waveText = "Wave Animation!";
		var titleText = "Concept of Random";
		var waveTextX = (_canvas.Width - waveText.Length) / 2;
		var titleTextX = (_canvas.Width - titleText.Length) / 2;
		var titleTextY = 1;

		var startTime = DateTime.Now;

		var amplitude = 1.5;
		var frequency = 0.2;
		var wavePeriod = (2 * Math.PI) / frequency;
		var waveDuration = TimeSpan.FromSeconds(wavePeriod);

		while ((DateTime.Now - startTime) < waveDuration) {
			var currentTime = DateTime.Now;
			_canvas.Clear();
			_canvas.CreateBorder();
			_canvas.Text(titleTextX, titleTextY, titleText);

			for (int i = 0; i < waveText.Length; i++) {
				int x = waveTextX + i;
				int y = (int)(Math.Sin((i * frequency) + (currentTime.Ticks / 10000000.0)) * amplitude + 4);
				_canvas.Set(x, y, waveText[i], ConsoleColor.White, ConsoleColor.Black);
			}

			_canvas.AutoResize = true;
			_canvas.Render();
			System.Threading.Thread.Sleep(100);
		}

		// Clear the canvas and reset for the next Tick() call
		_canvas.Clear();
		_canvas.CreateBorder();
		MakeTitle();
		_canvas.Render();
	}
}