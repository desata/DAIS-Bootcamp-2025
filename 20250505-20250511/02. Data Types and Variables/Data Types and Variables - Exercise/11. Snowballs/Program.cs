using System.Numerics;

namespace _11._Snowballs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            BigInteger snowballValue = 0;
            BigInteger maxSnowballValue = 0;
            Dictionary<int, List<int>> snowballData = new Dictionary<int, List<int>>();
            int snowNumber = 0;
            string result = "";

            for (int i = 0; i < n; i++)
            {
                int snowballSnow = int.Parse(Console.ReadLine());
                int snowballTime = int.Parse(Console.ReadLine());
                int snowballQuality = int.Parse(Console.ReadLine());

                snowballData.Add(i, new List<int> { snowballSnow, snowballTime, snowballQuality });

            }

            foreach (var snowball in snowballData)
            {
                snowballValue = BigInteger.Pow(snowball.Value[0] / snowball.Value[1], (int)snowball.Value[2]);

                if (snowballValue > maxSnowballValue)
                {
                    maxSnowballValue = snowballValue;
                    snowNumber = snowball.Key;
                    result = $"{snowball.Value[0]} : {snowball.Value[1]} = {maxSnowballValue} ({snowball.Value[2]})";
                }


            }

            Console.WriteLine(result);
        }
    }
}
