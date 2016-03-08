namespace HeroesOfFate.Contracts
{
    using System.Collections.Generic;

    public interface IItemChest
    {
        IEnumerable<IItem> LootTable { get; }

        void AddItemToChest(IItem item);
    }
}