using System.Collections.Generic;
using System.Drawing;

namespace Bezier.FileManager
{
    public class Parser
    {
        private readonly string _content;

        public Parser(string content)
        {
            _content = content;
        }

        public IEnumerable<Point> GetParsedContent()
        {
            foreach (var line in _content.Split(';'))
            {
                var points = line.Split(',');

                for (int i = 0; i < points.Length - 1; i += 2)
                {
                    yield return new Point(int.Parse(points[i]), int.Parse(points[i + 1]));
                }
            }
        }
    }
}
