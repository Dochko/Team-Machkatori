namespace HeroesOfFate.Models.Characters.Monsters
{
    using System;
    using System.Collections.Generic;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.GameEngine;

    public abstract class Monster : Character, IMonster
    {
        private const short MonsterLevelDefault = 1;

        private const double GoldRewardDefault = 50;

        private const int ExpirienceRewardDefault = 2;

        private readonly List<IItem> lootTable;

        private int expirienceReward;

        private double goldReward;

        protected Monster(double damageMin, double damageMax, double health)
            : base(MonsterLevelDefault, damageMin, damageMax, health)
        {
            this.lootTable = new List<IItem>();
            this.GoldReward = GoldRewardDefault;
            this.ExpirienceReward = (int)((ExpirienceRewardDefault * this.Health) * 0.40);
        }

        public IEnumerable<IItem> LootTable
        {
            get
            {
                return this.lootTable;
            }
        }

        public double GoldReward
        {
            get
            {
                return this.goldReward;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Gold reward"));
                }

                this.goldReward = value;
            }
        }

        public int ExpirienceReward
        {
            get
            {
                return this.expirienceReward;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Expirience reward"));
                }

                this.expirienceReward = value;
            }
        }

        public void LevelUp(int level, int levelGained)
        {
            this.Level = level;
            this.DamageMin += 10 * levelGained;
            this.DamageMax += 10 * levelGained;
            this.Health += 25 * levelGained;
            this.GoldReward += 30 * levelGained;
        }

        public void AddItemToLootTable(params IItem[] items)
        {
            foreach (var item in items)
            {
                this.lootTable.Add(item);
            }
        }

        // Test !!!
        public override string ToString()
        {
            return string.Format(this.GetType().Name);
        }
    }
}