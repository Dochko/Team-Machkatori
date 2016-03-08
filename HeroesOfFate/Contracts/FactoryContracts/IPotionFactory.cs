namespace HeroesOfFate.Contracts.FactoryContracts
{
    public interface IPotionFactory
    {
        IItem CreatePotion(string potionType, string id, decimal price);
    }
}