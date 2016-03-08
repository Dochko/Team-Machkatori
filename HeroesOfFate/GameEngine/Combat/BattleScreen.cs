using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesOfFate.Contracts;
using HeroesOfFate.GameEngine;
using HeroesOfFate.Models.Characters.Heroes;
using HeroesOfFate.Models.Characters.Monsters;
using HeroesOfFate.Models.Items.Potions;

namespace HeroesOfFate.GameEngine.Combat
{
    public class BattleScreen
    {
        private List<string> battleArea1 = new List<string>();
        private List<string> battleArea2 = new List<string>();
        private List<string> commandShow = new List<string>();
        private Core core;
        private int monsterCheck;


        public BattleScreen(Core core, int monsterCheck)
        {
            this.core = core;
            this.monsterCheck = monsterCheck;

        }

        public void StartBattle()
        {
            ScreenClear();
            core.MonsterFactory();
            List<IMonster> monsters = core.Database.Monsters.ToList();
            Random rnd = new Random();
            IMonster monster;
            if (monsterCheck == 0)
            {
                
                int monsterChoice = rnd.Next(0, 5);
                monster = this.MonsterSelect(monsterChoice, monsters);
            }
            else
            {
                monster = new Boss();
            }
            double monsterMaxHealth = monster.Health;
            ScreenUpdate(core.Hero, monster);
            DrawBattle();
            bool check = true;
            int specialHitCD = 0;
            int hardHitCD = 0;
            while (check)
            {
                try
                {
                    int dmg;
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            dmg = HeroHit(ref monster, rnd.Next((int)core.Hero.DamageMin, (int)core.Hero.DamageMax + 1));// damage must be converted to int instead of double !!!
                            DrawScreen.AddLineToBuffer(ref battleArea2, "You hitted your opponent for " + dmg + " amount of damage!");
                            if (monster.Health > 0)
                            {
                                check = this.MonsterDoDamage(rnd, monster, check);
                            }
                            else
                            {
                                check = this.BattleEnd(monster, check, monsterMaxHealth);
                            }
                            if (specialHitCD > 0) specialHitCD--;
                            if (hardHitCD > 0) hardHitCD--;
                            break;
                        case 2:
                            if (hardHitCD == 0)
                            {
                                dmg = HeroHit(ref monster, rnd.Next((int)core.Hero.DamageMin, (int)core.Hero.DamageMax + 1) * 2);
                                DrawScreen.AddLineToBuffer(ref battleArea2, "You used y and did strong hit to your opponent for " + dmg + " amount of damage!");
                                if (monster.Health > 0)
                                {
                                    check = this.MonsterDoDamage(rnd, monster, check);
                                }
                                else
                                {
                                    check = this.BattleEnd(monster, check, monsterMaxHealth);
                                }
                                hardHitCD = 2;
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(ref battleArea2, string.Format("You can`t use that skill... you still have {0} turns in CD", hardHitCD));
                            }
                            break;
                        case 3:
                            var healthPotion = core.Hero.Inventory.FirstOrDefault(x => x is HealthPotion);
                            if (healthPotion != null)
                            {
                                DrawScreen.AddLineToBuffer(ref battleArea2,
                                    "You used potion to restore 100 amount of HP");

                                core.Hero.ApplyPotionEffect((HealthPotion)healthPotion);
                                core.Hero.RemoveItemFromInventory(healthPotion);
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(ref battleArea2,
                                  "You dont have any HP potions");
                            }
                            check = this.MonsterDoDamage(rnd, monster, check);
                            if (specialHitCD > 0) specialHitCD--;
                            if (hardHitCD > 0) hardHitCD--;
                            break;
                        case 4:
                            if (specialHitCD == 0)
                            {
                                dmg = HeroHit(ref monster, rnd.Next((int)core.Hero.DamageMin, (int)core.Hero.DamageMax + 1) * 4) ;
                                DrawScreen.AddLineToBuffer(ref battleArea2, "You used special hit wich did " + dmg + " amount of damage!");
                                if (monster.Health > 0)
                                {
                                    check = this.MonsterDoDamage(rnd, monster, check);
                                }
                                else
                                {
                                    check = this.BattleEnd(monster, check, monsterMaxHealth);
                                }
                                specialHitCD = 4;
                            }
                            else
                            {
                                DrawScreen.AddLineToBuffer(ref battleArea2, string.Format("You can`t use that skill... you still have {0} turns in CD", specialHitCD));
                            }
                            break;
                        case 0:
                            break;
                        default:
                            DrawScreen.AddLineToBuffer(ref battleArea2, ExceptionConstants.InvalidCommandException);
                            break;
                    }
                    
                    DrawBattle();
                    if (command == 0) check = false;
                    if (check == false)
                    {
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    DrawScreen.AddLineToBuffer(ref battleArea2, ExceptionConstants.InvalidCommandException);
                    DrawBattle();
                }
                catch (OverflowException)
                {
                    DrawScreen.AddLineToBuffer(ref battleArea2, ExceptionConstants.InvalidCommandException);
                    DrawBattle();
                }
            }
            
        }

