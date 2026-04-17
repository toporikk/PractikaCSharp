using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите число: ");
        long n = Math.Abs(long.Parse(Console.ReadLine()));

        bool isOrdered = true;
        while (n > 9)
        {
            long currentDigit = n % 10;     
            long nextDigit = (n / 10) % 10;
            if (nextDigit <= currentDigit)
            {
                isOrdered = false;
                break;
            }

            n /= 10; 
        }

        if (isOrdered)
            Console.WriteLine("Да, цифры упорядочены по возрастанию справа налево.");
        else
            Console.WriteLine("Нет, цифры не упорядочены по возрастанию справа налево.");
    }
}
