namespace HeroesOfFate.Models.Items.Weapons.TwoHWeapons
{
    public class Bow : Weapon
    {
        public Bow(string id, double weaponAttack, decimal price)
            : base(id, weaponAttack, price)
        {
            this.IsOneH = false;
        }
    }
}