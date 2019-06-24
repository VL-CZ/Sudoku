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
        private Random generator = new Random();

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
                    var cellValue = values[generator.Next(values.Count)];
                    values.Remove(cellValue);

                    SudokuCell cell = new SudokuCell(cellValue, true);
                    row.Add(cell);
                }
                Cells.Add(row);
            }
        }
    }
}
