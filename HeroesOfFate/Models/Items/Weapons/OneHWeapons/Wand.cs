namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class Wand : Weapon
    {
        public Wand(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = true;
        }
    }
}