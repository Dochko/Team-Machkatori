namespace HeroesOfFate.Models.Items.Armors
{
    public class Legs : Armor
    {
        public Legs(string id, double armorDefence, decimal price)
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.Legs;
        }
    }
}