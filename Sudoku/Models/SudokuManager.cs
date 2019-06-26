using Sudoku.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuManager
    {
        #region Fields and Properties

        /// <summary>
        /// number generator
        /// </summary>
        private readonly static Random generator = new Random();

        /// <summary>
        /// board with sudoku
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// values of solved sudoku
        /// </summary>
        private readonly List<List<int>> sudokuSolutionValues;

        #endregion

        public SudokuManager(Board board, List<List<int>> sudokuSolutionValues)
        {
            Board = board;
            this.sudokuSolutionValues = sudokuSolutionValues;
        }

        /// <summary>
        /// retrieve all incorrectly filled sudoku cells
        /// </summary>
        private List<SudokuCell> GetWrongValues()
        {
            var incorrectValues = new List<SudokuCell>();
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (!Board[i, j].IsEmpty() && Board[i, j].Value != sudokuSolutionValues[i][j].ToString())
                    {
                        incorrectValues.Add(Board[i, j]);
                    }
                }
            }
            return incorrectValues;
        }

        /// <summary>
        /// highlight wrongly filled sudoku cells 
        /// </summary>
        public void ShowWrongValues()
        {
            foreach (SudokuCell cell in Board.GetAllCells())
            {
                cell.RemoveHighlight();
            }

            var incorrectCells = GetWrongValues();
            foreach (SudokuCell cell in incorrectCells)
            {
                cell.HighLight();
            }
        }

        /// <summary>
        /// clear wrongly filled sudoku cells
        /// </summary>
        public void ClearWrongValues()
        {
            var incorrectCells = GetWrongValues();
            foreach (SudokuCell cell in incorrectCells)
            {
                cell.RemoveHighlight();
                cell.ClearValue();
            }
        }

        /// <summary>
        /// execute a hint - fill empty cell
        /// </summary>
        public void ShowHint()
        {
            var emptyCells = Board.GetAllCells().Where(x => x.IsEmpty()).ToList();
            int index = generator.Next(emptyCells.Count);

            if (emptyCells.Count==0)
            {
                return;
            }

            SudokuCell selectedCell = emptyCells[index];
            var position = Board.GetPosition(selectedCell);

            selectedCell.Value = sudokuSolutionValues[position.Item1][position.Item2].ToString();

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
            for (int i = 0; i < Board.Size; i++)
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
