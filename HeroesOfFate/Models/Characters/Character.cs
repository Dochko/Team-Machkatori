namespace HeroesOfFate.Models.Characters
{
    using System;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.GameEngine;

    public abstract class Character : ICharacter
    {
        private double damageMax;

        private double damageMin;

        private double health;

        private int level;

        protected Character(int level, double health, double damageMin, double damageMax)
        {
            this.Level = level;
            this.DamageMin = damageMin;
            this.DamageMax = damageMax;
            this.Health = health;
            this.IsDead = false;
        }

        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Level"));
                }

                this.level = value;
            }
        }

        public double Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value <= 0)
                {
                    this.health = 0;
                    this.IsDead = true;
                }

                this.health = value;
            }
        }

        public double DamageMin
        {
            get
            {
                return this.damageMin;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Minimum damage"));
                }

                this.damageMin = value;
            }
        }

        public double DamageMax
        {
            get
            {
                return this.damageMax;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Maximum damage"));
                }

                this.damageMax = value;
            }
        }

        public bool IsDead { get; private set; }
    }
}