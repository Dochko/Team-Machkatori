namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class Mace : Weapon
    {
        public Mace(string id, double weaponAttack, decimal price)
            : base(id, weaponAttack, price)
        {
            this.IsOneH = true;
        }
    }
}