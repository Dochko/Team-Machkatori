namespace HeroesOfFate.Models.Characters
{
    using System;
    using System.Collections.Generic;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.GameEngine;

    public class Merchant : IMerchant
    {
        private const decimal MerchantStartingGold = 500;

        private readonly List<IItem> merchantItems;

        private decimal merchantGold;

        public Merchant()
        {
            this.merchantItems = new List<IItem>();
            this.merchantGold = MerchantStartingGold;
        }

        public List<IItem> MerchantItems
        {
            get
            {
                return this.merchantItems;
            }
        }

        public decimal MerchantGold
        {
            get
            {
                return this.merchantGold;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(string.Format(ExceptionConstants.NullOrNegativeException, "Merchant Gold"));
                }

                this.merchantGold = value;
            }
        }

        public void AddItemToMerchant(IItem item)
        {
            this.merchantItems.Add(item);
        }

        public void RemoveItemFromMerchant(IItem item)
        {
            this.merchantItems.Remove(item);
        }

        public void BuyItemFromMerchant(IItem item)
        {
            this.MerchantGold += item.Price;
            this.RemoveItemFromMerchant(item);
        }

        public void SellItemToMerchant(IItem item)
        {
            this.AddItemToMerchant(item);
            this.MerchantGold -= item.Price;
        }
    }
}