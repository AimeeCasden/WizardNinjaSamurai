using System;
using System.Collections.Generic;

namespace WizardNinjaSamurai
{
    class Human
    {
        // All human attributes subject to change per dervied class
        public string Name;
        public int Strength;
        public int Intelligence;
        public int Dexterity;
        public int health;
        public int Health
        // Health is to remain public but we have to GET that attribute in order to access it.
        {
            get { return health; }
            // set { health =  }
        }

        // setting specific atttributes for Human
        public Human(string name)
        {
            Name = name;
            Strength = 3;
            Intelligence = 3;
            Dexterity = 3;
            health = 100;
        }
        public Human(string name, int str, int intel, int dex, int hp)
        {
            Name = name;
            Strength = str;
            Intelligence = intel;
            Dexterity = dex;
            health = hp;
        }
        // Building the attack method 
        public virtual int Attack(Human target)
        {
            int dmg = Strength * 3;
            target.health -= dmg;
            Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
            return target.health;
        }

        // Have the Human be attacked which decreases their target health from the amount of damage dealt 
        public int BeAttacked(Human target, int dmg)
        {
            target.health -= dmg;
            Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
            return target.health;
        }

        // Have a BeHealed method that heals the target Human by 10 Intelligence
        public int BeHealed(Human target, int amount)
        {
            // have the target health increase 
            target.health += amount;
            Console.WriteLine($"{target.Name} was healed by {amount} points!");
            return target.health;
        }

    }
        // New wizard class
    class Wizard : Human
    {
        // derived class has different attributes because it is NOT a HUMAN
        public Wizard(string name) : base(name)
        {
            Intelligence = 25;
            health = 50;
        }
        // Override attack method to Wizard. This reduces the target by 5 Intelligence and heals the Wizard by the amount of damage dealt.
        public override int Attack(Human target)
        {
            int dmg = Intelligence * 5;
            base.BeAttacked(target, dmg);
            health += dmg;
            Console.WriteLine($"{this.Name} healed by {dmg} points!");
            return target.Health;
        }
        // Wizard has a method called Heal, which then heals the target Human by 10 * Intelligence
        public int Heal(Human target)
        {
            int amount = Intelligence * 10;
            base.BeHealed(target, amount);
            return target.Health;
        }
    }

    class Ninja : Human
    {
        // make a constructor that sets the Ninja's default dexterity to 175
        public Ninja(string name) : base(name)
        {
            Dexterity = 175;
        }
        // Create and override attack method to reduce the target by 5 Dexterity and then a 20% change of dealing an addition 10 points of damage
        public override int Attack(Human target)
        {
            int dmg = Dexterity * 5;
            // random method to randomly deal additional 10 points of damage
            Random rand = new Random();
            // 20% chance of dealing 10 points of damage.
            // The range of the chance is 0,5 because there is a 1 in 5 chance that there will be 10 points of damage dealt.
            int chance = rand.Next(0, 5);
            // if there is a 0% chance of dealing 10 damage increase because that is in the range of chance.
            if (chance == 0)
            {
                dmg += 10;
            }
            base.BeAttacked(target, dmg);
            return target.Health;
        }
        // Ninja needs a method called Steal which reduces a target Human's health by 5 and adds this amount to its own health.
        public int Steal(Human target)
        {
            // reduces the targets health by 5
            base.BeAttacked(target, 5);
            health += 5;
            return target.Health;
        }
    }
    class Samurai : Human
    {
        // baseHealth is private so other players cannot affect the base health. Makes it so the 200 doesn't rise or fall - total health stays as 200. Ya get me?
        private int baseHealth = 200;

        // constructor that sets the Samurai's health to 200
        public Samurai(string name) : base(name)
        {
            health = baseHealth;
        }
        // Have an overrride method Attack which then reduces the target by 0 if it has 50 reminaing health points.
        // Samurai also needs a Meditate method which heals the Samurai back to full health
        public int Meditate()
        {
            // meditate calls the base Attack method to redeuce the target to 0
            health = baseHealth;
            return health;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Human Greg = new Human("Greg");
            Human Dean = new Human("Dean");
            Wizard Gandalf = new Wizard("Gandalf");
            Samurai Tonchu = new Samurai("Tonchu");

            Greg.Attack(Gandalf);
            Dean.Attack(Tonchu);
            Tonchu.Attack(Dean);
            Gandalf.Attack(Dean);
            Dean.Attack(Tonchu);



            Console.WriteLine(Greg.Dexterity);
        }
    }
}
