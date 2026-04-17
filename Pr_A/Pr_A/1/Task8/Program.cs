using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите цену 1 кг конфет (A): ");
        if (double.TryParse(Console.ReadLine(), out double price))
        {
            Console.WriteLine("\nСтоимость конфет за разный вес:");
            for (double weight = 0.1; weight <= 1.05; weight += 0.1)
            {
                double currentWeight = Math.Round(weight, 1);
                if (currentWeight > 1.0) break; 

                double cost = currentWeight * price;

                Console.WriteLine($"{currentWeight} кг = {cost:F4}");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введите корректное число.");
        }
    }
}
