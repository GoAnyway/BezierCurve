using Bezier.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace Bezier.Commands
{
    public class DrawCommand : ICommand
    {
        private readonly MainViewModel _model;

        public DrawCommand(MainViewModel model) => _model = model;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                foreach (var bezierCurve in _model.BezierCurves)
                {
                    foreach (var line in bezierCurve)
                    {
                        _model.BezierCanvas.Children.Add(line);
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"This Bezier Curve is already drown");
            }
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
    }
}
