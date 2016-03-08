using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesOfFate.Contracts.Item_Contracts
{
    public interface IWeapon : IDamage
    {
        bool IsOneH { get; set; }
    }
}
