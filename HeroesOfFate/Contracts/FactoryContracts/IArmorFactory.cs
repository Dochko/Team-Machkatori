namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IArmorFactory
    {
        IItem CreateArmor(string armorType, string id, double armorDeffence, decimal price);
    }
}