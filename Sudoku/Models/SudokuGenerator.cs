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

        #endregion

        public SudokuGenerator(Board board, GameDifficulty gameDifficulty)
        {
            difficulty = gameDifficulty;
            Board = board;
            completeSudokuValues = new List<List<int>>();
            GetValidCompleteSudoku();
        }

        /// <summary>
        /// get random valid completed sudoku grid
        /// </summary>
        private void GetValidCompleteSudoku()
        {
            completeSudokuValues = ListExtensions.ValidCompletedSudoku;

            for (int i = 1; i <= Board.BoardSize; i++)
            {
                Board.GetSquareByID(i).FillWithValues(completeSudokuValues[i - 1]);
            }

        }

        /// <summary>
        /// generates valid sudoku grid
        /// </summary>
        public void RemoveValues()
        {

        }

    }
}
