namespace HeroesOfFate.Models.Items.Armors
{
    public class Gloves : Armor
    {
        public Gloves(string id, double armorDefence, decimal price)
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.Gloves;
        }
    }
}