namespace _08._Beer_Kegs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfKegs = int.Parse(Console.ReadLine());
            string maxModel = "";
            double maxVolume = 0;


            for (int i = 0; i < numberOfKegs; i++)
            {
                
                string model = Console.ReadLine();
                double radius = double.Parse(Console.ReadLine());
                int height = int.Parse(Console.ReadLine());

                double volume = Math.PI * Math.Pow(radius, 2) * height;

                if (volume >= maxVolume)
                {
                    maxVolume = volume;
                    maxModel = model;
                }
            }
            Console.WriteLine(maxModel);
        }
    }
}
