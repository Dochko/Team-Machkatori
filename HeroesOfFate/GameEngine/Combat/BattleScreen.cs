namespace HeroesOfFate.GameEngine.Combat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroesOfFate.Contracts;
    using HeroesOfFate.Models.Characters.Heroes;
    using HeroesOfFate.Models.Characters.Monsters;
    using HeroesOfFate.Models.Items.Potions;

    public class BattleScreen
    {
        private readonly Core core;

        private readonly int monsterCheck;

        private List<string> battleArea1 = new List<string>();

        private List<string> battleArea2 = new List<string>();

        private List<string> commandShow = new List<string>();

        public BattleScreen(Core core, int monsterCheck)
        {
            this.core = core;
            this.monsterCheck = monsterCheck;
        }

        public void StartBattle()
        {
            this.ScreenClear();
            this.core.MonsterFactory();
            List<IMonster> monsters = this.core.Database.Monsters.ToList();
            Random rnd = new Random();
            IMonster monster;
            if (this.monsterCheck == 0)
            {
                int monsterChoice = rnd.Next(0, 5);
                monster = this.MonsterSelect(monsterChoice, monsters);
            }
            else
            {
                monster = new Boss();
            }

            double monsterMaxHealth = monster.Health;
            this.ScreenUpdate(this.core.Hero, monster);
            this.DrawBattle();
            bool check = true;
            int specialHitCd = 0;
            int hardHitCd = 0;
            while (check)
            {
                try
                {
                    int dmg;
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            dmg = this.HeroHit(
                                ref monster, 
                                rnd.Next((int)this.core.Hero.DamageMin, (int)this.core.Hero.DamageMax + 1));

                                // damage must be converted to int instead of double !!!
                            DrawScreen.AddLineToBuffer(
                                ref this.battleArea2, 
                                "You hitted your opponent for " + dmg + " amount of damage!");
                            if (monster.Health > 0)
                            {
                                check = this.MonsterDoDamage(rnd, monster, check);
                            }
                            else
                            {
                                check = this.BattleEnd(monster, check, monsterMaxHealth);
                            }

                            if (specialHitCd > 0)
                            {
                                specialHitCd--;
                            }

                            if (hardHitCd > 0)
                            {
                                hardHitCd--;
                            }

                            break;
                        case 2:
                            if (hardHitCd == 0)
                            {
                                dmg = this.HeroHit(
                                    ref monster, 
                                    rnd.Next((int)this.core.Hero.DamageMin, (int)this.core.Hero.DamageMax + 1) * 2);
                                DrawScreen.AddLineToBuffer(
                                    ref this.battleArea2, 
                                    "You used your Crushing Attack and did strong hit to your opponent for " + dmg + " amount of damage!");
                                if (monster.Health > 0)
                                {
                                    check = this.MonsterDoDamage(rnd, monster, check);
                                }
                                else
                                {
                                    check = this.BattleEnd(monster, check, monsterMaxHealth);
                                }

                                hardHitCd = 2;
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(
                                    ref this.battleArea2, 
                                    string.Format(
                                        "You can`t use that skill... you still have {0} turns in CD", 
                                        hardHitCd));
                            }

                            break;
                        case 3:
                            var healthPotion = this.core.Hero.Inventory.FirstOrDefault(x => x is HealthPotion);
                            if (healthPotion != null)
                            {
                                DrawScreen.AddLineToBuffer(
                                    ref this.battleArea2, 
                                    "You used potion to restore 100 amount of HP");

                                this.core.Hero.ApplyPotionEffect((HealthPotion)healthPotion);
                                this.core.Hero.RemoveItemFromInventory(healthPotion);
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(ref this.battleArea2, "You dont have any HP potions");
                            }

                            check = this.MonsterDoDamage(rnd, monster, check);
                            if (specialHitCd > 0)
                            {
                                specialHitCd--;
                            }

                            if (hardHitCd > 0)
                            {
                                hardHitCd--;
                            }

                            break;
                        case 4:
                            if (specialHitCd == 0)
                            {
                                dmg = this.HeroHit(
                                    ref monster, 
                                    rnd.Next((int)this.core.Hero.DamageMin, (int)this.core.Hero.DamageMax + 1) * 4);
                                DrawScreen.AddLineToBuffer(
                                    ref this.battleArea2, 
                                    "You used special hit witch did " + dmg + " amount of damage!");
                                if (monster.Health > 0)
                                {
                                    check = this.MonsterDoDamage(rnd, monster, check);
                                }
                                else
                                {
                                    check = this.BattleEnd(monster, check, monsterMaxHealth);
                                }

                                specialHitCd = 4;
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(
                                    ref this.battleArea2, 
                                    string.Format(
                                        "You can`t use that skill... you still have {0} turns in CD", 
                                        specialHitCd));
                            }

                            break;
                        case 0:
                            break;
                        default:
                            DrawScreen.AddLineToBuffer(ref this.battleArea2, ExceptionConstants.InvalidCommandException);
                            break;
                    }

                    this.DrawBattle();
                    if (command == 0)
                    {
                        check = false;
                    }

                    if (check == false)
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    DrawScreen.AddLineToBuffer(ref this.battleArea2, ExceptionConstants.InvalidCommandException);
                    this.DrawBattle();
                }
                catch (OverflowException)
                {
                    DrawScreen.AddLineToBuffer(ref this.battleArea2, ExceptionConstants.InvalidCommandException);
                    this.DrawBattle();
                }
            }
        }

        private bool BattleEnd(IMonster monster, bool check, double monsterMaxHealth)
        {
            DrawScreen.AddLineToBuffer(ref this.battleArea2, "Monster is dead!!");
            this.core.Hero.Exp += monster.ExpirienceReward;
            this.core.Hero.Gold += monster.GoldReward;
            DrawScreen.AddLineToBuffer(
                ref this.battleArea2, 
                "You earned " + monster.ExpirienceReward + " Exp, and " + monster.GoldReward + " gold.");
            monster.Health = 0;
            this.UpdateHpBar(this.core.Hero, monster);
            try
            {
                var item = this.core.LootRandomItem();
                DrawScreen.AddLineToBuffer(ref this.battleArea2, item.Id);
            }
            catch (ArgumentException e)
            {
                DrawScreen.AddLineToBuffer(ref this.battleArea2, e.Message);
            }

            check = false;
            monster.Health = monsterMaxHealth;
            return check;
        }

        private bool MonsterDoDamage(Random rnd, IMonster monster, bool check)
        {
            var dmg = this.MonsterHit(this.core.Hero, rnd.Next((int)monster.DamageMin, (int)monster.DamageMax + 1));
            if (dmg <= 0)
            {
                DrawScreen.AddLineToBuffer(
                ref this.battleArea2,
                "Monster blows glanced from your heavy armor and did 0 damage!");
            }
            else
            {
                DrawScreen.AddLineToBuffer(
                 ref this.battleArea2,
                 "Monster hitted you for " + (dmg - (this.core.Hero.Armor * this.core.Hero.ArmorRed)) + " amount of damage!"); 
            }

            if (this.core.Hero.Health <= 0)
            {
                DrawScreen.AddLineToBuffer(ref this.battleArea2, "Game Over! You have been defeated.");
                this.UpdateHpBar(this.core.Hero, monster);
                DrawScreen.Draw(this.battleArea1, this.battleArea2);
                Environment.Exit(0);

                // check = false;
            }
            else
            {
                this.UpdateHpBar(this.core.Hero, monster);
            }

            return check;
        }

        private int HeroHit(ref IMonster monster, int damage)
        {
            monster.Health -= damage;
            if (monster.Health < 0)
            {
                monster.Health = 0;
            }

            return damage;
        }

        private int MonsterHit(Hero hero, int damage)
        {
            if ((damage - (hero.Armor * hero.ArmorRed)) < 0)
            {
                damage = 0;
            }
            else
            {
                hero.Health -= damage - (hero.Armor * hero.ArmorRed);
            }
            
            if (hero.Health < 0)
            {
                hero.Health = 0;
            }

            return damage;
        }

        private IMonster MonsterSelect(int number, List<IMonster> monsters)
        {
            IMonster monster = monsters[number];
            this.core.ImplementItems();
            return monster;
        }

        private void UpdateHpBar(Hero hero, IMonster monster)
        {
            var i = this.battleArea1.FindIndex(x => x.Contains("HP:"));
            this.battleArea1[i] = " ".PadLeft(4, ' ') + ("HP: " + hero.Health).PadRight(50, ' ') + "HP: "
                                  + monster.Health;
        }

        private void ScreenUpdate(Hero hero, IMonster monster)
        {
            this.FillArea(hero, monster);
            this.CommandsShow();
            this.CombineArea();
        }

        private void ScreenClear()
        {
            this.battleArea1.Clear();
            this.battleArea2.Clear();
        }

        private void FillArea(Hero hero, IMonster monster)
        {
            DrawScreen.AddLineToBuffer(ref this.battleArea1, Environment.NewLine);
            DrawScreen.AddLineToBuffer(
                ref this.battleArea1, 
                " ".PadLeft(4, ' ') + hero.Name.PadRight(50, ' ') + monster);
            DrawScreen.AddLineToBuffer(
                ref this.battleArea1, 
                " ".PadLeft(4, ' ') + ("Damage " + hero.DamageMin + " - " + hero.DamageMax).PadRight(50, ' ') + "Damage " + monster.DamageMin + " - " + monster.DamageMax);
            DrawScreen.AddLineToBuffer(
                ref this.battleArea1, 
                " ".PadLeft(4, ' ') + ("HP: " + hero.Health).PadRight(50, ' ') + "HP: " + monster.Health);
            DrawScreen.AddLineToBuffer(
                ref this.battleArea1, 
                " ".PadLeft(4, ' ') + ("LVL: " + hero.Level).PadRight(50, ' ') + "LVL: " + monster.Level);
            for (int i = 0; i < 2; i++)
            {
                DrawScreen.AddLineToBuffer(ref this.battleArea1, Environment.NewLine);
            }

            DrawScreen.AddLineToBuffer(ref this.battleArea1, new string('-', 90));
        }

        private void CommandsShow()
        {
            DrawScreen.AddLineToBuffer(ref this.commandShow, "1.Normal hit.");
            DrawScreen.AddLineToBuffer(ref this.commandShow, "2.Use Crush.");
            DrawScreen.AddLineToBuffer(ref this.commandShow, "3.Use HP potion.");
            DrawScreen.AddLineToBuffer(ref this.commandShow, "4.Use Ultimate skill.");
            for (int i = 0; i < 5; i++)
            {
                DrawScreen.AddLineToBuffer(ref this.commandShow, Environment.NewLine);
            }
        }

        private void CombineArea()
        {
            var temp = this.commandShow.Concat(this.battleArea1);
            this.battleArea1 = temp.ToList();
        }

        private void DrawBattle()
        {
            DrawScreen.Draw(this.battleArea1, this.battleArea2);
        }
    }
}