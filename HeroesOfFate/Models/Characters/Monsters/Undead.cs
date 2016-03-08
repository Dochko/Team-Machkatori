namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Undead : Monster
    {
        private const double MonsterDamageMinDefault = 30;
        private const double MonsterDamageMaxDefault = 50;
        private const double MonsterHealthDefault = 100;

        public Undead() 
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {

        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}