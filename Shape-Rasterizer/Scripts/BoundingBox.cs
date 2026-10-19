using System;
using System.Drawing;

public struct BoundingBox
{
    public int top, bottom, left, right;

    public BoundingBox(PointF Point1, PointF Point2, PointF Point3)
    {
        top = (int)MathF.Ceiling(Math.Max(Point1.Y, Math.Max(Point2.Y, Point3.Y)));
        bottom = (int)MathF.Floor(Math.Min(Point1.Y, Math.Min(Point2.Y, Point3.Y)));
        left = (int)MathF.Floor(Math.Min(Point1.X, Math.Min(Point2.X, Point3.X)));
        right = (int)MathF.Ceiling(Math.Max(Point1.X, Math.Max(Point2.X, Point3.X)));
    }
}