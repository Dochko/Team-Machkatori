namespace HeroesOfFate.Contracts
{
    public interface ICharacter
    {
        double Health { get; set; }

        double DamageMin { get; set; }

        double DamageMax { get; set; }

        int Level { get; set; }

        bool IsDead { get; }
    }
}
