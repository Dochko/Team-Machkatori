using System;
using System.Reflection.Emit;

namespace HeroesOfFate.GameEngine.IO
{
    public class ConsoleReader
    {
        public string ReadCommand()
        {
            var input = Console.ReadLine();

            return input;
        }
    }
}