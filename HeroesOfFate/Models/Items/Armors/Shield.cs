namespace HeroesOfFate.Models.Items.Armors
{
    public class Shield : Armor
    {
        public Shield(string id, double armorDefence, decimal price)
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.OffHand;
        }
    }
}