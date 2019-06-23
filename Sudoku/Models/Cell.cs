using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sudoku.Models
{
    public class Cell : ObservableObject
    {
        private int id;

        /// <summary>
        /// id of the cell (initialized in ctor)
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }

        private int value;

        /// <summary>
        /// cell value
        /// </summary>
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                RaisePropertyChanged();
            }
        }

        public Cell(int content)
        {
            Value = content;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}