namespace HeroesOfFate.GameEngine.IO
{
    using System;

    public class ConsoleReader
    {
        public string ReadCommand()
        {
            var input = Console.ReadLine();

            return input;
        }
    }
}