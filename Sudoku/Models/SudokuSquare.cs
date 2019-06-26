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
    class SudokuSquare
    {
        #region Fields and Properties

        /// <summary>
        /// number generator
        /// </summary>
        private readonly static Random generator = new Random();

        /// <summary>
        /// number of cells in the square
        /// </summary>
        private readonly int numberOfCells = 9;

        /// <summary>
        /// id of the square
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// number of cells in one dimension
        /// </summary>
        public int CellsInOneDimension
        {
            get
            {
                return (int)Math.Sqrt(numberOfCells);
            }
        }

        /// <summary>
        /// all cells in the square
        /// </summary>
        public ObservableCollection<ObservableCollection<SudokuCell>> Cells { get; }

        /// <summary>
        /// get cell in seleted row and column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public SudokuCell this[int row, int column]
        {
            get
            {
                return Cells[row][column];
            }
        }

        #endregion

        public SudokuSquare(int id)
        {
            Id = id;
            Cells = new ObservableCollection<ObservableCollection<SudokuCell>>();
        }

        public SudokuSquare(int id, int numberOfCells)
        {
            Id = id;
            this.numberOfCells = numberOfCells;
        }

        /// <summary>
        /// generate empty cell in this square
        /// </summary>
        private void GenerateCells()
        {
            for (int i = 1; i <= CellsInOneDimension; i++)
            {
                var row = new ObservableCollection<SudokuCell>();
                for (int j = 1; j <= CellsInOneDimension; j++)
                {
                    row.Add(new SudokuCell());
                }
                Cells.Add(row);
            }

            //var values = new List<int>();
            //for (int i = 1; i <= numberOfCells; i++)
            //{
            //    values.Add(i);
            //}

            //for (int i = 1; i <= CellsInOneDimension; i++)
            //{
            //    var row = new ObservableCollection<SudokuCell>();
            //    for (int j = 1; j <= CellsInOneDimension; j++)
            //    {
            //        int isGenerated = generator.Next(0, 2);
            //        SudokuCell cell;

            //        if (isGenerated == 0)
            //        {
            //            cell = new SudokuCell();
            //        }
            //        else
            //        {
            //            int cellValue = values[generator.Next(values.Count)];
            //            values.Remove(cellValue);

            //            cell = new SudokuCell(cellValue, true);
            //        }
            //        row.Add(cell);
            //    }
            //    Cells.Add(row);
            //}
        }

        /// <summary>
        /// returns number of non-empty cells 
        /// </summary>
        /// <returns></returns>
        public int FilledCells()
        {
            int count = GetAllCells().Where(x => !x.IsEmpty()).Count();
            return count;
        }

        /// <summary>
        /// get all cells in N-th row or column
        /// </summary>
        /// <param name="N"></param>
        /// <param name="selectionType">Row/column</param>
        /// <returns></returns>
        public List<SudokuCell> GetNThRowOrColumn(int N, SelectionType selectionType)
        {
            if (N < 0 || N >= CellsInOneDimension)
            {
                throw new ArgumentOutOfRangeException();
            }
            var list = new List<SudokuCell>();
            for (int i = 0; i < CellsInOneDimension; i++)
            {
                switch (selectionType)
                {
                    case SelectionType.Row:
                        list.Add(Cells[N][i]);
                        break;
                    case SelectionType.Column:
                        list.Add(Cells[i][N]);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        /// <summary>
        /// get all sudoku cells in this sudoku square in 1-dimensional List
        /// </summary>
        /// <returns></returns>
        public List<SudokuCell> GetAllCells()
        {
            return Cells.SelectMany(x => x).ToList();
        }

        /// <summary>
        /// fill this square with values (top to bottom, left to right)
        /// </summary>
        /// <param name="values"></param>
        public void FillWithValues(List<int> values)
        {
            Cells.Clear();

            if (!values.HasUniqueValuesInRange(1, numberOfCells))
            {
                throw new ArgumentException();
            }

            int index = 0;
            for (int i = 0; i < CellsInOneDimension; i++)
            {
                var row = new ObservableCollection<SudokuCell>();
                for (int j = 0; j < CellsInOneDimension; j++)
                {
                    row.Add(new SudokuCell(values[index], true));
                    index++;
                }
                Cells.Add(row);
            }
        }
        
        /// <summary>
        /// get all non-empty cells in the square
        /// </summary>
        /// <returns></returns>
        public List<SudokuCell> GetAllNonEmptyCells()
        {
            return GetAllCells().Where(x => !x.IsEmpty()).ToList();
        }
    }
}
