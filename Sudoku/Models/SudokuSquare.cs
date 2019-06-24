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
        private readonly int numberOfCells = 9;

        public int Id { get; }

        public ObservableCollection<ObservableCollection<SudokuCell>> Cells { get; }

        public SudokuSquare(int id)
        {
            Id = id;
        }

        public SudokuSquare(int id, int numberOfCells) : this(id)
        {
            this.numberOfCells = numberOfCells;
        }
    }
}
