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
        /// get all cells on the board
        /// </summary>
        public ObservableCollection<ObservableCollection<SudokuCell>> Cells
        {
            get
            {
                //var listOfCells = new ObservableCollection<ObservableCollection<SudokuCell>>();

                //// select 3 squares in the same row
                //for (int i = 1; i <= BoardSize - 2; i += 3)
                //{
                //    var squares = new List<SudokuSquare>();

                //    for (int j = 0; j < SquaresPerDimension; j++)
                //    {
                //        squares.Add(GetSquareByID(i + j));
                //    }

                //    var rows = new ObservableCollection<ObservableCollection<SudokuCell>>();

                //    // loop through the squares
                //    for (int j = 0; j < SquaresPerDimension; j++)
                //    {
                //        var row = new ObservableCollection<SudokuCell>();

                //        // iterate through rows in the square
                //        for (int r = 0; r < SquaresPerDimension; r++)
                //        {

                //        }
                //    }

                //    foreach (var row in rows)
                //    {
                //        listOfCells.Add(row);
                //    }
                //}
                //return listOfCells;
                return null;
            }
        }

        /// <summary>
        /// collection of all sudoku squares
        /// </summary>
        public ObservableCollection<ObservableCollection<SudokuSquare>> Squares { get; }

        /// <summary>
        /// get size of the board (number of cells in 1 dimension) - also number of cells in each square and number of squares on the board
        /// </summary>
        public int BoardSize { get; } = 9;

        /// <summary>
        /// number of squares per 1 dimension (also number of cells in square per 1 dimension )
        /// </summary>
        public int SquaresPerDimension
        {
            get
            {
                return (int)Math.Sqrt(BoardSize);
            }
        }

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
            Squares = new ObservableCollection<ObservableCollection<SudokuSquare>>();

            CreateSquares();
        }

        /// <summary>
        /// create Sudoku squares on the board
        /// </summary>
        private void CreateSquares()
        {
            for (int i = 0; i < SquaresPerDimension; i++)
            {
                var row = new ObservableCollection<SudokuSquare>();
                for (int j = 1; j <= SquaresPerDimension; j++)
                {
                    row.Add(new SudokuSquare(3 * i + j));
                }
                Squares.Add(row);
            }
        }

        /// <summary>
        /// get square with specified ID, returns null if ID isn't found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SudokuSquare GetSquareByID(int id)
        {
            for (int i = 0; i < SquaresPerDimension; i++)
            {
                for (int j = 0; j < SquaresPerDimension; j++)
                {
                    if (Squares[i][j].Id == id)
                    {
                        return Squares[i][j];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get N-th row from the board (indexes start from 1)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public List<SudokuCell> GetNthRow(int N)
        {
            return null;
        }

        /// <summary>
        /// Get N-th column from the board (indexes start from 1)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public List<SudokuCell> GetNthColumn(int N)
        {
            return null;
        }
    }
}
