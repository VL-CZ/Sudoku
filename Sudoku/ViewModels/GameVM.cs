﻿using Sudoku.Enums;
using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModels
{
    /// <summary>
    /// Game ViewModel
    /// </summary>
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
        private SudokuGenerator Generator { get; }

        /// <summary>
        /// tool used for hints and detecting boxes with wrong value
        /// </summary>
        public SudokuManager Manager { get; }

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
            Generator = new SudokuGenerator(Board, difficulty);
            Manager = new SudokuManager(Board, Generator.SolvedSudokuValues);
        }
    }
}
