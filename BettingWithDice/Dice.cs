using System;

namespace Dice
{
    class Dice
    {
        private static int sides;
        public Dice()
        {
            sides = 6;
        }

        public  int rollDice(long nd) //nd = numDice
        {
            Random rand = new Random();
            if (nd == 1)
            {
                int newSide = rand.Next(1, sides + 1);
                return newSide;
            }
            for (int i = 1; i <= nd; i++)
            {
                
                int newSide = rand.Next(1, sides + 1);
                Console.WriteLine("Dice Number " + i + " has landed on " + newSide);
            }

            return 0;
        }
    }
}
