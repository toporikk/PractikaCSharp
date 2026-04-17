using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите номер масти (1-4): ");
        int m = int.Parse(Console.ReadLine());
        switch (m)
        {
            case 1:
                Console.WriteLine("пики");
                break;
            case 2:
                Console.WriteLine("трефы");
                break;
            case 3:
                Console.WriteLine("бубны");
                break;
            case 4:
                Console.WriteLine("червы");
                break;
            default:
                Console.WriteLine("Ошибка: масти с таким номером не существует.");
                break;
        }
    }
}
