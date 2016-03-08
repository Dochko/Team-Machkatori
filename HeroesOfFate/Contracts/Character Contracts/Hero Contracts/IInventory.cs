namespace HeroesOfFate.Contracts
{
    public interface IInventory
    {
        void AddItemToInventory(IItem item);

        void RemoveItemFromInventory(IItem item);
    }
}
