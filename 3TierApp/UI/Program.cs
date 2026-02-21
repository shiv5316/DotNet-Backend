using System;
using BL;

namespace UI
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    BLCalc bl = new BLCalc();

        //    Console.WriteLine("Enter first number:");
        //    int a = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("Enter second number:");
        //    int b = Convert.ToInt32(Console.ReadLine());

        //    int result = bl.AddNumbers(a, b);

        //    Console.WriteLine("Result = " + result);
        //    Console.ReadLine();
        //}



        //String Reverse
        static void Main(string[] args)
        {
            BLCalc bl=new BLCalc();
            Console.WriteLine("Enter string:");
            string input=Console.ReadLine();
            string result=bl.Reverse(input);
            Console.WriteLine("Reversed string= "+result);
            Console.ReadLine();
        }
    }
}
