using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов массива: ");
        int n = int.Parse(Console.ReadLine());

        double[] arr = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введите {i + 1}-й элемент: ");
            arr[i] = double.Parse(Console.ReadLine());
        }

        int indexMax = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > arr[indexMax])
            {
                indexMax = i;
            }
        }

        Console.WriteLine($"\nМаксимальный элемент: {arr[indexMax]}");
        Console.WriteLine($"Порядковый номер: {indexMax + 1}");
    }
}
