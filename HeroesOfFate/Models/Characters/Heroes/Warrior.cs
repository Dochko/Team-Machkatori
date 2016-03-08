namespace HeroesOfFate.Models.Characters.Heroes
{
    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Items.Armors;
    using HeroesOfFate.Models.Items.Weapons.OneHWeapons;

    public class Warrior : Hero
    {
        private const double DamageMinDefault = 25;

        private const double DamageMaxDefault = 75;

        private const double HealthDefault = 250;

        private const double ArmorDefault = 125;

        private const double MaxHealthDefault = HealthDefault;

        private const double ArmorReduction = 0.1;

        public Warrior(string name, Race heroRace)
            : base(
                name, 
                heroRace, 
                DamageMinDefault, 
                DamageMaxDefault, 
                HealthDefault, 
                ArmorDefault, 
                ArmorReduction, 
                MaxHealthDefault)
        {
            this.StandartItems();
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }

        protected override void StandartItems()
        {
            IItem sword = new Sword("Wooden sword", 10, 5);
            IItem shield = new Shield("Wooden shield", 10, 5);
            this.AddItemToInventory(sword);
            this.AddItemToInventory(shield);
            this.Equip(sword);
            this.Equip(shield);
        }
    }
}