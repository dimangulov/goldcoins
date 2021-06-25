using System;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Performing some task... ");
            var game = new Game();
            var iterationCount = 1000000;
            var realIterationCount = 0;
            var runCount = 0;
            var winCount = 0;
            var skipCount = 0;
            var failCount = 0;
            
            double computeWinPercent()
            {
                if (runCount < 1) return 0;
                return ((double)winCount) / runCount;
            }
            
            double computeFailPercent()
            {
                if (runCount < 1) return 0;
                return ((double)failCount) / runCount;
            }
            
            double computeSkipPercent()
            {
                if (realIterationCount < 1) return 0;
                return ((double)skipCount) / realIterationCount;
            }
            
            using (var progress = new ProgressBar()) {
                while (true)
                {
                    realIterationCount++;
                    var result = game.Run();

                    if (result == Game.GameResult.Skip)
                    {
                        skipCount++;
                        continue;
                    }
                    
                    runCount++;

                    if (result == Game.GameResult.Fail)
                    {
                        failCount++;
                    }
                    else if (result == Game.GameResult.Win)
                    {
                        winCount++;
                    }
                    
                    progress.Report(computeWinPercent());

                    if (runCount >= iterationCount)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"First gold, second gold:");
            Console.WriteLine($"Win: {computeWinPercent() * 100}%");
            Console.WriteLine($"Fail: {computeFailPercent() * 100}%");
            Console.WriteLine($"Skip: {computeSkipPercent() * 100}%");
            Console.WriteLine($"Global condition, you choose a stack with 2 gold coins:");
            Console.WriteLine($"Win: {(double)winCount / (realIterationCount) * 100}%");
            Console.WriteLine($"Fail: {(double)failCount / (realIterationCount) * 100}%");
            Console.WriteLine($"Skip: {(double)skipCount / (realIterationCount) * 100}%");
        }
        
        
    }
}