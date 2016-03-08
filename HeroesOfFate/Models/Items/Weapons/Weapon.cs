namespace HeroesOfFate.Models.Items.Weapons
{
    using HeroesOfFate.Contracts.Item_Contracts;

    public class Weapon : Item, IWeapon
    {
        public Weapon(string id, double weaponAttack, decimal price)
            : base(id, price)
        {
            this.Type = ItemType.MainHand;
            this.WeaponAttack = weaponAttack;
        }

        public override bool IsOneH { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Damage: {1}, Price: {2}", base.ToString(), this.WeaponAttack, this.Price);
        }
    }
}