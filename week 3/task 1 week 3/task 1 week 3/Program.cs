using System;

class Character
{
    public string Name;
    public int Health;
    public int Attack;

    // Constructor
    public Character(string n, int h, int a)
    {
        Name = n;
        Health = h;
        Attack = a;
    }

    // Copy Constructor
    public Character(Character c)
    {
        Name = c.Name;
        Health = c.Health;
        Attack = c.Attack;
    }
}

class Program
{
    static void Main()
    {
        Character c1 = new Character("Warrior", 100, 15);
        Character c2 = new Character(c1); // clone
        c2.Name = "Clone";

        int round = 1;

        while (c1.Health > 0 && c2.Health > 0)
        {
            Console.WriteLine("Round " + round);

            c2.Health -= c1.Attack;
            c1.Health -= c2.Attack;

            Console.WriteLine(c1.Name + " Health: " + c1.Health);
            Console.WriteLine(c2.Name + " Health: " + c2.Health);

            round++;
        }

        if (c1.Health > c2.Health)
            Console.WriteLine("Winner: " + c1.Name);
        else
            Console.WriteLine("Winner: " + c2.Name);
    }
}