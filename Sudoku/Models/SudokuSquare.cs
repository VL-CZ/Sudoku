using Sudoku.Enums;
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
        private static Random generator = new Random();

        private readonly int numberOfCells = 9;

        public int Id { get; }

        public int CellsInOneDimension
        {
            get
            {
                return (int)Math.Sqrt(numberOfCells);
            }
        }

        public ObservableCollection<ObservableCollection<SudokuCell>> Cells { get; }

        public SudokuSquare(int id)
        {
            Id = id;
            Cells = new ObservableCollection<ObservableCollection<SudokuCell>>();
            GenerateCellValues();
        }

        public SudokuSquare(int id, int numberOfCells)
        {
            Id = id;
            this.numberOfCells = numberOfCells;
        }

        /// <summary>
        /// fill square with values 1-9 (each value is used once)
        /// </summary>
        private void GenerateCellValues()
        {
            var values = new List<int>();
            for (int i = 1; i <= numberOfCells; i++)
            {
                values.Add(i);
            }

            for (int i = 1; i <= CellsInOneDimension; i++)
            {
                var row = new ObservableCollection<SudokuCell>();
                for (int j = 1; j <= CellsInOneDimension; j++)
                {
                    int isGenerated = generator.Next(0, 2);
                    SudokuCell cell;

                    if (isGenerated == 0)
                    {
                        cell = new SudokuCell();
                    }
                    else
                    {
                        int cellValue = values[generator.Next(values.Count)];
                        values.Remove(cellValue);

                        cell = new SudokuCell(cellValue, true);
                    }
                    row.Add(cell);
                }
                Cells.Add(row);
            }
        }

        /// <summary>
        /// returns number of non-empty cells 
        /// </summary>
        /// <returns></returns>
        public int FilledCells()
        {
            int count = 0;

            for (int i = 0; i < CellsInOneDimension; i++)
            {
                for (int j = 0; j < CellsInOneDimension; j++)
                {
                    if (Cells[i][j].Value != null)
                    {
                        count++;
                    }
                }
            }
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
    }
}
