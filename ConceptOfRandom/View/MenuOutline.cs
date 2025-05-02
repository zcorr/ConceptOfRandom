namespace ConceptOfRandom.view;
using ConsoleRenderer;

public class MenuOutline : IConsoleCanvas{
	// Instance variable of our console renderer
	private ConsoleCanvas _canvas = new ConsoleCanvas();
	private double _framerate = 30;
	private DateTime _previousframe = DateTime.Now;
	
	
	public void Render() {
		_canvas.Render();
	}

	public void Tick() {
		_canvas.Clear();
		_canvas.CreateBorder();

		var currentTime = DateTime.Now;
		if ((currentTime - _previousframe).TotalMilliseconds >= 1000 / _framerate) {
			var text = "Wave Animation!";
			var width = _canvas.Width;
			var height = 5; // Adjust wave height
			var frequency = 0.05; // Lower frequency for smoother motion

			for (var i = 0; i < text.Length; i++) {
				var x = i + (width - text.Length) / 2; // Center horizontally
				var y = (int)(Math.Sin((i + currentTime.Ticks / 10000000.0) * frequency) * height / 2 + height / 2);
				_canvas.Set(x, y, text[i], ConsoleColor.White, ConsoleColor.Black);
			}

			_previousframe = currentTime;
		}

		_canvas.AutoResize = true;
		_canvas.Render();
		System.Threading.Thread.Sleep(50);
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

}