namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class Dagger : Weapon
    {
        private const bool IsOneH = true;

        public Dagger(ItemType type, double weaponAttack, string id, decimal price) : base(type, weaponAttack, id, price, IsOneH)
        {

        }
    }
}