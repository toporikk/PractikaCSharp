using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class UserRatingService
    {
        private IRatingStrategy _strategy;

        public UserRatingService(IRatingStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IRatingStrategy strategy)
        {
            _strategy = strategy;
        }

        public void DisplayRating(int userId)
        {
            double rating = _strategy.CalculateRating(userId);
            Console.WriteLine($"Итоговый рейтинг пользователя {userId}: {rating}\n");
        }
    }
}
