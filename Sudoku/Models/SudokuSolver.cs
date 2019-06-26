using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuSolver
    {
        private Board Board { get; }
        private readonly List<List<List<int>>> possibleValues;

        public SudokuSolver(Board sudokuValues)
        {
            Board = sudokuValues;
            possibleValues = new List<List<List<int>>>();
        }


    }
}
