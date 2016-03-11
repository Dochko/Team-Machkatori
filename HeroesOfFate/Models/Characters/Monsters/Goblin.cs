namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Goblin : Monster
    {
        private const double MonsterDamageMinDefault = 20;

        private const double MonsterDamageMaxDefault = 40;

        private const double MonsterHealthDefault = 80;

        public Goblin()
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}