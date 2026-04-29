using Task2;

class Program
{
    static void Main()
    {
        int testUserId = 101;

        var ratingService = new UserRatingService(new SimpleRating());
        ratingService.DisplayRating(testUserId);

        ratingService.SetStrategy(new ComplexRating());
        ratingService.DisplayRating(testUserId);

        ratingService.SetStrategy(new MachineLearningRating());
        ratingService.DisplayRating(testUserId);
    }
}