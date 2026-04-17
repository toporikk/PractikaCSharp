using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите число: ");
        string input = Console.ReadLine();
        string result = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                int digit = (int)char.GetNumericValue(c);
                if (digit % 2 != 0)
                {
                    result += c; 
                }
            }
        }

        if (result == "")
        {
            Console.WriteLine("Результат: (пусто, все цифры были чётными)");
        }
        else
        {
            Console.WriteLine($"Результат: {result}");
        }
    }
}
