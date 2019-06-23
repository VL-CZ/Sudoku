using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class Board
    {
        private int[,] cells;

        private int BoardSize { get; } = 9;

        public Board()
        {
            cells = new int[BoardSize, BoardSize];
        }
    }
}
