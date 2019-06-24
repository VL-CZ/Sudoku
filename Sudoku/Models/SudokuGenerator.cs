using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuGenerator
    {
        public Board Board { get; }

        public SudokuGenerator(Board board)
        {
            Board = board;
        }
    }
}
