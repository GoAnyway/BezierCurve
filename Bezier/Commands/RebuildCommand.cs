using Bezier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Drawing.Point;

namespace Bezier.Commands
{
    public class RebuildCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly BezierCurveModel _bezierCurveModel = new BezierCurveModel();
        private double _parameter = 0.0;
        public Timer Timer { get; } = new Timer(40);

        public RebuildCommand(MainViewModel model) => _mainViewModel = model;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if(!Timer.Enabled)
            {
                Timer.Elapsed += TimerTick;
                Timer.Start();
            }
        }

        private void Render()
        {
            if(_mainViewModel.BezierCurves.Count > 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _mainViewModel.BezierCanvas.Children.Clear();

                    var lines = _mainViewModel.BezierCurves.Last();
                    var points = new List<Point>();
                    foreach (var line in lines.Skip(lines.Count - 4))
                    {
                        points.Add(new Point((int)line.X1, (int)line.Y1));
                    }

                    Polyline allPointsLine = new Polyline
                    {
                        Stroke = Brushes.Pink,
                        StrokeThickness = 2
                    };
                    foreach (var point in points)
                    {
                        allPointsLine.Points.Add(new System.Windows.Point(point.X, point.Y));
                    }
                    _mainViewModel.BezierCanvas.Children.Add(allPointsLine);


                    foreach (var line in _mainViewModel.BezierCurves.Last())
                    {
                        _mainViewModel.BezierCanvas.Children.Add(line);
                    }

                    var line1 = new Line
                    {
                        Stroke = Brushes.Green,
                        StrokeThickness = 2,
                        X1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 0).X,
                        X2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 0).Y,
                        Y1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 1).X,
                        Y2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 1).Y
                    };
                    var line2 = new Line
                    {
                        Stroke = Brushes.Green,
                        StrokeThickness = 2,
                        X1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 1).X,
                        X2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 1).Y,
                        Y1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 2).X,
                        Y2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 1, 2).Y
                    };
                    _mainViewModel.BezierCanvas.Children.Add(line1);
                    _mainViewModel.BezierCanvas.Children.Add(line2);


                    var line3 = new Line
                    {
                        Stroke = Brushes.Gold,
                        StrokeThickness = 2,
                        X1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 2, 0).X,
                        X2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 2, 0).Y,
                        Y1 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 2, 1).X,
                        Y2 = _bezierCurveModel.CasteljosAlgorithm(points, _parameter, 2, 1).Y

                    };
                    _mainViewModel.BezierCanvas.Children.Add(line3);

                    Ellipse ellipse = new Ellipse
                    {
                        Fill = Brushes.Red,
                        Height = 7,
                        Width = 7
                    };
                });
            }
            
        }

        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            if (_parameter > 1.0)
            {
                _parameter = 0.0;
                Timer.Stop();
            }
            else
            {
                _parameter += 0.0050;
                Render();
            }
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
    }
}