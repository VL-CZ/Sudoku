using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class Board
    {
        /// <summary>
        /// collections of all cells on the board
        /// </summary>
        public ObservableCollection<ObservableCollection<Cell>> Cells { get; set; }

        /// <summary>
        /// get size of the board (in 1 dimension)
        /// </summary>
        public int BoardSize { get; } = 9;

        /// <summary>
        /// number of cells on the board
        /// </summary>
        public int CellCount
        {
            get
            {
                return BoardSize * BoardSize;
            }
        }

        public Board()
        {
            Cells = new ObservableCollection<ObservableCollection<Cell>>();
            FillBoard();
        }

        /// <summary>
        /// fill board (generate sudoku) according to sudoku rules
        /// </summary>
        private void FillBoard()
        {
            Random random = new Random();

            for (int i = 0; i < BoardSize; i++)
            {
                var row = new ObservableCollection<Cell>();

                for (int j = 0; j < BoardSize; j++)
                {
                    row.Add(new Cell(random.Next(1, BoardSize + 1)));
                }

                Cells.Add(row);
            }
        }

    }
}
