using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

// This class uses Bresenham's rasterization algorithm
public static class LineRasterizer
{
    public static List<Point> GetRasterizedLine(Point startPoint, Point endPoint)
    {
        int lineWidth = endPoint.X - startPoint.X;
        int lineHeight = endPoint.Y - startPoint.Y;
        float lineSlope = lineWidth == 0 ? 0 : (float)lineHeight / lineWidth;
        List<Point> linePixels = new List<Point>();

        if (IsLineValid(startPoint, endPoint))
        {
            PrepareLine(ref startPoint, ref endPoint, ref lineWidth, ref lineHeight, lineSlope);
            RasterizeLine(startPoint, endPoint, lineWidth, lineHeight, linePixels);
            OrganizeResult(linePixels, startPoint, lineSlope);
        }
        return linePixels;
    }

    private static bool IsLineValid(Point startPoint, Point endPoint)
    {
        if (startPoint.X <= endPoint.X)
        {
            if (startPoint.X >= 0 && endPoint.X >= 0)
            {
                if (startPoint.Y >= 0 && endPoint.Y >= 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("\nError: (Y.start) and (Y.end) can't be negative.");
                }
            }
            else
            {
                Console.WriteLine("\nError: (X.start) and (X.end) can't be negative.");
            }
        }
        else
        {
            Console.WriteLine("\nError: (X.start <= X.end) must hold.");
        }
        return false;
    }

    private static void PrepareLine(ref Point startPoint, ref Point endPoint, ref int lineWidth, ref int lineHeight, float lineSlope)
    {
        if (lineSlope < 0)
        {
            InvertLineSlopeToPositive(ref endPoint, lineHeight);
        }
        if (Math.Abs(lineSlope) > 1)
        {
            SwapLine_X_And_Y_Coordinates(ref startPoint, ref endPoint);
        }

        // Update data
        lineWidth = endPoint.X - startPoint.X;
        lineHeight = endPoint.Y - startPoint.Y;
    }

    private static void InvertLineSlopeToPositive(ref Point endPoint, int lineHeight)
    {
        endPoint = new Point(endPoint.X, endPoint.Y + 2 * Math.Abs(lineHeight));
    }

    private static void SwapLine_X_And_Y_Coordinates(ref Point startPoint, ref Point endPoint)
    {
        startPoint = new Point(startPoint.Y, startPoint.X);
        endPoint = new Point(endPoint.Y, endPoint.X);
    }

    private static void RasterizeLine(Point startPoint, Point endPoint, int lineWidth, int lineHeight, List<Point> linePixels)
    {
        int x = startPoint.X;
        int y = startPoint.Y;
        int nextPixel_Y_Decider = 0;

        while (x <= endPoint.X)
        {
            AddPixel(linePixels, x, y);

            x = PrepareNextPixel_X(x);
            y = PrepareNextPixel_Y(y, lineWidth, lineHeight, ref nextPixel_Y_Decider);
        }
    }

    private static void AddPixel(List<Point> linePixels, int x, int y)
    {
        linePixels.Add(new Point(x, y));
    }

    private static int PrepareNextPixel_X(int x)
    {
        return ++x;
    }

    private static int PrepareNextPixel_Y(int y, int lineWidth, int lineHeight, ref int nextPixel_Y_Decider)
    {
        if (ShouldPixelSiftUp(lineWidth, lineHeight, nextPixel_Y_Decider))
        {
            y++;
            nextPixel_Y_Decider += 2 * (lineHeight - lineWidth);
        }
        else
        {
            nextPixel_Y_Decider += 2 * lineHeight;
        }
        return y;
    }

    private static bool ShouldPixelSiftUp(int lineWidth, int lineHeight, int nextPixel_Y_Decider)
    {
        return 2 * lineHeight - lineWidth + nextPixel_Y_Decider > 0;
    }

    private static void OrganizeResult(List<Point> linePixels, Point startPoint, float lineSlope)
    {
        if (Math.Abs(lineSlope) > 1)
        {
            SwapLinePixels_X_And_Y_Coordinates(linePixels);
        }
        if (lineSlope < 0)
        {
            InvertSlopeOfRasterizedLineToNegative(linePixels, startPoint, lineSlope);
        }
    }

    private static void SwapLinePixels_X_And_Y_Coordinates(List<Point> linePixels)
    {
        for (int i = 0; i < linePixels.Count(); i++)
        {
            Point pixel = linePixels[i];
            linePixels[i] = new Point(pixel.Y, pixel.X);
        }
    }

    private static void InvertSlopeOfRasterizedLineToNegative(List<Point> linePixels, Point startPoint, float lineSlope)
    {
        for (int i = 0; i < linePixels.Count(); i++)
        {
            Point pixel = linePixels[i];
            if (Math.Abs(lineSlope) > 1)
            {
                linePixels[i] = new Point(pixel.X, pixel.Y - 2 * (pixel.Y - startPoint.X));
            }
            else
            {
                linePixels[i] = new Point(pixel.X, pixel.Y - 2 * (pixel.Y - startPoint.Y));
            }
        }
    }
}