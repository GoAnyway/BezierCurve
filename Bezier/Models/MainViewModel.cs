using Bezier.Commands;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Bezier.Models
{
    public class MainViewModel
    {
        public Timer RebuildTimer { get; } = new Timer(40); 
        public Canvas BezierCanvas { get; } = new Canvas();
        public List<List<Line>> BezierCurves { get; private set; } = new List<List<Line>>();

        public ICommand DrawCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand RebuildCommand { get; private set; }
        public ICommand OpenFileCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public MainViewModel()
        {
            DrawCommand = new DrawCommand(this);
            ClearCommand = new ClearCommand(this);
            RebuildCommand = new RebuildCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
            ExitCommand = new ExitCommand();
        }
    }
}
