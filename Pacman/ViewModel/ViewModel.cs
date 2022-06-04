using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pacman
{
    class ViewModel
    {
        public Game Game { get; set; }
        RelayCommand _Left;
        RelayCommand _Right;
        RelayCommand _Up;
        RelayCommand _Down;

        public ViewModel()
        {
            Game = new Game();
            Game.StartGame();
        }
        public ICommand Up
        {
            get
            {
                if (_Up == null)
                    _Up = new RelayCommand((x) =>
                    {
                        Game.UpMove();
                    });
                return _Up;
            }
        }
        public ICommand Down
        {
            get
            {
                if (_Down == null)
                    _Down = new RelayCommand((x) =>
                    {
                        Game.DownMove();
                    });
                return _Down;
            }
        }
        public ICommand Left
        {
            get
            {
                if (_Left == null)
                    _Left = new RelayCommand((x) =>
                    {
                        Game.LeftMove();
                    });
                return _Left;

            }
        }
        public ICommand Right
        {
            get
            {
                if (_Right == null)
                    _Right = new RelayCommand((x) =>
                    {
                        Game.RightMove();
                    });
                return _Right;
            }
        }
    }
}
