using System;
using System.Collections.Generic;
using System.Drawing;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Choose a primitive to rasterize:");
        Console.WriteLine("1-Line");
        Console.WriteLine("2-Triangle");

        switch (Console.ReadLine())
        {
            case "1":
                ShowLineRasterizationInputs();
                break;

            case "2":
                ShowPolygonRasterizationInputs();
                break;

            default:
                Console.WriteLine("Invalid Entery.");
                break;
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }

    private static void ShowLineRasterizationInputs()
    {
        Console.WriteLine("\nEnter start point of the line:");
        Console.Write("X1: ");
        int x1 = int.Parse(Console.ReadLine());
        Console.Write("Y1: ");
        int y1 = int.Parse(Console.ReadLine());

        Console.WriteLine("\nEnter end point of the line:");
        Console.Write("X2: ");
        int x2 = int.Parse(Console.ReadLine());
        Console.Write("Y2: ");
        int y2 = int.Parse(Console.ReadLine());

        List<Point> myLine = LineRasterizer.GetRasterizedLine(new Point(x1, y1), new Point(x2, y2));

        PrintResult(myLine);
    }

    private static void ShowPolygonRasterizationInputs()
    {
        Console.WriteLine("\nEnter point1 of the triangle:");
        Console.Write("X1: ");
        int x1 = int.Parse(Console.ReadLine());
        Console.Write("Y1: ");
        int y1 = int.Parse(Console.ReadLine());

        Console.WriteLine("\nEnter point2 of the triangle:");
        Console.Write("X2: ");
        int x2 = int.Parse(Console.ReadLine());
        Console.Write("Y2: ");
        int y2 = int.Parse(Console.ReadLine());

        Console.WriteLine("\nEnter point3 of the triangle:");
        Console.Write("X2: ");
        int x3 = int.Parse(Console.ReadLine());
        Console.Write("Y2: ");
        int y3 = int.Parse(Console.ReadLine());

        List<PointF> myTriangle = TriangleRasterizer.GetRasterizedTriangle(new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3));

        PrintResult(myTriangle);
    }

    /// Result printing ///
    public static void PrintResult(List<Point> rasterizedTriangle)
    {
        if (rasterizedTriangle.Count != 0)
        {
            Console.WriteLine("\nPixel coordinates of the rasterized triangle:");
            foreach (Point pixel in rasterizedTriangle)
            {
                Console.WriteLine("({0}, {1})", pixel.X, pixel.Y);
            }
        }
    }

    public static void PrintResult(List<PointF> rasterizedLine)
    {
        if (rasterizedLine.Count != 0)
        {
            Console.WriteLine("\nPixel coordinates of the rasterized line:");
            foreach (PointF pixel in rasterizedLine)
            {
                Console.WriteLine("({0}, {1})", pixel.X, pixel.Y);
            }
        }
    }
}