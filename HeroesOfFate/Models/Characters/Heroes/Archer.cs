using HeroesOfFate.Contracts;
using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

namespace HeroesOfFate.Models.Characters.Heroes
{
    public class Archer : Hero
    {
        private const double DamageMinDefault = 75;
        private const double DamageMaxDefault = 100;
        private const double HealthDefault = 200;
        private const double ArmorDefault = 100;
        private const double MaxHealthDefault = HealthDefault;
        private const double ArmorReduction = 0.05;

        public Archer(string name, Race heroRace)
            : base(name, heroRace, DamageMinDefault, DamageMaxDefault, HealthDefault, ArmorDefault, ArmorReduction,MaxHealthDefault)
        {
            this.StandartItems();
        }

        protected override void StandartItems()
        {
            IItem bow = new Bow("Wooden bow", 20, 15);
            AddItemToInventory(bow);
            Equip(bow);
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}