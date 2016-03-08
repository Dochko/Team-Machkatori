namespace HeroesOfFate.Models.Characters.Heroes
{
    using HeroesOfFate.Contracts;
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

        protected override void StandartItems()
        {
            IItem staff = new Staff("Wooden staff", 30, 15);
            this.AddItemToInventory(staff);
            this.Equip(staff);
        }
    }
}