namespace HeroesOfFate.GameEngine
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Contracts.FactoryContracts;
    using HeroesOfFate.Factories;
    using HeroesOfFate.GameEngine.IO;
    using HeroesOfFate.Models.Characters;
    using HeroesOfFate.Models.Characters.Heroes;
    using HeroesOfFate.Models.Characters.Monsters;
    using HeroesOfFate.Models.Items.Chests;

    public class Core
    {
        private static readonly string[] HeroesDamage = { "20 - 50", "30 - 60", "50 - 80" };

        private static readonly string[] HeroesHealth = { "250", "200", "150" };

        private static readonly string[] HeroesArmor = { "100", "75", "50" };

        private readonly IArmorFactory armorFactory = new ArmorFactory();

        private readonly Database database = new Database();

        private readonly Merchant merchant = new Merchant();

        private readonly IGoldChest goldChest = new GoldChest("Gold chest", 500, 50);

        private readonly IPotionFactory potionFactory = new PotionFactory();

        private readonly ConsoleReader reader = new ConsoleReader();

        private readonly IWeaponFactory weaponFactory = new WeaponFactory();

        private readonly ConsoleWriter writer = new ConsoleWriter();

        public Core()
        {
            this.Hero = this.Hero;
            this.Merchant = this.merchant;
            this.Database = this.database;
        }

        public Hero Hero { get; private set; }

        public Merchant Merchant { get; private set; }

        public Database Database { get; private set; }

        // Method for starting the game
        public void Run()
        {
            using (StreamReader file = new StreamReader("..\\..\\resources\\merchantItems.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] inputArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ParseTextfileToMerchant(inputArgs);
                }
            }

            this.StartScreen();

            this.Hero.ChangedLevel += (sender, eventArgs) =>
                {
                    foreach (var m in this.Database.Monsters)
                    {
                        m.LevelUp(eventArgs.Level, eventArgs.LevelsGained);
                    }
                };
        }

        // Populating items in the game and inserting them to the monsters and item chests
        public void ImplementItems()
        {
            using (StreamReader file = new StreamReader("..\\..\\resources\\items.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] inputArgs = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ParseTextfileToDatabase(inputArgs);
                }

                foreach (var monster in this.database.Monsters)
                {
                    foreach (var item in this.database.Items)
                    {
                        monster.AddItemToLootTable(item);
                    }
                }

                foreach (IItemChest chest in this.database.ItemChests)
                {
                    foreach (var item in this.database.Items)
                    {
                        chest.AddItemToChest(item);
                    }
                }
            }
        }

        public void MonsterFactory()
        {
            this.database.AddMonster(new Goblin(), new Ogre(), new Troll(), new Undead(), new Wolf());
        }

        // The method for getting random loot from monsters and item chests
        public IItem LootRandomItem()
        {
            Random random = new Random();

            int result = random.Next(0 - (this.database.Items.Count() / 2), this.database.Items.Count());

            if (result < 0)
            {
                throw new ArgumentException(ExceptionConstants.NothingLootedException);
            }

            this.Hero.AddItemToInventory(this.database.GetitemByIndex(result));

            return this.database.GetitemByIndex(result);
        }

        public string LootGoldChest()
        {
            this.Hero.Gold += this.goldChest.Gold;
            this.Hero.Exp += this.goldChest.Exp;
            return string.Format(this.goldChest.Gold + " " + this.goldChest.Exp);
        }

        // Commands info screen leading back to the start screen
        private void CommandsInfo()
        {
            Console.Clear();
            StringBuilder output = new StringBuilder();

            output.AppendLine("Those are the in game commands:");
            output.AppendLine(
                "-move (number of steps) (direction) - the number must be integer,\n the directions can be: right/left/up/down");
            output.AppendLine("-Info - view hero information");
            output.AppendLine("-inventory - view the equipped items and the inventory of the hero");
            output.AppendLine("-exit - quits the current game" + Environment.NewLine);
            output.AppendLine(
                "Use \"back\" to go to the Start Screen again or\n \"quit\" to exit(the monster will find you never the less).");

            this.writer.WriteLine(output.ToString());

            bool check = true;

            while (check)
            {
                try
                {
                    string input = this.reader.ReadLine();
                    switch (input)
                    {
                        case "back":
                            Console.Clear();
                            this.StartScreen();
                            check = false;
                            break;
                        case "quit":
                            Environment.Exit(0);
                            break;
                        default:
                            throw new ArgumentException(ExceptionConstants.InvalidCommandException);
                    }
                }
                catch (ArgumentException e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }
        }

        private void StartScreen()
        {
            Console.Clear();

            StringBuilder chooseOption = new StringBuilder();

            string title = "Heroes Of Fate";
            chooseOption.AppendLine(title.PadLeft(45).ToUpper());
            chooseOption.AppendLine(Environment.NewLine);
            chooseOption.AppendLine("Start New Adventure (start)" + Environment.NewLine);
            chooseOption.AppendLine("Commands (help)" + Environment.NewLine);
            chooseOption.AppendLine("Exit");
            this.writer.WriteLine(chooseOption.ToString());

            string[] inputParams = this.reader.ReadLine().Split();

            switch (inputParams[0])
            {
                case "start":
                    this.CreateHero();
                    break;
                case "help":
                    this.CommandsInfo();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentException(ExceptionConstants.InvalidCommandException);
            }
        }

        // Hero creation screen leading to the map screen
        private void CreateHero()
        {
            Console.Clear();

            StringBuilder chooseOption = new StringBuilder();

            chooseOption.AppendLine("To create your hero write it in the following format:");
            chooseOption.AppendLine("(hero profession) (hero name) (hero race)" + Environment.NewLine);
            chooseOption.AppendLine("Choose your hero's profession:");
            chooseOption.AppendLine("The available professions are: Warrior, Archer and Mage" + Environment.NewLine);
            chooseOption.AppendLine("The Warrior has:                 The Archer has:                The Mage has:");
            chooseOption.AppendLine(
                string.Format(
                "Basic attack: {0}            Basic attack: {1}          Basic attack: {2}",
                HeroesDamage[0], 
                HeroesDamage[1], 
                HeroesDamage[2]));
            
            chooseOption.AppendLine(
                string.Format(
                "Health: {0}                      Health: {1}                    Health: {2}",
                HeroesHealth[0],
                HeroesHealth[1],
                HeroesHealth[2]));

            chooseOption.AppendLine(
                string.Format(
                "Armor: {0}                       Armor: {1}                      Armor: {2}{3}",
                HeroesArmor[0],
                HeroesArmor[1],
                HeroesArmor[2],
                Environment.NewLine));

            chooseOption.AppendLine("What is your hero's name and race ?");
            chooseOption.AppendLine("The available races are: Human, Elf, Orc, Dwarf or Werewolf");
            this.writer.WriteLine(chooseOption.ToString());

            bool check = true;

            while (check)
            {
                try
                {
                    string[] inputParams = this.reader.ReadLine().Split();
                    switch (inputParams[0])
                    {
                        case "warrior":
                            if (inputParams[2] == "human")
                            {
                                this.Hero = new Warrior(inputParams[1], Race.Human);
                            }
                            else if (inputParams[2] == "elf")
                            {
                                this.Hero = new Warrior(inputParams[1], Race.Elf);
                            }
                            else if (inputParams[2] == "orc")
                            {
                                this.Hero = new Warrior(inputParams[1], Race.Orc);
                            }
                            else if (inputParams[2] == "dwarf")
                            {
                                this.Hero = new Warrior(inputParams[1], Race.Dwarf);
                            }
                            else if (inputParams[2] == "werewolf")
                            {
                                this.Hero = new Warrior(inputParams[1], Race.Werewolf);
                            }
                            else
                            {
                                throw new ArgumentException(
                                    string.Format(ExceptionConstants.CharCreationException, "Race"));
                            }

                            check = false;
                            break;

                        case "archer":
                            if (inputParams[2] == "human")
                            {
                                this.Hero = new Archer(inputParams[1], Race.Human);
                            }
                            else if (inputParams[2] == "elf")
                            {
                                this.Hero = new Archer(inputParams[1], Race.Elf);
                            }
                            else if (inputParams[2] == "orc")
                            {
                                this.Hero = new Archer(inputParams[1], Race.Orc);
                            }
                            else if (inputParams[2] == "dwarf")
                            {
                                this.Hero = new Archer(inputParams[1], Race.Dwarf);
                            }
                            else if (inputParams[2] == "werewolf")
                            {
                                this.Hero = new Archer(inputParams[1], Race.Werewolf);
                            }
                            else
                            {
                                throw new ArgumentException(
                                    string.Format(ExceptionConstants.CharCreationException, "race"));
                            }

                            check = false;
                            break;

                        case "mage":
                            if (inputParams[2] == "human")
                            {
                                this.Hero = new Mage(inputParams[1], Race.Human);
                            }
                            else if (inputParams[2] == "elf")
                            {
                                this.Hero = new Mage(inputParams[1], Race.Elf);
                            }
                            else if (inputParams[2] == "orc")
                            {
                                this.Hero = new Mage(inputParams[1], Race.Orc);
                            }
                            else if (inputParams[2] == "dwarf")
                            {
                                this.Hero = new Mage(inputParams[1], Race.Dwarf);
                            }
                            else if (inputParams[2] == "werewolf")
                            {
                                this.Hero = new Mage(inputParams[1], Race.Werewolf);
                            }
                            else
                            {
                                throw new ArgumentException(
                                    string.Format(ExceptionConstants.CharCreationException, "Race"));
                            }

                            check = false;
                            break;

                        case "exit":
                            Environment.Exit(0);
                            break;

                        default:
                            throw new ArgumentException(
                                string.Format(ExceptionConstants.CharCreationException, "Class"));
                    }

                    this.ImplementItems();
                }
                catch (IndexOutOfRangeException)
                {
                    this.writer.WriteLine(string.Format(ExceptionConstants.CharCreationException, "Name"));
                }
                catch (ArgumentException e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }
        }

        // Loading the items from prewritten text file to the Database
        private void ParseTextfileToDatabase(string[] inputArgs)
        {
            string itemType = inputArgs[0];
            string itemName = inputArgs[1];
            string itemId = inputArgs[2];
            try
            {
                switch (itemType)
                {
                    case "potion":
                        decimal price = decimal.Parse(inputArgs[3]);
                        IItem potion = this.potionFactory.CreatePotion(itemName, itemId, price);
                        this.database.AddItem(potion);
                        break;
                    case "weapon":
                        double weaponAttack = double.Parse(inputArgs[3]);
                        decimal weaponPrice = decimal.Parse(inputArgs[4]);
                        IItem weapon = this.weaponFactory.CreateWeapon(itemName, itemId, weaponAttack, weaponPrice);
                        this.database.AddItem(weapon);
                        break;
                    case "armor":
                        double armorDeffence = double.Parse(inputArgs[3]);
                        decimal armorPrice = decimal.Parse(inputArgs[4]);
                        IItem armor = this.armorFactory.CreateArmor(itemName, itemId, armorDeffence, armorPrice);
                        this.database.AddItem(armor);
                        break;
                    default:
                        throw new ArgumentException(string.Format(ExceptionConstants.InvalidItemException, "Potion"));
                }
            }
            catch (ArgumentException e)
            {
                this.writer.WriteLine(e.Message);
            }
        }

        // Loading the items from prewritten text file to the Merchant Store
        private void ParseTextfileToMerchant(string[] inputArgs)
        {
            string itemType = inputArgs[0];
            string itemName = inputArgs[1];
            string itemId = inputArgs[2];
            try
            {
                switch (itemType)
                {
                    case "potion":
                        decimal price = decimal.Parse(inputArgs[3]);
                        IItem potion = this.potionFactory.CreatePotion(itemName, itemId, price);
                        this.Merchant.AddItemToMerchant(potion);
                        break;
                    case "weapon":
                        double weaponAttack = double.Parse(inputArgs[3]);
                        decimal weaponPrice = decimal.Parse(inputArgs[4]);
                        IItem weapon = this.weaponFactory.CreateWeapon(itemName, itemId, weaponAttack, weaponPrice);
                        this.Merchant.AddItemToMerchant(weapon);
                        break;
                    case "armor":
                        double armorDeffence = double.Parse(inputArgs[3]);
                        decimal armorPrice = decimal.Parse(inputArgs[4]);
                        IItem armor = this.armorFactory.CreateArmor(itemName, itemId, armorDeffence, armorPrice);
                        this.Merchant.AddItemToMerchant(armor);
                        break;
                    default:
                        throw new ArgumentException(string.Format(ExceptionConstants.InvalidItemException, "Potion"));
                }
            }
            catch (ArgumentException e)
            {
                this.writer.WriteLine(e.Message);
            }
        }
    }
}