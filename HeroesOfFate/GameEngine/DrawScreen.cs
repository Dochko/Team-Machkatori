using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesOfFate.GameEngine
{
    static class DrawScreen
    {
        public static List<string> area1 = new List<string>();
        public static List<string> area2 = new List<string>();
        const int areaHeights = (40 - 2) / 2;
        public static List<string> AddMap()
        {
            List<string> map = new List<string>();
            using (StreamReader file = new StreamReader("..\\..\\resources\\map.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    map.Add(line);
                }
            }
            return map;
        }
        public static void AddLineToBuffer(ref List<string> areaBuffer, string line)
        {
            areaBuffer.Insert(0, line);

            if (areaBuffer.Count == areaHeights)
            {
                areaBuffer.RemoveAt(areaHeights - 1);
            }
        }
        public static void drawScreen(List<string> areaSelect1, List<string> areaSelect2)
        {
            Console.Clear();

            // Draw the area divider
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, areaHeights);
                Console.Write('=');
            }

            int currentLine = areaHeights - 1;
            
            //draw first area
            for (int i = 0; i < areaSelect1.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));
                Console.WriteLine(areaSelect1[i]);

            }

            currentLine = (areaHeights * 2);
            //draw second area
            for (int i = 0; i < areaSelect2.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));
                Console.WriteLine(areaSelect2[i]);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("> ");
        }
    }
}
