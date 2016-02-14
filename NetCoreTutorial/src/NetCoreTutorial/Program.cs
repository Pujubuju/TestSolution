using System;
using MyLibrary;

namespace NetCoreTutorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hellow world from .net Core! :)");

            Greeter greeter = new Greeter();

            Console.WriteLine(greeter.Greet());

            Console.ReadKey();
        }
    }
}
