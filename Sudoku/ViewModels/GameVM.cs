using Sudoku.Enums;
using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModels
{
    class GameVM : BaseVM
    {
        #region Fields and Properties

        /// <summary>
        /// difficulty of the sudoku
        /// </summary>
        private readonly GameDifficulty difficulty;

        /// <summary>
        /// sudoku generator
        /// </summary>
        private readonly SudokuGenerator generator;

        /// <summary>
        /// tool used for hints and detecting boxes with wrong value
        /// </summary>
        public readonly SudokuSolver solver;

        /// <summary>
        /// sudoku board
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// game timer
        /// </summary>
        public GameTimer Timer { get; }

        #endregion

        public GameVM(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            Board = new Board();
            Timer = new GameTimer();
            generator = new SudokuGenerator(Board);
            solver = new SudokuSolver(Board);
        }
    }
}
