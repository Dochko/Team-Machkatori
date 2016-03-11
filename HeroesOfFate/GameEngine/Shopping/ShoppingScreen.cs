namespace HeroesOfFate.GameEngine.Shopping
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.GameEngine.IO;
    using HeroesOfFate.Models.Characters.Heroes;

    public class ShoppingScreen
    {
        private const int MerchantInventoryCounter = 1;

        private const int HeroInventoryCounter = 1;

        private readonly Core core;

        private readonly ConsoleReader reader = new ConsoleReader();

        private List<string> itemsArea = new List<string>();

        private List<string> statusArea = new List<string>();

        public ShoppingScreen(Core core)
        {
            this.core = core;
        }

        // Start the shopping screen
        public void StartShopping()
        {
            this.ScreenClear();
            DrawScreen.AddLineToBuffer(ref this.statusArea, "For more info type (help)");
            this.ScreenUpdate();
            bool check = true;
            while (check)
            {
                try
                {
                    string[] commandArgs = this.reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (commandArgs[0])
                    {
                        case "buy":
                            this.BuyItem(commandArgs);
                            break;

                        case "sell":
                            this.SellItem(commandArgs);
                            break;

                        case "help":
                            this.HelpMethod();
                            break;

                        case "back":
                            check = false;
                            break;

                        default:
                            DrawScreen.AddLineToBuffer(ref this.statusArea, ExceptionConstants.InvalidCommandException);
                            break;
                    }

                    this.ScreenClear();
                    this.ScreenUpdate();

                    if (check == false)
                    {
                        DrawScreen.AddLineToBuffer(ref this.statusArea, "Thank you for supporting the Merchant's Guild. Visit us again !");
                        DrawScreen.AddLineToBuffer(ref this.statusArea, "Press any key to continue ...");
                        this.ScreenClear();
                        this.ScreenUpdate();
                        this.reader.ReadKey();
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

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        private void BuyItem(string[] commandArgs)
        {
            var merchantItemInInventory = this.core.Merchant.MerchantItems.ElementAt(int.Parse(commandArgs[1]) - 1);

            if (this.core.Hero.Gold < (double)merchantItemInInventory.Price)
            {
                DrawScreen.AddLineToBuffer(ref this.statusArea, "You don't have enough gold for that item !");
            }
            else if (this.core.Hero.Inventory.Count() > 7)
            {
                DrawScreen.AddLineToBuffer(ref this.statusArea, "You cannot carry more items !");
            }
            else
            {
                this.core.Hero.AddItemToInventory(merchantItemInInventory);

                this.core.Hero.Gold -= (double)merchantItemInInventory.Price;

                DrawScreen.AddLineToBuffer(
                    ref this.statusArea,
                    string.Format(
                        "You bought the {0} {1} for {2} gold.",
                        merchantItemInInventory.Type,
                        merchantItemInInventory.Id, 
                        merchantItemInInventory.Price));

                this.core.Merchant.BuyItemFromMerchant(merchantItemInInventory);
            }
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed. Suppression is OK here.")]
        private void SellItem(string[] commandArgs)
        {
            var heroItemInInventory = this.core.Hero.Inventory.ElementAt(int.Parse(commandArgs[1]) - 1);

            if (this.core.Merchant.MerchantGold < heroItemInInventory.Price)
            {
                DrawScreen.AddLineToBuffer(ref this.statusArea, "The Merchant is poor soul and can't afford your item ...");
            }
            else
            {
                this.core.Hero.Gold += (double)heroItemInInventory.Price;

                DrawScreen.AddLineToBuffer(
                    ref this.statusArea,
                    string.Format(
                        "You sold the {0} {1} for {2} gold.",
                        heroItemInInventory.Type, 
                        heroItemInInventory.Id, 
                        heroItemInInventory.Price));

                this.core.Merchant.SellItemToMerchant(heroItemInInventory);

                this.core.Hero.RemoveItemFromInventory(heroItemInInventory);
            }
        }

        private void HelpMethod()
        {
            DrawScreen.AddLineToBuffer(
                ref this.statusArea,
                "Use (buy) (item index from merchant inventory) to buy the specific item of the merchant.");

            DrawScreen.AddLineToBuffer(
                ref this.statusArea,
                "Use (sell) (item index from hero inventory) to sell the specific item to the merchant.");

            DrawScreen.AddLineToBuffer(ref this.statusArea, "Use (back) to return to the map.");
        }

        private void ScreenClear()
        {
            this.itemsArea.Clear();
        }

        private void ScreenUpdate()
        {
            this.FillArea(this.core.Hero, this.core.Merchant);
            this.DrawShopping();
        }

        private void FillArea(Hero hero, IMerchant merchant)
        {
            DrawScreen.AddLineToBuffer(ref this.itemsArea, string.Format("Merchant Gold: {0}", this.core.Merchant.MerchantGold));
            int merchantItemsCounter = MerchantInventoryCounter;
            foreach (var item in this.core.Merchant.MerchantItems)
            {
                DrawScreen.AddLineToBuffer(ref this.itemsArea, merchantItemsCounter + ". " + item);
                merchantItemsCounter++;
            }

            DrawScreen.AddLineToBuffer(ref this.itemsArea, new string('-', 90));
            int heroItemsCounter = HeroInventoryCounter;
            DrawScreen.AddLineToBuffer(ref this.itemsArea, string.Format("Your gold: {0}", this.core.Hero.Gold));
            foreach (var item in this.core.Hero.Inventory)
            {
                DrawScreen.AddLineToBuffer(ref this.itemsArea, heroItemsCounter + ". " + item);
                heroItemsCounter++;
            }
        }

        private void DrawShopping()
        {
            DrawScreen.Draw(this.itemsArea, this.statusArea);
        }
    }
}