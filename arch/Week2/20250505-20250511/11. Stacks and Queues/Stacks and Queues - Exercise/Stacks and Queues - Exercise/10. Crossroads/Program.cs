namespace _10._Crossroads;
class Program
{
    static void Main()
    {
        int greenLight = int.Parse(Console.ReadLine());
        int freeWindow = int.Parse(Console.ReadLine());

        Queue<string> carsQueue = new Queue<string>();
        int totalCarsPassed = 0;

        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            if (input == "green")
            {
                int timeLeft = greenLight;

                while (carsQueue.Count > 0 && timeLeft > 0)
                {
                    string car = carsQueue.Dequeue();
                    int carLength = car.Length;

                    if (carLength <= timeLeft)
                    {
                        // Car fully passes during green light
                        timeLeft -= carLength;
                        totalCarsPassed++;
                    }
                    else
                    {
                        int remaining = carLength - timeLeft;

                        if (remaining <= freeWindow)
                        {
                            // Car finishes during free window
                            totalCarsPassed++;
                            break; // Free window only applies to one car
                        }
                        else
                        {
                            // Crash!
                            int hitIndex = timeLeft + freeWindow;
                            Console.WriteLine("A crash happened!");
                            Console.WriteLine($"{car} was hit at {car[hitIndex]}.");
                            return;
                        }
                    }
                }
            }
            else
            {
                carsQueue.Enqueue(input);
            }
        }

        Console.WriteLine("Everyone is safe.");
        Console.WriteLine($"{totalCarsPassed} total cars passed the crossroads.");
    }
}