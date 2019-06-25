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

        private List<List<int>> completeSudokuValues;

        /// <summary>
        /// board with sudoku
        /// </summary>
        public Board Board { get; }

        #endregion

        public SudokuGenerator(Board board)
        {
            Board = board;
            completeSudokuValues = new List<List<int>>();
            GetValidCompleteSudoku();
        }

        /// <summary>
        /// get valid completed sudoku grid
        /// </summary>
        private void GetValidCompleteSudoku()
        {
            // this is a valid sudoku grid
            completeSudokuValues = new List<List<int>>
            {
                new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, // 1st square
                new List<int> { 4, 5, 6, 7, 8, 9, 1, 2, 3 }, // 2nd square ...
                new List<int> { 7,8,9,1,2,3,4,5,6},
                new List<int> { 2,3,4,5,6,7,8,9,1},
                new List<int> {5,6,7,8,9,1,2,3,4},
                new List<int> {8,9,1,2,3,4,5,6,7},
                new List<int> {3,4,5,6,7,8,9,1,2},
                new List<int> {6,7,8,9,1,2,3,4,5},
                new List<int> {9,1,2,3,4,5,6,7,8}
            };

            for (int i = 1; i <= Board.BoardSize; i++)
            {
                Board.GetSquareByID(i).FillWithValues(completeSudokuValues[i - 1]);
            }
        }

        /// <summary>
        /// generates valid sudoku grid
        /// </summary>
        public void GenerateSudoku()
        {

        }

    }
}
