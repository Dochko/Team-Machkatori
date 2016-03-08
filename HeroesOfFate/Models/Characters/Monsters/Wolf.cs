namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Wolf : Monster
    {
        private const double MonsterDamageMinDefault = 20;

        private const double MonsterDamageMaxDefault = 40;

        private const double MonsterHealthDefault = 80;

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