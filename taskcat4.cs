using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        Warrior warrior = new Warrior("ragol 3enab", 70, 50);
        Mage mage = new Mage("Harry potter", 80, 40);
        Archer archer = new Archer("Robin Hod", 65, 30);
        BaseHero[] heroes = new BaseHero[] { warrior, mage, archer };

        Console.WriteLine("Welcome to the Hero Battle Game!");
        Console.WriteLine("--------------------------------------");

        Console.WriteLine("Choose your hero:");
        Console.WriteLine("--------------------------------------");

        foreach (BaseHero hero in heroes)
        {
            Console.WriteLine("Hero: " + hero.Name);
            Console.WriteLine("Health: " + hero.Health);
            Console.WriteLine("Power: " + hero.Power);
            Console.WriteLine("--------------------------------------");
        }

        Console.WriteLine("1. ragol 3enab      2. Harry potter        3. Robin Hod");

        BaseHero yourHero = null;
        string choice = (Console.ReadLine() ?? string.Empty).Trim();

        switch (choice)
        {
            case "1":
                Console.WriteLine("You selected: " + warrior.Name);
                yourHero = warrior;
                break;
            case "2":
                Console.WriteLine("You selected: " + mage.Name);
                yourHero = mage;
                break;
            case "3":
                Console.WriteLine("You selected: " + archer.Name);
                yourHero = archer;
                break;
            default:
                Console.WriteLine("Invalid choice, defaulting to Warrior.");
                yourHero = warrior;
                break;
        }

        Console.WriteLine("=======================================");
        Console.WriteLine("           BATTLE BEGINS");
        Console.WriteLine("=======================================");
        Console.WriteLine("You selected: " + yourHero.Name);
        Console.WriteLine("Choose your target:");
        Console.WriteLine("--------------------------------------");

        List<BaseHero> targets = new List<BaseHero>();
        foreach (var h in heroes)
        {
            if (!ReferenceEquals(h, yourHero))
                targets.Add(h);
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {targets[i].Name} (HP: {targets[i].Health}, Power: {targets[i].Power})");
        }

        string targetChoice = (Console.ReadLine() ?? string.Empty).Trim();
        int targetIndex;
        BaseHero enemy = null;
        if (int.TryParse(targetChoice, out targetIndex) && targetIndex >= 1 && targetIndex <= targets.Count)
        {
            enemy = targets[targetIndex - 1];
        }
        else
        {
            Console.WriteLine("Invalid target choice, defaulting to first available target.");
            enemy = targets[0];
        }
        Console.WriteLine("--------------------------------------");
        Console.WriteLine($"Battle: {yourHero.Name} vs {enemy.Name}");
        Console.WriteLine("--------------------------------------");
        Battle(yourHero, enemy);
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void Battle(BaseHero attacker, BaseHero target)
    {
        while (attacker.Health > 0 && target.Health > 0)
        {
            attacker.Attack(target);
            if (target.Health <= 0)
            {
                Console.WriteLine(target.Name + " has been defeated!");
                break;
            }

            target.Attack(attacker);
            if (attacker.Health <= 0)
            {
                Console.WriteLine(attacker.Name + " has been defeated!");
                break;
            }
        }
    }
}

public abstract class BaseHero
{
    string name;
    int health;
    int power;

    public BaseHero(string _name, int _health, int _power)
    {
        name = _name;
        health = _health;
        power = _power;
    }

    public abstract void Attack(BaseHero target);

    public void Introduce()
    {
        Console.WriteLine("Hi, I am your hero!");
    }

    public void Heal()
    {
        health += 20;
        Console.WriteLine(name + " healed 20 HP :) ");
    }

    public void Heal(int amount)
    {
        health += amount;
        Console.WriteLine(name + " healed " + amount + " HP :) ");
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Power
    {
        get { return power; }
        set { power = value; }
    }
}

class Warrior : BaseHero
{
    public Warrior(string name, int health, int power) : base(name, health, power)
    {
    }

    public override void Attack(BaseHero target)
    {
        Console.WriteLine("Warrior attacks with a sword !");
        target.Health = Math.Max(0, target.Health - Power);
        Console.WriteLine(target.Name + " takes " + Power + " damage and now has " + target.Health + " HP left.");
    }
}

class Mage : BaseHero
{
    public Mage(string name, int health, int power) : base(name, health, power)
    {
    }
    public override void Attack(BaseHero target)
    {
        Console.WriteLine("Mage attacks with a fireball !");
        target.Health = Math.Max(0, target.Health - Power);
        Console.WriteLine(target.Name + " takes " + Power + " damage and now has " + target.Health + " HP left.");
    }
}

class Archer : BaseHero
{
    public Archer(string name, int health, int power) : base(name, health, power)
    {
    }
    public override void Attack(BaseHero target)
    {
        Console.WriteLine(" attacks with a bow and arrow !");
        target.Health = Math.Max(0, target.Health - Power);
        Console.WriteLine(target.Name + " takes " + Power + " damage and now has " + target.Health + " HP left.");
    }
}


