namespace HeroesOfFate.Models.Characters.Heroes
{
    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Items.Armors;
    using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

    public class Archer : Hero
    {
        private const double DamageMinDefault = 30;

        private const double DamageMaxDefault = 60;

        private const double HealthDefault = 200;

        private const double ArmorDefault = 75;

        private const double MaxHealthDefault = HealthDefault;

        private const double ArmorReduction = 0.05;

        public Archer(string name, Race heroRace)
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
            IItem woodenBow = new Bow("Wooden bow", 20, 50);
            this.EquipStandartItems(woodenBow);

            IItem leatherArmor = new Body("Leather Armor", 30, 50);
            this.EquipStandartItems(leatherArmor);

            IItem hood = new Helm("Hood", 10, 10);
            this.EquipStandartItems(hood);

            IItem leatherLeggings = new Legs("Leather Leggings", 15, 20);
            this.EquipStandartItems(leatherLeggings);

            IItem leatherBoots = new Boots("Leather Boots", 5, 10);
            this.EquipStandartItems(leatherBoots);

            IItem leatherGloves = new Gloves("Leather Gloves", 5, 10);
            this.EquipStandartItems(leatherGloves);
        }
    }
}