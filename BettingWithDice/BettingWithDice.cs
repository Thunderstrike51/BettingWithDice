using System;
// find out how to import the dice class from the other file

namespace BettingWithDice
{
    class Program
    {
        static void Main(string[] args)
        {
            Betting game1 = new Betting();
            game1.rules();
            Console.WriteLine("");
            while (true)
            {
                game1.game();
                string answer = "";
                Console.WriteLine("");
                while (true)
                {
                    Console.Write("Would you like to play again?(y or n) ");
                    answer = Console.ReadLine();
                    if (!answer.ToLower().Equals("y") && !answer.ToLower().Equals("n"))
                    {
                        Console.WriteLine("Please enter a valid answer.");
                        Console.WriteLine("");
                        continue;
                    }
                    else
                        break;
                }
                if (answer.ToLower().Equals("y")) {
                    game1.playAgain(answer);
                    continue;
                }
                else
                    break;
            }
        }
    }

    class Betting
    {
        private static long cashAmount;
        private static long player1Amount;
        private static long player2Amount;
        private static int rollAmount;
        private static long betAmountPlayer1;
        private static long betAmountPlayer2;
        private int numGames;


        public Betting()
        {
            cashAmount = 1500;
            player1Amount = cashAmount;
            player2Amount = cashAmount;
            rollAmount = 0;
            betAmountPlayer1 = 100;
            betAmountPlayer2 = 100;
            numGames = 0;
        }
        public void rules()
        {
            Console.WriteLine("How the game works!");
            Console.WriteLine("First: You can either set the custom cash amount for each player, or leave it default at $1500.");
            Console.WriteLine("Second: Select how many dice you want to roll(5,10,15,20).");
            Console.WriteLine("Third: Ask each player how much they want to bet.");
            //Console.WriteLine("Fourth: The higher your bet, the better chance you have at winning");
            Console.WriteLine("Lastly it will print the results, the new balance amounts, and ask if you want to play again.");
        }

        public void game()
        {
            
            Dice dice1 = new Dice();
            Dice dice2 = new Dice();
            string answer = "";
            
            while (numGames == 0)
            {
                Console.Write("Would you like to leave the cash default(def) or set your own?(custom) ");
                answer = Console.ReadLine();
                if (!answer.ToLower().Equals("def") && !answer.ToLower().Equals("custom"))
                {
                    continue;
                }
                else
                    break;
            }
            if (answer.ToLower().Equals("custom"))
            {
                string newAmount = "";
                while (true)
                {
                    long temp = 0;
                    Console.Write("Please enter your custom amount: ");
                    newAmount = Console.ReadLine();
                    if (Int64.TryParse(newAmount, out temp))
                    {
                        cashAmount = temp;
                        player1Amount = cashAmount;
                        player2Amount = cashAmount;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("ENTER A NUBMER");
                        Console.WriteLine("");
                        continue;
                    }
                }
            }

            // show amout to each player
            Console.WriteLine("");
            Console.WriteLine("Player 1 amount: " + player1Amount);
            Console.WriteLine("Player 2 amount: " + player2Amount);

            // ask how many dice they want to roll
            while (true)
            {
                Console.WriteLine("");
                Console.Write("How many dice would you like to roll?(5,10,15,20) ");
                answer = Console.ReadLine();
                if (!answer.ToLower().Equals("5") && !answer.ToLower().Equals("10") && !answer.ToLower().Equals("15") && !answer.ToLower().Equals("20"))
                {
                    Console.WriteLine("Enter a valid amount.");
                    continue;
                }
                else
                    break;

            }

            // ask how much each player wants to bet
            while (true)
            {
                Console.WriteLine("");
                Console.Write("Player 1 bet amount: ");
                answer = Console.ReadLine();
                if (Int64.TryParse(answer, out betAmountPlayer1))
                {
                    if (betAmountPlayer1 < 0 || betAmountPlayer1 > player1Amount)
                    {
                        Console.WriteLine("Please enter a valid number.");
                        continue;
                    }
                    else if (betAmountPlayer1 <= player1Amount)
                        break;
                }
                else
                {
                    Console.WriteLine("That is an invalid amount.");
                    continue;
                }
            }

            while (true)
            {
                Console.WriteLine("");
                Console.Write("Player 2 bet amount: ");
                answer = Console.ReadLine();
                if (Int64.TryParse(answer, out betAmountPlayer2))
                {
                    if (betAmountPlayer2 < 0 || betAmountPlayer2 > player2Amount)
                    {
                        Console.WriteLine("Please enter a valid number.");
                        continue;
                    }
                    else if (betAmountPlayer2 <= player2Amount)
                        break;
                }
                else
                {
                    Console.WriteLine("That is an invalid amount.");
                    continue;
                }
            }
            // roll the dice
            while (true)
            {
                if (Int32.TryParse(answer, out rollAmount))
                {
                    //Dice dice1 = new Dice();
                    //Dice dice2 = new Dice();
                    dice1.rollDiceNoShow(rollAmount, true);
                    dice2.rollDiceNoShow(rollAmount, false);
                    break;
                }
                else
                    Console.WriteLine("Please enter a vaild number");
                continue;
            }

            // print who had the higher sum
            Console.WriteLine("");
            Console.WriteLine("Player 1 had a score of: " + dice1.getPlayer1Sum());
            Console.WriteLine("Player 2 had a score of: " + dice2.getPlayer2Sum());
            Console.WriteLine("");
            winner(dice1, dice2);

            // ask if they want to play again

        }

