using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите длину первого катета: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите длину второго катета: ");
        double b = Convert.ToDouble(Console.ReadLine());

        double c = Math.Sqrt(a * a + b * b);
        double perimeter = a + b + c;

        Console.WriteLine($"Периметр треугольника: {perimeter:F2}");
    }
}