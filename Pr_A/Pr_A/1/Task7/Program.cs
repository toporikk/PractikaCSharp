using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите A: ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Введите B: ");
        int b = int.Parse(Console.ReadLine());

        if (a > b) { int temp = a; a = b; b = temp; }

        // --- 1. FOR ---
        Console.WriteLine("\nСпособ FOR:");
        for (int i = a; i <= b; i++)
        {
            if ((Math.Abs(i) % 10) % 2 == 0) 
                Console.Write(i + " ");
        }

        // --- 2. WHILE ---
        Console.WriteLine("\n\nСпособ WHILE:");
        int j = a;
        while (j <= b)
        {
            if ((Math.Abs(j) % 10) % 2 == 0)
                Console.Write(j + " ");
            j++;
        }

        // --- 3. DO WHILE ---
        Console.WriteLine("\n\nСпособ DO WHILE:");
        int k = a;
        do
        {
            if ((Math.Abs(k) % 10) % 2 == 0)
                Console.Write(k + " ");
            k++;
        } while (k <= b);

        Console.WriteLine("\n\nГотово! Нажмите любую клавишу...");
        Console.ReadKey();
    }
}
