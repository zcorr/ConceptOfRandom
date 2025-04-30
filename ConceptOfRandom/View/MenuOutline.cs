namespace ConceptOfRandom.view;
using ConsoleRenderer;

public class MenuOutline : IConsoleCanvas{
	// Instance variable of our console renderer
	private ConsoleCanvas _canvas = new ConsoleCanvas();
	private double _framerate = 60;
	private DateTime _previousframe = DateTime.Now;
	
	
	public void Render() {
		_canvas.Render();
	}

	public void Tick() {
		_canvas.Clear();
		_canvas.CreateBorder();
		
		var currentTime = DateTime.Now;
		if ((currentTime - _previousframe).TotalMilliseconds >= _framerate) {
			// animations would in here every tick
			
			_previousframe = currentTime;
		}

		_canvas.AutoResize = true;
		_canvas.Set(250, 250, ConsoleColor.DarkBlue);
		_canvas.Render();
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