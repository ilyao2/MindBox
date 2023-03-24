using System.Diagnostics;
using Shapes;

namespace ShapesTest;

public class TriangleTest
{
    private const double Tolerance = 0.001;


    [Theory]
    [MemberData(nameof(AllPointsOnLineTestData))]
    public void AllPointsOnLineException(Point pointA, Point pointB, Point pointC)
    {
        // Arrange
        Triangle? triangle = null;
        var expectedErrorMsg = $"Points must not be on a straight line! Tolerance: {Tolerance}";

        // Act
        void Act()
        {
            triangle = new Triangle(pointA, pointB, pointC, Tolerance);
        }

        // Assert
        var verticalExc = Assert.Throws<ArgumentException>(Act);
        Assert.Equal(expectedErrorMsg, verticalExc.Message);
        Assert.Null(triangle);
    }

    [Fact]
    public void PointsNotOnLineWithBigToleranceException()
    {
        // Arrange
        Triangle? triangle = null;
        var pointA = new Point(1, 0);
        var pointB = new Point(1, 5);
        var pointC = new Point(1.5, 1);
        const double tolerance = 1;
        var expectedErrorMsg = $"Points must not be on a straight line! Tolerance: {tolerance}";

        // Act
        void Act()
        {
            triangle = new Triangle(pointA, pointB, pointC, tolerance);
        }

        // Assert
        var exc = Assert.Throws<ArgumentException>(Act);
        Assert.Equal(expectedErrorMsg, exc.Message);
        Assert.Null(triangle);
    }

    [Fact]
    public void NegativeToleranceException()
    {
        // Arrange
        Triangle? triangle = null;
        var point = new Point(0, 0);
        const double tolerance = -1;
        const string expectedErrorMsg = "Tolerance must be greater then or equal to 0";

        // Act
        void Act()
        {
            triangle = new Triangle(point, point, point, tolerance);
        }

        // Assert
        var exc = Assert.Throws<ArgumentException>(Act);
        Assert.Equal(expectedErrorMsg, exc.Message);
        Assert.Null(triangle);
    }

    [Theory]
    [MemberData(nameof(CorrectCalcAreaTestData))]
    public void CorrectCalcArea(Point pointA, Point pointB, Point pointC, double expectedArea)
    {
        // Arrange
        var triangle = new Triangle(pointA, pointB, pointC);

        // Act
        var factArea = triangle.Area;

        // Assert
        Assert.Equal(expectedArea, factArea, 0.001);
    }

    [Theory]
    [MemberData(nameof(CorrectCalcRightTestData))]
    public void CorrectCalcRight(Point pointA, Point pointB, Point pointC, bool expectedIsRight)
    {
        // Arrange
        var triangle = new Triangle(pointA, pointB, pointC);

        // Act
        var factIsRight = triangle.IsRight;

        // Assert
        Assert.Equal(expectedIsRight, factIsRight);
    }

    [Fact]
    public void LazyCalcArea()
    {
        // Arrange
        var triangle = new Triangle(new Point(0, 0), new Point(1, 1), new Point(0, 1));
        const double expectedArea = 0.5;
        var watch = new Stopwatch();

        // Act
        watch.Start();
        var area1 = triangle.Area;
        watch.Stop();
        var time1 = watch.Elapsed;

        watch.Restart();
        var area2 = triangle.Area;
        watch.Stop();
        var time2 = watch.Elapsed;

        watch.Restart();
        var area3 = triangle.Area;
        watch.Stop();
        var time3 = watch.Elapsed;
        watch.Restart();

        // Assert
        Assert.Equal(expectedArea, area1, Tolerance);
        Assert.Equal(expectedArea, area2, Tolerance);
        Assert.Equal(expectedArea, area3, Tolerance);

        Assert.True(time1 > time2, $"2nd Area getting ({time2}) not faster then 1st ({time1})");
        Assert.True(time1 > time3, $"3rd Area getting ({time3}) not faster then 1st ({time1})");
    }

    [Fact]
    public void LazyCalcIsRight()
    {
        // Arrange
        var triangle = new Triangle(new Point(0, 0), new Point(1, 1), new Point(0, 1));
        const bool expectedIsRight = true;
        var watch = new Stopwatch();

        // Act
        watch.Start();
        var isRight1 = triangle.IsRight;
        watch.Stop();
        var time1 = watch.Elapsed;

        watch.Restart();
        var isRight2 = triangle.IsRight;
        watch.Stop();
        var time2 = watch.Elapsed;

        watch.Restart();
        var isRight3 = triangle.IsRight;
        watch.Stop();
        var time3 = watch.Elapsed;
        watch.Restart();

        // Assert
        Assert.Equal(expectedIsRight, isRight1);
        Assert.Equal(expectedIsRight, isRight2);
        Assert.Equal(expectedIsRight, isRight3);

        Assert.True(time1 > time2, $"2nd Area getting ({time2}) not faster then 1st ({time1})");
        Assert.True(time1 > time3, $"3rd Area getting ({time3}) not faster then 1st ({time1})");
    }

    public static IEnumerable<object[]> AllPointsOnLineTestData()
    {
        yield return new object[] { new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        yield return new object[] { new Point(1, -4), new Point(1, 5), new Point(1, 1) };
        yield return new object[] { new Point(-4, 1), new Point(5, 1), new Point(1, 1) };
        yield return new object[] { new Point(-1, -1), new Point(0, 0), new Point(1, 1) };
        yield return new object[] { new Point(-1, 1), new Point(0, 0), new Point(1, -1) };
    }

    public static IEnumerable<object[]> CorrectCalcAreaTestData()
    {
        yield return new object[] { new Point(-2.5, -1), new Point(2.5, -1), new Point(0, 5), 15 };
        yield return new object[] { new Point(5, 5), new Point(0, 5), new Point(5, 0), 12.5 };
        yield return new object[] { new Point(-4, -1), new Point(-4, -4), new Point(-1, -2), 4.5 };
    }

    public static IEnumerable<object[]> CorrectCalcRightTestData()
    {
        yield return new object[] { new Point(-2.5, -1), new Point(2.5, -1), new Point(0, 5), false };
        yield return new object[] { new Point(-2.5, -1), new Point(2.5, -1), new Point(-2.5, 5), true };
    }
}