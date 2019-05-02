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
using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool LeftRecMovement = false, RightRecMovement = false, BallMovement = false, IsPaused = true;
        public bool Leftupkeydown = false, Leftdownkeydown = false, Rightupkeydown = false, Rightdownkeydown = false;
        public int leftplayertop, leftplayerbottom, rightplayertop, rightplayerbottom, movementx, movementy;
        public string LeftMovement = "", RightMovement = "", roundwinner = "", gamewinner = "";
        public int LRY = 0, RRY = 0;
        public int BallXmove, BallYmove, P1Score = 0, P2Score = 0;
        public string Gamemode = "2P";
        public string leftrecmoving = "", rightrecmoving = "", ballmoving = "";
        public int leftmovementcounter = 0, rightmovementcounter = 0, ballmovementcounter = 0;
        public int leftfirstcheck = 0, leftsecondcheck = 0, rightfirstcheck = 0, rightsecondcheck = 0;
        public bool startingnewround = true;
        public string Theme = "";

       

        public int alterspeedcounter = 0;



        public double alterspeed = 3, BallX = 0, BallY = 0, ballfirstcheck = 0, ballsecondcheck = 0;


        //There is a bit of a glitch in the game. I haven't fixed it yet. Let's see if you can find it. It honestly, could be considered a bit of a feature, a possible lucky 2nd chance if you will.

        public MainWindow()
        {
            InitializeComponent();
            Start();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a timer to listen for different actions inside here:
            DispatcherTimer Listener = new DispatcherTimer();
            Listener.Interval = TimeSpan.FromMilliseconds(33);
            Listener.Tick += ListenerTicker;
            Listener.Start();
        }

        private void Start()
        {
          
            StartingScreen.Visibility = Visibility.Visible;

            Random InitalX = new Random();
            Random InitalY = new Random();
            BallXmove = InitalX.Next(-4, -4);
            while (BallXmove == 0)
            {
                BallXmove = InitalX.Next(-4, 4);
            }
            BallYmove = InitalY.Next(-4, 4);
            while (BallYmove == 0)
            {
                BallYmove = InitalY.Next(-4, 4);
            }

            CanvasTopLeft.Visibility = Visibility.Hidden;
            lblPlayer1Name.Visibility = Visibility.Hidden;
            lblPlayer2Name.Visibility = Visibility.Hidden;
            LblP1Score.Visibility = Visibility.Hidden;
            LblP2Score.Visibility = Visibility.Hidden;
            btnPauseGame.Visibility = Visibility.Hidden;
            lblpausenote.Visibility = Visibility.Hidden;


        }
        // when the key goes up, which will stop rectangle movement
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Gamemode == "1P")
            {
                if (e.Key == Key.Up)
                {
                    LeftRecMovement = false;
                    Leftupkeydown = false;
                    // RightRecMovement = false;

                }
                if (e.Key == Key.Down)
                {
                    LeftRecMovement = false;
                    Leftdownkeydown = false;
                    // RightRecMovement = true;
                }
            }
            else if (Gamemode == "2P")
            {
                if (e.Key == Key.W)
                {
                    LeftRecMovement = false;
                    Leftupkeydown = false;
                    // RightRecMovement = false;

                }
                if (e.Key == Key.S)
                {
                    LeftRecMovement = false;
                    Leftdownkeydown = false;
                    // RightRecMovement = true;
                }
                if (e.Key == Key.Up)
                {
                    RightRecMovement = false;
                    Rightupkeydown = false;
                    // RightRecMovement = false;

                }
                if (e.Key == Key.Down)
                {
                    RightRecMovement = false;
                    Rightdownkeydown = false;
                    // RightRecMovement = true;
                }
            }
           

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Gamemode == "1P" )
            {
                if (IsPaused == false)
                {
                    if (e.Key == Key.Up)
                    {
                        LeftRecMovement = true;
                        LeftMovement = "up";
                        Leftupkeydown = true;
                        // RightRecMovement = false;   move to ws keys

                    }
                    if (e.Key == Key.Down)
                    {
                        LeftRecMovement = true;
                        LeftMovement = "down";
                        Leftdownkeydown = true;
                        //RightRecMovement = false;
                    }
                }
            }
            else if (Gamemode == "2P")
            {
                if (IsPaused == false)
                {
                    if (e.Key == Key.W)
                    {
                        LeftRecMovement = true;
                        LeftMovement = "up";
                        Leftupkeydown = true;
                    }
                    if (e.Key == Key.S)
                    {
                        LeftRecMovement = true;
                        LeftMovement = "down";
                        Leftdownkeydown = true;
                    }
                }
                if (IsPaused == false)
                {
                    if (e.Key == Key.Up)
                    {
                        RightRecMovement = true;
                        RightMovement = "up";
                        Rightupkeydown = true;
                    }
                    if (e.Key == Key.Down)
                    {
                        RightRecMovement = true;
                        RightMovement = "down";
                        Rightdownkeydown = true;
                    }
                }
            }
            if (e.Key == Key.Space)
            {
                WinningScreen.Visibility = Visibility.Hidden;
                if (startingnewround == true)
                {
                    BallMovement = true;
                    LeftRecMovement = true;
                    RightRecMovement = true;
                    IsPaused = false;
                    RESET();
                }
                else
                {
                    LeftRecMovement = !LeftRecMovement;
                    RightRecMovement = !RightRecMovement;
                    BallMovement = !BallMovement;
                    IsPaused = !IsPaused;
                }
                startingnewround = false;

            }
        }
      

       
        private void ListenerTicker(object sender, EventArgs e)
        {
            LocCheck();
           
            if (alterspeedcounter == 200)
            {
                if (IsPaused == false)
                {
                      alterspeed = alterspeed + 0.5;
                }
                alterspeedcounter = 0;
            }
            alterspeedcounter++;
            if (Gamemode == "1P")
            {
                if (LeftRecMovement == true && LeftMovement == "up" && LRY < 340 && Leftupkeydown == true)
                {
                    LRY = LRY + 20;
                    RectangleLeft.Margin = new Thickness(50, 0, 0, LRY);
                }
                if (LeftRecMovement == true && LeftMovement == "down" && LRY > -340 && Leftdownkeydown == true)
                {
                    LRY = LRY - 20;
                    RectangleLeft.Margin = new Thickness(50, 0, 0, LRY);
                }
            }
            else if (Gamemode == "2P")
            {
                if (LeftRecMovement == true && LeftMovement == "up" && LRY < 340 && Leftupkeydown == true)
                {
                    LRY = LRY + 20;
                    RectangleLeft.Margin = new Thickness(50, 0, 0, LRY);
                }
                if (LeftRecMovement == true && LeftMovement == "down" && LRY > -340 && Leftdownkeydown == true)
                {
                    LRY = LRY - 20;
                    RectangleLeft.Margin = new Thickness(50, 0, 0, LRY);
                }
                if (RightRecMovement == true && RightMovement == "up" && RRY < 340 && Rightupkeydown == true)
                {
                    RRY = RRY + 20;
                    RectangleRight.Margin = new Thickness(0, 0, 50, RRY);
                }
                if (RightRecMovement == true && RightMovement == "down" && RRY > -340 && Rightdownkeydown == true)
                {
                    RRY = RRY - 20;
                    RectangleRight.Margin = new Thickness(0, 0, 50, RRY);
                }
            }
            if (BallMovement == true)
            {
                
                BallX = BallX + (BallXmove * alterspeed);
                BallY = BallY + (BallYmove * alterspeed);
                Ball.Margin = new Thickness(BallX, 0, 0, BallY);
                //left, top, right bottom       
            }
        }


    

        public void LocCheck()
        {
            //Bottom
            if (BallY <= -420)
            {
                BallYmove = -BallYmove;
            }
            //TOP
            if (BallY >= 420)
            {
                BallYmove = -BallYmove;
            }
            //LeftPaddle
            if (BallX <= -640 && (BallY <= LRY + 100 && BallY >= LRY - 100 ))
            {
                if (leftrecmoving == "NOT")
                {
                    BallXmove = -BallXmove;
                }
                if (leftrecmoving == "UP")
                {
                    if (ballmoving == "UP")
                    {
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                    if (ballmoving == "DOWN")
                    {
                        BallXmove = -(BallXmove + (BallYmove / 2));
                        BallYmove = -BallYmove;
                    }
                    else
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove + (BallYmove / 2);
                    }
                }
                if (leftrecmoving == "DOWN")
                {
                    if (ballmoving == "UP")
                    {
                        BallXmove = (BallXmove + (BallYmove / 2));
                        BallYmove = -BallYmove;
                    }
                    if (ballmoving == "DOWN")
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                    else
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                }
            }
            //RightPaddle
            if (BallX >= 640 && (BallY <= RRY + 100 && BallY >= RRY - 100))
            {
                if (rightrecmoving == "NOT")
                {
                    BallXmove = -BallXmove;
                }
                if (rightrecmoving == "UP")
                {
                    if (ballmoving == "UP")
                    {
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                    if (ballmoving == "DOWN")
                    {
                        BallXmove = -(BallXmove + (BallYmove / 2));
                        BallYmove = -BallYmove;
                    }
                    else
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove + (BallYmove / 2);
                    }
                }
                if (rightrecmoving == "DOWN")
                {
                    if (ballmoving == "UP")
                    {
                        BallXmove = (BallXmove+ (BallYmove /2));
                        BallYmove = -BallYmove;

                    }
                    if (ballmoving == "DOWN")
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                    else
                    {
                        BallXmove = -BallXmove;
                        BallYmove = BallYmove - (BallYmove / 2);
                    }
                }
            }
            //Winner is P1
            if (BallX >= 765)
            {
                lblWinner.Content = "The Winner of this Round is Player 1!";
                roundwinner = "P1";
                BallMovement = false;
                WhoWon();
            }
            //Winner is P2
            if (BallX <= -765)
            {
                lblWinner.Content = "The Winner of this Round is Player 2!";
                roundwinner = "P2";
                BallMovement = false;
                WhoWon();
            }

            //counters to determine if movement is occuring    

            leftmovementcounter++;
            rightmovementcounter++;
            ballmovementcounter++;
            if (leftmovementcounter == 3)
            {
                leftfirstcheck = LRY;
            }
            if (leftmovementcounter == 6)
            {
                leftsecondcheck = LRY;
            }
            if (rightmovementcounter == 3)
            {
                rightfirstcheck = RRY;
            }
            if (rightmovementcounter == 6)
            {
                rightsecondcheck = RRY;
            }
            if (ballmovementcounter == 3)
            {
                ballfirstcheck = BallY;
            }
            if (ballmovementcounter == 6)
            {
                ballsecondcheck = BallY;
            }
            // checking to see if we are moving upwards or downwards
            if (leftmovementcounter == 6)
            {
                if (leftfirstcheck < leftsecondcheck)
                {
                    leftrecmoving = "UP";
                    //LblP1Score.Content = "Moving up";
                }
                if (leftfirstcheck > leftsecondcheck)
                {
                    leftrecmoving = "DOWN";
                    //LblP1Score.Content = "Moving down";
                }
                if (leftfirstcheck == leftsecondcheck)
                {
                    leftrecmoving = "NOT";
                    //LblP1Score.Content = "Not Moving";
                }
            }
            if (rightmovementcounter == 6)
            {
                if (rightfirstcheck < rightsecondcheck)
                {
                    rightrecmoving = "UP";
                    //LblP2Score.Content = "Moving up";
                }
                if (rightfirstcheck > rightsecondcheck)
                {
                    rightrecmoving = "DOWN";
                    //LblP2Score.Content = "Moving down";
                }
                if (rightfirstcheck == rightsecondcheck)
                {
                    rightrecmoving = "NOT";
                    //LblP2Score.Content = "Not Moving";
                }
            }
            if (ballmovementcounter == 6)
            {
                if (ballfirstcheck < ballsecondcheck)
                {
                    ballmoving = "UP";
                    //LblP2Score.Content = "Moving up";
                }
                if (ballfirstcheck > ballsecondcheck)
                {
                    ballmoving = "DOWN";
                    //LblP2Score.Content = "Moving down";
                }
            }
            //reset movement counter
            if (leftmovementcounter == 6)
            {
                leftmovementcounter = 0;
            }
            if (rightmovementcounter == 6)
            {
                rightmovementcounter = 0;
            }
            if (ballmovementcounter == 6)
            {
                ballmovementcounter = 0;
            }
        }

        private void WhoWon()
        {
            IsPaused = true;
            if (roundwinner == "P1")
            {
                P1Score++;
                LblP1Score.Content = P1Score;
            }
            else if (roundwinner == "P2")
            {
                P2Score++;
                LblP2Score.Content = P2Score;
            }
            WinningScreen.Visibility = Visibility.Visible;
            roundwinner = "";
            RESET();
        }
        /*
        private void btnNewRound_Click(object sender, RoutedEventArgs e)
        {
            WinningScreen.Visibility = Visibility.Hidden;
            BallMovement = true;
            LeftRecMovement = true;
            RightRecMovement = true;
            RESET();
        }*/
        private void btnPauseGame_Click(object sender, RoutedEventArgs e)
        {
            WinningScreen.Visibility = Visibility.Hidden;
            if (startingnewround == true)
            {
                BallMovement = true;
                LeftRecMovement = true;
                RightRecMovement = true;
                IsPaused = false;
                RESET();
            }
            else
            {
                LeftRecMovement = !LeftRecMovement;
                RightRecMovement = !RightRecMovement;
                BallMovement = !BallMovement;
                IsPaused = !IsPaused;
            }
            startingnewround = false;

        }

     
        private void btnLeaderboards_Click(object sender, RoutedEventArgs e)
        {
            lblLeaderboard.Visibility = Visibility.Visible;
        }

        private void btnBoring_Click(object sender, RoutedEventArgs e)
        {
            Theme = "Boring";
            MakeVisible();
            RectangleLeft.Fill = Brushes.Black;
            RectangleRight.Fill = Brushes.Black;
            DecorationCenterRec.Fill = Brushes.LightSkyBlue;
            DecorationRightRec.Fill = Brushes.LightSkyBlue;
            DecorationLeftrRec.Fill = Brushes.LightSkyBlue;
            DecorationRightRec.Fill = Brushes.LightSkyBlue;
            DecorationOuterCircle.Fill = Brushes.LightSkyBlue;
            DecorationInnerCircle.Fill = Brushes.White;
            Ball.Fill = Brushes.Black;

        }
        //Took a while but I figured out how to do pictures. This is possibly the site that helped the most:
        //https://stackoverflow.com/questions/22576588/invalid-uri-the-format-of-the-uri-could-not-be-determined
        //GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/RedVsBlue.jpg", UriKind.Absolute)));
        private void btnRedVsBlue_Click(object sender, RoutedEventArgs e)
        {
            Theme = "RedVsBlue";
            MakeVisible();
            GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/RedVsBlue.jpg", UriKind.Absolute)));
            RectangleLeft.Fill = Brushes.White;                 // Left Paddle
            RectangleRight.Fill = Brushes.White;                // Right Paddle
            DecorationCenterRec.Fill = Brushes.White;           // Center Decorative Rectangle
            DecorationLeftrRec.Fill = Brushes.DarkRed;          // Decoratvie Left Rectangle
            DecorationRightRec.Fill = Brushes.MidnightBlue;     // Decorative Right Rectangle
            DecorationOuterCircle.Fill = Brushes.White;         // Decoratvie Outer Circle
            DecorationInnerCircle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/RedVsBlue.jpg", UriKind.Absolute)));         // Decorative InnerCircle
            Ball.Fill = Brushes.White;                          // Ball
        }
        private void btnSoccer_Click(object sender, RoutedEventArgs e)
        {
            Theme = "Soccer";
            MakeVisible();
            GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SoccerBackGround.jpg", UriKind.Absolute)));
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            Theme = "Space";
            MakeVisible();
            GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Space.jpg", UriKind.Absolute)));
            RectangleLeft.Fill = Brushes.White;                 // Left Paddle
            RectangleRight.Fill = Brushes.White;                // Right Paddle
            DecorationCenterRec.Fill = Brushes.White;           // Center Decorative Rectangle
            DecorationLeftrRec.Fill = Brushes.White;          // Decoratvie Left Rectangle
            DecorationRightRec.Fill = Brushes.White;     // Decorative Right Rectangle
            DecorationOuterCircle.Fill = DecorationInnerCircle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Earth.jpg", UriKind.Absolute)));         // Decoratvie Outer Circle
            Ball.Fill = Brushes.White;                          // Ball
        }

        private void btnWizard_Click(object sender, RoutedEventArgs e)
        {
            Theme = "Wizard";
            MakeVisible();
            GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Wizard Field.jpg", UriKind.Absolute)));
            RectangleLeft.Fill = Brushes.White;                 // Left Paddle
            RectangleRight.Fill = Brushes.White;                // Right Paddle
            DecorationCenterRec.Fill = Brushes.White;           // Center Decorative Rectangle
            DecorationLeftrRec.Fill = Brushes.White;          // Decoratvie Left Rectangle
            DecorationRightRec.Fill = Brushes.White;     // Decorative Right Rectangle
            DecorationOuterCircle.Fill = Brushes.White;         // Decoratvie Outer Circle
            DecorationInnerCircle.Fill = Brushes.White;         // Decorative InnerCircle
            Ball.Fill = Brushes.White;                          // Ball
        }
        private void MakeVisible()
        {
            StartingScreen.Visibility = Visibility.Hidden;
            CanvasTopLeft.Visibility = Visibility.Visible;
            lblPlayer1Name.Visibility = Visibility.Visible;
            lblPlayer2Name.Visibility = Visibility.Visible;
            LblP1Score.Visibility = Visibility.Visible;
            LblP2Score.Visibility = Visibility.Visible;
            btnPauseGame.Visibility = Visibility.Visible;
            lblpausenote.Visibility = Visibility.Visible;
            if (Theme == "Space")
            {
                DecorationInnerCircle.Visibility = Visibility.Hidden;
            }
            else
            {
                DecorationInnerCircle.Visibility = Visibility.Visible;
            }
            if (Theme == "Wizard")
            {
                //Pics visible
            }
            else
            {
                //Pics not visible
            }
        }

        private void btnNGMouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
        }
        private void btnNGMouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
        }
        private void btnNG1MouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
        }
        private void btnNG1MouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
        }
        private void btnNG2MouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
        }
        private void btnNG2MouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
        }
        private void btnVOMouseEnter(object sender, MouseEventArgs e)
        {
            btnChangeTheme.Visibility = Visibility.Visible;
        }
        private void btnVOMouseLeave(object sender, MouseEventArgs e)
        {
            btnChangeTheme.Visibility = Visibility.Hidden;
        }
        private void btnCTMouseEnter(object sender, MouseEventArgs e)
        {
            btnChangeTheme.Visibility = Visibility.Visible;
        }
        private void btnCTMouseLeave(object sender, MouseEventArgs e)
        {
            btnChangeTheme.Visibility = Visibility.Hidden;
        }


        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            lblLeaderboard.Visibility = Visibility.Hidden;

        }
        

        private void RESET()
        {
            startingnewround = true;
            alterspeed = 3;
            BallX = 0;
            BallY = 0;
            Ball.Margin = new Thickness(BallX, 0, 0, BallY);
            LRY = 50;
            RRY = 50;
            RectangleLeft.Margin = new Thickness(50,0,0,0);
            RectangleRight.Margin = new Thickness(0, 0, 50, 0);
        }

    }

}//60 frames per second
