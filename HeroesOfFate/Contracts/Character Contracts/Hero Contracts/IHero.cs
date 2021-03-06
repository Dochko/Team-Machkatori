﻿namespace HeroesOfFate.Contracts
{
    using System.Collections.Generic;

    using HeroesOfFate.Models.Characters.Heroes;

    public interface IHero : ICharacter
    {
        string Name { get; set; }

        double Armor { get; set; }

        double Gold { get; set; }

        int Exp { get; set; }

        IEnumerable<IItem> Inventory { get; }

        IEnumerable<IItem> Equipment { get; }

        Race HeroRace { get; set; }

        double MaxHealth { get; set; }

        void StandartItems();
    }
}