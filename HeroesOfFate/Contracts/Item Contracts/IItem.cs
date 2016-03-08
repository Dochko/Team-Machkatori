using HeroesOfFate.Models.Items;

namespace HeroesOfFate.Contracts
{
    public interface IItem
    {
        ItemType Type { get; set; }

        double WeaponAttack{ get; set; }

        double ArmorDefence{ get; set; }

        double HealthEffect { get; set; }

        string Id { get; set; }

        decimal Price { get; set; }

        bool IsOneH { get; set; }
    }
}