using Sudoku.Enums;
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
        #region Fields and Properties

        /// <summary>
        /// get all cells on the board
        /// </summary>
        public List<List<SudokuCell>> Cells
        {
            get
            {
                var cells = new List<List<SudokuCell>>();
                for (int i = 0; i < BoardSize; i++)
                {
                    cells.Add(GetNthRow(i));
                }
                return cells;
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

        /// <summary>
        /// get cell in selected row and column 
        /// </summary>
        /// <param name="row">index of the row</param>
        /// <param name="column">index of the column</param>
        /// <returns></returns>
        public SudokuCell this[int row, int column]
        {
            get
            {
                var selectedRow = GetNthRow(row);
                return selectedRow.ElementAt(column);
            }
        }

        #endregion

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
        /// Get N-th row from the board (row indexes start from 0)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public List<SudokuCell> GetNthRow(int N)
        {
            int squareRowIndex = N / SquaresPerDimension;
            int innerRowIndex = N % SquaresPerDimension;

            if (squareRowIndex < 0 || squareRowIndex >= SquaresPerDimension)
            {
                throw new IndexOutOfRangeException();
            }

            int squareID = (squareRowIndex - 1) * SquaresPerDimension + 1;

            var rowSquares = new List<SudokuSquare>();

            // add all squares in the selected row to list
            for (int i = 0; i < SquaresPerDimension; i++)
            {
                rowSquares.Add(GetSquareByID(squareID + i));
            }

            var row = new List<SudokuCell>();
            foreach (SudokuSquare square in rowSquares)
            {
                row.AddRange(square.GetNThRowOrColumn(innerRowIndex, SelectionType.Row));
            }

            return row;
        }

        /// <summary>
        /// Get N-th column from the board (indexes start from 0)
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public List<SudokuCell> GetNthColumn(int N)
        {
            int squareColIndex = N / SquaresPerDimension;
            int innerColumnIndex = N % SquaresPerDimension;

            if (squareColIndex < 0 || squareColIndex >= SquaresPerDimension)
            {
                throw new IndexOutOfRangeException();
            }

            int squareID = squareColIndex + 1;

            var colSquares = new List<SudokuSquare>();
            for (int i = squareID; i <= CellCount; i += SquaresPerDimension)
            {
                colSquares.Add(GetSquareByID(i));
            }

            var column = new List<SudokuCell>();
            foreach (SudokuSquare square in colSquares)
            {
                column.AddRange(square.GetNThRowOrColumn(innerColumnIndex, SelectionType.Column));
            }

            return column;
        }

        /// <summary>
        /// get all squares on the board in 1-dimensional list
        /// </summary>
        /// <returns></returns>
        public List<SudokuSquare> GetAllSquares()
        {
            return Squares.SelectMany(x => x).ToList();
        }

        /// <summary>
        /// get all squares on the board in 1-dimensional list
        /// </summary>
        /// <returns></returns>
        public List<SudokuCell> GetAllCells()
        {
            return Cells.SelectMany(x => x).ToList();
        }

        /// <summary>
        /// get number of non-empty cells
        /// </summary>
        /// <returns></returns>
        public int FilledCells()
        {
            return GetAllSquares().Sum(x => x.FilledCells());
        }
    }
}
