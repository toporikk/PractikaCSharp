using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());
        if (number < 100 || number > 999)
        {
            Console.WriteLine("Ошибка: число не является трёхзначным.");
            return;
        }
        int hundreds = number / 100;        
        int tens = (number / 10) % 10;     
        int units = number % 10;            
        bool allDigitsDifferent = (hundreds != tens) && (tens != units) && (hundreds != units);
        Console.WriteLine($"Все цифры числа {number} различны: {allDigitsDifferent}");
    }
}