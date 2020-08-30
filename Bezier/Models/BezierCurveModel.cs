using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Bezier.Models
{
    public class BezierCurveModel
    {
        public PointF GetPointCoordinatesFromPrmtr(PointF startPoint, PointF endPoint, double parameter)
        {
            PointF pointCoordinates = new PointF
            {
                X = Convert.ToSingle(endPoint.X * parameter + startPoint.X * (1 - parameter)),
                Y = Convert.ToSingle(endPoint.Y * parameter + startPoint.Y * (1 - parameter))
            };

            return pointCoordinates;
        }

        public PointF CasteljosAlgorithm(List<Point> points, double parameter, int levelNumber, int pointNumber)
        {
            if (levelNumber == 0)
                return points[pointNumber];
            else
                return GetPointCoordinatesFromPrmtr(CasteljosAlgorithm(points, parameter, levelNumber - 1, pointNumber), CasteljosAlgorithm(points, parameter, levelNumber - 1, pointNumber + 1), parameter);
        }

        public List<Line> GetNewBezierCurve(List<Point> points)
        {
            var bezier = new List<Line>();
            int number = 200;
            double step = 1.0 / number;
            double parameter = 0.0;
            PointF startPoint = CasteljosAlgorithm(points, parameter, 3, 0);

            for (int i = 0; i < number; i++)
            {
                parameter += step;
                PointF nextPoint = CasteljosAlgorithm(points, parameter, 3, 0);
                var line = new Line
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 5,
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = nextPoint.X,
                    Y2 = nextPoint.Y
                };
                bezier.Add(line);
                startPoint = nextPoint;
            }

            return bezier;
        }
    }
}
