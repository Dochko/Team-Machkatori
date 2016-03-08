namespace HeroesOfFate.Factories
{
    using System;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Contracts.FactoryContracts;
    using HeroesOfFate.GameEngine;
    using HeroesOfFate.Models.Items.Armors;

    public class ArmorFactory : IArmorFactory
    {
        public IItem CreateArmor(string armorType, string id, double armorDeffence, decimal price)
        {
            switch (armorType)
            {
                case "body":
                    return new Body(id, armorDeffence, price);
                case "boots":
                    return new Boots(id, armorDeffence, price);
                case "gloves":
                    return new Gloves(id, armorDeffence, price);
                case "helm":
                    return new Helm(id, armorDeffence, price);
                case "legs":
                    return new Legs(id, armorDeffence, price);
                case "shield":
                    return new Shield(id, armorDeffence, price);
                default:
                    throw new ArgumentException(string.Format(ExceptionConstants.MissingException, "armor"));
            }
        }
    }
}