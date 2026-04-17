using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество чисел n: ");
        if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
        {
            double maxAbs = 0;

            for (int i = 1; i <= n; i++)
            {
                Console.Write($"Введите число a{i}: ");
                double a = double.Parse(Console.ReadLine());
                double currentAbs = Math.Abs(a);
                if (i == 1 || currentAbs > maxAbs)
                {
                    maxAbs = currentAbs;
                }
            }

            Console.WriteLine($"\nМаксимальный модуль |ai|: {maxAbs}");
        }
        else
        {
            Console.WriteLine("Ошибка: n должно быть натуральным числом.");
        }
    }
}
