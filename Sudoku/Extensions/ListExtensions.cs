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
    }
}
