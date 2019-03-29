using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using TitTacToeGame;

namespace NeatTest.ConsoleApp.AI
{
    public class GameEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong _evalCount;
        private bool _stopConditionSatisfied;

        #region IPhenomeEvaluator<IBlackBox> Members

        /// <summary>
        /// Gets the total number of evaluations that have been performed.
        /// </summary>
        public ulong EvaluationCount
        {
            get { return _evalCount; }
        }

        /// <summary>
        /// Gets a value indicating whether some goal fitness has been achieved and that
        /// the the evolutionary algorithm/search should stop. This property's value can remain false
        /// to allow the algorithm to run indefinitely.
        /// </summary>
        public bool StopConditionSatisfied
        {
            get { return _stopConditionSatisfied; }
        }

         public FitnessInfo Evaluate(IBlackBox box)
        {
            double fitness = 0;

            //generate the brain with the algorithm
            IPlayer p1 = new NeatBrain(box);
            IPlayer p2 = new RandomBrain();

            //Play 100 games
            for (int i = 0; i < 100; i++)
            {
                Game Game = new Game(p1, p2);
                try
                {
                    Game.PlayUntilFinished();
                    //score
                    fitness += GetScore(Game.GetGameState());
                }
                catch
                {
                    fitness += Game.NumberOfMoves;
                }
                
            }

            return new FitnessInfo(fitness, fitness);
        }

        private double GetScore(WinnerEnum winnerEnum)
        {
            switch(winnerEnum)
            {
                case WinnerEnum.Draw:
                    return 5;
                case WinnerEnum.Player1:
                    return 10;
                case WinnerEnum.Player2:
                    return 0;
            }
            return 0;
        }

        public void Reset()
        {
        }
        #endregion
    }
}
