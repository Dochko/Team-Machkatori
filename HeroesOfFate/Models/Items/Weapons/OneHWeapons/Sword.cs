namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class Sword : Weapon
    {
        public Sword(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = true;
        }
    }
}