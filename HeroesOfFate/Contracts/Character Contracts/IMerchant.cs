using System.Collections.Generic;

namespace HeroesOfFate.Contracts
{
    public interface IMerchant
    {
        List<IItem> MerchantItems { get; set; }
    }
}