using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Extensions;

namespace Sudoku.Models
{
    class SudokuSolver
    {
        #region Fields and Properties

        /// <summary>
        /// 
        /// </summary>
        private List<int>[,] possibleValues;

        /// <summary>
        /// 
        /// </summary>
        public Board Board { get; }

        #endregion

        public SudokuSolver(Board board)
        {
            Board = board;
            possibleValues = new List<int>[Board.Size, Board.Size];
            FillWithBaseValues();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillWithBaseValues()
        {
            //for (int i = 0; i < Board.Size; i++)
            //{
            //    for (int j = 0; j < Board.Size; j++)
            //    {
            //        possibleValues[i, j] = new List<int>();
            //        possibleValues[i, j].Add(int.Parse(Board[i, j].Value));
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Board"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public bool IsOnlyPossibleMove(int row, int column, int value)
        {
            var cellsRow = Board.GetNthRow(row).GetValuesFromCells();
            var cellsCol = Board.GetNthColumn(column).GetValuesFromCells();
            var cellsSquare = Board.GetSquareFromPosition(row, column).GetAllCells().GetValuesFromCells();

            var invalidValues = cellsRow.Union(cellsCol).Union(cellsSquare).ToList();
            invalidValues.Remove(value);

            var validValues = this.Board.AllSudokuValues.Except(invalidValues);

            return (validValues.Count() == 1 && validValues.First() == value);
        }

    }
}