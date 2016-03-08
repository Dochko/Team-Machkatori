namespace HeroesOfFate.GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroesOfFate.GameEngine.Combat;
    using HeroesOfFate.Models.Characters.Heroes;

    public static class Engine
    {
        private const char WallSymbol = '█';

        private const char ChestSymbol = '▓';

        private const char MonsterSymbol = '§';

        private const char BossSymbol = 'B';

        private const char HeroSymbol = 'H';

        private static readonly Core Core = new Core();

        private static readonly List<string> Map = DrawScreen.AddMap(); // adding map to the game

        private static readonly int[] HeroPosition = { 5, 1 };

        private static char specSymbol = '═';

        public static void GameStart()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 90;
            Core.Run();

            foreach (string v in Map)
            {
                // loading the map to be printed
                DrawScreen.AddLineToBuffer(ref DrawScreen.Area1, v);
            }

            DrawScreen.Draw(DrawScreen.Area1, DrawScreen.Area2);

            while (true)
            {
                string command = Console.ReadLine();
                string[] splitCommand = command.Split(' ');
                try
                {
                    switch (splitCommand[0])
                    {
                        case "move":
                            Move(command, splitCommand);
                            break;
                        case "inventory":
                            DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, Environment.NewLine);
                            DrawScreen.AddLineToBuffer(
                                ref DrawScreen.Area2, 
                                "Your inventory.. make your choice.(press help for info)");
                            InventoryWork();
                            break;
                        case "info":
                            DrawScreen.Draw(Info(), DrawScreen.Area2);
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            throw new ArgumentException(ExceptionConstants.InvalidCommandException);
                    }
                }
                catch (ArgumentException e)
                {
                    DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, e.Message);
                }

                DrawScreen.Draw(DrawScreen.Area1, DrawScreen.Area2);
            }
        }

        private static List<string> Info()
        {
            List<string> info = new List<string>();
            string[] infoString = Core.Hero.ToString().Split('\n');
            foreach (var v in infoString)
            {
                DrawScreen.AddLineToBuffer(ref info, v);
            }

            return info;
        }

        // inventory method
        private static void InventoryWork()
        {
            DrawInventory(null, string.Empty);
            bool check = true;
            while (check)
            {
                string[] inputArgs = Console.ReadLine().Split(' ');

                try
                {
                    switch (inputArgs[0])
                    {
                        case "equip":
                            Core.Hero.Equip(Core.Hero.Inventory.ElementAt(int.Parse(inputArgs[1]) - 1));
                            break;
                        case "help":
                            DrawInventory(HelpInventoryArea(), inputArgs[0]);
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "back":
                            check = false;
                            break;
                        default:
                            throw new ArgumentException(ExceptionConstants.InvalidCommandException);
                    }
                }
                catch (ArgumentException e)
                {
                    DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, e.Message);
                }

                DrawInventory(null, string.Empty);
            }
        }

        private static List<string> HelpInventoryArea()
        {
            List<string> helpArea = new List<string>();
            DrawScreen.AddLineToBuffer(ref helpArea, "Usable commands for inventory menu:");
            DrawScreen.AddLineToBuffer(
                ref helpArea, 
                " - equip (item number) : equiping item coresponding to the specific number");
            DrawScreen.AddLineToBuffer(ref helpArea, " - back : return to the map.");
            for (int i = 0; i < 9; i++)
            {
                DrawScreen.AddLineToBuffer(ref helpArea, Environment.NewLine);
            }

            return helpArea;
        }

        private static void DrawInventory(List<string> newArea, string command)
        {
            List<string> inventoryArea = new List<string>();
            List<string> equipArea = new List<string>();

            if (command != "help")
            {
                DrawScreen.AddLineToBuffer(ref inventoryArea, "Your inventory items:");
                int i = 1;
                foreach (var items in Core.Hero.Inventory)
                {
                    DrawScreen.AddLineToBuffer(ref inventoryArea, i + ". " + items);
                    i++;
                }

                for (int j = i; j < 8; j++)
                {
                    DrawScreen.AddLineToBuffer(ref inventoryArea, Environment.NewLine);
                }

                DrawScreen.AddLineToBuffer(ref inventoryArea, new string('-', 90));
                DrawScreen.AddLineToBuffer(ref inventoryArea, "Your equipped items:");
                foreach (var equip in Core.Hero.Equipment)
                {
                    DrawScreen.AddLineToBuffer(ref equipArea, equip.ToString());
                }

                var listCombine = equipArea.Concat(inventoryArea);
                inventoryArea = listCombine.ToList();
                DrawScreen.Draw(inventoryArea, DrawScreen.Area2);
            }
            else
            {
                DrawScreen.Draw(newArea, DrawScreen.Area2);
            }
        }



        // move method
        private static void Move(string command, string[] splitCommand)
        {
            DrawScreen.AddLineToBuffer(
                ref DrawScreen.Area2, 
                string.Format(ExceptionConstants.MovingMessage, splitCommand[1], splitCommand[2]));

                // output here "command"
            int[] tempposition = { HeroPosition[0], HeroPosition[1] };
            try
            {
                ChangeHeroCoordinates(int.Parse(splitCommand[1]), splitCommand[2]);
                CheckValidMapMove(Map.Count, Map[0].Length);
                HeroMove(Map, tempposition, HeroPosition, splitCommand[2], Core.Hero);
                if (((Core.Hero.Health + int.Parse(splitCommand[1])) * 2) <= Core.Hero.MaxHealth)
                {
                    Core.Hero.Health += int.Parse(splitCommand[1]) * 2; // Health regen per move
                }
                else
                {
                    Core.Hero.Health = Core.Hero.MaxHealth;
                }
            }
            catch (ArgumentException e)
            {
                HeroPosition[0] = tempposition[0];
                HeroPosition[1] = tempposition[1];
                DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, e.Message);
            }
            catch (FormatException)
            {
                DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, ExceptionConstants.InvalidNumberEnteredException);
            }

            DrawScreen.Area1.Clear();
            foreach (string v in Map)
            {
                DrawScreen.AddLineToBuffer(ref DrawScreen.Area1, v);
            }
        }

        private static void CheckValidMapMove(int height, int width)
        {
            // check if move is outside the map
            if ((HeroPosition[0] > height || HeroPosition[1] > width) || (HeroPosition[0] <= 0 || HeroPosition[1] <= 0))
            {
                throw new ArgumentException(ExceptionConstants.OutsideMapBoundariesException);
            }
        }

        private static bool CheckForWallsInPAth(List<string> map, int[] oldPosition, int[] newPosition)
        {
            // checking if there is a wall on the road
            if (oldPosition[0] < newPosition[0] || oldPosition[1] < newPosition[1])
            {
                if (oldPosition[0] == newPosition[0])
                {
                    char[] tempMapOldLine = map[oldPosition[0] - 1].ToCharArray();
                    for (int i = oldPosition[1] - 1; i <= newPosition[1] - 1; i++)
                    {
                        if (tempMapOldLine[i] == MonsterSymbol)
                        {
                            ChangeHeroCoordinate(i + 1, 1);
                            return true;
                        }

                        if (tempMapOldLine[i] == WallSymbol)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = oldPosition[0] - 1; i <= newPosition[0] - 1; i++)
                    {
                        char[] tempMapOldLine = map[i].ToCharArray();
                        if (tempMapOldLine[i] == MonsterSymbol)
                        {
                            ChangeHeroCoordinate(i + 1, 0);
                            return true;
                        }

                        if (tempMapOldLine[oldPosition[1] - 1] == WallSymbol)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (oldPosition[0] == newPosition[0])
                {
                    char[] tempMapOldLine = map[oldPosition[0] - 1].ToCharArray();
                    for (int i = newPosition[1] - 1; i <= oldPosition[1] - 1; i++)
                    {
                        if (tempMapOldLine[i] == MonsterSymbol)
                        {
                            ChangeHeroCoordinate(i + 1, 1);
                            return true;
                        }

                        if (tempMapOldLine[i] == WallSymbol)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    for (int i = newPosition[0] - 1; i <= oldPosition[0] - 1; i++)
                    {
                        char[] tempMapOldLine = map[i].ToCharArray();
                        if (tempMapOldLine[i] == MonsterSymbol)
                        {
                            ChangeHeroCoordinate(i + 1, 0);
                            return true;
                        }

                        if (tempMapOldLine[oldPosition[1] - 1] == WallSymbol)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static int SpecialSymbolReach(char symbol, string command, Hero hero)
        {
            // special symbols what to do when you find one
            switch (symbol)
            {
                case ChestSymbol:
                    DrawScreen.AddLineToBuffer(
                        ref DrawScreen.Area2, 
                        string.Format(ExceptionConstants.SomethingHappen, "chest", "try to open it"));

                    Random rnd = new Random();
                    if (rnd.Next(1, 3) == 1)
                    {
                        string[] receivedGold = Core.LootGoldChest().Split(' ');
                        string foundGoldChestMsg = string.Format("received " + receivedGold[0] + " gold and " + receivedGold[1] + " exp");
                        
                        DrawScreen.AddLineToBuffer(
                            ref DrawScreen.Area2,
                            string.Format(ExceptionConstants.SomethingHappen, "gold chest", foundGoldChestMsg));
                    }
                    else
                    {
                        try
                        {
                            var item = Core.LootRandomItem();
                            DrawScreen.AddLineToBuffer(
                                ref DrawScreen.Area2, 
                                string.Format(ExceptionConstants.SomethingHappen, "item chest", "recieved " + item.Id));
                        }
                        catch (ArgumentException e)
                        {
                            DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, e.Message);
                        }
                    }

                    if (command == "up" || command == "down")
                    {
                        specSymbol = '║';
                    }
                    else
                    {
                        specSymbol = '═';
                    }

                    return 1;
                case MonsterSymbol:
                    DrawScreen.AddLineToBuffer(
                        ref DrawScreen.Area2, 
                        string.Format(ExceptionConstants.SomethingHappen, "monster", "start fighting"));
                    if (command == "up" || command == "down")
                    {
                        specSymbol = '║';
                    }
                    else
                    {
                        specSymbol = '═';
                    }

                    BattleScreen battle = new BattleScreen(Core, 0);
                    battle.StartBattle();
                    return 2;
                case BossSymbol:
                    DrawScreen.AddLineToBuffer(
                        ref DrawScreen.Area2, 
                        string.Format(ExceptionConstants.SomethingHappen, "Boss", "start fighting"));
                    if (command == "up" || command == "down")
                    {
                        specSymbol = '║';
                    }
                    else
                    {
                        specSymbol = '═';
                    }

                    BattleScreen bossBattle = new BattleScreen(Core, 1);
                    bossBattle.StartBattle();
                    DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, Environment.NewLine);
                    DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, "YOU HAVE WON THE GAME !! CONGRATZ !!");
                    DrawScreen.AddLineToBuffer(ref DrawScreen.Area2, Environment.NewLine);
                    DrawScreen.Draw(Info(), DrawScreen.Area2);
                    Environment.Exit(0);
                    return 3;
                default:
                    return 0;
            }
        }

        private static List<string> HeroMove(
            List<string> oldMap, 
            int[] oldPosition, 
            int[] newPosition, 
            string command, 
            Hero hero)
        {
            // moving method
            char[] tempMapOldLine = oldMap[oldPosition[0] - 1].ToCharArray();
            char[] tempMapNewLine = oldMap[newPosition[0] - 1].ToCharArray();
            if (!CheckForWallsInPAth(oldMap, oldPosition, newPosition))
            {
                throw new ArgumentException(ExceptionConstants.WallReachException);
            }

            tempMapOldLine[oldPosition[1] - 1] = specSymbol;
            oldMap[oldPosition[0] - 1] = new string(tempMapOldLine);
            int temp = SpecialSymbolReach(tempMapNewLine[newPosition[1] - 1], command, hero);
            if (temp == 0)
            {
                specSymbol = tempMapNewLine[newPosition[1] - 1];
            }

            tempMapNewLine = oldMap[newPosition[0] - 1].ToCharArray();
            tempMapNewLine[newPosition[1] - 1] = HeroSymbol;
            oldMap[newPosition[0] - 1] = new string(tempMapNewLine);
            return oldMap;
        }

        private static void ChangeHeroCoordinate(int newValue, int position)
        {
            HeroPosition[position] = newValue;
        }

        private static void ChangeHeroCoordinates(int steps, string direction)
        {
            if (direction == "left")
            {
                HeroPosition[1] -= steps;
            }
            else if (direction == "right")
            {
                HeroPosition[1] += steps;
            }
            else if (direction == "up")
            {
                HeroPosition[0] -= steps;
            }
            else if (direction == "down")
            {
                HeroPosition[0] += steps;
            }
            else
            {
                throw new ArgumentException(ExceptionConstants.WrongDirectionException);
            }
        }
    }
}