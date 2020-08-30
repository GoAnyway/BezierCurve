using Bezier.FileManager;
using Bezier.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bezier.Commands
{
    public class OpenFileCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly BezierFileManager _fileManager = new BezierFileManager();
        private readonly BezierCurveModel _bezierCurveModel = new BezierCurveModel();

        public OpenFileCommand(MainViewModel model) => _mainViewModel = model;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                var filePath = _fileManager.OpenFileDialogWindow();

                _mainViewModel.BezierCurves.Clear();
                var pointsFromFile = _fileManager.GetPointsFromFile(filePath);
                if (pointsFromFile.Count() > 0)
                {
                    foreach (var points in pointsFromFile)
                    {
                        _mainViewModel.BezierCurves.Add(_bezierCurveModel.GetNewBezierCurve(points));
                    }
                }
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
    }
}
