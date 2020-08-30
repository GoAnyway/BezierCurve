using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Point = System.Drawing.Point;

namespace Bezier.FileManager
{
    public class BezierFileManager
    {
        private FileReader _fileReader;
        private Parser _parser;

        public IEnumerable<List<Point>> GetPointsFromFile(string filePath)
        {
            var points = new List<Point>();
            _parser = new Parser(GetContent(filePath));
            foreach (var point in _parser.GetParsedContent())
            {
                points.Add(point);
            
                if(points.Count == 4)
                {
                    yield return points;
                    points.Clear();
                }
            }
        }

        public string GetContent(string filePath)
        {
            _fileReader = new FileReader(filePath);

            return _fileReader.GetContentFromFile();
        }

        public string OpenFileDialogWindow()
        {
            string systemDisk = new DirectoryInfo(Process.GetCurrentProcess()
                                                        .MainModule
                                                        .FileName).Root
                                                                  .Name;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = systemDisk,
                    Title = "Choose file with Beziers",
                    Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var filePath = openFileDialog.FileName;

                    return filePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            throw new InvalidOperationException("File was not selected");
        }
    }
}
