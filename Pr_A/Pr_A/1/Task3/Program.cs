using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A (-5 <= A <= 5): ");
        double A = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите N (1 <= N <= 10): ");
        int N = Convert.ToInt32(Console.ReadLine());
        if (A < -5 || A > 5)
        {
            Console.WriteLine("Ошибка: A выходит за пределы [-5, 5]");
            return;
        }
        if (N < 1 || N > 10)
        {
            Console.WriteLine("Ошибка: N выходит за пределы [1, 10]");
            return;
        }
        double result = 1.0;
        for (int i = 1; i <= N; i++)
        {
            result *= A;
        }
        Console.WriteLine($"{result:F4}");
    }
}