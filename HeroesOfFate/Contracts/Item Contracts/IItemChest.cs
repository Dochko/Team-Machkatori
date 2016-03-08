using System.Collections.Generic;

namespace HeroesOfFate.Contracts
{
    public interface IItemChest
    {
        IEnumerable<IItem> LootTable { get; }

        void AddItemToChest(IItem item);
    }
}