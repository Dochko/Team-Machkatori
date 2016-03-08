namespace HeroesOfFate.Events
{
    public class HeroChangeLevelEventArgs
    {
        public HeroChangeLevelEventArgs(int level, int levelsGained)
        {
            this.Level = level;
            this.LevelsGained = levelsGained;
        }


        public int Level { get; set; }
        public int LevelsGained { get; set; }
    }
}
