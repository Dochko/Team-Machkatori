namespace HeroesOfFate.Models.Items.Weapons.TwoHWeapons
{
    public class Greatsword : Weapon
    {
        public Greatsword(string id, double weaponAttack, decimal price) 
            : base(id, weaponAttack, price)
        {
            this.IsOneH = false;
        }
    }
}