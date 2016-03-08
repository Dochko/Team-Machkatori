namespace HeroesOfFate.Models.Items.Chests
{
    using System;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.GameEngine;

    public class GoldChest : Chest, IGoldChest
    {
        private double gold;

        private int exp;

        public GoldChest(string id, double gold, int exp)
            : base(id)
        {
            this.Gold = gold;
            this.Exp = exp;
        }

        public double Gold
        {
            get
            {
                return this.gold;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionConstants.NullOrNegativeException, "Gold"));
                }

                this.gold = value;
            }
        }

        public int Exp
        {
            get
            {
                return this.exp;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionConstants.NullOrNegativeException, "Exp"));
                }

                this.exp = value;
            }
        }
    }
}