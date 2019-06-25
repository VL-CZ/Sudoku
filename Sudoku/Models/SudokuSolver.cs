using Sudoku.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuSolver
    {
        #region Fields and Properties

        /// <summary>
        /// board with sudoku
        /// </summary>
        public Board Board { get; }

        #endregion

        public SudokuSolver(Board board)
        {
            Board = board;
        }

        /// <summary>
        /// highlight wrongly filled sudoku cells 
        /// </summary>
        public void ShowWrongValues()
        {

        }

        /// <summary>
        /// clear wrongly filled sudoku cells
        /// </summary>
        public void ClearWrongValues()
        {

        }

        /// <summary>
        /// execute a hint - fill empty cell
        /// </summary>
        public void ShowNextMove()
        {

        }

        /// <summary>
        /// checks if sudoku is solved
        /// </summary>
        public bool IsSolved()
        {
            if (Board.FilledCells() < Board.CellCount)
            {
                return false;
            }
            var allRowsColsAndSquares = new List<List<SudokuCell>>();
            for (int i = 0; i < Board.BoardSize; i++)
            {
                allRowsColsAndSquares.Add(Board.GetNthColumn(i));
                allRowsColsAndSquares.Add(Board.GetNthRow(i));
            }
            var squares = Board.GetAllSquares();
            allRowsColsAndSquares.AddRange(squares.Select(x => x.GetAllCells()));

            return allRowsColsAndSquares.TrueForAll(list => list.HasAllSudokuValues());
        }

    }
}
