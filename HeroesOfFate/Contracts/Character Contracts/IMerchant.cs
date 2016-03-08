namespace HeroesOfFate.Contracts
{
    using System.Collections.Generic;

    public interface IMerchant
    {
        List<IItem> MerchantItems { get; set; }
    }
}