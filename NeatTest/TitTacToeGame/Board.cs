using System;
using System.Collections.Generic;
using System.Text;

namespace TitTacToeGame
{
    public class Board
    {
        public Square[,] Squares { get; } = new Square[3,3] ;

        public Board()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Squares[i, j] = new Square();
                }
            }
        }

        public WinnerEnum DetectWinner()
        {
            PlayerEnum? localRet;

            for (int i = 0; i < 3; i++)
            {
                localRet = CheckHorizontal(i);
                if (localRet.HasValue)
                    return (WinnerEnum)localRet.Value;
            }
            for (int i = 0; i < 3; i++)
            {
                localRet = CheckVertical(i);
                if (localRet.HasValue)
                    return (WinnerEnum)localRet.Value;
            }

            localRet = CheckDiagonal1();
            if (localRet.HasValue)
                return (WinnerEnum)localRet.Value;

            localRet = CheckDiagonal2();
            if (localRet.HasValue)
                return (WinnerEnum)localRet.Value;

            if (WholeBoardFilledUp())
                return WinnerEnum.Draw;
            else
                return WinnerEnum.NobodyYet;
        }

        private bool WholeBoardFilledUp()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Squares[i, j].State == Square.StateEnum.Empty)
                        return false;
                }
            }
            return true;
        }

        private PlayerEnum? CheckDiagonal2()
        {
            if (Squares[0, 0].State != Square.StateEnum.Empty &&
                Squares[0, 0].State == Squares[1, 1].State && 
                Squares[1, 1].State == Squares[2, 2].State)

                    return (PlayerEnum)Squares[0, 0].State;
            return null;
        }

        private PlayerEnum? CheckDiagonal1()
        {
            if (Squares[0, 2].State != Square.StateEnum.Empty &&
               Squares[0, 2].State == Squares[1, 1].State &&
               Squares[2, 0].State == Squares[1, 1].State)

                return (PlayerEnum)Squares[0, 2].State;
            return null;
        }

        private PlayerEnum? CheckVertical(int i)
        {
            if (Squares[i, 0].State != Square.StateEnum.Empty &&
               Squares[i, 0].State == Squares[i, 1].State &&
               Squares[i, 1].State == Squares[i, 2].State)

                return (PlayerEnum)Squares[i, 0].State;
            return null;
        }

        private PlayerEnum? CheckHorizontal(int i)
        {
            if (Squares[0, i].State != Square.StateEnum.Empty &&
               Squares[0, i].State == Squares[1, i].State &&
               Squares[1, i].State == Squares[2, i].State)

                return (PlayerEnum)Squares[0, i].State;
            return null;
        }
    }
}
