namespace ConceptOfRandom.view;

public interface IConsoleCanvas {
	// Render the console display
	void Render();
	
	// Tick to update the console display
	//void Tick();
	
	// Create console border display
	void CreateBorder();
	
	// Set the console display to auto resize
	bool AutoResize { get; set; } // true to auto resize, false to not auto resize
	
	// Clear the console display
	void Clear();
	
	void Text(int x, int y, string text);

	int Width { get; } 
}