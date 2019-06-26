using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuSolver
    {
        //private Board Board { get; }
        //private List<List<List<int>>> possibleValues;

        //public SudokuSolver(Board sudokuValues)
        //{
        //    //Board = sudokuValues;
        //    possibleValues = new List<List<List<int>>>();
        //}

        public SudokuSolver()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public bool Solve(Board board)
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int column = 0; column < board.Size; column++)
                {
                    if (board[row, column].IsEmpty())
                    {
                        for (int number = SudokuCell.minValue; number <= SudokuCell.maxValue; number++)
                        {
                            board[row, column].Value = number.ToString();
                            if (board.IsValid(row, column) && Solve(board))
                            {
                                return true;
                            }
                            board[row, column].ClearValue();
                        }
                        return false;
                    }
                }
            }
            return true;
        }

    }
}