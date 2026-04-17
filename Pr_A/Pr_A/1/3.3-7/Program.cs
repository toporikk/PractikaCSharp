using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите натуральное число n: ");
        if (long.TryParse(Console.ReadLine(), out long n) && n > 0)
        {
            int count = 0;
            for (long i = 1; i <= n; i++)
            {
                if (n % i == 0)
                {
                    count++;
                }
            }

            Console.WriteLine($"Количество делителей числа {n}: {count}");
        }
        else
        {
            Console.WriteLine("Ошибка: введите целое положительное число.");
        }
    }
}
