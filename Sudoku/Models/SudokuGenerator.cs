using Sudoku.Enums;
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
        private const int removedHardValues= 50;

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
            GetValidCompleteSudoku();

            // set SolvedSudokuValues
            SolvedSudokuValues = new List<List<int>>();
            for (int i = 0; i < Board.Size; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < Board.Size; j++)
                {
                    row.Add(int.Parse(Board[i, j].Value));
                }
                SolvedSudokuValues.Add(row);
            }

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
        }

        /// <summary>
        /// generates valid sudoku grid
        /// </summary>
        private void RemoveValues()
        {
            int removedValues = 0;

            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    removedValues = removedEasyValues;
                    break;
                case GameDifficulty.Normal:
                    removedValues = removedNormalValues;
                    break;
                case GameDifficulty.Hard:
                    removedValues = removedHardValues;
                    break;
                default:
                    break;
            }

            var allCells = Board.GetAllCells();
            for (int i = 0; i < removedValues; i++)
            {
                int index = generator.Next(allCells.Count);

                SudokuCell selectedCell = allCells[index];
                selectedCell.ClearValue();
                allCells.Remove(selectedCell);
            }
        }


    }
}
