using System;
using System.Collections.Generic;
using System.Text;

namespace TitTacToeGame
{
    public class Square
    {
        public enum StateEnum
        {
            Empty = 0,
            Player1 = 1,
            Player2 = -1
        }
        public StateEnum State { get; set; } = StateEnum.Empty;
    }
}
