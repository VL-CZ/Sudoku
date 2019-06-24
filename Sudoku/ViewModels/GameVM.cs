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
        public Board Board { get; }
        public GameTimer Timer { get; }
        private readonly GameMode difficulty;

        public GameVM()
        {
            Board = new Board();
            Timer = new GameTimer();
        }
    }
}
