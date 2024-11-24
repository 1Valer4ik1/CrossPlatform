using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Шляхи до файлів
        string input = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\INPUT.txt");
        string output = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\OUTPUT.txt");

        // Зчитування даних з INPUT.TXT
        var lines = File.ReadAllLines(input);
        int N = int.Parse(lines[0]); // Кількість листів
        var sheets = new List<Sheet>();

        for (int i = 0; i < N; i++)
        {
            var values = lines[i + 1].Split(' ');
            double ai = double.Parse(values[0], CultureInfo.InvariantCulture);
            double bi = double.Parse(values[1], CultureInfo.InvariantCulture);
            sheets.Add(new Sheet(i + 1, ai, bi));
        }

        // Сортування для досягнення максимального часу розчинення
        sheets.Sort((x, y) =>
        {
            double xMin = Math.Min(x.A, x.B);
            double yMin = Math.Min(y.A, y.B);
            return yMin.CompareTo(xMin);
        });

        // Обчислення часу розчинення перегородки
        double dissolutionTimeA = 0;
        double totalTime = 0;

        foreach (var sheet in sheets)
        {
            dissolutionTimeA += sheet.A;              // Час розчинення рідиною A
            totalTime = Math.Max(totalTime, dissolutionTimeA) + sheet.B; // Додаємо час для рідини B
        }

        // Запис результатів у OUTPUT.TXT
        using (var writer = new StreamWriter(output))
        {
            writer.WriteLine(totalTime.ToString("F3", CultureInfo.InvariantCulture));
            writer.WriteLine(string.Join(" ", sheets.Select(s => s.Index)));
        }
    }

    // Клас для опису листів
    class Sheet
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
