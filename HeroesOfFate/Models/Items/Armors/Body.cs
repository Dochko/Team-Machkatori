namespace HeroesOfFate.Models.Items.Armors
{
    public class Body : Armor
    {
        public Body(string id, double armorDefence, decimal price) 
            : base(id, armorDefence, price)
        {
            this.Type = ItemType.Body;
        }
    }
}