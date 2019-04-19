using System;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Player warrior = new Warrior();
            Player mage = new Mage();
            Archer archer = new Archer();
            //archer.IsLight = true;
            Console.WriteLine($"Has Armour:{warrior.CanWearArmor()} Attack Score: {warrior.Attack()}");
            Console.WriteLine($"Has Armour:{mage.CanWearArmor()} Attack Score: {mage.Attack()}");
            Console.WriteLine($"Has Armour:{archer.CanWearArmor()} Attack Score: {archer.Attack()}");
            Console.ReadLine();

        }
    }
}
