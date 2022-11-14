namespace Client.Helpers;

public static class RandomGenerator
{
    public static Task<int> NumberGenerator(int max)
    {
        var random = new Random();
        return Task.FromResult(random.Next(1, max));
    }

    public static int NumberGenerator(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max);
    }

    public static async Task<IList<int>> ListNumberGenerator(int listSize)
    {
        var listNumberGenerator = new List<int>();
        if (listSize == 1)
        {
            var randomNumber = await NumberGenerator(listSize);
            listNumberGenerator.Add(randomNumber);
            return await Task.FromResult<IList<int>>(listNumberGenerator);
        }

        var numbersToGenerate = Math.Floor((double) (listSize / 3));
        while (listNumberGenerator.Count != (int) numbersToGenerate)
        {
            var randomNumber = await NumberGenerator(listSize);
            if (!listNumberGenerator.Contains(randomNumber))
            {
                listNumberGenerator.Add(randomNumber);
            }
        }

        return await Task.FromResult<IList<int>>(listNumberGenerator);
    }
}