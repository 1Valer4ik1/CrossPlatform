using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string input = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\INPUT.txt");
        string output = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\OUTPUT.txt");

        // Зчитуємо розміри поля M x N
        var lines = File.ReadAllLines(input);
        string[] inputData = lines[0].Split(' ');
        int M = int.Parse(inputData[0]); // кількість рядків
        int N = int.Parse(inputData[1]); // кількість стовпців

        int count = 0;

        // Генерація всіх можливих візерунків для поля M x N
        int totalPatterns = 1 << (M * N); // 2^(M*N)
        for (int mask = 0; mask < totalPatterns; mask++)
        {
            // Перетворення маски в поле
            int[,] field = new int[M, N];
            int idx = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    field[i, j] = (mask >> idx) & 1;
                    idx++;
                }
            }

            // Перевірка на симпатичність
            if (IsSympathetic(field))
            {
                count++;
            }
        }

        // Запис результатів у файл OUTPUT.TXT
        using (var writer = new StreamWriter(output))
        {
            writer.WriteLine(count);
        }
    }

    // Функція для перевірки симпатичності візерунка
    static bool IsSympathetic(int[,] field)
    {
        for (int i = 1; i < field.GetLength(0); i++) // Перевіряємо від 1 до M-1
        {
            for (int j = 1; j < field.GetLength(1); j++) // Перевіряємо від 1 до N-1
            {
                // Перевірка на квадрат 2x2 з однаковими кольорами
                if (field[i, j] == field[i - 1, j - 1] &&
                    field[i, j] == field[i - 1, j] &&
                    field[i, j] == field[i - 1, j - 1] &&
                    field[i, j] == field[i, j - 1])
                {
                    return false; // Якщо такий квадрат знайдений, візерунок не симпатичний
                }
            }
        }
        return true; // Якщо жоден квадрат не знайдений, візерунок симпатичний
    }
}
