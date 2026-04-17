using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Проверка треугольника на прямоугольность.");
        Console.Write("Введите сторону a: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double b = double.Parse(Console.ReadLine());

        Console.Write("Введите сторону c: ");
        double c = double.Parse(Console.ReadLine());
        bool isRight = (a * a + b * b == c * c) ||
                       (a * a + c * c == b * b) ||
                       (b * b + c * c == a * a);

        if (isRight)
        {
            Console.WriteLine("Результат: Треугольник является прямоугольным.");
        }
        else
        {
            Console.WriteLine("Результат: Треугольник НЕ является прямоугольным.");
        }
    }
}
