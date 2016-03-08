using System;
using HeroesOfFate.Contracts;
using HeroesOfFate.GameEngine;

namespace HeroesOfFate.Models.Items.Chests
{
    public abstract class Chest : IChest
    {
        private string id;

        protected Chest(string id)
        {
            this.Id = id;
        }

        public string Id
        {
            get { return this.id; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionConstants.NullOrEmptyException, "Chest ID"));
                }
                this.id = value; 
            }
        }
    }
}