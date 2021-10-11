using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace pong
{
    public class BallViewModel : INotifyPropertyChanged
    {
        public BallViewModel(float ymin, float ymax)
        {
            Thick = 8;
            positionArray = new Point[4];
            SetPosition();

            _ymin = ymin;
            _ymax = ymax;
        }

        public float X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X1));
                OnPropertyChanged(nameof(X2));
            }
        }
        public float Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        private float _ymin;
        private float _ymax;
        private Point[] positionArray;

        public float X1 => X - Thick / 2;
        public float X2 => X + Thick / 2;


        public float VX { get; set; }
        public float VY { get; set; }

        public float Thick { get; set; }

        public PointCollection Position { get; set; }

        private float x;

        internal bool CheckGoal()
        {
            return X < 0 || X > 800;
        }

        private float y;

        private void SetPosition()
        {
            positionArray[0].X = (int)(X - Thick / 2);
            positionArray[0].Y = (int)(Y - Thick / 2);
            positionArray[1].X = (int)(X - Thick / 2);
            positionArray[1].Y = (int)(Y + Thick / 2);
            positionArray[2].X = (int)(X + Thick / 2);
            positionArray[2].Y = (int)(Y + Thick / 2);
            positionArray[3].X = (int)(X + Thick / 2);
            positionArray[3].Y = (int)(Y - Thick / 2);
            Position = new PointCollection(positionArray);
            OnPropertyChanged(nameof(Position));
        }

        internal void StartGame()
        {
            X = 400;
            Y = 225;
            VX = 2;
            VY = 0;
        }

        public void UpdatePosition(PlayerViewModel p1, PlayerViewModel p2)
        {
            X += VX;
            if (CheckPlayerColission(p1, p2)) VX *= -1;

            Y += VY;
            if (CheckVerticalColission()) VY *= -1;

            SetPosition();
        }

        private bool CheckPlayerColission(PlayerViewModel p1, PlayerViewModel p2)
        {
            var colission = false;
            if (VX > 0)
            {
                colission = X + Thick / 2 >= p2.X 
                    &&
                    X < p2.X
                    &&
                    Y < p2.Y2 
                    && 
                    Y > p2.Y1;
                if(colission)
                {
                    VY += (Y - p2.Y) / p2.Length * VX;
                }
            }
            else
            {
                colission = X - Thick / 2 <= p1.X 
                    &&
                    X > p1.X
                    &&
                    Y < p1.Y2 
                    && Y > p1.Y1;
                if(colission)
                {
                    VY -= (Y - p1.Y) / p1.Length * VX;
                }
            }

            return colission;
        }

        public bool CheckVerticalColission()
        {
            return (Y <= _ymin + Thick / 2 || Y >= _ymax - Thick/2);
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
