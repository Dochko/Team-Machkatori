namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class Axe : Weapon
    {
        public Axe(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = true;
        }
    }
}