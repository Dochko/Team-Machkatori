﻿namespace HeroesOfFate.Models.Characters.Monsters
{
    public class Ogre : Monster
    {
        private const double MonsterDamageMinDefault = 40;

        private const double MonsterDamageMaxDefault = 60;

        private const double MonsterHealthDefault = 150;

        public Ogre()
            : base(MonsterHealthDefault, MonsterDamageMinDefault, MonsterDamageMaxDefault)
        {
        }

        public override string ToString()
        {
            return string.Format(base.ToString());
        }
    }
}