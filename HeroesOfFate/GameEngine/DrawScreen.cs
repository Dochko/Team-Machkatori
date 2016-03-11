namespace HeroesOfFate.GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class DrawScreen
    {
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static List<string> Area1 = new List<string>();

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static List<string> Area2 = new List<string>();

        private const int MapAreaHeights = (40 - 2) / 2;

        public static List<string> AddMap(int[] heroPostion)
        {
            List<string> map = new List<string>();
            Random random = new Random();
            int result = random.Next(1, 4);
            StreamReader file = StreamReader.Null;
            switch (result)
            {
                case 1:
                    file = new StreamReader("..\\..\\resources\\DefaultMap.txt");
                    heroPostion[0] = 5;
                    heroPostion[1] = 1;
                    break;

                case 2:
                    file = new StreamReader("..\\..\\resources\\SoftUniMap.txt");
                    heroPostion[0] = 6;
                    heroPostion[1] = 24;
                    break;

                case 3:
                    file = new StreamReader("..\\..\\resources\\SrubskaSkara.txt");
                    heroPostion[0] = 5;
                    heroPostion[1] = 9;
                    break;
            }
            
            using (file)
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

            if (areaBuffer.Count == MapAreaHeights)
            {
                areaBuffer.RemoveAt(MapAreaHeights - 1);
            }
        }

        public static void Draw(List<string> areaSelect1, List<string> areaSelect2)
        {
            Console.Clear();

            // Draw the area divider
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, MapAreaHeights);
                Console.Write('=');
            }

            int currentLine = MapAreaHeights - 1;
            
            // draw first area
            for (int i = 0; i < areaSelect1.Count; i++)
            {
                Console.SetCursorPosition(0, currentLine - (i + 1));
                Console.WriteLine(areaSelect1[i]);
            }
            
            currentLine = MapAreaHeights * 2;

            // draw second area
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