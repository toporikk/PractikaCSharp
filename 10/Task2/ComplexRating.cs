using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class ComplexRating : IRatingStrategy
    {
        public double CalculateRating(int userId)
        {
            Console.WriteLine("Используется сложный алгоритм (активность + карма + отзывы).");
            return 8.5; 
        }
    }
}
