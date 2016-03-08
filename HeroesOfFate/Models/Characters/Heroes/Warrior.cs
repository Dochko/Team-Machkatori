using HeroesOfFate.Contracts;
using HeroesOfFate.Models.Items.Armors;
using HeroesOfFate.Models.Items.Weapons.OneHWeapons;
using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

namespace HeroesOfFate.Models.Characters.Heroes
{
    public class Warrior : Hero
    {
        private const double DamageMinDefault = 25;
        private const double DamageMaxDefault = 75;
        private const double HealthDefault = 250;
        private const double ArmorDefault = 125;
        private const double MaxHealthDefault = HealthDefault;
        private const double ArmorReduction = 0.1;

        public Warrior(string name, Race heroRace)
            :base(name, heroRace, DamageMinDefault, DamageMaxDefault, HealthDefault, ArmorDefault, ArmorReduction, MaxHealthDefault)
        {
            this.StandartItems();
        }

        protected override void StandartItems()
        {
            IItem sword = new Sword("Wooden sword", 10, 5);
            IItem shield = new Shield("Wooden shield", 10, 5);
            AddItemToInventory(sword);
            AddItemToInventory(shield);
            Equip(sword);
            Equip(shield);
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}