namespace HeroesOfFate.Models.Items.Weapons.TwoHWeapons
{
    public class Greataxe : Weapon
    {
        public Greataxe(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = false;
        }
    }
}