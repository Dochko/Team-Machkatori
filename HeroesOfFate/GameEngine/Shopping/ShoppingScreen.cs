namespace HeroesOfFate.GameEngine.Shopping
{
    using System;
    using System.Collections.Generic;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Characters.Heroes;

    public class ShoppingScreen
    {
        private readonly Core core;

        private List<string> merchantItemsArea = new List<string>();

        private List<string> heroInventoryItemsArea = new List<string>();

        private List<string> statusArea = new List<string>();

        public ShoppingScreen(Core core)
        {
            this.core = core;
        }

        public void Shopping()
        {
            
        }

        private void ScreenClear()
        {
            this.merchantItemsArea.Clear();
            this.heroInventoryItemsArea.Clear();
        }

        private void FillArea(Hero hero, IMerchant merchant)
        {
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, "Merchants Items:");
            int merchantItemsCounter = 1;
            foreach (var item in this.core.Merchant.MerchantItems)
            {
                DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, merchantItemsCounter + ". " + item);
                merchantItemsCounter++;
            }

            for (int j = merchantItemsCounter; j < 16; j++)
            {
                DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, Environment.NewLine);
            }
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, new string('-', 90));

            int heroItemsCounter = 1;
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, "Your inventory items:");
            foreach (var item in this.core.Hero.Inventory)
            {
                DrawScreen.AddLineToBuffer(ref this.heroInventoryItemsArea, heroItemsCounter + ". " + item);
                heroItemsCounter++;
            }
        }
    }
}