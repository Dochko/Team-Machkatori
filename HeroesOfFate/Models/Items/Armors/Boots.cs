namespace HeroesOfFate.Models.Items.Armors
{
    public class Boots : Armor
    {
        public Boots(string id, double armorDefence, decimal price)
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.Boots;
        }
    }
}