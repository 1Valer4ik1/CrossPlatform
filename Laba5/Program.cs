using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LabTools
{
    public class SheetProcessor
    {
        public static void ExecuteLab1(string input, string output)
        {
            var lines = File.ReadAllLines(input);
            int N = int.Parse(lines[0]);
            var sheets = new List<Sheet>();

            for (int i = 0; i < N; i++)
            {
                var values = lines[i + 1].Split(' ');
                double ai = double.Parse(values[0], CultureInfo.InvariantCulture);
                double bi = double.Parse(values[1], CultureInfo.InvariantCulture);
                sheets.Add(new Sheet(i + 1, ai, bi));
            }

            sheets.Sort((x, y) => Math.Min(y.A, y.B).CompareTo(Math.Min(x.A, x.B)));

            double totalTime = 0, dissolutionTimeA = 0;
            foreach (var sheet in sheets)
            {
                dissolutionTimeA += sheet.A;
                totalTime = Math.Max(totalTime, dissolutionTimeA) + sheet.B;
            }

            using (var writer = new StreamWriter(output))
            {
                writer.WriteLine(totalTime.ToString("F3"));
                writer.WriteLine(string.Join(" ", sheets.Select(s => s.Index)));
            }
        }
    }

    public class Sheet
    {
        public int Index { get; }
        public double A { get; }
        public double B { get; }

        public Sheet(int index, double a, double b)
        {
            Index = index;
            A = a;
            B = b;
        }
    }
}
