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

namespace pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) (this.DataContext as MainWindowViewModel).Player1.MoveUp = true;
            if (e.Key == Key.Down) (this.DataContext as MainWindowViewModel).Player1.MoveDown = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) (this.DataContext as MainWindowViewModel).Player1.MoveUp = false;
            if (e.Key == Key.Down) (this.DataContext as MainWindowViewModel).Player1.MoveDown = false;
        }
    }
}
