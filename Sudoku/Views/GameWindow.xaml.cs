﻿using Sudoku.Enums;
using Sudoku.Models;
using Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Sudoku.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly GameVM gameVM;

        public GameWindow(GameDifficulty difficulty)
        {
            InitializeComponent();
            gameVM = new GameVM(difficulty);
            DataContext = gameVM;

            ShowSudokuSquares();
        }

        /// <summary>
        /// unfocus cell on enter click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GameIC.Focus();
            }
        }

        /// <summary>
        /// quit app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        /// <summary>
        /// clone UI element
        /// </summary>
        /// <param name="orig"></param>
        /// <returns></returns>
        private static UIElement CloneElement(UIElement orig)
        {
            if (orig == null)
                return (null);
            string s = XamlWriter.Save(orig);
            StringReader stringReader = new StringReader(s);

            XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());
            UIElement clonedElement = (UIElement)XamlReader.Load(xmlReader);
            xmlReader.Dispose();
            return clonedElement;
        }

        /// <summary>
        /// create controls, set binding and show Sudoku squares
        /// </summary>
        private void ShowSudokuSquares()
        {
            int squareCount = gameVM.Board.Size;
            var sudokuSquares = new List<SudokuSquare>();

            for (int i = 2; i <= squareCount; i++)
            {
                sudokuSquares.Add(gameVM.Board.GetSquareByID(i));
            }

            foreach (var square in sudokuSquares)
            {
                ItemsControl ic = (ItemsControl)CloneElement(GameIC);
                ic.ItemsSource = square.Cells;
                GameGrid.Children.Add(ic);

                Grid.SetRow(ic, (square.Id - 1) / 3);
                Grid.SetColumn(ic, (square.Id - 1) % 3);
            }

            GameIC.ItemsSource = gameVM.Board.GetSquareByID(1).Cells;
        }

        /// <summary>
        /// check if sudoku is solved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (gameVM.Manager.IsSolved())
            {
                gameVM.Timer.StopTimer();
                WinnerWindow window = new WinnerWindow(TimeTextBlock.Text);
                window.ShowDialog();
            }
        }

        /// <summary>
        /// show wrong values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowWrongValuesButton_Click(object sender, RoutedEventArgs e)
        {
            gameVM.Manager.ShowWrongValues();
        }

        /// <summary>
        /// clear wrong values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearWrongButton_Click(object sender, RoutedEventArgs e)
        {
            gameVM.Manager.ClearWrongValues();
        }

        /// <summary>
        /// show hint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHintButton_Click(object sender, RoutedEventArgs e)
        {
            gameVM.Manager.ShowHint();
        }
    }
}
