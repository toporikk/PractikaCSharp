using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите число n: ");
        long n = long.Parse(Console.ReadLine());

        Console.Write("Введите цифру k (0-9): ");
        int k = int.Parse(Console.ReadLine());

        int count = 0;
        long tempN = Math.Abs(n);
        if (tempN == 0 && k == 0) count = 1;

        while (tempN > 0)
        {
            if (tempN % 10 == k)
            {
                count++;
            }
            tempN /= 10;
        }

        Console.WriteLine($"Цифра {k} встречается в числе {n} раз(а): {count}");
        Console.ReadKey();
    }
}
