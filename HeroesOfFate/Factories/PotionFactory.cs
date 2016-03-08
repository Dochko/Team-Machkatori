namespace HeroesOfFate.Factories
{
    using System;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Contracts.FactoryContracts;
    using HeroesOfFate.GameEngine;
    using HeroesOfFate.Models.Items.Potions;

    public class PotionFactory : IPotionFactory
    {
        public IItem CreatePotion(string potionType, string id, decimal price)
        {
            switch (potionType)
            {
                case "healthPotion":
                    return new HealthPotion(id, price);
                default:
                    throw new ArgumentException(string.Format(ExceptionConstants.MissingException, "potion"));
            }
        }
    }
}