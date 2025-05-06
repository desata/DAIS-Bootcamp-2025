using System.ComponentModel;
using System;
using System.Diagnostics.CodeAnalysis;

namespace _05._Special_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //A number is special when its sum of digits is 5, 7 or 11.
            //Write a program to read an integer n and for all numbers in the range 1…n to print the number and if it is
            //special or not (True / False).


            int input = int.Parse(Console.ReadLine());
            

            for (int i = 1; i <= input; i++)
            {
                int sum = 0;
                int number = i;
                while (number > 0)
                {
                    sum += number % 10;
                    number /= 10;
                }
                bool special = false;

                if (sum == 5 || sum == 7 || sum == 11)
                {
                    special = true;
                }
                Console.WriteLine($"{i} -> {special}");
            }
        }
    }
}
