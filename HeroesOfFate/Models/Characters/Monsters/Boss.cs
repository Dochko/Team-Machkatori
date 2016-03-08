namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Boss : Monster
    {
        private const double MonsterDamageMinDefault = 100;
        private const double MonsterDamageMaxDefault = 200;
        private const double MonsterHealthDefault = 400;

        public Boss() 
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {

        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}