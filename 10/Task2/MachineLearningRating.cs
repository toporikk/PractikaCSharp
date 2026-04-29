using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class MachineLearningRating : IRatingStrategy
    {
        public double CalculateRating(int userId)
        {
            Console.WriteLine("Используется нейросетевая модель для прогноза рейтинга.");
            return 9.9;
        }
    }
}
