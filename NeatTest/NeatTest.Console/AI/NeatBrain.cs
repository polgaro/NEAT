using SharpNeat.Phenomes;
using System;
using System.Collections.Generic;
using System.Text;
using TitTacToeGame;
using static TitTacToeGame.Game;

namespace NeatTest.ConsoleApp.AI
{
    public class NeatBrain : IPlayer
    {
        IBlackBox _brain;
        public NeatBrain(IBlackBox brain)
        {
            _brain = brain;
        }

        public void Move(Game game)
        {
            MoveDTO move;
            
            // Clear the network
            _brain.ResetState();

            // Convert the game board into an input array for the network
            setInputSignalArray(_brain.InputSignalArray, game.Board);

            // Activate the network
            _brain.Activate();

            move = GenerateMove(game);
            
            game.Move(move);
        }

        private MoveDTO GenerateMove(Game game)
        {
            double max = -1;
            int idxMax = -1;
            for (int i = 0; i < 9; i++)
            {
                if (game.CanMakeMove(GetDTO(i)))
                {
                    if (_brain.OutputSignalArray[i] > max)
                    {
                        max = _brain.OutputSignalArray[i];
                        idxMax = i;
                    }
                }
            }
            return GetDTO(idxMax);
        }

        private MoveDTO GetDTO(int idxMax)
        {
            return new MoveDTO
            {
                X = GetX(idxMax),
                Y = GetY(idxMax)
            };
        }

        private int GetY(int idxMax)
        {
            return idxMax / 3;
        }

        private int GetX(int idxMax)
        {
            return idxMax % 3;
        }

        private void setInputSignalArray(ISignalArray inputSignalArray, Board board)
        {
            inputSignalArray[0] = (int)board.Squares[0, 0].State;
            inputSignalArray[1] = (int)board.Squares[1, 0].State;
            inputSignalArray[2] = (int)board.Squares[2, 0].State;
            inputSignalArray[3] = (int)board.Squares[0, 1].State;
            inputSignalArray[4] = (int)board.Squares[1, 1].State;
            inputSignalArray[5] = (int)board.Squares[2, 1].State;
            inputSignalArray[6] = (int)board.Squares[0, 2].State;
            inputSignalArray[7] = (int)board.Squares[1, 2].State;
            inputSignalArray[8] = (int)board.Squares[2, 2].State;
        }
    }
}
