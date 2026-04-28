using System;
using System.Collections.Generic;
using System.IO;

namespace FileOrderSystem
{
    class Program
    {
        static void Main()
        {
            string fileName = "file.data";

            List<Order> orders = new List<Order>
            {
                new Order(1, "Ноутбук", 2),
                new Order(2, "Мышь", 10),
                new Order(3, "Монитор", 1)
            };

            OrderFileWriter writer = new OrderFileWriter(fileName);
            OrderFileReader reader = new OrderFileReader(fileName);

            writer.WriteOrders(orders);

            bool isOk = reader.VerifyFile();

            Console.WriteLine($"\nРезультат проверки файла: {(isOk ? "УСПЕШНО" : "ОШИБКА")}");

            Console.ReadKey();
        }
    }
}
