using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sudoku.Models
{
    public class SudokuCell : ObservableObject
    {
        #region Fields and Properties

        /// <summary>
        /// minimum possible value of the cell
        /// </summary>
        public const int minValue = 1;

        /// <summary>
        /// maximum possible value of the cell
        /// </summary>
        public const int maxValue = 9;

        private string value;

        /// <summary>
        /// cell value
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                string stringValue = value;
                if (IsDefaultValue == false && (string.IsNullOrEmpty(stringValue) ||
                    (int.TryParse(stringValue, out int intValue) && intValue >= minValue && intValue <= maxValue)))
                {
                    this.value = stringValue;
                }
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// is this value default? (player can't change it)
        /// </summary>
        public bool IsDefaultValue { get; }

        private Brush background;

        /// <summary>
        /// background colour of the cell
        /// </summary>
        public Brush Background
        {
            get
            {
                if (IsDefaultValue)
                {
                    return Brushes.LightGray;
                }
                else
                    return Brushes.WhiteSmoke;
            }
            set
            {
                background = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public SudokuCell()
        {
            IsDefaultValue = false;
            Value = "";
        }

        public SudokuCell(int? value, bool defaultValue)
        {
            Value = value.ToString();
            IsDefaultValue = defaultValue;
        }

        /// <summary>
        /// checks whether this cell is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return String.IsNullOrEmpty(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}