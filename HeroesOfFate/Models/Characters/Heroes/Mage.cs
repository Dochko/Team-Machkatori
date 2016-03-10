namespace HeroesOfFate.Models.Characters.Heroes
{
    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Items.Armors;
    using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

    public class Mage : Hero
    {
        private const double DamageMinDefault = 75;

        private const double DamageMaxDefault = 125;

        private const double HealthDefault = 150;

        private const double ArmorDefault = 75;

        private const double MaxHealthDefault = HealthDefault;

        private const double ArmorReduction = 0.03;

        public Mage(string name, Race heroRace)
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
            IItem staff = new Staff("Wooden Staff", 30, 50);
            this.EquipStandartItems(staff);

            IItem sagesRobe = new Body("Sage's Robe", 10, 50);
            this.EquipStandartItems(sagesRobe);

            IItem softUnderwear = new Legs("Soft Underwear", 1, 5);
            this.EquipStandartItems(softUnderwear);

            IItem softBoots = new Boots("Soft Boots", 2, 10);
            this.EquipStandartItems(softBoots);
        }
    }
}