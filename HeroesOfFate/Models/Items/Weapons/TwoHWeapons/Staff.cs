namespace HeroesOfFate.Models.Items.Weapons.TwoHWeapons
{
    public class Staff : Weapon
    {
        public Staff(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = false;
        }
    }
}