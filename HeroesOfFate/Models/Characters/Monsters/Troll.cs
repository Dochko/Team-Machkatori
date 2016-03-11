namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Troll : Monster
    {
        private const double MonsterDamageMinDefault = 50;

        private const double MonsterDamageMaxDefault = 70;

        private const double MonsterHealthDefault = 200;

        public Troll()
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}