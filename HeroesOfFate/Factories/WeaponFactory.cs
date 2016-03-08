namespace HeroesOfFate.Factories
{
    using System;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Contracts.FactoryContracts;
    using HeroesOfFate.GameEngine;
    using HeroesOfFate.Models.Items.Weapons.OneHWeapons;
    using HeroesOfFate.Models.Items.Weapons.TwoHWeapons;

    public class WeaponFactory : IWeaponFactory
    {
        public IItem CreateWeapon(string weaponType, string id, double weaponAttack, decimal price)
        {
            switch (weaponType)
            {
                case "axe":
                    return new Axe(id, weaponAttack, price);
                case "handCrossbow":
                    return new HandCrossbow(id, weaponAttack, price);
                case "mace":
                    return new Mace(id, weaponAttack, price);
                case "sword":
                    return new Sword(id, weaponAttack, price);
                case "wand":
                    return new Wand(id, weaponAttack, price);
                case "bow":
                    return new Bow(id, weaponAttack, price);
                case "greatAxe":
                    return new Greataxe(id, weaponAttack, price);
                case "greatSword":
                    return new Greatsword(id, weaponAttack, price);
                case "staff":
                    return new Staff(id, weaponAttack, price);
                default:
                    throw new ArgumentException(string.Format(ExceptionConstants.MissingException, "weapon"));
            }
        }
    }
}