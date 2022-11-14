namespace DiningHall.Helpers;

public static class RandomGenerator
{
    public static int NumberGenerator(int max)
    {
        var random = new Random();
        return random.Next(1, max);
    }

    public static int NumberGenerator(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max);
    }
}