namespace HeroesOfFate.Contracts.Item_Contracts
{
    public interface IWeapon : IDamage
    {
        bool IsOneH { get; set; }
    }
}