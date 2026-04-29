using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class SimpleRating : IRatingStrategy
    {
        public double CalculateRating(int userId)
        {
            Console.WriteLine("Используется простой алгоритм расчета (по количеству постов).");
            return 5.0; 
        }
    }
}
