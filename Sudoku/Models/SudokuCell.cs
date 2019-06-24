﻿using System;
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
        /// <summary>
        /// minimum possible value of the cell
        /// </summary>
        private static readonly int minValue = 1;

        /// <summary>
        /// maximum possible value of the cell
        /// </summary>
        private static readonly int maxValue = 9;

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

        private int? value;

        /// <summary>
        /// cell value
        /// </summary>
        public int? Value
        {
            get
            {
                return value;
            }
            set
            {
                int? val = value;
                if (IsDefaultValue == false && (val == null) || (val >= minValue && val <= maxValue))
                {
                    this.value = val;
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

        public SudokuCell(int? value, bool defaultValue)
        {
            Value = value;
            IsDefaultValue = defaultValue;
        }

        public SudokuCell()
        {
            IsDefaultValue = false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}