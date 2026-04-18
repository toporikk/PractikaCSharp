using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер матрицы N (N < 10): ");
        int n = int.Parse(Console.ReadLine());

        if (n >= 10 || n <= 0)
        {
            Console.WriteLine("Ошибка: N должно быть меньше 10 и больше 0.");
            return;
        }

        Console.Write("Введите начало диапазона a: ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Введите конец диапазона b: ");
        int b = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        Console.WriteLine("\nИсходная матрица:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(a, b + 1); 
                Console.Write($"{matrix[i, j],4} ");
            }
            Console.WriteLine();
        }

        Console.Write("\nВведите число H для сравнения: ");
        int h = int.Parse(Console.ReadLine());
        int countLessH = 0;

        foreach (int item in matrix)
        {
            if (item < h) countLessH++;
        }

        Console.Write($"Введите номер столбца k (от 1 до {n}): ");
        int k = int.Parse(Console.ReadLine());

        int oddInColumn = 0;
        if (k >= 1 && k <= n)
        {
            for (int i = 0; i < n; i++)
            {
                if (matrix[i, k - 1] % 2 != 0)
                {
                    oddInColumn++;
                }
            }
        }
        else
        {
            Console.WriteLine("Ошибка: неверный номер столбца.");
        }

        Console.WriteLine($"Количество чисел меньше {h}: {countLessH}");
        if (k >= 1 && k <= n)
            Console.WriteLine($"Количество нечётных элементов в {k}-м столбце: {oddInColumn}");
    }
}
