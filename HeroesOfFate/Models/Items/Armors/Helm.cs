namespace HeroesOfFate.Models.Items.Armors
{
    public class Helm : Armor
    {
        public Helm(string id, double armorDefence, decimal price) 
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.Helmet;
        }
    }
}