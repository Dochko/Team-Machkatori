using System.Collections.Generic;
using HeroesOfFate.Contracts;

namespace HeroesOfFate.Models.Items.Chests
{
    public class ItemChest : Chest, IItemChest
    {
        private readonly List<IItem> lootTable;

        public ItemChest(string id) : base(id)
        {
            this.lootTable = new List<IItem>();
        }

        public IEnumerable<IItem> LootTable
        {
            get { return this.lootTable; }
        }

        public void AddItemToChest(IItem item)
        {
            this.lootTable.Add(item);
        }
    }
}