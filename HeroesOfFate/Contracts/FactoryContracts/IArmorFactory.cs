using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesOfFate.Contracts.Item_Contracts;

namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IArmorFactory
    {
        IItem CreateArmor(string armorType, string id, double armorDeffence, decimal price);


    }
}
