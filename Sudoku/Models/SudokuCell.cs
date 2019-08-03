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

        /// <summary>
        /// is this cell highlighted?
        /// </summary>
        private bool isHighlighted = false;

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
                string stringValue = value.Trim();
                if (IsDefaultValue == false && (string.IsNullOrEmpty(stringValue) ||
                    (int.TryParse(stringValue, out int intValue) && intValue >= minValue && intValue <= maxValue)))
                {
                    this.value = stringValue;
                }
                RaisePropertyChanged();
            }
        }

        private bool isDefaultValue;

        /// <summary>
        /// is this value default? (player can't change it)
        /// </summary>
        public bool IsDefaultValue
        {
            get
            {
                return isDefaultValue;
            }
            set
            {
                isDefaultValue = value;
                RaisePropertyChanged();
            }
        }

        private Brush background;

        /// <summary>
        /// background colour of the cell
        /// </summary>
        public Brush Background
        {
            get
            {
                if (isHighlighted)
                {
                    return background;
                }
                else if (IsDefaultValue)
                {
                    return Brushes.LightGray;
                }
                else
                    return Brushes.WhiteSmoke;
            }
            private set
            {
                background = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public SudokuCell()
        {
            Value = "";
            IsDefaultValue = false;
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

        /// <summary>
        /// hightlight this cell (set background to red)
        /// </summary>
        public void HighLight()
        {
            isHighlighted = true;
            Background = Brushes.Red;
        }

        /// <summary>
        /// remove highlight
        /// </summary>
        public void RemoveHighlight()
        {
            isHighlighted = false;
            RaisePropertyChanged(nameof(Background));
        }

        /// <summary>
        /// clear value of this cell and set IsDefaultValue to false
        /// </summary>
        public void ClearValue()
        {
            IsDefaultValue = false;
            Value = "";
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}