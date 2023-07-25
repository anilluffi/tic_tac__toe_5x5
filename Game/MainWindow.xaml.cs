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

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[,] gameBoard;
        private char currentPlayer = 'X';
        private int movesCount = 0;
        private short XwinCounter = 0, OwinCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            gameBoard = new char[15, 15];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    gameBoard[i, j] = ' ';
                }
            }

            NowHeWalks.Content = $"Сейчас ходит: {currentPlayer}";
        }

        

        private bool CheckWin(int row, int col)
        {
            // Проверяем горизонтальные линии
            for (int i = col - 4; i <= col; i++)
            {
                if (i < 0 || i + 4 >= gameBoard.GetLength(1))
                    continue;

                bool flag = true;
                for (int j = i; j < i + 5; j++)
                {
                    if (gameBoard[row, j] != currentPlayer)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    return true;
                }
            }

            // Проверяем вертикальные линии
            for (int i = row - 4; i <= row; i++)
            {
                if (i < 0 || i + 4 >= gameBoard.GetLength(0))
                    continue;

                bool flag = true;
                for (int j = i; j < i + 5; j++)
                {
                    if (gameBoard[j, col] != currentPlayer)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    return true;
                }
            }

            // Проверяем диагонали
            for (int i = -4; i <= 0; i++)
            {
                if (row + i < 0 || row + i + 4 >= gameBoard.GetLength(0) || col + i < 0 || col + i + 4 >= gameBoard.GetLength(1))
                    continue;

                bool flag = true;
                for (int j = 0; j < 5; j++)
                {
                    if (gameBoard[row + i + j, col + i + j] != currentPlayer)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    return true;
                }
            }

            for (int i = -4; i <= 0; i++)
            {
                if (row + i < 0 || row + i + 4 >= gameBoard.GetLength(0) || col - i < 4 || col - i >= gameBoard.GetLength(1))
                    continue;

                bool flag = true;
                for (int j = 0; j < 5; j++)
                {
                    if (gameBoard[row + i + j, col - i - j] != currentPlayer)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    return true;
                }
            }


            return false;
        }
        private void ResetGame()
        {

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = ' ';
                }
            }



            foreach (Button btn in gameGrid.Children)
            {
                btn.Content = " ";

            }


            movesCount = 0;
            CounterMove.Content = $"Ход: {movesCount}";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Window1 Window1 = new Window1(); 
            Window1.Show(); 
            this.Close(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (gameBoard[row, col] == ' ')
            {
                gameBoard[row, col] = currentPlayer;
                button.Content = currentPlayer;

                movesCount++;
                CounterMove.Content = $"Ход: {movesCount}";
                if (CheckWin(row, col))
                {
                    MessageBox.Show("Победил игрок " + currentPlayer);
                    if (currentPlayer == 'X')
                    {
                        XwinCounter++;
                       
                    }
                    else if (currentPlayer == 'O')
                    {
                        OwinCounter++;
                    }
                    
                    WinCounter.Content = $"счет 0: {OwinCounter}      X: {XwinCounter}";
                    ResetGame();
                    return;
                    
                }

                if (movesCount == 225)
                {
                    MessageBox.Show("Ничья!");
                    XwinCounter++;
                    OwinCounter++;
                    ResetGame();
                    return;
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                NowHeWalks.Content = $"Сейчас ходит: {currentPlayer}";
            }
            else
            {
                MessageBox.Show("Эта ячейка уже занята!");
            }
        }

    }
}
