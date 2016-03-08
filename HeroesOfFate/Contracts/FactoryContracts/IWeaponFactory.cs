namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IWeaponFactory
    {
        IItem CreateWeapon(string weaponType, string id, double effect, decimal price);
    }
}