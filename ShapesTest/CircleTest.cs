using System.Diagnostics;
using Shapes;

namespace ShapesTest;

public class CircleTest
{
    private const double Tolerance = 0.00001;

    [Theory]
    [InlineData(0)]
    [InlineData(-0.001)]
    [InlineData(-5)]
    public void RadiusNotPositiveException(double radius)
    {
        // Arrange
        Circle? circle = null;
        const string expectedErrorMsg = "Radius must be bigger then 0";

        // Act
        void Act()
        {
            circle = new Circle(radius);
        }

        // Assert
        var exc = Assert.Throws<ArgumentException>(Act);
        Assert.Equal(expectedErrorMsg, exc.Message);
        Assert.Null(circle);
    }

    [Theory]
    [InlineData(1, Math.PI)]
    [InlineData(0.01, Math.PI / 10000)]
    [InlineData(5, Math.PI * 25)]
    public void CorrectCalcArea(double radius, double expectedArea)
    {
        // Arrange
        var circle = new Circle(radius);

        // Act
        var area = circle.Area;

        // Assert
        Assert.Equal(expectedArea, area, Tolerance);
    }

    [Fact]
    public void LazyCalcArea()
    {
        // Arrange
        var circle = new Circle(5);
        var watch = new Stopwatch();

        // Act
        watch.Start();
        var area1 = circle.Area;
        watch.Stop();
        var time1 = watch.Elapsed;

        watch.Restart();
        var area2 = circle.Area;
        watch.Stop();
        var time2 = watch.Elapsed;

        watch.Restart();
        var area3 = circle.Area;
        watch.Stop();
        var time3 = watch.Elapsed;
        watch.Restart();

        // Assert
        const double expectedArea = Math.PI * 25;
        Assert.Equal(expectedArea, area1, Tolerance);
        Assert.Equal(expectedArea, area2, Tolerance);
        Assert.Equal(expectedArea, area3, Tolerance);

        Assert.True(time1 > time2, $"2nd Area getting ({time2}) not faster then 1st ({time1})");
        Assert.True(time1 > time3, $"3rd Area getting ({time3}) not faster then 1st ({time1})");
    }
}