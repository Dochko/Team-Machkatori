namespace HeroesOfFate.Models.Items.Weapons.OneHWeapons
{
    public class SpellBook : Weapon
    {
        private const bool IsOneH = true;

        public SpellBook(ItemType type, double weaponAttack, string id, decimal price) : base(type, weaponAttack, id, price, IsOneH)
        {

        }
    }
}