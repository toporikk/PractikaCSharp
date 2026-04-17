using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите значение x: ");
        if (double.TryParse(Console.ReadLine(), out double x))
        {
            double y;
            if (x >= 0.1 && x <= 1.5)
            {
                y = Math.Log(Math.Exp(x) + 4) - 2 * x;
                Console.WriteLine($"Результат (1-я ветвь): y = {y:F4}");
            }
            else if (x > 1.5)
            {
                y = Math.Pow(x, 2) - 1;
                Console.WriteLine($"Результат (2-я ветвь): y = {y:F4}");
            }
            else
            {
                Console.WriteLine("Значение x не входит в область определения функции (x < 0.1).");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введено некорректное число.");
        }
    }
}
