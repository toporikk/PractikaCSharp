using System;
using System.Linq;

class Program
{
    static void Main()
    {
        CarPart[] parts = new CarPart[]
        {
            new Battery("Bosch Silver"),
            new Gearbox("ZF 8HP"),
            new Battery("Varta Blue"),
            new Gearbox("Manual 5-speed")
        };

        foreach (var part in parts)
        {
            if (part is IMechanicalComponent mechanicalPart)
            {
                Console.WriteLine($"Найдена механическая деталь: {part.Name}");
                mechanicalPart.CheckWear(); 
            }
        }
    }
}
