using System;

namespace LSPDemo
{
    // Parent class
    public class SchoolBag
    {
        public virtual void CarryBooks()
        {
            Console.WriteLine("SchoolBag is carrying books");
        }
    }

    // Good child
    public class ZipBag : SchoolBag
    {
        public override void CarryBooks()
        {
            Console.WriteLine("ZipBag is carrying books");
        }
    }

    // Problem child
    public class DecorativeBag : SchoolBag
    {
        public override void CarryBooks()
        {
            throw new Exception("❌ DecorativeBag cannot carry books");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LSP Simple Demo ===");

            try
            {
                // Parent reference holding ZipBag
                SchoolBag bag1 = new ZipBag();
                Console.WriteLine("\nUsing ZipBag:");
                bag1.CarryBooks();   // ✅ Works

                // Parent reference holding DecorativeBag
                SchoolBag bag2 = new DecorativeBag();
                Console.WriteLine("\nUsing DecorativeBag:");
                bag2.CarryBooks();   // ❌ Crash
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nProgram Ended");
            Console.ReadLine();
        }
    }
}