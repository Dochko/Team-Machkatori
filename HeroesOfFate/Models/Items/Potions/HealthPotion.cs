using HeroesOfFate.Contracts;

namespace HeroesOfFate.Models.Items.Potions
{
    public class HealthPotion : Potion,IHealth
    {
        private const double HealthEffectDefault = 150;

        public HealthPotion(string id, decimal price) 
            : base(id, price)
        {
            this.HealthEffect = HealthEffectDefault;
        }

    }
}