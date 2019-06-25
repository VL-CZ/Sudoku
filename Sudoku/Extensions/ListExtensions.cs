using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Extensions
{
    static class ListExtensions
    {
        /// <summary>
        /// checks whether all values are in range <paramref name="min"/> - <paramref name="max"/> exactly once
        /// </summary>
        /// <param name="values">list of values</param>
        /// <param name="min">minimum of the range (inclusive)</param>
        /// <param name="max">maximum of the range (inclusive)</param>
        /// <returns></returns>
        public static bool HasUniqueValuesInRange(this List<int> values, int min, int max)
        {
            if (values.Where(x => (x > max || x < min)).Count() > 0)
            {
                return false;
            }
            for (int i = min; i <= max; i++)
            {
                if (!values.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// checks if the list <paramref name="sudokuCells"/> has all sudoku values (1-9) exactly once
        /// </summary>
        /// <param name="sudokuCells">list of cells</param>
        /// <returns></returns>
        public static bool HasAllSudokuValues(this List<SudokuCell> sudokuCells)
        {
            if (sudokuCells.Count != SudokuCell.maxValue)
            {
                return false;
            }
            var values = sudokuCells.Select(cell => cell.Value.Trim());

            for (int i = 1; i <= sudokuCells.Count; i++)
            {
                if (!values.Contains(i.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// get valid completed sudoku
        /// </summary>
        /// <returns></returns>
        public static List<List<int>> GetValidCompletedSudoku()
        {
                return new List<List<int>>
                {
                    new List<int> {1,8,4,5,6,2,3,9,7}, // 1st square
                    new List<int> {9,6,3,7,4,8,5,1,2}, // 2nd square ...
                    new List<int> {7,2,5,3,1,9,8,6,4},
                    new List<int> {2,3,9,7,5,6,4,1,8},
                    new List<int> {6,5,7,1,8,4,2,3,9},
                    new List<int> {1,4,8,2,9,3,6,5,7},
                    new List<int> {9,4,1,6,2,3,8,7,5},
                    new List<int> {3,7,6,8,9,5,4,2,1},
                    new List<int> {5,8,2,4,7,1,9,3,6}
                };
        }
    }
}
