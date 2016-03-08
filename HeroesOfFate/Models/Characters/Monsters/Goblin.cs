namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Goblin : Monster
    {
        private const double MonsterDamageMinDefault = 10;
        private const double MonsterDamageMaxDefault = 30;
        private const double MonsterHealthDefault = 60;

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