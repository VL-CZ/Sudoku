using Sudoku.Enums;
using Sudoku.ViewModels;
using Sudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EasyGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow(GameDifficulty.Easy);
            window.Show();
            this.Close();
        }

        private void NormalGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow(GameDifficulty.Normal);
            window.Show();
            this.Close();
        }

        private void HardGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow(GameDifficulty.Hard);
            window.Show();
            this.Close();
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
