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
        /// checks if the list <paramref name="sudokuCells"/> has all sudoku values (1-9) exactly once
        /// </summary>
        /// <param name="sudokuCells">list of cells</param>
        /// <returns></returns>
        public static bool HasAllSudokuValues(this List<SudokuCell> sudokuCells)
        {
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
