using System;
using System.Collections.Generic;
using System.Drawing;

// This class uses Pineda's rasterization algorithm
public static class TriangleRasterizer
{
    public static List<Point> GetRasterizedTriangle(PointF Point1, PointF Point2, PointF Point3)
    {
        BoundingBox triangleBoundingBox = new BoundingBox(Point1, Point2, Point3);
        List<PointF> PointsInClockwiseOrder = SortPointsInClockwiseOrder(Point1, Point2, Point3);
        List<Point> trianglePixels = new List<Point>();
        List<float> rowEdgeFunctions = InitializeEdgeFunctionsPerEdge(PointsInClockwiseOrder, triangleBoundingBox);

        for (int y = triangleBoundingBox.top; y >= triangleBoundingBox.bottom; y--)
        {
            List<float> edgeFunctions = new List<float>(rowEdgeFunctions);
            for (int x = triangleBoundingBox.left; x <= triangleBoundingBox.right; x++)
            {
                if (IsPixelInsideTriangle(edgeFunctions))
                {
                    trianglePixels.Add(new Point(x, y));
                }
                UpdateEdgeFunctionsRow(edgeFunctions, PointsInClockwiseOrder);
            }
            UpdateEdgeFunctionsColumn(rowEdgeFunctions, PointsInClockwiseOrder);
        }
        return trianglePixels;
    }

    private static List<PointF> SortPointsInClockwiseOrder(PointF Point1, PointF Point2, PointF Point3)
    {
        List<PointF> Points = new List<PointF> { Point1, Point2, Point3 };
        float centerOfTriangleX = ((Point1.X + Point2.X + Point3.X) / 3f);
        float centerOfTriangleY = ((Point1.Y + Point2.Y + Point3.Y) / 3f);

        Points.Sort((a, b) =>Math.Atan2(
            b.Y - centerOfTriangleY,
            b.X - centerOfTriangleX).CompareTo(Math.Atan2(a.Y - centerOfTriangleY, a.X - centerOfTriangleX)));
        return Points;
    }

    private static List<float> InitializeEdgeFunctionsPerEdge(List<PointF> PointsInClockwiseOrder, BoundingBox triangleBoundingBox)
    {
        float px = triangleBoundingBox.left + 0.5f;
        float py = triangleBoundingBox.top + 0.5f;
        List<float> edgeFunctions = new List<float>(3);

        for (int i = 0; i < 3; i++)
        {
            PointF a = PointsInClockwiseOrder[i];
            PointF b = PointsInClockwiseOrder[(i + 1) % 3];
            edgeFunctions.Add((px - a.X) * (b.Y - a.Y) - (py - a.Y) * (b.X - a.X));
        }
        return edgeFunctions;
    }

    private static bool IsPixelInsideTriangle(List<float> edgeFunctions)
    {
        return edgeFunctions[0] >= 0 && edgeFunctions[1] >= 0 && edgeFunctions[2] >= 0;
    }

    private static void UpdateEdgeFunctionsRow(List<float> edgeFunctions, List<PointF> PointsInClockwiseOrder)
    {
        for (int i = 0; i < 3; i++)
        {
            edgeFunctions[i] += PointsInClockwiseOrder[(i + 1) % 3].Y - PointsInClockwiseOrder[i].Y;
        }
    }

    private static void UpdateEdgeFunctionsColumn(List<float> rowEdgeFunctions, List<PointF> PointsInClockwiseOrder)
    {
        for (int i = 0; i < 3; i++)
        {
            rowEdgeFunctions[i] += PointsInClockwiseOrder[(i + 1) % 3].X - PointsInClockwiseOrder[i].X;
        }
    }
}