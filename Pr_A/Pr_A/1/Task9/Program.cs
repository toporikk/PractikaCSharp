using System;

class Program
{
    static void Main()
    {
        double A = 0.1;
        double B = 2.1;
        int M = 20;

        double H = (B - A) / M;

        double x = A;

        Console.WriteLine("{0,10} | {1,10}", "x", "y");
        Console.WriteLine("---------------------------");

        for (int i = 0; i <= M; i++)
        {
            double y = x * Math.Exp(-Math.Pow(x, 2));
            Console.WriteLine("{0,10:F2} | {1,10:F4}", x, y);
            x += H;
        }

        Console.ReadKey();
    }
}
