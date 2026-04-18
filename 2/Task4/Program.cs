using System;

class Program
{
    static void Main()
    {
        int[,] matrix = {
            { 1, 5, 3 },
            { 4, 2, 8 },
            { 7, 1, 0 }
        };

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        Console.WriteLine("Матрица:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write(matrix[i, j] + "\t");
            Console.WriteLine();
        }

        Console.Write($"\nВведите номер строки k (от 1 до {rows}): ");
        int k = int.Parse(Console.ReadLine()) - 1;

        Console.Write($"Введите номер столбца s (от 1 до {cols}): ");
        int s = int.Parse(Console.ReadLine()) - 1;

        if (k < 0 || k >= rows || s < 0 || s >= cols)
        {
            Console.WriteLine("Ошибка: индекс за пределами массива.");
            return;
        }

        int rowSum = 0;
        for (int j = 0; j < cols; j++)
        {
            rowSum += matrix[k, j];
        }

        int colSum = 0;
        for (int i = 0; i < rows; i++)
        {
            colSum += matrix[i, s];
        }

        int maxVal = Math.Max(rowSum, colSum);

        Console.WriteLine($"\nСумма элементов {k + 1}-й строки: {rowSum}");
        Console.WriteLine($"Сумма элементов {s + 1}-го столбца: {colSum}");
        Console.WriteLine($"Максимальное из этих двух чисел: {maxVal}");
    }
}
