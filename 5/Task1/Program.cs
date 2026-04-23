using System;

class Program
{
    static void Main()
    {
        Movement[] movements = new Movement[]
        {
            new Walking(),
            new Running(),
            new Cycling()
        };

        Console.WriteLine("Начинаем движение:");
        foreach (var m in movements)
        {
            m.Move();
        }
    }
}
