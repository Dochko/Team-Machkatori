namespace HeroesOfFate.Models.Items.Potions
{
    using HeroesOfFate.Contracts.Item_Contracts;

    public abstract class Potion : Item, IPotion
    {
        protected Potion(string id, decimal price)
            : base(id, price)
        {
            this.Type = ItemType.Potion;
        }

        public override string ToString()
        {
            return string.Format("{0}, Price: {1}", base.ToString(), this.Price);
        }
    }
}