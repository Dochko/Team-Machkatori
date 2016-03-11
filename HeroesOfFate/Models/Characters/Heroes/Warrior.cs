namespace HeroesOfFate.Models.Characters.Heroes
{
    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Items.Armors;
    using HeroesOfFate.Models.Items.Weapons.OneHWeapons;

    public class Warrior : Hero
    {
        private const double DamageMinDefault = 20;

        private const double DamageMaxDefault = 50;

        private const double HealthDefault = 250;

        private const double ArmorDefault = 100;

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

        protected override sealed void StandartItems()
        {
            IItem sword = new Sword("Wooden sword", 10, 25);
            this.EquipStandartItems(sword);

            IItem shield = new Shield("Wooden shield", 10, 25);
            this.EquipStandartItems(shield);

            IItem chainMail = new Body("Chain Mail", 50, 50);
            this.EquipStandartItems(chainMail);

            IItem chainGloves = new Gloves("Chain Gloves", 20, 20);
            this.EquipStandartItems(chainGloves);

            IItem chainLeggings = new Legs("Chain Leggings", 35, 35);
            this.EquipStandartItems(chainLeggings);

            IItem chainHelm = new Helm("Chain Helm", 30, 30);
            this.EquipStandartItems(chainHelm);

            IItem chainBoots = new Boots("Chain Boots", 25, 25);
            this.EquipStandartItems(chainBoots);
        }
    }
}