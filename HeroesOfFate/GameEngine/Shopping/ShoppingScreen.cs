namespace HeroesOfFate.GameEngine.Shopping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Characters.Heroes;

    public class ShoppingScreen
    {
        private const int MerchantInventoryCounter = 1;

        private const int HeroInventoryCounter = 1;

        private readonly Core core;

        private List<string> merchantItemsArea = new List<string>();

        //private List<string> heroInventoryItemsArea = new List<string>();

        private List<string> statusArea = new List<string>();

        public ShoppingScreen(Core core)
        {
            this.core = core;
        }

        public void StartShopping()
        {
            this.ScreenClear();
            this.FillArea(this.core.Hero, this.core.Merchant);
            this.DrawShopping();
            bool check = true;
            while (check)
            {
                try
                {
                    string[] commandArgs = Console.ReadLine()
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (commandArgs[0])
                    {
                        case "buy":
                            if (this.core.Hero.Gold
                                < (double)
                                  this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1).Price)
                            {
                                DrawScreen.AddLineToBuffer(
                                    ref this.statusArea,
                                    "You don't have enough gold for that item !");
                            }
                            else
                            {
                                this.core.Hero.AddItemToInventory(
                                    this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1));

                                this.core.Hero.Gold -=
                                    (double)
                                    this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1).Price;

                                DrawScreen.AddLineToBuffer(
                                    ref this.statusArea,
                                    string.Format(
                                        "You bought {0} for {1} gold.",
                                        this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1).Id,
                                        this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1).Price));

                                this.core.Merchant.BuyItemFromMerchant(
                                    this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1));
                            }

                            break;

                        case "sell":
                            if (this.core.Merchant.MerchantGold
                                < this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1).Price)
                            {
                                DrawScreen.AddLineToBuffer(
                                    ref this.statusArea,
                                    "The Merchant is poor soul and can't afford your item ...");
                            }
                            else
                            {
                                this.core.Hero.Gold +=
                                    (double)this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1).Price;

                                DrawScreen.AddLineToBuffer(
                                    ref this.statusArea,
                                    string.Format(
                                        "You sold {0} for {1} gold.",
                                        this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1).Id,
                                        this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1).Price));

                                this.core.Merchant.SellItemToMerchant(
                                    this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1));

                                this.core.Hero.RemoveItemFromInventory(
                                    this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1));
                            }

                            break;

                        case "back":
                            check = false;
                            break;
                    }

                    this.FillArea(this.core.Hero, this.core.Merchant);
                    this.DrawShopping();

                    if (check == false)
                    {
                        Console.Write("Thank you for supporting the Merchant's Guild. Visit us again !");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    DrawScreen.AddLineToBuffer(ref this.statusArea, ExceptionConstants.InvalidCommandException);
                    this.StartShopping();
                }
                catch (IndexOutOfRangeException)
                {
                    DrawScreen.AddLineToBuffer(ref this.statusArea, ExceptionConstants.InvalidCommandException);
                    this.StartShopping();
                }
                catch (ArgumentOutOfRangeException)
                {
                    DrawScreen.AddLineToBuffer(ref this.statusArea, ExceptionConstants.InvalidCommandException);
                    this.StartShopping();
                }
            }
        }

        private void ScreenClear()
        {
            this.merchantItemsArea.Clear();
        }

        private void FillArea(Hero hero, IMerchant merchant)
        {
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, string.Format("Merchant Gold: {0}", this.core.Merchant.MerchantGold));
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, "Merchant Items:");
            int merchantItemsCounter = MerchantInventoryCounter;
            foreach (var item in this.core.Merchant.MerchantItems)
            {
                DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, merchantItemsCounter + ". " + item);
                merchantItemsCounter++;
            }

            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, new string('-', 90));

            int heroItemsCounter = HeroInventoryCounter;
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, string.Format("Your gold: {0}", this.core.Hero.Gold));
            DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, "Your inventory items:");
            foreach (var item in this.core.Hero.Inventory)
            {
                if (!this.core.Hero.Inventory.Any())
                {
                    DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, "You have no items in the inventory.");
                }
                else
                {
                    DrawScreen.AddLineToBuffer(ref this.merchantItemsArea, heroItemsCounter + ". " + item);
                    heroItemsCounter++;
                }
            }
        }

        private void DrawShopping()
        {
            DrawScreen.Draw(this.merchantItemsArea, this.statusArea);
        }
    }
}