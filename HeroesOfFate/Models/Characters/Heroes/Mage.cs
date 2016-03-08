using HeroesOfFate.Contracts;
using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

namespace HeroesOfFate.Models.Characters.Heroes
{
    public class Mage : Hero
    {
        private const double DamageMinDefault = 75;
        private const double DamageMaxDefault = 125;
        private const double HealthDefault = 150;
        private const double ArmorDefault = 75;
        private const double MaxHealthDefault = HealthDefault;
        private const double ArmorReduction = 0.03;

        

        public Mage(string name, Race heroRace)
            : base(name, heroRace, DamageMinDefault, DamageMaxDefault, HealthDefault, ArmorDefault, ArmorReduction, MaxHealthDefault)
        {
            this.StandartItems();
        }

        protected override void StandartItems()
        {
            IItem staff = new Staff("Wooden staff", 30, 15);
            AddItemToInventory(staff);
            Equip(staff);
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}