        private bool BattleEnd(IMonster monster, bool check, double monsterMaxHealth)
        {
            DrawScreen.AddLineToBuffer(ref battleArea2, "Monster is dead!!");
            core.Hero.Exp += monster.ExpirienceReward;
            core.Hero.Gold += monster.GoldReward;
            DrawScreen.AddLineToBuffer(ref battleArea2,
                "You earned " + monster.ExpirienceReward + " Exp, and " + monster.GoldReward + " gold.");
            monster.Health = 0;
            this.UpdateHpBar(core.Hero, monster);
            try
            {
                var item = core.LootRandomItem();
                DrawScreen.AddLineToBuffer(ref battleArea2, item.Id);
            }
            catch (ArgumentException e)
            {
                DrawScreen.AddLineToBuffer(ref battleArea2, e.Message);
            }
            check = false;
            monster.Health = monsterMaxHealth;
            return check;
        }

        private bool MonsterDoDamage(Random rnd, IMonster monster, bool check)
        {
            int dmg;
            dmg = this.MonsterHit(core.Hero, rnd.Next((int) monster.DamageMin, (int) monster.DamageMax + 1));
            DrawScreen.AddLineToBuffer(ref battleArea2,
                "Monster hitted you for " + (dmg - (core.Hero.Armor*core.Hero.ArmorRed)) + " amount of damage!");
            if (core.Hero.Health <= 0)
            {
                DrawScreen.AddLineToBuffer(ref battleArea2, "Game Over! You have been defeated.");
                this.UpdateHpBar(core.Hero, monster);
                DrawScreen.drawScreen(battleArea1, battleArea2);
                Environment.Exit(0);
                //check = false;
            }
            else
            {
                this.UpdateHpBar(core.Hero, monster);
            }
            return check;
        }

        private int HeroHit(ref IMonster monster, int damage)
        {
            monster.Health -= damage;
            if (monster.Health < 0) monster.Health = 0;
            return damage;
        }
        private int MonsterHit(Hero hero, int damage)
        {
            hero.Health -= damage - (hero.Armor * hero.ArmorRed);
            if (hero.Health < 0) hero.Health = 0;
            return damage;
        }
        private IMonster MonsterSelect(int number, List<IMonster> monsters)
        {
            IMonster monster = monsters[number];
            core.ImplementItems();
            return monster;
        }
        private void UpdateHpBar(Hero hero, IMonster monster)
        {
            var i = battleArea1.FindIndex(x => x.Contains("HP:"));
            battleArea1[i] = " ".PadLeft(4, ' ') + ("HP: " + hero.Health).PadRight(50, ' ') + "HP: " + monster.Health;
        }
        private void ScreenUpdate(Hero hero, IMonster monster)
        {
            this.FillArea(hero, monster);
            this.CommandsShow();
            this.CombineArea();
        }
        private void ScreenClear()
        {
            battleArea1.Clear();
            battleArea2.Clear();
        }
        private void FillArea(Hero hero, IMonster monster)
        {
            DrawScreen.AddLineToBuffer(ref battleArea1, Environment.NewLine);
            DrawScreen.AddLineToBuffer(ref battleArea1,
                " ".PadLeft(4, ' ') + hero.Name.PadRight(50, ' ') + monster.ToString());
            DrawScreen.AddLineToBuffer(ref battleArea1,
                " ".PadLeft(4, ' ') + ("Damage " + hero.DamageMin + " - " + hero.DamageMax).PadRight(50, ' ') + "Damage " + monster.DamageMin + " - " + monster.DamageMax);
            DrawScreen.AddLineToBuffer(ref battleArea1,
                " ".PadLeft(4, ' ') + ("HP: " + hero.Health).PadRight(50, ' ') + "HP: " + monster.Health);
            DrawScreen.AddLineToBuffer(ref battleArea1,
                " ".PadLeft(4, ' ') + ("LVL: " + hero.Level).PadRight(50, ' ') + "LVL: " + monster.Level);
            for (int i = 0; i < 2; i++)
            {
                DrawScreen.AddLineToBuffer(ref battleArea1, Environment.NewLine);
            }
            DrawScreen.AddLineToBuffer(ref battleArea1, new string('-', 90));
        }
        private void CommandsShow()
        {
            DrawScreen.AddLineToBuffer(ref commandShow, "1.Normal hit.");
            DrawScreen.AddLineToBuffer(ref commandShow, "2.Use Crush.");
            DrawScreen.AddLineToBuffer(ref commandShow, "3.Use HP potion.");
            DrawScreen .AddLineToBuffer(ref commandShow, "4.Use Ultimate skill.");
            for (int i = 0; i < 5; i++)
            {
                DrawScreen.AddLineToBuffer(ref commandShow, Environment.NewLine);
            }
        }
        private void CombineArea()
        {
            var temp = commandShow.Concat(battleArea1);
            battleArea1 = temp.ToList();
        }
        private void DrawBattle()
        {
            DrawScreen.drawScreen(battleArea1, battleArea2);
        }

    }
}
