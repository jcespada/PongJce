using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace pong
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string score;

        public MainWindowViewModel()
        {
            float ymin = 30;
            float ymax = 430;

            Player1 = new PlayerViewModel(ymin, ymax, false);
            Player2 = new PlayerViewModel(ymin, ymax, true);

            Ball = new BallViewModel(ymin, ymax);
            ReStartGame();
            Score = "0 - 0";

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();

        }

        public string Score 
        { 
            get => score;
            set { score = value; OnPropertyChanged(); }
        }

        public BallViewModel Ball { get; set; }
        public PlayerViewModel Player1 { get; set; }
        public PlayerViewModel Player2 { get; set; }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(5);
            Ball.UpdatePosition(Player1, Player2);
            Player1.UpdatePosition(Ball);
            Player2.UpdatePosition(Ball);
            if (Ball.CheckGoal())
            {
                if (Ball.VX > 0) { Player1.Points++; }
                else { Player2.Points++; }
                Score = Player1.Points.ToString() + " - " + Player2.Points.ToString();
                ReStartGame();
            }
        }

        private void ReStartGame()
        {
            Player1.StartGame(50, 225);
            Player2.StartGame(750, 225);
            Ball.StartGame();
            Thread.Sleep(1000);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
