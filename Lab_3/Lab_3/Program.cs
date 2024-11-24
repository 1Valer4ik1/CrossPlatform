using System;
using System.IO;

class Program
{
    static int N, M;
    static int[,] grid;
    static int[,] dist;
    static (int, int)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) }; // Рух праворуч, вниз, вліво, вгору

    static void Main()
    {
        // Шляхи до файлів
        string input = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\INPUT.TXT");
        string output = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\OUTPUT.TXT");

        try
        {
            // Зчитуємо всі рядки з вхідного файлу
            var lines = File.ReadAllLines(input);

            // Перевірка, чи є достатньо рядків у файлі
            if (lines.Length < 1)
            {
                Console.WriteLine("Вхідний файл порожній.");
                return;
            }

            // Зчитуємо розміри N та M з першого рядка
            string[] sizes = lines[0].Split(' ');
            if (sizes.Length != 2)
            {
                Console.WriteLine($"Невірний формат розміру. Рядок містить: {sizes.Length} значень, очікуються 2.");
                return;
            }

            // Перевірка на коректність значень N і M
            try
            {
                N = int.Parse(sizes[0]);
                M = int.Parse(sizes[1]);
            }
            catch (FormatException)
            {
                Console.WriteLine("Невірний формат числа для розміру N або M.");
                return;
            }

            // Ініціалізація масивів
            grid = new int[N, M];
            dist = new int[N, M];

            // Перевірка правильності кількості рядків у файлі
            if (lines.Length != N + 1)
            {
                Console.WriteLine("Невірна кількість рядків у файлі, потрібно на 1 більше.");
                return;
            }

            // Зчитуємо карту зараженості
            for (int i = 0; i < N; i++)
            {
                // Перевірка на правильний формат рядка
                var row = lines[i + 1].Split(' ');

                if (row.Length != M)
                {
                    Console.WriteLine($"Невірна кількість елементів у рядку {i + 1}. Очікується {M} елементів, але знайдено {row.Length}.");
                    return;
                }

                // Заповнення карти зараженості
                for (int j = 0; j < M; j++)
                {
                    grid[i, j] = int.Parse(row[j]);
                    dist[i, j] = int.MaxValue; // Ініціалізація відстаней нескінченністю
                }
            }

            // Використовуємо алгоритм Дейкстри для пошуку мінімальної радіації
            var priorityQueue = new SortedSet<(int dist, int x, int y)>(Comparer<(int dist, int x, int y)>.Create((a, b) => a.dist != b.dist ? a.dist - b.dist : a.x - b.x));
            dist[0, 0] = grid[0, 0]; // Відстань до початкової клітини
            priorityQueue.Add((dist[0, 0], 0, 0));

            while (priorityQueue.Count > 0)
            {
                var (currentDist, x, y) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                // Якщо ми досягли правої нижньої клітини
                if (x == N - 1 && y == M - 1)
                {
                    break;
                }

                // Перевіряємо сусідів
                foreach (var (dx, dy) in directions)
                {
                    int nx = x + dx, ny = y + dy;

                    // Перевірка, щоб індекси не виходили за межі масиву
                    if (nx >= 0 && nx < N && ny >= 0 && ny < M)
                    {
                        int newDist = currentDist + grid[nx, ny];
                        if (newDist < dist[nx, ny])
                        {
                            dist[nx, ny] = newDist;
                            priorityQueue.Add((dist[nx, ny], nx, ny));
                        }
                    }
                }
            }

            // Записуємо результат у вихідний файл
            using (var writer = new StreamWriter(output))
            {
                writer.WriteLine(dist[N - 1, M - 1]); // Виводимо мінімальну сумарну дозу радіації
            }

            Console.WriteLine("Результат обчислений та записаний у вихідний файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}