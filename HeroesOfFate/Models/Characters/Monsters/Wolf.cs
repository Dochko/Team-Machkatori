namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Wolf : Monster
    {
        private const double MonsterDamageMinDefault = 20;

        private const double MonsterDamageMaxDefault = 50;

        private const double MonsterHealthDefault = 100;

        public Wolf()
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}