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
        public string LeftMovement = "", RightMovement = "", roundwinner = "", gamewinner = "", P1Name = "Player 1", P2Name = "Player 2";
        public double LRY = 0, RRY = 0, LPBY = 0, RPBY = 0;
        public int BallXmove, BallYmove, P1Score = 0, P2Score = 0;
        public string Gamemode = "2P";
        public string leftrecmoving = "", rightrecmoving = "", ballmoving = "";
        public int leftmovementcounter = 0, rightmovementcounter = 0, ballmovementcounter = 0;
        public double leftfirstcheck = 0, leftsecondcheck = 0, rightfirstcheck = 0, rightsecondcheck = 0;
        public bool startingnewround = true, gameover = false;
        public string Theme = "", GoalLimitEntry = "";
        bool hitsleft = false, hitsright = false, GoalLimitNum = true;
        public int alterspeedcounter = 0, GoalLimit = 10, cpucounter = 0, CPUcounter = 0;
        public double alterspeed = 12, BallX = 0, BallY = 0, ballfirstcheck = 0, ballsecondcheck = 0;
        public double CPUY = 0, tempbally, CPUgohere;
      
        bool radiochecked = false, namesentered = false, startinggood = false;

        

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
            BallXmove = InitalX.Next(-2, 2);
            while (BallXmove == 0)
            {
                BallXmove = InitalX.Next(-2, 2);
            }
            BallYmove = InitalY.Next(-2, 2);
            while (BallYmove == 0)
            {
                BallYmove = InitalY.Next(2, 2);
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

                }
                if (e.Key == Key.Down)
                {
                    LeftRecMovement = false;
                    Leftdownkeydown = false;
                }
                if (e.Key == Key.W)
                {
                    LeftRecMovement = false;
                    Leftupkeydown = false;
                }
                if (e.Key == Key.S)
                {
                    LeftRecMovement = false;
                    Leftdownkeydown = false;
                }
            }
            else if (Gamemode == "2P")
            {
                if (e.Key == Key.W)
                {
                    LeftRecMovement = false;
                    Leftupkeydown = false;
                }
                if (e.Key == Key.S)
                {
                    LeftRecMovement = false;
                    Leftdownkeydown = false;
                }
                if (e.Key == Key.Up)
                {
                    RightRecMovement = false;
                    Rightupkeydown = false;

                }
                if (e.Key == Key.Down)
                {
                    RightRecMovement = false;
                    Rightdownkeydown = false;
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
                    }
                    if (e.Key == Key.Down)
                    {
                        LeftRecMovement = true;
                        LeftMovement = "down";
                        Leftdownkeydown = true;
                    }
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
        public void StartingScreenCheck()
        {
            // Checks the input for Score limit and sets a BoolValue.
            GoalLimitNum = int.TryParse((TxtScoreLimit.Text), out GoalLimit);
            lblScoreLimitOutput.Content = GoalLimit;
            
            //Checks that a radiobutton is clicked and then returns a bool.
            if (Radio2P.IsChecked == true || RadioSingleP.IsChecked == true)
            {
                radiochecked = true;
            }
            if (Radio2P.IsChecked == true)
            {
                P1Name = TxtP1Name.Text;
                P2Name = TxtP2Name.Text;
            }
            if (RadioSingleP.IsChecked == true)
            {
                P1Name = TxtP1Name.Text;
                P2Name = "Computer";
            }

            // Checks the name input(s) and returns a bool
            if (P1Name == "" || P2Name == "")
            {
                namesentered = false;
            }
            else
            {
                namesentered = true;
            }
            if (GoalLimitNum == true && radiochecked == true && namesentered == true)
            {
                startinggood = true;
            }
            TB1output.Text = P1Name;
            TBP2output.Text = P2Name;
        }
  
        private void ListenerTicker(object sender, EventArgs e)
        {
            LocCheck();
           
            if (alterspeedcounter == 150)
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

                Random CPURand = new Random();
                CPUY = CPURand.Next(-30, 30);

               
                if (RightRecMovement == true)
                {
                    cpucounter++;
                    if (cpucounter == 15)
                    {
                        tempbally = BallY;
                        cpucounter = 0;
                    }


                    CPUY = CPUY + tempbally;

                    if (RRY < CPUY && ballmoving == "UP")
                    {
                        RRY = RRY + 20;
                    }
                    if (RRY > CPUY && ballmoving == "DOWN")
                    {
                        RRY = RRY - 20;
                    }
                    if (RRY < 300 || RRY > -300)
                    {
                     RectangleRight.Margin = new Thickness(0, 0, 50, RRY);
                    }

                    //LocateCPU
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
            if (Theme == "Wizard")
            {
                LPBY = LRY;
                RPBY = RRY;
                LeftImage.Margin = new Thickness(10, 0, 0, LPBY);
                RightImage.Margin = new Thickness(0, 0, 25, RPBY);
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
            ThemeChanges();
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
            if (BallX <= -640 && BallY <= LRY + 100 && BallY >= LRY - 100 )
            {
                hitsright = false;
                hitsleft = true;
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
            if (BallX >= 640 && BallY <= RRY + 100 && BallY >= RRY - 100)
            {
                hitsleft = false;
                hitsright = true;
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
                roundwinner = "P1";
                BallMovement = false;
                WhoWon();
            }
            //Winner is P2
            if (BallX <= -765)
            {
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
            LeftRecMovement = false;
            RightRecMovement = false;
            if (roundwinner == "P1")
            {
                P1Score++;
            }
            else if (roundwinner == "P2")
            {
                P2Score++;
            }
            if (P1Score == GoalLimit || P2Score == GoalLimit)
            {
                gameover = true;
            }
            else
            {
                gameover = false;
            }
            if (gameover == true)
            {
                btnNewRound.Content = "Click the pause button below to start the next game.";
                if (roundwinner == "P1")
                {
                    lblWinner.Text = $"The Winner of the is {P1Name}. Congratulations!";
                    LblP1Score.Content = P1Score;
                }
                else if (roundwinner == "P2")
                {
                    lblWinner.Text = $"The Winner of the game is {P2Name}. Congratulations!";
                    LblP2Score.Content = P2Score;
                }
            }
            else
            {
                btnNewRound.Content = "Click the pause button below to start the next round.";
                if (roundwinner == "P1")
                {
                    lblWinner.Text = $"The Winner of this Round is {P1Name}";
                    LblP1Score.Content = P1Score;
                }
                else if (roundwinner == "P2")
                {
                    lblWinner.Text = $"The Winner of this Round is {P2Name}";
                    LblP2Score.Content = P2Score;
                }
            }
           
            WinningScreen.Visibility = Visibility.Visible;
            roundwinner = "";
            RESET();
        }


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
        private void btnBoring_Click(object sender, RoutedEventArgs e)
        {
            StartingScreenCheck();
            if (startinggood == true)
            {
                Theme = "Boring";
                MakeVisible();
                GameGrid.Background = Brushes.White;
                RectangleLeft.Fill = Brushes.Black;
                RectangleRight.Fill = Brushes.Black;
                DecorationCenterRec.Fill = Brushes.LightSkyBlue;
                DecorationRightRec.Fill = Brushes.LightSkyBlue;
                DecorationLeftrRec.Fill = Brushes.LightSkyBlue;
                DecorationRightRec.Fill = Brushes.LightSkyBlue;
                DecorationOuterCircle.Fill = Brushes.LightSkyBlue;
                DecorationInnerCircle.Fill = Brushes.White;
                Ball.Fill = Brushes.Black;
                CanvasChangeTheme.Visibility = Visibility.Hidden;
                lblWinner.Foreground = Brushes.Black;
            }
            if (GoalLimitNum == false)
            {
                lblGoalLimitError.Visibility = Visibility.Visible;
            }
            else
            {
                lblGoalLimitError.Visibility = Visibility.Hidden;
            }
            if (radiochecked == false)
            {
                lblRadioError.Visibility = Visibility.Visible;
            }
            else
            {
                lblRadioError.Visibility = Visibility.Hidden;
            }
            if (namesentered == false)
            {
                if (radiochecked == true)
                {
                   lblNameError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblNameError.Visibility = Visibility.Hidden;
            }

        }
        //Took a while but I figured out how to do pictures. This is possibly the site that helped the most:
        //https://stackoverflow.com/questions/22576588/invalid-uri-the-format-of-the-uri-could-not-be-determined
        //GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/RedVsBlue.jpg", UriKind.Absolute)));
        private void btnRedVsBlue_Click(object sender, RoutedEventArgs e)
        {
            StartingScreenCheck();
            if (startinggood == true)
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
                lblWinner.Foreground = Brushes.White;
                CanvasChangeTheme.Visibility = Visibility.Hidden;
            }
            if (GoalLimitNum == false)
            {
                lblGoalLimitError.Visibility = Visibility.Visible;
            }
            else
            {
                lblGoalLimitError.Visibility = Visibility.Hidden;
            }
            if (radiochecked == false)
            {
                lblRadioError.Visibility = Visibility.Visible;
            }
            else
            {
                lblRadioError.Visibility = Visibility.Hidden;
            }
                      if (namesentered == false)
            {
                if (radiochecked == true)
                {
                    lblNameError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblNameError.Visibility = Visibility.Hidden;
            }
        }
        private void btnSoccer_Click(object sender, RoutedEventArgs e)
        {
            StartingScreenCheck();
            if (startinggood == true)
            {
                Theme = "Soccer";
                MakeVisible();
                GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SoccerBackGround.jpg", UriKind.Absolute)));
                RectangleLeft.Fill = Brushes.White;                 // Left Paddle
                RectangleRight.Fill = Brushes.White;                // Right Paddle
                DecorationCenterRec.Fill = Brushes.White;           // Center Decorative Rectangle
                DecorationLeftrRec.Fill = Brushes.White;          // Decoratvie Left Rectangle
                DecorationRightRec.Fill = Brushes.White;     // Decorative Right Rectangle
                DecorationOuterCircle.Fill = Brushes.White;         // Decoratvie Outer Circle
                DecorationInnerCircle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SoccerBackGround.jpg", UriKind.Absolute)));         // Decorative InnerCircle
                Ball.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SoccerBall.png", UriKind.Absolute)));
                CanvasChangeTheme.Visibility = Visibility.Hidden;// Ball
                lblWinner.Foreground = Brushes.White;
            }
            if (GoalLimitNum == false)
            {
                lblGoalLimitError.Visibility = Visibility.Visible;
            }
            else
            {
                lblGoalLimitError.Visibility = Visibility.Hidden;
            }
            if (radiochecked == false)
            {
                lblRadioError.Visibility = Visibility.Visible;
            }
            else
            {
                lblRadioError.Visibility = Visibility.Hidden;
            }
            if (namesentered == false)
            {
                if (radiochecked == true)
                {
                    lblNameError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblNameError.Visibility = Visibility.Hidden;
            }
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            StartingScreenCheck();
            if (startinggood == true)
            {
                Theme = "Space";
                MakeVisible();
                GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Space.jpg", UriKind.Absolute)));
                RectangleLeft.Fill = Brushes.White;                 // Left Paddle
                RectangleRight.Fill = Brushes.White;                // Right Paddle
                DecorationCenterRec.Fill = Brushes.White;           // Center Decorative Rectangle
                DecorationLeftrRec.Fill = Brushes.Black;          // Decoratvie Left Rectangle
                DecorationRightRec.Fill = Brushes.Black;     // Decorative Right Rectangle
                DecorationOuterCircle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Earth.jpg", UriKind.Absolute)));         // Decoratvie Outer Circle
                Ball.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Astroid.png", UriKind.Absolute)));        //Ball
                CanvasChangeTheme.Visibility = Visibility.Hidden;
                lblWinner.Foreground = Brushes.White;
            }
            if (GoalLimitNum == false)
            {
                lblGoalLimitError.Visibility = Visibility.Visible;
            }
            else
            {
                lblGoalLimitError.Visibility = Visibility.Hidden;
            }
            if (radiochecked == false)
            {
                lblRadioError.Visibility = Visibility.Visible;
            }
            else
            {
                lblRadioError.Visibility = Visibility.Hidden;
            }
            if (namesentered == false)
            {
                if (radiochecked == true)
                {
                    lblNameError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblNameError.Visibility = Visibility.Hidden;
            }

        }

        private void btnWizard_Click(object sender, RoutedEventArgs e)
        {
            StartingScreenCheck();
            if (startinggood == true)
            {
                Theme = "Wizard";
                MakeVisible();
                GameGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/Wizard Field.jpg", UriKind.Absolute)));
                RectangleLeft.Fill = Brushes.White;                 // Left Paddle
                RectangleRight.Fill = Brushes.White;                // Right Paddle
                DecorationCenterRec.Fill = Brushes.LightSlateGray;           // Center Decorative Rectangle
                DecorationLeftrRec.Fill = Brushes.LightSlateGray;          // Decoratvie Left Rectangle
                DecorationRightRec.Fill = Brushes.LightSlateGray;     // Decorative Right Rectangle
                DecorationOuterCircle.Fill = Brushes.LightSlateGray;         // Decoratvie Outer Circle
                DecorationInnerCircle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/MagicCircle.png", UriKind.Absolute)));         // Decorative InnerCircle
                Ball.Fill = Brushes.White;                          // Ball
                LeftImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/WizardLeft2.png", UriKind.Absolute));
                RightImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/WizardRight.png", UriKind.Absolute));
                CanvasChangeTheme.Visibility = Visibility.Hidden;
                lblWinner.Foreground = Brushes.White;
            }
            if (GoalLimitNum == false)
            {
                lblGoalLimitError.Visibility = Visibility.Visible;
            }
            else
            {
                lblGoalLimitError.Visibility = Visibility.Hidden;
            }
            if (radiochecked == false)
            {
                lblRadioError.Visibility = Visibility.Visible;
            }
            else
            {
                lblRadioError.Visibility = Visibility.Hidden;
            }
            if (namesentered == false)
            {
                if (radiochecked == true)
                {
                    lblNameError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lblNameError.Visibility = Visibility.Hidden;
            }
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
                DecorationInnerCircle2.Visibility = Visibility.Visible;
                LeftImage.Visibility = Visibility.Visible;
                RightImage.Visibility = Visibility.Visible;
               // Random Color = new Random
            }
            else
            {
                LeftImage.Visibility = Visibility.Hidden;
                RightImage.Visibility = Visibility.Hidden;
                DecorationInnerCircle2.Visibility = Visibility.Hidden;
            }
            if (Theme == "RedVsBlue")
            {
                Ball.Stroke = Brushes.White;
            }
            else
            {
                Ball.Stroke = null;
            }
        }
        public void ThemeChanges()
        {
            if (Theme == "Wizard")
            {
                if (hitsleft == true)
                {
                    Ball.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SpellRight2.png", UriKind.Absolute)));
                }
                if (hitsright == true)
                {
                    Ball.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/SpellLeft.png", UriKind.Absolute)));
                }
            }
            if (Theme == "RedVsBlue")
            {
                if (BallX > 0)
                {
                    Ball.Fill = Brushes.Blue;
                }
                if (BallX < 0)
                {
                    Ball.Fill = Brushes.Red;
                }


            }
        }
        public bool intobutton = false, exitsthemecanvas = false;
        public string visible = "makevisible";
        private void btnChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            if (visible == "makevisible")
            {
                CanvasChangeTheme.Visibility = Visibility.Visible;
                btnChangeTheme.Visibility = Visibility.Visible;
                visible = "makeinvisible";
            }
            else if (visible == "makeinvisible")
            {
                CanvasChangeTheme.Visibility = Visibility.Hidden;
                btnChangeTheme.Visibility = Visibility.Hidden;
                visible = "makevisible";
            }
        }

        private void btnNGMouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
            btnMainScreen.Visibility = Visibility.Visible;
        }
        private void btnNGMouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
            btnMainScreen.Visibility = Visibility.Hidden;
        }
        private void btnNG1MouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
            btnMainScreen.Visibility = Visibility.Visible;
        }
        private void btnNG1MouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
            btnMainScreen.Visibility = Visibility.Hidden;
        }
        private void btnNG2MouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
            btnMainScreen.Visibility = Visibility.Visible;
        }

        private void btnNG2MouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
            btnMainScreen.Visibility = Visibility.Hidden;
        }
        private void btnMainScreen_MouseLeave(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Hidden;
            btnNewGameSinglePlayer.Visibility = Visibility.Hidden;
            btnMainScreen.Visibility = Visibility.Hidden;
        }

        private void btnMainScreen_MouseEnter(object sender, MouseEventArgs e)
        {
            btnNewGame2Player.Visibility = Visibility.Visible;
            btnNewGameSinglePlayer.Visibility = Visibility.Visible;
            btnMainScreen.Visibility = Visibility.Visible;
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
        private void btnChangeTheme_MouseLeave(object sender, MouseEventArgs e)
        {
            btnChangeTheme.Visibility = Visibility.Hidden;
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            lblLeaderboard.Visibility = Visibility.Hidden;
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            RESET();
            StartingScreen.Visibility = Visibility.Visible;
        }

        private void btnLeaderboards_Click(object sender, RoutedEventArgs e)
        {
            lblLeaderboard.Visibility = Visibility.Visible;
        }

        private void RadioSingleP_Checked(object sender, RoutedEventArgs e)
        {
            Gamemode = "1P";
            RadioSingleP.IsChecked = true;
            Radio2P.IsChecked = false;
            lblP1Name.Visibility = Visibility.Visible;
            TxtP1Name.Visibility = Visibility.Visible;
            lblP2Name.Visibility = Visibility.Hidden;
            TxtP2Name.Visibility = Visibility.Hidden;
        }

        private void Radio2P_Checked(object sender, RoutedEventArgs e)
        {
            Radio2P.IsChecked = true;
            RadioSingleP.IsChecked = false;
            Gamemode = "2P";
            lblP1Name.Visibility = Visibility.Visible;
            TxtP1Name.Visibility = Visibility.Visible;
            lblP2Name.Visibility = Visibility.Visible;
            TxtP2Name.Visibility = Visibility.Visible;
        }

        private void btnNewGame2Player_Click(object sender, RoutedEventArgs e)
        {
            gameover = true;
            Gamemode = "2P";
            if (TBP2output.Text != "Player 2")
            {
                TBP2output.Text = "Player 2";
            }
            RESET();
        }

        private void btnNewGameSinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            gameover = true;
            Gamemode = "1P";
            RESET();
        }

        private void RESET()
        {
            if (gameover == true)
            {
                P1Score = 0;
                P2Score = 0;
                LblP1Score.Content = 0;
                LblP2Score.Content = 0;
                gameover = false;
            }
            startingnewround = true;
            alterspeed = 12;
            BallX = 0;
            BallY = 0;
            Ball.Margin = new Thickness(BallX, 0, 0, BallY);
            LRY = 50;
            RRY = 50;
            RectangleLeft.Margin = new Thickness(50,0,0,0);
            RectangleRight.Margin = new Thickness(0, 0, 50, 0);
            LblP1Score.Content = 0;
            LblP2Score.Content = 0;
            P1Score = 0;
            P2Score = 0;
        }

    }

}//60 frames per second