        public void winner(Dice dice1, Dice dice2)
        {
            // check for the winner and update the cash amounts and show's their new balance

            if(dice1.getPlayer1Sum() > dice2.getPlayer2Sum())
            {
                Console.WriteLine("Player 1 wins!");
                Console.WriteLine("");
                player1Amount += betAmountPlayer2;
                player2Amount -= betAmountPlayer2;
                Console.WriteLine("Player 1's new balance: " + player1Amount);
                Console.WriteLine("Player 2's new balance: " + player2Amount);

            }
            else if (dice1.getPlayer1Sum() == dice2.getPlayer2Sum())
            {
                Console.WriteLine("It's a tie! no one wins!");
                Console.WriteLine("");
                Console.WriteLine("Player 1's balance: " + player1Amount);
                Console.WriteLine("Player 2's balance: " + player2Amount);
            }
            else
            {
                Console.WriteLine("Player 2 wins!");
                Console.WriteLine("");
                player2Amount +=  betAmountPlayer1;
                player1Amount -= betAmountPlayer1;
                Console.WriteLine("Player 1's new balance: " + player1Amount);
                Console.WriteLine("Player 2's new balance: " + player2Amount);
            }

        }
        public void playAgain(string answer)
        {
            if (answer.ToLower().Equals("y"))
            {
                numGames++;
            }
        }
    }

    class Dice
    {
        private int player1Sum;
        private int player2Sum;
        private static int sides;
        public Dice()
        {
            sides = 6;
            player1Sum = 0;
            player2Sum = 0;
        }

        public int rollDice(long nd) //nd = numDice
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

        public void rollDiceNoShow(long nd, Boolean player1) //nd = numDice
        {
            Random rand = new Random();
            // if there is only one dice
            if (player1)
            {
                int newSide = rand.Next(1, sides + 1);
                player1Sum += newSide;
            }
            else
            {
                int newSide = rand.Next(1, sides + 1);
                player2Sum += newSide;
            }

            // more than one dice
            if (player1)
            {
                for (int i = 1; i <= nd; i++)
                {

                    int newSide = rand.Next(1, sides + 1);
                    player1Sum += newSide;
                }
            }
            else
            {
                for (int i = 1; i <= nd; i++)
                {

                    int newSide = rand.Next(1, sides + 1);
                    player2Sum += newSide;
                }
            }

        }

        public int getPlayer1Sum()
        {

            return player1Sum;
        }

        public int getPlayer2Sum()
        {
            return player2Sum;
        }
    }
}
