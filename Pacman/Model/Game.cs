using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pacman
{
    public enum DIRECTION { UP, DOWN, LEFT, RiGHT }

    class Game : BaseGameObject
    {
        public static DIRECTION direction { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public static DIRECTION directionEnemy1 { get; set; }
        public static DIRECTION directionEnemy2 { get; set; }

        MessageBoxResult result;

        Timer timer;
        public PlayerUnit Player { get; set; }
        public BaseGameObject Field { get; set; }
        public BaseGameObject Finish { get; set; }
        public BaseGameObject Enemy1 { get; set; }
        public BaseGameObject Enemy2 { get; set; }
        public BaseGameObject Coin { get; set; }
        public BaseGameObject Coin2 { get; set; }
        public BaseGameObject Coin3 { get; set; }
        public BaseGameObject Coin4 { get; set; }
        public BaseGameObject Coin5 { get; set; }
        public BaseGameObject Coin6 { get; set; }
        public BaseGameObject Block { get; set; }
        public BaseGameObject Block2 { get; set; }
        public BaseGameObject Block3 { get; set; }

        public Game()
        {
            WindowWidth = 414;
            WindowHeight = 435;                        
            direction = DIRECTION.RiGHT;
            directionEnemy1 = DIRECTION.LEFT;
            directionEnemy2 = DIRECTION.UP;

            Field = new BaseGameObject
            {
                Sprite = @"\Sprites\field2.jpg",
                X = 0,
                Y = 0,
                Width = 342,
                Height = 342
            };
            Finish = new PlayerUnit
            {
                X = 295,
                Y = 0,
                Width = 80,
                Height=30,
                Sprite = @"\Sprites\finish.png"
            };
            Block = new PlayerUnit
            {
                X = 0,
                Y = 280,
                Width = 300,
                Height=10,
                Sprite = @"\Sprites\Block.jpg"
            };
            Block2 = new PlayerUnit
            {
                X = 100,
                Y = 180,
                Width = 200,
                Height = 5,
                Sprite = @"\Sprites\Block.jpg"
            };
            Block3 = new PlayerUnit
            {
                X = 100,
                Y = 80,
                Width = 300,
                Height = 10,
                Sprite = @"\Sprites\Block.jpg"
            };
            Player = new PlayerUnit
            {
                X = 20,
                Y = 320,
                Width = 50,
                Height= 50,
                Sprite = @"\Sprites\Pacman.gif"
            };
            Enemy1 = new BaseGameObject
            {
                X = 320,
                Y = 210,
                Width = 50,
                Height=50,
                Sprite = @"\Sprites\enem1.gif"
            };
            Enemy2 = new BaseGameObject
            {
                X = 30,
                Y = 50,
                Width = 50,
                Height = 50,
                Sprite = @"\Sprites\enem1.gif"
            };
            Coin = new BaseGameObject
            {
                X = 120,
                Y = 325,
                Width = 30,
                Height=30,
                Sprite = @"\Sprites\coin.png"
            };
            Coin2 = new BaseGameObject
            {
                X = 200,
                Y = 325,
                Width = 30,
                Height = 30,
                Sprite = @"\Sprites\coin.png"
            };
            Coin3 = new BaseGameObject
            {
                X = 270,
                Y = 325,
                Width = 30,
                Height = 30,
                Sprite = @"\Sprites\coin.png"
            };
            Coin4 = new BaseGameObject
            {
                X = 120,
                Y = 115,
                Width = 30,
                Height = 30,
                Sprite = @"\Sprites\coin.png"
            };
            Coin5 = new BaseGameObject
            {
                X = 200,
                Y = 115,
                Width = 30,
                Height = 30,
                Sprite = @"\Sprites\coin.png"
            };
            Coin6 = new BaseGameObject
            {
                X = 270,
                Y = 115,
                Width = 30,
                Height = 30,
                Sprite = @"\Sprites\coin.png"
            };
        }
        public void SetUnit()
        {
            Player.X = 20;
            Player.Y = 320;
            Player.Angle = 0;
            direction = DIRECTION.RiGHT;
            Coin.Sprite = @"\Sprites\coin.png";
            Coin2.Sprite = @"\Sprites\coin.png";
            Coin3.Sprite = @"\Sprites\coin.png";
            Coin4.Sprite = @"\Sprites\coin.png";
            Coin5.Sprite = @"\Sprites\coin.png";
            Coin6.Sprite = @"\Sprites\coin.png";
        }
        public void StartGame()
        {
            if (timer!=null)
            {
                timer.Dispose();                
            }
            SetUnit();
            timer = new Timer(Update);
            timer.Change(1000, 10);
        }
        private void Update(object obj)
        {
            MovePlayer();
            MoveEnemy1();
            MoveEnemy2();
            CollectCoin();
            WinLevel();
        }
        public void MoveEnemy2()
        {
            switch (directionEnemy2)
            {
                case DIRECTION.UP:
                    if (Enemy2.Y == Field.Y+10)
                        directionEnemy2 = DIRECTION.DOWN;
                    if (Enemy2.Y > Field.Y)
                        Enemy2.Y--;
                    break;
                case DIRECTION.DOWN:
                    if (Enemy2.Y + Enemy2.Height == Block.Y)
                        directionEnemy2 = DIRECTION.UP;
                    if (Enemy2.Y + Enemy2.Height < Block.Y)
                        Enemy2.Y++;
                    break;
            }
        }
        public void MoveEnemy1()
        {
            switch (directionEnemy1)
            {
                case DIRECTION.LEFT:
                    if (Enemy1.X == Block2.X)
                        directionEnemy1 = DIRECTION.RiGHT;
                    if (Enemy1.X > Block2.X)
                        Enemy1.X--;
                    break;
                case DIRECTION.RiGHT:
                    if (Enemy1.X == Field.Width)
                        directionEnemy1 = DIRECTION.LEFT;
                    if (Enemy1.X < Field.Width)
                        Enemy1.X++;
                    break;
            }
        }
        public void MovePlayer()
        {
            switch (direction)
            { 
                case DIRECTION.UP:
                    if (Player.Y <= Field.Y + 6)
                        return;

                    if (
                            (Player.X<=Block.X+Block.Width)
                            && (Player.Y<Block.Y+10)
                            && (Player.Y>=Block.Y)
                        )
                        return;

                    if (
                            (Player.X <= Block2.X + Block2.Width)
                            && (Player.X>=Block2.X)
                            && (Player.Y < Block2.Y + 10)
                            && (Player.Y >= Block2.Y)
                            ||
                            (Player.X+Player.Width <= Block2.X + Block2.Width)
                            && (Player.X+Player.Width >= Block2.X)
                            && (Player.Y < Block2.Y + 10)
                            && (Player.Y >= Block2.Y)
                        )
                        return;

                    if (
                            (Player.X+Player.Width >= Block3.X)
                            && (Player.Y < Block3.Y + 10)
                            && (Player.Y >= Block3.Y)
                        )
                        return;

                    Player.Y--;
                    GameOver();

                    break;

                case DIRECTION.DOWN:
                    if (Player.Y >= (Field.Y + Field.Height))
                        return;

                    if (
                            (Player.X <= Block.X + Block.Width) 
                            && (Player.Y + Player.Height <= Block.Y + 10) 
                            && (Player.Y + Player.Height >= Block.Y)
                        )
                        return;

                    if (
                            (Player.X <= Block2.X + Block2.Width)
                            && (Player.X >= Block2.X)
                            && (Player.Y + Player.Height < Block2.Y + 10)
                            && (Player.Y + Player.Height >= Block2.Y)
                            ||
                            (Player.X + Player.Width <= Block2.X + Block2.Width)
                            && (Player.X + Player.Width >= Block2.X)
                            && (Player.Y + Player.Height < Block2.Y + 10)
                            && (Player.Y + Player.Height >= Block2.Y)
                        )
                        return;

                    if (
                            (Player.X + Player.Width >= Block3.X)
                            && (Player.Y + Player.Height <= Block3.Y + 10)
                            && (Player.Y + Player.Height >= Block3.Y)
                        )
                        return;

                    Player.Y++;
                    GameOver();
                    break;

                case DIRECTION.LEFT:
                    if (Player.X <= Field.X+6)
                        return;

                    if (
                            (Block.Y >= Player.Y) 
                            && (Block.Y <= Player.Y + Player.Height) 
                            && (Player.X <= Block.X + Block.Width)
                            || 
                            (Block.Y + 5 >= Player.Y) 
                            && (Block.Y + 5 <= Player.Y + Player.Height) 
                            && (Player.X <= Block.X + Block.Width)
                        )
                        return;

                    if (
                            (Block2.Y >= Player.Y)
                            && (Block2.Y <= Player.Y + Player.Height)
                            && (Player.X <= Block2.X + Block2.Width)
                            &&(Player.X >= Block2.X)
                            ||
                            (Block2.Y + 5 >= Player.Y)
                            && (Block2.Y + 5 <= Player.Y + Player.Height)
                            && (Player.X + Player.Width <= Block2.X + Block2.Width)
                            && (Player.X + Player.Width >= Block2.X)
                        )
                        return;

                    Player.X--;
                    GameOver();
                    break;

                case DIRECTION.RiGHT:
                    if (Player.X >= (Field.X + Field.Width))
                        return;

                    if (
                            (Block3.Y >= Player.Y)
                            && (Block3.Y <= Player.Y + Player.Height)
                            && (Player.X + Player.Width >= Block3.X)
                            ||
                            (Block3.Y + 5 >= Player.Y)
                            && (Block3.Y + 5 <= Player.Y + Player.Height)
                            && (Player.X + Player.Width >= Block3.X)
                        )
                        return;

                    if (
                            (Block2.Y >= Player.Y)
                            && (Block2.Y <= Player.Y + Player.Height)
                            && (Player.X <= Block2.X + Block2.Width)
                            && (Player.X >= Block2.X)
                            ||
                            (Block2.Y + 5 >= Player.Y)
                            && (Block2.Y + 5 <= Player.Y + Player.Height)
                            && (Player.X + Player.Width <= Block2.X + Block2.Width)
                            && (Player.X + Player.Width >= Block2.X)
                        )
                        return;

                    Player.X++;
                    GameOver();
                    break;

                default:
                    break;                
            }
        }
        public void WinLevel()
        {
            if (Player.X>=Finish.X && Player.Y<=Finish.Y+Finish.Height)
            {
                timer.Dispose();
                result = MessageBox.Show(
                    "WINNER!\n Начать сначала?",
                    "WIN",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information
                    );
                switch (result)
                {
                    case MessageBoxResult.OK:
                        StartGame();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
        public void CollectCoin()
        {
            if (
                    (
                        (Player.X >= Coin.X) 
                        && (Player.X <= Coin.X + Coin.Width)
                        && (Player.Y >= Coin.Y) 
                        && (Player.Y <= Coin.Y + Coin.Height)
                    )
                    || 
                    (
                        (Coin.X >= Player.X) 
                        && (Coin.X <= Player.X + Player.Width)
                        && (Coin.Y + Coin.Height >= Player.Y) 
                        && (Coin.Y + Coin.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin.Sprite = null;
            }
            if (
                    (
                        (Player.X >= Coin2.X)
                        && (Player.X <= Coin2.X + Coin2.Width)
                        && (Player.Y >= Coin2.Y)
                        && (Player.Y <= Coin2.Y + Coin2.Height)
                    )
                    ||
                    (
                        (Coin2.X >= Player.X)
                        && (Coin2.X <= Player.X + Player.Width)
                        && (Coin2.Y + Coin2.Height >= Player.Y)
                        && (Coin2.Y + Coin2.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin2.Sprite = null;
            }
            if (
                    (
                        (Player.X >= Coin3.X)
                        && (Player.X <= Coin3.X + Coin3.Width)
                        && (Player.Y >= Coin3.Y)
                        && (Player.Y <= Coin3.Y + Coin3.Height)
                    )
                    ||
                    (
                        (Coin3.X >= Player.X)
                        && (Coin3.X <= Player.X + Player.Width)
                        && (Coin3.Y + Coin3.Height >= Player.Y)
                        && (Coin3.Y + Coin3.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin3.Sprite = null;
            }
            if (
                    (
                        (Player.X >= Coin4.X)
                        && (Player.X <= Coin4.X + Coin4.Width)
                        && (Player.Y >= Coin4.Y)
                        && (Player.Y <= Coin4.Y + Coin4.Height)
                    )
                    ||
                    (
                        (Coin4.X >= Player.X)
                        && (Coin4.X <= Player.X + Player.Width)
                        && (Coin4.Y + Coin4.Height >= Player.Y)
                        && (Coin4.Y + Coin4.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin4.Sprite = null;
            }
            if (
                    (
                        (Player.X >= Coin5.X)
                        && (Player.X <= Coin5.X + Coin5.Width)
                        && (Player.Y >= Coin5.Y)
                        && (Player.Y <= Coin5.Y + Coin5.Height)
                    )
                    ||
                    (
                        (Coin5.X >= Player.X)
                        && (Coin5.X <= Player.X + Player.Width)
                        && (Coin5.Y + Coin5.Height >= Player.Y)
                        && (Coin5.Y + Coin5.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin5.Sprite = null;
            }
            if (
                    (
                        (Player.X >= Coin6.X)
                        && (Player.X <= Coin6.X + Coin6.Width)
                        && (Player.Y >= Coin6.Y)
                        && (Player.Y <= Coin6.Y + Coin6.Height)
                    )
                    ||
                    (
                        (Coin6.X >= Player.X)
                        && (Coin6.X <= Player.X + Player.Width)
                        && (Coin6.Y + Coin6.Height >= Player.Y)
                        && (Coin6.Y + Coin6.Height <= Player.Y + Player.Height)
                    )
                )
            {
                Coin6.Sprite = null;
            }
        }
        public void GameOver()
        {
            if (
                    (
                        (Player.X >= Enemy1.X)
                        && (Player.X <= Enemy1.X + Enemy1.Width)
                        && (Player.Y >= Enemy1.Y)
                        && (Player.Y <= Enemy1.Y + Enemy1.Height)
                    )
                    ||
                    (
                        (Player.X+Player.Width >= Enemy1.X)
                        && (Player.X + Player.Width <= Enemy1.X + Enemy1.Width)
                        && (Player.Y+Player.Height >= Enemy1.Y)
                        && (Player.Y + Player.Height <= Enemy1.Y + Enemy1.Height)
                    )
                    ||
                    (
                        (Enemy1.X >= Player.X)
                        && (Enemy1.X <= Player.X + Player.Width)
                        && (Enemy1.Y + Enemy1.Height >= Player.Y)
                        && (Enemy1.Y + Enemy1.Height <= Player.Y + Player.Height)
                    )
                    ||
                    (
                        (Enemy1.X +Enemy1.Width>= Player.X)
                        && (Enemy1.X + Enemy1.Width <= Player.X + Player.Width)
                        && (Enemy1.Y + Enemy1.Height >= Player.Y)
                        && (Enemy1.Y + Enemy1.Height <= Player.Y + Player.Height)
                    )
                    ||
                    (
                        (Player.X >= Enemy2.X)
                        && (Player.X <= Enemy2.X + Enemy2.Width)
                        && (Player.Y >= Enemy2.Y)
                        && (Player.Y <= Enemy2.Y + Enemy2.Height)
                    )
                    ||
                    (
                        (Player.X +Player.Width>= Enemy2.X)
                        && (Player.X + Player.Width <= Enemy2.X + Enemy2.Width)
                        && (Player.Y +Player.Height>= Enemy2.Y)
                        && (Player.Y + Player.Height <= Enemy2.Y + Enemy2.Height)
                    )
                    || 
                    (
                        (Enemy2.X >= Player.X)
                        && (Enemy2.X <= Player.X + Player.Width)
                        && (Enemy2.Y + Enemy2.Height >= Player.Y)
                        && (Enemy2.Y + Enemy2.Height <= Player.Y + Player.Height)
                    )
                    ||
                    (
                        (Enemy2.X+Enemy2.Width >= Player.X)
                        && (Enemy2.X + Enemy2.Width <= Player.X + Player.Width)
                        && (Enemy2.Y + Enemy2.Height >= Player.Y)
                        && (Enemy2.Y + Enemy2.Height <= Player.Y + Player.Height)
                    )
                )
            {
                timer.Dispose();
                result = MessageBox.Show(
                    "GAME OVER!\n Начать сначала?",
                    "GAME OVER",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information
                    );
                switch (result)
                {
                    case MessageBoxResult.OK:
                        StartGame();
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
        public void RightMove()
        {
            Player.Angle = 0;
            direction = DIRECTION.RiGHT;
        }
        public void LeftMove()
        {
            Player.Angle = 180;
            direction = DIRECTION.LEFT;
        }
        public void UpMove()
        {
            Player.Angle = -90;
            direction = DIRECTION.UP;
        }
        public void DownMove()
        {
            Player.Angle = 90;
            direction = DIRECTION.DOWN;
        }
    }
}
