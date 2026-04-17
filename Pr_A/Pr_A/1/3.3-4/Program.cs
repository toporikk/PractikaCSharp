using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите начальное число n: ");
        if (long.TryParse(Console.ReadLine(), out long n) && n > 0)
        {
            int steps = 0;
            Console.Write($"Последовательность для {n}: {n}");

            while (n != 1)
            {
                if (n % 2 == 0)
                    n /= 2;
                else
                    n = 3 * n + 1;

                steps++;
                Console.Write($", {n}");
            }

            Console.WriteLine($"\n\nКоличество шагов до единицы: {steps}");
        }
        else
        {
            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }
}
