using System;
using System.Linq;

namespace ConsoleApp2
{
    public class Game
    {
        private readonly Random _random;

        public Game()
        {
            _random = new Random();
        }
        
        public enum GameResult
        {
            //1 silver
            Skip,
            //1 gold, 1 silver
            Fail,
            //1 gold, 1 gold
            Win
        }
        
        public GameResult Run()
        {
            var coins = Enumerable.Repeat(false, 6).ToArray();

            for (int column = 0; column < 2; column++)
            {
                while (true)
                {
                    var twoCoins = column == 1;
                    var columnIndex = _random.Next(0, 3);
                    
                    if (coins[columnIndex*2]) continue;
                    if (coins[columnIndex*2+1]) continue;

                    if (!twoCoins)
                    {
                        coins[columnIndex*2+_random.Next(0,2)] = true;
                    }
                    else
                    {
                        coins[columnIndex*2] = true;
                        coins[columnIndex*2+1] = true;
                    }
                    break;
                }
            }

            var selectedColumn = _random.Next(0, 3);

            var firstCoinIndex = selectedColumn * 2;

            if (!coins[firstCoinIndex])
            {
                return GameResult.Skip;
            }

            var secondCoinIndex = firstCoinIndex + 1;

            if (coins[secondCoinIndex])
            {
                return GameResult.Win;
            }

            return GameResult.Fail;
        }
    }
}