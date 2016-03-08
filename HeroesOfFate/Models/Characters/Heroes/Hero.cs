namespace HeroesOfFate.Models.Characters.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Contracts.Item_Contracts;
    using HeroesOfFate.Events;
    using HeroesOfFate.GameEngine;
    using HeroesOfFate.Models.Items;
    using HeroesOfFate.Models.Items.Potions;
    using HeroesOfFate.Models.Items.Weapons;

    public abstract class Hero : Character, IInventory
    {
        private const int LevelDefault = 1;

        private const double StartingGold = 0;

        private readonly List<IItem> equipment;

        private readonly List<IItem> inventory;

        private double armor;

        private double armorReduction;

        private int exp;

        private double gold;

        private double maxHealth;

        private string name;

        protected Hero(
            string name, 
            Race heroRace, 
            double damageMin, 
            double damageMax, 
            double health, 
            double armor, 
            double armorReduction, 
            double maxHealth, 
            double gold = StartingGold)
            : base(LevelDefault, health, damageMin, damageMax)
        {
            this.Name = name;
            this.Armor = armor;
            this.ArmorRed = armorReduction;
            this.HeroRace = heroRace;
            this.inventory = new List<IItem>();
            this.equipment = new List<IItem>();
            this.Gold = gold;
            this.Exp = this.exp;
            this.MaxHealth = maxHealth;
        }

        public event HeroLevelChangeEventHandler ChangedLevel;

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Hero name"));
                }

                if (value.Length < 3)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.LessThanException, "Hero name", "3 symbols."));
                }

                this.name = value;
            }
        }

        public double Armor
        {
            get
            {
                return this.armor;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Armor value"));
                }

                this.armor = value;
            }
        }

        public double ArmorRed
        {
            get
            {
                return this.armorReduction;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Armor red value"));
                }

                this.armorReduction = value;
            }
        }

        public int Exp
        {
            get
            {
                return this.exp;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Hero exp"));
                }

                if (value >= 100)
                {
                    this.LevelUp(value);
                }
                else
                {
                    this.exp = value;
                }
            }
        }

        public IEnumerable<IItem> Inventory
        {
            get
            {
                return this.inventory;
            }
        }

        public IEnumerable<IItem> Equipment
        {
            get
            {
                return this.equipment;
            }
        }

        public double Gold
        {
            get
            {
                return this.gold;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.NullOrNegativeException, "Hero gold"));
                }

                this.gold = value;
            }
        }

        public double MaxHealth
        {
            get
            {
                return this.maxHealth;
            }

            set
            {
                if (value < this.Health)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(ExceptionConstants.LessThanException, "Hero maxhealth", "hero health"));
                }

                this.maxHealth = value;
            }
        }

        private Race HeroRace { get; set; }

        public void AddItemToInventory(IItem item)
        {
            this.inventory.Add(item);
        }

        public void RemoveItemFromInventory(IItem item)
        {
            this.inventory.Remove(item);
        }

        public void ApplyPotionEffect(Potion potion)
        {
            this.Health += potion.HealthEffect;

            if (this.Health > this.MaxHealth)
            {
                this.Health = this.MaxHealth;
            }
        }

        public void Equip(IItem item)
        {
            bool isEquiped = false;

            foreach (IItem equipedItem in this.equipment.ToList())
            {
                if (item.Type == equipedItem.Type)
                {
                    if (item.Type == ItemType.MainHand)
                    {
                        // var weapon = new Weapon(item.Id, item.WeaponAttack, item.Price);
                        if (!item.IsOneH)
                        {
                            IItem shield = this.FindShield();
                            if (shield != null)
                            {
                                this.equipment.Remove(shield);
                                this.AddItemToInventory(shield);
                                this.RemoveItemEffect(shield);
                            }
                        }
                    }

                    this.equipment.Remove(equipedItem);
                    this.RemoveItemEffect(equipedItem);
                    this.AddItemToInventory(equipedItem);
                    this.equipment.Add(item);
                    this.ApplyItemEffect(item);
                    this.RemoveItemFromInventory(item);
                    isEquiped = true;
                }
            }

            if (!isEquiped)
            {
                if (item.Type == ItemType.OffHand)
                {
                    Weapon weapon = this.FindWeapon();
                    if (weapon != null)
                    {
                        if (!weapon.IsOneH)
                        {
                            this.equipment.Remove(weapon);
                            this.AddItemToInventory(weapon);
                            this.RemoveItemEffect(weapon);
                        }
                    }
                }

                this.equipment.Add(item);
                this.ApplyItemEffect(item);
                this.RemoveItemFromInventory(item);
            }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "{0}\nRace: {1}\nProfession: {2}\nLevel: {3}\nExp: {8}\nHP: {4}\nDamage: ({5} , {6})\nArmor: {7}\nGold: {9}",
                    this.Name,
                    this.HeroRace,
                    this.GetType().Name,
                    this.Level,
                    this.Health,
                    this.DamageMin,
                    this.DamageMax,
                    this.Armor,
                    this.Exp,
                    this.Gold);
        }

        protected abstract void StandartItems();

        private IItem FindShield()
        {
            foreach (var item in this.equipment)
            {
                if (item.Type == ItemType.OffHand)
                {
                    return item;
                }
            }

            return null;
        }

        private Weapon FindWeapon()
        {
            foreach (var item in this.equipment)
            {
                if (item.Type == ItemType.MainHand)
                {
                    return (Weapon)item;
                }
            }

            return null;
        }

        private void ApplyItemEffect(IItem item)
        {
            if (item is IWeapon)
            {
                this.DamageMin += item.WeaponAttack;
                this.DamageMax += item.WeaponAttack;
            }

            this.Armor += item.ArmorDefence;
        }

        private void RemoveItemEffect(IItem item)
        {
            this.DamageMin -= item.WeaponAttack;
            this.DamageMax -= item.WeaponAttack;
            this.Armor -= item.ArmorDefence;
        }

        private void LevelUp(int value)
        {
            int currentLevel = this.Level;
            this.Level += value / 100;
            this.exp = value % 100;
            this.MaxHealth += 20 * (value / 100);
            this.Health = this.MaxHealth;

            this.OnLevelChange(this, new HeroChangeLevelEventArgs(this.Level, this.Level - currentLevel));
        }

        private void OnLevelChange(object sender, HeroChangeLevelEventArgs eventArgs)
        {
            if (this.ChangedLevel != null)
            {
                this.ChangedLevel(sender, eventArgs);
            }
        }
    }
}