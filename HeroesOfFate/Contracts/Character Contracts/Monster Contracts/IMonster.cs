using System.Collections.Generic;

namespace HeroesOfFate.Contracts
{
    public interface IMonster : ICharacter
    {
        double GoldReward { get; set; }
        int ExpirienceReward { get; set; }

        IEnumerable<IItem> LootTable { get; }

        void LevelUp(int level, int levelGained);

        void AddItemToLootTable(params IItem[] items);
    }
}