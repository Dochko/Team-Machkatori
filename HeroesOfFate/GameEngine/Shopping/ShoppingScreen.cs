namespace HeroesOfFate.GameEngine.Shopping
{
    using System;
    using System.Collections.Generic;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Characters.Heroes;

    public class ShoppingScreen
    {
        private readonly Core core;

        private List<string> shoppingArea1 = new List<string>();

        private List<string> shoppingArea2 = new List<string>();

        private List<string> commandShow = new List<string>();

        public ShoppingScreen(Core core)
        {
            this.core = core;
        }

        public void Shopping()
        {
            
        }

        private void ScreenClear()
        {
            this.shoppingArea1.Clear();
            this.shoppingArea2.Clear();
        }

        private void FillArea(Hero hero, IMerchant merchant)
        {
            DrawScreen.AddLineToBuffer(ref this.shoppingArea1, Environment.NewLine);
        }
    }
}