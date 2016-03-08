namespace HeroesOfFate.GameEngine
{
    using System.Collections.Generic;

    using HeroesOfFate.Contracts;

    public class Database
    {
        private readonly ICollection<IItem> itemChests;

        private readonly ICollection<IItem> items;

        private readonly ICollection<IMonster> monsters;

        public Database()
        {
            this.monsters = new List<IMonster>();
            this.items = new List<IItem>();
            this.itemChests = new List<IItem>();
        }

        public IEnumerable<IMonster> Monsters
        {
            get
            {
                return this.monsters;
            }
        }

        public IEnumerable<IItem> Items
        {
            get
            {
                return this.items;
            }
        }

        public IEnumerable<IItem> ItemChests
        {
            get
            {
                return this.itemChests;
            }
        }

        public void AddMonster(params IMonster[] monsters)
        {
            foreach (var monster in monsters)
            {
                this.monsters.Add(monster);
            }
        }

        public void AddItem(IItem item)
        {
            this.items.Add(item);
        }

        public void AddItemChest(IItem itemChest)
        {
            this.itemChests.Add(itemChest);
        }

        public IItem GetitemByIndex(int index)
        {
            int count = 0;

            foreach (var item in this.Items)
            {
                if (count == index)
                {
                    return item;
                }

                count++;
            }

            return null;
        }
    }
}