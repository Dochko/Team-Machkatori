namespace HeroesOfFate.GameEngine.IO
{
    using System;

    public class ConsoleWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}