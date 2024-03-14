namespace App.Shared;

public static class Randomize
{
    public static char RandomChar() => RandomString(1)[0];

    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }

    public static int RandomNumber(int min, int max)
    {
        return Random.Shared.Next(min, max);
    }
}