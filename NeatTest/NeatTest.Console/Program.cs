using System;
using TitTacToeGame;

namespace NeatTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();
            Brain p1 = new Brain();
            Brain p2 = new Brain();

            while(Game.GetGameState() == WinnerEnum.NobodyYet)
            {
                if (Game.Turn == PlayerEnum.Player1)
                    p1.Move(Game);
                else
                    p2.Move(Game);
            }

            Draw(Game.Board);

            Console.WriteLine(Game.GetGameState().ToString());
            Console.ReadKey();
        }

        private static void Draw(Board board)
        {
            WriteLine(board.Squares, 0);
            WriteLine(board.Squares, 1);
            WriteLine(board.Squares, 2);
        }

        private static void WriteLine(Square[,] squares, int line)
        {
            WriteCell(squares[0, line]);
            WriteCell(squares[1, line]);
            WriteCell(squares[2, line]);
            Console.WriteLine();
        }

        private static void WriteCell(Square square)
        {
            Console.Write(TranslateValue(square.State));
        }

        private static string TranslateValue(Square.StateEnum state)
        {
            switch (state)
            {
                case Square.StateEnum.Player1:
                    return "X";
                case Square.StateEnum.Player2:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}
