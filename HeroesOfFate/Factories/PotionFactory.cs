using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesOfFate.Contracts;
using HeroesOfFate.Contracts.FactoryContracts;
using HeroesOfFate.Contracts.Item_Contracts;
using HeroesOfFate.Models.Items.Potions;
using HeroesOfFate.GameEngine;

namespace HeroesOfFate.Factories
{
    public class PotionFactory : IPotionFactory
    {
        public IItem CreatePotion(string potionType, string id, decimal price)
        {
            switch (potionType)
            {
                case"healthPotion":
                    return new HealthPotion(id,price);
                default:
                    throw new ArgumentException(string.Format(ExceptionConstants.MissingException, "potion"));
            }
        }
    }
}
