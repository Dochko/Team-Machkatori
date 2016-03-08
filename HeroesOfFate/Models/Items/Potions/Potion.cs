namespace HeroesOfFate.Models.Items.Potions
{
    using HeroesOfFate.Contracts.Item_Contracts;

    public class Potion : Item, IPotion
    {
        protected Potion(string id, decimal price)
            : base(id, price)
        {
            this.Type = ItemType.Potion;
        }
    }
}