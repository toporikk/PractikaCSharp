using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrderAnalysisSystem
{
    class Program
    {
        static void Main()
        {
            string fileName = "file.data";

            File.WriteAllLines(fileName, new[] {
                "1;Мышь;10",
                "2;Клавиатура;5",
                "3;Мышь;2",
                "4;Монитор;3",
                "5;Клавиатура;8",
                "6;Коврик;20"
            });

            OrderFileReader reader = new OrderFileReader(fileName);
            List<Order> loadedOrders = reader.ReadOrders();

            OrderProcessor processor = new OrderProcessor(loadedOrders);
            processor.GetMostPopularProducts(3);

            Console.ReadKey();
        }
    }
}
