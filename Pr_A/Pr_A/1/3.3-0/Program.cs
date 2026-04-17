using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите натуральное число: ");
        long n = long.Parse(Console.ReadLine());
        long max = 0;
        long min = 9;
        if (n == 0) min = 0;

        while (n > 0)
        {
            long digit = n % 10; 

            if (digit > max) max = digit; 
            if (digit < min) min = digit; 

            n /= 10; 
        }

        Console.WriteLine($"Наибольшая цифра: {max}");
        Console.WriteLine($"Наименьшая цифра: {min}");
    }
}
