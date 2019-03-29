using System;
using System.Collections.Generic;
using System.Text;

namespace TitTacToeGame
{
    public class Game
    {
        public Game()
        {
            Turn = PlayerEnum.Player1;
            Board = new Board();
        }

        #region Properties
        public PlayerEnum Turn { get; set; }
        public Board Board { get; }
        #endregion

        #region Methods

        public bool CanMakeMove(MoveDTO moveDTO)
        {
            return Board.Squares[moveDTO.X, moveDTO.Y].State == Square.StateEnum.Empty;
        }
        public MoveResponseDTO Move(MoveDTO moveDTO)
        {
            Board.Squares[moveDTO.X, moveDTO.Y].State = (Square.StateEnum) Turn;
            SwithTurn();

            return new MoveResponseDTO { Winner = Board.DetectWinner() };
        }

        public WinnerEnum GetGameState()
        {
            return Board.DetectWinner();
        }

        private void SwithTurn()
        {
            if (Turn == PlayerEnum.Player1)
                Turn = PlayerEnum.Player2;
            else
                Turn = PlayerEnum.Player1;
        }
        #endregion

        #region DTOs
        public class MoveResponseDTO
        {
            public WinnerEnum Winner { get; set; }
        }
        public class MoveDTO
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
        #endregion
    }
}
