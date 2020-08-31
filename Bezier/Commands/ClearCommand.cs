using Bezier.Models;
using System;
using System.Windows.Input;

namespace Bezier.Commands
{
    public class ClearCommand : ICommand
    {
        private readonly MainViewModel _model;

        public ClearCommand(MainViewModel model) => _model = model;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _model.BezierCanvas.Children.Clear();
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
    }
}
