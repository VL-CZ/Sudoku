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
        private readonly GameDifficulty difficulty;

        public Board Board { get; }
        public GameTimer Timer { get; }
        public SudokuGenerator Generator { get; }        

        public GameVM(GameDifficulty difficulty)
        {
            this.difficulty = difficulty;
            Board = new Board();
            Timer = new GameTimer();
            Generator = new SudokuGenerator(Board);
        }
    }
}
