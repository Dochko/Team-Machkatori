namespace HeroesOfFate.GameEngine.IO
{
    using System;
    using System.Reflection.Emit;

    public class ConsoleReader
    {
        public string ReadLine()
        {
            var input = Console.ReadLine();

            return input;
        }

        public ConsoleKeyInfo ReadKey()
        {
            var input = Console.ReadKey();

            return input;
        }
    }
}