namespace HeroesOfFate.Contracts
{
    using System.Collections.Generic;

    public interface IMerchant
    {
        List<IItem> MerchantItems { get; }

        decimal MerchantGold { get; set; }

        void AddItemToMerchant(IItem item);

        void RemoveItemFromMerchant(IItem item);

        void BuyItemFromMerchant(IItem item);

        void SellItemToMerchant(IItem item);
    }
}