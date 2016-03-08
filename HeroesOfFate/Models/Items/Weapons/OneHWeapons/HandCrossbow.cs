namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class HandCrossbow : Weapon
    {
        public HandCrossbow(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = true;
        }
    }
}