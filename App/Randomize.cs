namespace App
{
    public class Randomize
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public static char RandomChar() => RandomString(1)[0];

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}
