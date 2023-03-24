namespace Shapes;

public class Circle : IShape
{
    public double Radius { get; }
    public double Area => CalcArea();

    private double? _area;

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be bigger then 0");

        Radius = radius;
    }

    private double CalcArea()
    {
        return _area ??= Math.PI * Math.Pow(Radius, 2);
    }
}