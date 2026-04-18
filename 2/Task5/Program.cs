using System;
using System.Linq; 

class Program
{
    static void Main()
    {
        int[][] jagged = new int[3][];
        jagged[0] = new int[] { 1, 5, 2 };
        jagged[1] = new int[] { 10, 3, 45, 12 };
        jagged[2] = new int[] { 7, 8 };

        Console.WriteLine("Исходный массив:");
        PrintArray(jagged);

        for (int i = 0; i < jagged.Length; i++)
        {
            Array.Sort(jagged[i]);
            Array.Reverse(jagged[i]);
        }

        Console.WriteLine("\nМассив после сортировки строк по убыванию:");
        PrintArray(jagged);
    }

    static void PrintArray(int[][] arr)
    {
        foreach (var row in arr)
        {
            Console.WriteLine(string.Join("\t", row));
        }
    }
}
