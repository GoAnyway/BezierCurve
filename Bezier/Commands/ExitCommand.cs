using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Bezier.Commands
{
    public class ExitCommand : ICommand
    {
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Process.GetCurrentProcess().Kill();
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067
    }
}
