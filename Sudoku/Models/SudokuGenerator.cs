﻿using Sudoku.Enums;
using Sudoku.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuGenerator
    {
        #region Fields and Properties

        /// <summary>
        /// number of minimum row / column swaps during sudoku generating
        /// </summary>
        private const int minrowOrColumnSwaps = 50;

        /// <summary>
        /// number of maximum row / column swaps during sudoku generating
        /// </summary>
        private const int maxrowOrColumnSwaps = 100;

        /// <summary>
        /// number of removed values in easy difficulty
        /// </summary>
        private const int removedEasyValues = 30;

        /// <summary>
        /// number of removed values in normal difficulty
        /// </summary>
        private const int removedNormalValues = 40;

        /// <summary>
        /// number of removed values in hard difficulty
        /// </summary>
        private const int removedHardValues = 50;

        /// <summary>
        /// number generator
        /// </summary>
        private readonly static Random generator = new Random();

        /// <summary>
        /// difficulty of the sudoku
        /// </summary>
        private readonly GameDifficulty difficulty;

        /// <summary>
        /// valid completed sudoku (lists represent squares)
        /// </summary>
        private List<List<int>> completeSudokuValues;

        /// <summary>
        /// board with sudoku
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// solved sudoku values
        /// </summary>
        public List<List<int>> SolvedSudokuValues { get; }

        #endregion

        public SudokuGenerator(Board board, GameDifficulty gameDifficulty)
        {
            difficulty = gameDifficulty;
            Board = board;

            completeSudokuValues = new List<List<int>>();
            SolvedSudokuValues = new List<List<int>>();
            GetValidCompleteSudoku();
            RemoveValues();

        }

        /// <summary>
        /// get random valid completed sudoku grid
        /// </summary>
        private void GetValidCompleteSudoku()
        {
            completeSudokuValues = ListExtensions.GetValidCompletedSudoku();

            for (int i = 1; i <= Board.Size; i++)
            {
                Board.GetSquareByID(i).FillWithValues(completeSudokuValues[i - 1]);
            }

            int numberOfRepetitions = generator.Next(minrowOrColumnSwaps, maxrowOrColumnSwaps);

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                int option = generator.Next(0, 5);
                int squareNumber = generator.Next(Board.SquaresPerDimension);

                int outerIndex = squareNumber * Board.SquaresPerDimension;
                int innerIndex1 = generator.Next(Board.SquaresPerDimension);
                int innerIndex2 = generator.Next(Board.SquaresPerDimension);

                int first = outerIndex + innerIndex1;
                int second = outerIndex + innerIndex2;

                switch (option)
                {
                    case 0:
                        Board.SwapColumns(first, second);
                        break;
                    case 1:
                        Board.SwapRows(first, second);
                        break;
                    case 2:
                        Board.SwapColumnSquares(innerIndex1, innerIndex2);
                        break;
                    case 3:
                        Board.SwapRowSquares(innerIndex1, innerIndex2);
                        break;
                    case 4:
                        Board.Transponse();
                        break;
                    default:
                        break;
                }
            }

            // set SolvedSudokuValues
            for (int i = 0; i < Board.Size; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < Board.Size; j++)
                {
                    row.Add(int.Parse(Board[i, j].Value));
                }
                SolvedSudokuValues.Add(row);
            }
        }

        /// <summary>
        /// generates valid sudoku grid
        /// </summary>
        private void RemoveValues()
        {
            int valuesToRemove = 0;

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    valuesToRemove = removedEasyValues;
                    break;
                case GameDifficulty.Normal:
                    valuesToRemove = removedNormalValues;
                    break;
                case GameDifficulty.Hard:
                    valuesToRemove = removedHardValues;
                    break;
                default:
                    break;
            }

            int removedInCell = valuesToRemove / Board.Size - 1;
            int cleared = 0;

            var listOfSquares = Board.GetAllSquares();
            listOfSquares.Shuffle();

            // clear squares
            foreach (SudokuSquare square in listOfSquares)
            {
                int temp = ClearNRandomCells(square.GetAllNonEmptyCells(), removedInCell);
                cleared += temp;
                valuesToRemove -= temp;
            }

            var allCells = Board.GetAllNonEmptyCells(); // all non-empty cells

            // clear everything else 
            cleared += ClearNRandomCells(allCells, valuesToRemove);
        }

        /// <summary>
        /// clear N random cells (if possible) from collection
        /// </summary>
        /// <param name="cells">collection of cells</param>
        /// <param name="N"></param>
        /// <returns>number of successfully cleared cells</returns>
        private int ClearNRandomCells(List<SudokuCell> cells, int N)
        {
            int cleared = 0;
            int invalid = 0;

            while (cleared <= N)
            {
                if (cleared + invalid >= cells.Count)
                {
                    break;
                }
                int index = generator.Next(cells.Count);

                SudokuCell selectedCell = cells[index];

                if (IsOnlyPossibleMove(Board.GetRow(selectedCell), Board.GetColumn(selectedCell), int.Parse(selectedCell.Value)))
                {
                    selectedCell.ClearValue();
                    cleared++;
                }
                else
                {
                    invalid++;
                }
                cells.Remove(selectedCell);
            }
            return cleared;
        }

        /// <summary>
        /// checks whether removal of the value is possible move ()
        /// </summary>
        /// <param name="Board"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private bool IsOnlyPossibleMove(int row, int column, int value)
        {
            // check if here can be another value, if no -> remove the value
            var cellsRow = Board.GetNthRow(row).GetValuesFromCells();
            var cellsCol = Board.GetNthColumn(column).GetValuesFromCells();
            var cellsSquare = Board.GetSquareFromPosition(row, column).GetAllCells().GetValuesFromCells();

            var invalidValues = cellsRow.Union(cellsCol).Union(cellsSquare).ToList();
            invalidValues.Remove(value);

            var validValues = this.Board.AllSudokuValues.Except(invalidValues);

            if (validValues.Count() == 1 && validValues.First() == value)
                return true;

            // check if this is the ONLY possible place for value in the square, if yes -> remove the value
            bool valueCanBeElsewhere = false;
            SudokuCell cell = Board[row, column];
            var emptyCellsInSquare = Board.GetSquareFromPosition(row, column).GetAllCells().Where(x => x.IsEmpty()).ToList(); // all empty cells in square

            for (int i = 0; i < emptyCellsInSquare.Count; i++)
            {
                var selectedRow = Board.GetNthRow(Board.GetRow(emptyCellsInSquare[i]));
                var selectedCol = Board.GetNthColumn(Board.GetColumn(emptyCellsInSquare[i]));

                if (selectedRow.Contains(cell))
                {
                    selectedRow.Remove(cell);
                }
                if (selectedCol.Contains(cell))
                {
                    selectedCol.Remove(cell);
                }

                selectedRow.Add(cell);
                selectedCol.Add(cell);

                if (selectedRow.IsSudokuValid() && selectedCol.IsSudokuValid())
                {
                    valueCanBeElsewhere = true;
                }
            }
            return !valueCanBeElsewhere;
        }
    }
}
