using System;
using System.Collections.Generic;
using System.Text;

namespace TitTacToeGame
{
    public class Game
    {
        private IPlayer player1;
        private IPlayer player2;
        private int numberOfMoves;

        public Game(IPlayer p1, IPlayer p2)
        {
            player1 = p1;
            player2 = p2;
            Turn = PlayerEnum.Player1;
            Board = new Board();
            numberOfMoves = 0;
        }

        public void PlayUntilFinished()
        {
            while (GetGameState() == WinnerEnum.NobodyYet)
            {
                if (Turn == PlayerEnum.Player1)
                    player1.Move(this);
                else
                    player2.Move(this);
                numberOfMoves++;
            }
        }

        #region Properties
        public PlayerEnum Turn { get; set; }
        public Board Board { get; }
        public int NumberOfMoves { get { return numberOfMoves; } }

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
