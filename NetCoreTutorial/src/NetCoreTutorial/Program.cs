using System;
using MyLibrary;

namespace NetCoreTutorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hellow world from .net Core! :)");

            MyClass myClass = new MyClass();

            Console.WriteLine(myClass.Greet());

            Console.ReadKey();
        }
    }
}
