using System;

namespace DALCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            BLCalc bl = new BLCalc();

            Console.WriteLine("Enter first number:");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter second number:");
            int b = Convert.ToInt32(Console.ReadLine());

            int result = bl.AddNumbers(a, b);

            Console.WriteLine("Result = " + result);
            Console.ReadLine();
        }
    }
}
