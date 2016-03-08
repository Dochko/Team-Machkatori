using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesOfFate.Contracts.Item_Contracts;

namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IWeaponFactory
    {
        IItem CreateWeapon(string weaponType,string id, double effect, decimal price);

    }
}
