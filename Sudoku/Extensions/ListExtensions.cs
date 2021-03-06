﻿using Sudoku.Models;
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
        /// number generator
        /// </summary>
        private static readonly Random rng = new Random();

        /// <summary>
        /// checks whether all values are in range <paramref name="min"/> - <paramref name="max"/> exactly once
        /// </summary>
        /// <param name="values">list of values</param>
        /// <param name="min">minimum of the range (inclusive)</param>
        /// <param name="max">maximum of the range (inclusive)</param>
        /// <returns></returns>
        public static bool HasUniqueValuesInRange(this List<int> values, int min, int max)
        {
            if (values.Where(x => (x > max || x < min)).Any())
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
        /// check whether this list is sudoku valid (contains unique numbers or empty cells)
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static bool IsSudokuValid(this List<SudokuCell> cells)
        {
            var numbers = cells.Where(x => !x.IsEmpty()).Select(x => x.Value).ToList();
            if (numbers.Count == numbers.Distinct().Count())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// get all empty cells
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static List<SudokuCell> GetEmptyCells(this List<SudokuCell> cells)
        {
            return cells.Where(cell => cell.IsEmpty()).ToList();
        }

        /// <summary>
        /// get values from list of cells
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public static List<int> GetValuesFromCells(this List<SudokuCell> cells)
        {
            return cells.Where(x => !x.IsEmpty()).Select(x => int.Parse(x.Value)).ToList();
        }

        /// <summary>
        /// shuffle list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
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
