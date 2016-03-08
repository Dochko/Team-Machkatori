namespace HeroesOfFate.Models.Items.Armors
{
    using HeroesOfFate.Contracts.Item_Contracts;

    public class Armor : Item, IArmor
    {
        protected Armor(string id, double armorDefence, decimal price)
            : base(id, price)
        {
            this.ArmorDefence = armorDefence;
        }

        public override string ToString()
        {
            return string.Format("{0}, Defence: {1}, Price: {2}", base.ToString(), this.ArmorDefence, this.Price);
        }
    }
}