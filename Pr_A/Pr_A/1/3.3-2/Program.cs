using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Двузначные числа, сумма квадратов цифр которых кратна 13:");
        for (int i = 10; i <= 99; i++)
        {
            int firstDigit = i / 10; 
            int secondDigit = i % 10; 
            int sumOfSquares = (firstDigit * firstDigit) + (secondDigit * secondDigit);
            if (sumOfSquares % 13 == 0)
            {
                Console.WriteLine($"Число: {i} (Сумма квадратов: {sumOfSquares})");
            }
        }

        Console.ReadKey();
    }
}
