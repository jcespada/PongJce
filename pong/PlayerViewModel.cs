using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pong
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        public PlayerViewModel(float ymin, float ymax, bool auto)
        {
            Length = 50;
            _ymin = ymin;
            _ymax = ymax;
            _auto = auto;
            Points = 0;
        }

        public float Length { get; set; }

        public float X { get; set; }

        public float Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y1));
                OnPropertyChanged(nameof(Y2));
            }
        }

        public float Y1 => Y - Length / 2;
        public float Y2 => Y + Length / 2;

        public int Points 
        { 
            get => points;
            set { points = value; OnPropertyChanged(); }
        }

        public bool _auto;
        private float y;
        private float _ymin;
        private float _ymax;
        private int points;

        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }

        public void UpdatePosition(BallViewModel ball)
        {
            if (_auto)
            {
                Y += Math.Min(Math.Abs(ball.Y - Y), 2 * Math.Sign(ball.Y - Y));
            }
            else
            {
                if (MoveUp) Y -= 2;
                if (MoveDown) Y += 2;
            }
            if (Y - Length / 2 < _ymin) Y = _ymin + Length / 2;
            if (Y + Length / 2 > _ymax) Y = _ymax - Length / 2;
        }

        internal void StartGame(float xini, float yini)
        {
            Y = yini;
            X = xini;
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
