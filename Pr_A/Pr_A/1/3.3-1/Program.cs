using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Трёхзначные автоморфные числа:");
        for (int n = 100; n <= 999; n++)
        {
            long square = (long)n * n;
            if (square % 1000 == n)
            {
                Console.WriteLine($"{n} (квадрат {square})");
            }
        }

        Console.ReadKey();
    }
}
