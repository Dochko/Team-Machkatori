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
            int i = 1;
            foreach (var item in this.core.Merchant.MerchantItems)
            {
                DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, i + ". " + item);
                i++;
            }

            for (int j = i; j < 8; j++)
            {
                DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, Environment.NewLine);
            }


        }
    }
}