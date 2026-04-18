using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Введите числа через пробел: ");
        int[] a = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        if (a.Length == 0) return;

        bool alternates = true;
        for (int i = 0; i < a.Length - 1; i++)
        {
            if (a[i] * a[i + 1] > 0)
            {
                alternates = false;
                break;
            }
        }

        if (alternates)
        {
            Console.WriteLine("Знаки чередуются. Исходная последовательность:");
            Console.WriteLine(string.Join(" ", a));
        }
        else
        {
            Console.WriteLine("Чередования нет. Отрицательные члены:");
            var negatives = a.Where(x => x < 0);
            Console.WriteLine(string.Join(" ", negatives));
        }
    }
}
