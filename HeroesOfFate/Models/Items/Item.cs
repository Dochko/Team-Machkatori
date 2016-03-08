using System;
using HeroesOfFate.Contracts;
using HeroesOfFate.GameEngine;

namespace HeroesOfFate.Models.Items
{
    public abstract class Item : IItem
    {
        private ItemType type;
        private string id;
        private decimal price;
        private double weaponAttack;
        private double armorDefence;
        private double healthEffect;

        protected Item(string id,decimal price)
        {
            this.Type = type;
            this.Id = id;
            this.Price = price;
            this.WeaponAttack = weaponAttack;
            this.ArmorDefence = armorDefence;
            this.HealthEffect = healthEffect;
        }

        public ItemType Type 
        {
            get { return this.type; }
            set { this.type = value; } 
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionConstants.NullOrNegativeException, "Item id"));
                }
                id = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(string.Format(ExceptionConstants.NullOrNegativeException, "Item price"));
                }
                price = value;
            }
        }

        public virtual bool IsOneH { get; set; }
        //TODO: validation
        public double WeaponAttack 
        { 
            get {return this.weaponAttack;}
            set
            {
                if (value < 0)
	            {
		            throw new ArgumentOutOfRangeException(string.Format(ExceptionConstants.LessThanException, "Weapon attack", 0));
	            }
                this.weaponAttack = value;
            }
        }

        public double ArmorDefence 
        { 
            get {return this.armorDefence;}
            set
            {
                if (value < 0)
	            {
		            throw new ArgumentOutOfRangeException(string.Format(ExceptionConstants.LessThanException, "Armor defence", 0));
	            }
                this.armorDefence = value;
            }
        }

        public double HealthEffect 
        { 
            get {return this.healthEffect;}
            set
            {
                if (value < 0)
	            {
		            throw new ArgumentOutOfRangeException(string.Format(ExceptionConstants.LessThanException, "Health effect", 0));
	            }
                this.healthEffect = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Type: {0}, Name: {1}", this.GetType().Name, this.Id);
        }
    }
}