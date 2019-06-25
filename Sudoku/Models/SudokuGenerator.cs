using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    class SudokuGenerator
    {
        #region Fields and Properties

        /// <summary>
        /// board with sudoku
        /// </summary>
        public Board Board { get; }

        #endregion

        public SudokuGenerator(Board board)
        {
            Board = board;
        }
        
        public void GenerateSudoku()
        {
            
        }

    }
}
