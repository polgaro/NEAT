using System;
using TitTacToeGame;
using static TitTacToeGame.Game;

namespace NeatTest.ConsoleApp
{
    internal class RandomBrain: IPlayer
    {
        public RandomBrain()
        {

        }

        public void Move(Game game)
        {
            MoveDTO move;
            do
            {
                move = GenerateMove();
            }
            while (!game.CanMakeMove(move));

            game.Move(move);
        }

        private MoveDTO GenerateMove()
        {
            MoveDTO dto = new MoveDTO();
            int ubound = 3;
            dto.X = new Random().Next(ubound);
            dto.Y = new Random().Next(ubound);
            return dto;
        }
    }
}