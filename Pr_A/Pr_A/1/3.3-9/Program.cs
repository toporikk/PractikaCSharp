using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Двузначные числа, равные утроенному произведению своих цифр:");

        for (int i = 10; i <= 99; i++)
        {
            int d1 = i / 10; 
            int d2 = i % 10; 

            if (i == 3 * (d1 * d2))
            {
                Console.WriteLine(i);
            }
        }

        Console.ReadKey();
    }
}
