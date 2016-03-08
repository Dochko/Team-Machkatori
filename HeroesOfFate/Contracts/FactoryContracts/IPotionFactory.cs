using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesOfFate.Contracts.Item_Contracts;

namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IPotionFactory
    {
        IItem CreatePotion(string potionType, string id, decimal price);
    }
}
