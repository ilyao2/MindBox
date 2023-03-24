namespace Shapes;

public class Triangle : IShape
{
    public Point PointA { get; }
    public Point PointB { get; }
    public Point PointC { get; }
    public double Tolerance { get; }
    public double Area => CalcArea();
    public bool IsRight => CalcIsRight();

    private double? _area;
    private bool? _isRight;


    public Triangle(Point pointA, Point pointB, Point pointC, double tolerance = 0.0001)
    {
        if (tolerance < 0)
        {
            throw new ArgumentException("Tolerance must be greater then or equal to 0");
        }

        Tolerance = tolerance;

        if ((pointA.Y - pointB.Y) * pointC.X + (pointB.X - pointA.X) * pointC.Y +
            (pointA.X * pointB.Y - pointB.X * pointA.Y) <= Tolerance)
        {
            throw new ArgumentException($"Points must not be on a straight line! Tolerance: {Tolerance}");
        }

        PointA = pointA;
        PointB = pointB;
        PointC = pointC;
    }

    private double CalcArea()
    {
        return _area ??= 0.5 * Math.Abs((PointB.X - PointA.X) * (PointC.Y - PointA.Y) -
                                        (PointC.X - PointA.X) * (PointB.Y - PointA.Y));
    }

    private bool CalcIsRight()
    {
        if (_isRight.HasValue)
            return _isRight.Value;

        var quadSide1 = Math.Pow(SideByPoints(PointA, PointB), 2);
        var quadSide2 = Math.Pow(SideByPoints(PointB, PointC), 2);
        var quadSide3 = Math.Pow(SideByPoints(PointC, PointA), 2);

        _isRight = Math.Abs(quadSide1 + quadSide2 - quadSide3) < Tolerance ||
                   Math.Abs(quadSide2 + quadSide3 - quadSide1) < Tolerance ||
                   Math.Abs(quadSide3 + quadSide1 - quadSide2) < Tolerance;

        return _isRight.Value;
    }

    private static double SideByPoints(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }
}