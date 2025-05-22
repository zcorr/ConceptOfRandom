using ConceptOfRandom.view;
using NSubstitute;

namespace ConceptOfRandomTests;

public class MenuTest1 {
	
	private IConsoleCanvas _mockCanvas = Substitute.For<IConsoleCanvas>();
	
	[Fact]
	public void TestRendering()
	{
		_mockCanvas.Render();
		_mockCanvas.Received(1).Render();
	}
	

	[Fact]
	public void TestCreateBorder() {
		_mockCanvas.Render();
		_mockCanvas.CreateBorder();
		_mockCanvas.Received(1).CreateBorder();
	}
	
	[Fact]
	public void TestAutoResize() {
		_mockCanvas.AutoResize = true;
		Assert.True(_mockCanvas.AutoResize);
		_mockCanvas.AutoResize = false;
		Assert.False(_mockCanvas.AutoResize);
	}
	
	[Fact]
	public void TestClear() {
		_mockCanvas.Clear();
		_mockCanvas.Received(1).Clear();
	}

}