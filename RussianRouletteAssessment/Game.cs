﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Media;

/*
 * Todo:Highscore Board save after each game
 */
namespace RussianRouletteAssessment
{
    public partial class frm_Game : Form
    {
        //private variables
        //Graphics
        private Bitmap buffer;
        private Graphics GameGraphics;

        //Sounds
        SoundPlayer GameAudio;

        //Animations
        private Animations GameAnimations = new Animations();
        private Animation Anim_Intro = new Animation();
        private Animation Anim_LoadBullet = new Animation();
        private Animation Anim_SpinChamber = new Animation();
        private Animation Anim_PointDirection = new Animation();
        private Animation Anim_Fire = new Animation();
        private Animation Anim_AltFire = new Animation(); //used for when player is turning gun away
        private Animation Anim_Death = new Animation();
        private Animation Anim_Survive = new Animation();
        private Animation Anim_DeusExMachina = new Animation();

        //Gamestate
        private enum GameStates
        {
            Intro = 0,
            LoadBullet,//starts with bullet out, transition starts when user places bullet, ends when animation of bullet is done
            SpinChamber,//starts with Chamber out, transition is when user starts to close chamber and spins it, ends when Chamber is spun
            PointDirection,//Used to figure out where player wants to point gun
            Fire,//starts with finger on trigger, transitions when player activates the trigger, ends when firing animation is done
            Death,//starts with gun fired but whether or not player survived is indeterminate, transition is automatic after 1.5 seconds
                  //ends with players death, players relief, relief cause of act of god
            Survive,//starts with gun fired but whether or not player survived is indeterminate, transition is automatic after 1.5 seconds
                    //ends with players relief
            DeusExMachina//starts with gun fired but whether or not player survived is indeterminate, transition is automatic after 1.5 seconds
                         //ends with relief because of an act of god
        }
        private GameStates StateOfTheGame = GameStates.Intro;

        //Game Logic
        private bool BulletLoad = false;
        private bool NextDeath = false;
        private bool PointingAway = false; //has the user selected to point away
        private bool Hammer = false; //false = hammer isn't set, true = hammer is pulled back ready to fire
        private bool Triggered = false; //trigger is pulled
        private bool GameEnded = false;
        private bool GameWon = false;
        private bool DeusExMachina = false;
        private int[] Chambers = new int[6] { 0, 0, 0, 0, 0, 0 };
        private int Bullet = 0; //0=NoBullet,1=Bullet in chamber slot 1, 2=Bullet in chamber slot 2...
        private int CurrentChamber = 0; //first chamber, used to keep track of what chamber the hammer will hit when triggered
        private int PointAwayChances = 2; //two chances to point gun somewhere else

        //cheats
        private string[] GameCheats;
        private bool Cheating = false;
        private bool GodMode = false;
        private bool ExtraLife = false;
        private bool ExtraChance = false;

        //stats
        private int Game_Highscore;
        private int Game_CurrentScore;
        private int Game_TimesPlayed;
        private int Game_Deaths;
        private int Game_BulletsFired;
        private int Game_CloseCalls;
        private int Game_DeiExMachinas;
        private bool Game_Saved;

        //font
        Font GameTextFont;
        PrivateFontCollection pfc = new PrivateFontCollection();
        StringFormat sf = new StringFormat();

        //public
        public static bool NewGame = false; //if user sets this then Main form will start a new game

        //private methods
        private void PaintCanvas()
        {
            GameGraphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, pnl_canvas.Width, pnl_canvas.Height));
            DrawGameObjects(GameGraphics);
            pnl_canvas.CreateGraphics().DrawImageUnscaled(buffer, 0, 0);
        }

        private void DrawGameObjects(Graphics gameGraphics)
        {
            switch (StateOfTheGame)
            {
                case GameStates.Intro:
                    Anim_Intro.Advance(); // never stops
                    gameGraphics.DrawImage(Anim_Intro.CurrentImage(), 0, 0);
                    break;
                case GameStates.LoadBullet:
                    gameGraphics.DrawImage(Anim_LoadBullet.CurrentImage(), 0, 0);
                    break;
                case GameStates.SpinChamber:
                    gameGraphics.DrawImage(Anim_SpinChamber.CurrentImage(), 0, 0);
                    break;
                case GameStates.PointDirection:
                    if (PointingAway) Anim_PointDirection.Index = 1; //select second image
                    else Anim_PointDirection.Index = 0;
                    gameGraphics.DrawImage(Anim_PointDirection.CurrentImage(), 0, 0);
                    break;
                case GameStates.Fire:
                    if (PointingAway) gameGraphics.DrawImage(Anim_AltFire.CurrentImage(), 0, 0);
                    else gameGraphics.DrawImage(Anim_Fire.CurrentImage(), 0, 0);
                    break;
                case GameStates.Death:
                    gameGraphics.DrawImage(Anim_Death.CurrentImage(), 0, 0);
                    break;
                case GameStates.Survive:
                    gameGraphics.DrawImage(Anim_Survive.CurrentImage(), 0, 0);
                    break;
                case GameStates.DeusExMachina:
                    gameGraphics.DrawImage(Anim_DeusExMachina.CurrentImage(), 0, 0);
                    break;
                default:
                    break;
            }
            if (GameEnded)
            {
                if (GameWon)
                {
                    gameGraphics.DrawString("You've won, with " + Game_CurrentScore + " points", GameTextFont, Brushes.Black, 12.0f, pnl_canvas.Height - 25, sf);
                    if (DeusExMachina)
                    {
                        gameGraphics.DrawString("SOMEHOW?!?", GameTextFont, Brushes.Black, 12.0f, pnl_canvas.Height/2, sf);
                    }
                }
                else
                {
                    gameGraphics.DrawString("You've lost, with " + Game_CurrentScore + " points", GameTextFont, Brushes.Black, 12.0f, pnl_canvas.Height - 25, sf);
                }
                gameGraphics.DrawString("Play again?", GameTextFont, Brushes.Black, 400.0f, pnl_canvas.Height - 25, sf);
                gameGraphics.DrawString("Yes", GameTextFont, Brushes.Black, 520, pnl_canvas.Height - 25, sf);
                gameGraphics.DrawString("No", GameTextFont, Brushes.Black, 560, pnl_canvas.Height - 25, sf);
            }
        }

        public frm_Game()
        {
            try
            {
                //assembly = Assembly.GetExecutingAssembly();
                InitializeComponent();
                //apparently this sets up the form to do painting in the most simplest way
                //I understand OptimizedDoubleBuffer as I have used buffers before when creating animations in software
                //Not 100% sure about UserPaint and AllPaintinInWmPaint need to look these up
                //Although WmPaint sounds like the Windows Message event in C++ win32 painting
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

                buffer = new Bitmap(pnl_canvas.Width, pnl_canvas.Height);
                GameGraphics = Graphics.FromImage(buffer);

                //load in TestImages
                //GameAnimations.AddAnimation(Anim_Intro, new string[] { "RussianRouletteAssessment.TestImages.Intro.png" });
                //GameAnimations.AddAnimation(Anim_LoadBullet, new string[] { "RussianRouletteAssessment.TestImages.LoadBullet.png" });
                //GameAnimations.AddAnimation(Anim_SpinChamber, new string[] { "RussianRouletteAssessment.TestImages.SpinChamber.png" });
                //GameAnimations.AddAnimation(Anim_Fire, new string[] { "RussianRouletteAssessment.TestImages.Fire.png" });
                //GameAnimations.AddAnimation(Anim_Death, new string[] { "RussianRouletteAssessment.TestImages.Died.png" });
                //GameAnimations.AddAnimation(Anim_Survive, new string[] { "RussianRouletteAssessment.TestImages.Survived.png" });
                GameAnimations.AddAnimation(Anim_DeusExMachina, new string[] { "RussianRouletteAssessment.TestImages.SurvivedDeusExMachina.png" });
                //load in Real Images
                GameAnimations.AddAnimation(Anim_Intro, new string[] {  "RussianRouletteAssessment.IntroAnimation.Intro_1-01.png",
                                                                        "RussianRouletteAssessment.IntroAnimation.Intro_1-02.png",
                                                                        "RussianRouletteAssessment.IntroAnimation.Intro_1-03.png",
                                                                        "RussianRouletteAssessment.IntroAnimation.Intro_1-04.png",
                                                                        "RussianRouletteAssessment.IntroAnimation.Intro_1-05.png",
                                                                        "RussianRouletteAssessment.IntroAnimation.Intro_1-06.png" });

                GameAnimations.AddAnimation(Anim_LoadBullet, new string[] { "RussianRouletteAssessment.LoadBulletAnimation.LoadBullet_1-01.png",
                                                                            "RussianRouletteAssessment.LoadBulletAnimation.LoadBullet_1-02.png",
                                                                            "RussianRouletteAssessment.LoadBulletAnimation.LoadBullet_1-03.png"});

                GameAnimations.AddAnimation(Anim_SpinChamber, new string[] { "RussianRouletteAssessment.SpinChamberAnimation.SpinChamber_1-01.png" });

                GameAnimations.AddAnimation(Anim_PointDirection, new string[] { "RussianRouletteAssessment.PointDirectionAnimation.PointDirection_1-01.png",
                                                                                "RussianRouletteAssessment.PointDirectionAnimation.PointDirection_1-02.png"});

                GameAnimations.AddAnimation(Anim_Fire, new string[] {   "RussianRouletteAssessment.FireAnimation.Fire_1-01.png",
                                                                        "RussianRouletteAssessment.FireAnimation.Fire_1-02.png",
                                                                        "RussianRouletteAssessment.FireAnimation.Fire_1-03.png" });

                GameAnimations.AddAnimation(Anim_AltFire, new string[] {    "RussianRouletteAssessment.AltFireAnimation.Fire_2-01.png",
                                                                            "RussianRouletteAssessment.AltFireAnimation.Fire_2-02.png",
                                                                            "RussianRouletteAssessment.AltFireAnimation.Fire_2-03.png" });

                GameAnimations.AddAnimation(Anim_Survive, new string[] { "RussianRouletteAssessment.SurvivedAnimation.Survived_1-01.png" });

                GameAnimations.AddAnimation(Anim_Death, new string[] { "RussianRouletteAssessment.DeathAnimation.Died_1-01.png" });

                //stats
                Game_Highscore = 0;
                Game_CurrentScore = 0;
                Game_TimesPlayed = 0;
                Game_Deaths = 0;
                Game_BulletsFired = 0;
                Game_CloseCalls = 0;
                Game_DeiExMachinas = 0;

                //cheating off
                Cheating = false;

                //fonts
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                pfc.AddFontFile(Path.Combine(Application.StartupPath, "../../iosevka-regular.ttf"));
                GameTextFont = new Font(pfc.Families[0], 13, FontStyle.Regular);
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error: " + ex);
            }
        }

        public frm_Game(string[] cheats) : this()
        {
            GameCheats = cheats;
            Cheating = true;
        }

        private void frm_Game_Load(object sender, EventArgs e)
        {
            //Setup misc
            NewGame = false; // make sure this is turned off until user chooses to turn it on again
            this.Text = "Russian Roulette - Welcome " + frm_PlayerProfile.profileName;

            //reset variables
            Game_CurrentScore = 0;
            Bullet = 0;
            BulletLoad = false;
            Chambers = new int[6] { 0, 0, 0, 0, 0, 0 };
            CurrentChamber = 0;
            PointAwayChances = 2;
            PointingAway = false;
            Hammer = false;
            Triggered = false;
            Game_Saved = false;
            //cheats
            GodMode = false;
            ExtraLife = false;
            ExtraChance = false;

            GameAnimations.ResetAll();
            GameAnimations.SetAllLoop(false);
            StateOfTheGame = GameStates.Intro;

            //handle cheats
            if (Cheating)
            {
                foreach (string cheat in GameCheats)
                {
                    switch (cheat)
                    {
                        case "Extra Chance":
                            ExtraChance = true;
                            PointAwayChances = 3;
                            break;
                        case "Extra Life":
                            ExtraLife = true;
                            break;
                        case "God Mode":
                            GodMode = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            //Init start of game
            AnimTimer.Interval = 40; //fps of around 25
            AnimTimer.Start();
            IntroTimer.Start();
            Anim_Intro.Loop = true;
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            PaintCanvas();
            UpdateGame();
        }

        private void UpdateGame()
        {
            switch (StateOfTheGame)
            {
                case GameStates.Intro:
                    if (!IntroTimer.Enabled)
                    {
                        IntroTimer.Start();
                    }
                    break;
                case GameStates.LoadBullet:
                    AnimTimer.Interval = 800;
                    if (Anim_LoadBullet.Ended)
                    {
                        Bullet = 1; //Place a bullet in gun
                        StateOfTheGame = GameStates.SpinChamber;
                        BulletLoad = false;
                    }
                    if (BulletLoad)
                    {
                        Anim_LoadBullet.Advance();
                    }
                    break;
                case GameStates.SpinChamber:
                    if (Anim_SpinChamber.Ended)
                    {
                        Random spinRandom = new Random(); //Randomize location of bullet
                        Bullet = spinRandom.Next(1, 6);
                        Chambers[Bullet - 1] = 1; //this is when we actually load the bullet but user doesn't know that
                        StateOfTheGame = GameStates.PointDirection;
                        AnimTimer.Interval = 40;
                    }
                    break;
                case GameStates.PointDirection:
                    break;
                case GameStates.Fire:
                    if (Triggered)
                    {
                        Hammer = false;
                        Triggered = false;

                        if (NextDeath)
                        {
                            NextDeath = false;
                            PointAwayChances = -1;
                            //No Change in points tried your luck and failed
                            Game_Die();
                        }
                        else if (CurrentChamber == 5 && !PointingAway) //you chose this path >:c
                        {
                            Game_CurrentScore -= 10;
                            Game_Die();
                        }
                        else if (CurrentChamber == 5 && PointingAway)
                        {
                            Game_CurrentScore += 30; // close calls are the most rewarding cause they're the most risky wins
                            Game_CloseCalls += 1;
                            Game_Survived();
                        }
                        else if (CurrentChamber != 5 && !PointingAway)
                        {
                            if (Chambers[CurrentChamber] == 1)
                            {
                                //No change in points
                                Game_Die();
                            }
                            else
                            {
                                Game_CurrentScore += 5; //Plus five points for every successful pull of the trigger
                                StateOfTheGame = GameStates.Survive; // click sound
                            }
                        }
                        else //PointAwayChances != 0 && CurrentChamber != 5 && PointingAway
                        {
                            if (Chambers[CurrentChamber] == 1)
                            {
                                //this isn't a close call. Close calls are only if you use your "point away" when there is only 1 chamber left
                                Game_Survived();
                                Game_CurrentScore += 10; //Made a good decision (lucky)
                                GameAudio = new SoundPlayer(Properties.Resources.Fire);
                                GameAudio.Play();
                                Game_BulletsFired += 1;
                                Anim_Fire.Advance(); //flash
                            }
                            else
                            {
                                //No change in points
                                StateOfTheGame = GameStates.Survive; // click sound
                            }
                        }
                        if (PointAwayChances == 0)
                        {
                            NextDeath = true;
                        }
                    }
                    break;
                case GameStates.Death:
                    Save_Game();
                    break;
                case GameStates.Survive:
                    Save_Game();
                    break;
                case GameStates.DeusExMachina:
                    Save_Game();
                    break;
                default:
                    break;
            }
        }

        private void IntroTimer_Tick(object sender, EventArgs e)
        {
            StateOfTheGame = GameStates.LoadBullet;
            IntroTimer.Stop();
        }

        private void pnl_canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (StateOfTheGame)
            {
                case GameStates.Intro: //skip intro
                    StateOfTheGame = GameStates.LoadBullet;
                    IntroTimer.Stop();
                    break;
                case GameStates.LoadBullet:
                    if (e.X > 130 && e.X < 180 && e.Y > 285 && e.Y < 385) //user clicked on first bullet
                    {
                        BulletLoad = true;
                        GameAudio = new SoundPlayer(Properties.Resources.Load);
                        GameAudio.Play();
                    }
                    break;
                case GameStates.SpinChamber:
                    if (e.X > 350 && e.X < 445 && e.Y > 75 && e.Y < 170) //user clicked on Chamber
                    {
                        GameAudio = new SoundPlayer(Properties.Resources.Chamber);
                        GameAudio.Play();
                        Anim_SpinChamber.Advance();
                    }
                    break;
                case GameStates.PointDirection:
                    if (e.X > pnl_canvas.Width / 2) //user clicked on Point Away
                    {
                        PointingAway = true;
                        PointAwayChances -= 1; //used up a chance
                    }
                    else if (e.X < pnl_canvas.Width / 2)// user clicks on Point at user
                    {
                        PointingAway = false;
                    }
                    PointDirectionTimer.Start();
                    break;
                case GameStates.Fire:
                    if (PointingAway)
                    {
                        if (e.X > 123 && e.X < 160 && e.Y > 90 && e.Y < 125 && Hammer == false) //user clicked on Hammer alt
                        {
                            GameAudio = new SoundPlayer(Properties.Resources.Hammer);
                            GameAudio.Play();
                            Hammer = true;
                            Anim_AltFire.Advance();
                        }
                        if (e.X > 175 && e.X < 220 && e.Y > 230 && e.Y < 290 && Hammer == true) //user clicked on Trigger alt
                        {
                            Triggered = true;
                        }
                    }
                    else //!PointingAway
                    {
                        if (e.X > 400 && e.X < 430 && e.Y > 90 && e.Y < 125 && Hammer == false) //user clicked on Hammer
                        {
                            GameAudio = new SoundPlayer(Properties.Resources.Hammer);
                            GameAudio.Play();
                            Hammer = true;
                            Anim_Fire.Advance();
                        }
                        if (e.X > 330 && e.X < 380 && e.Y > 230 && e.Y < 290 && Hammer == true) //user clicked on Trigger
                        {
                            Triggered = true;
                        }
                    }

                    break;
                case GameStates.Death:
                    if (GameEnded)
                    {
                        if (e.X > 520 && e.X < 555 && e.Y > pnl_canvas.Height - 25) //user clicked on Yes
                        {
                            NewGame = true;
                            this.Close();
                        }
                        else if (e.X > 560 && e.Y > pnl_canvas.Height - 25) //user clicked on No
                        {
                            NewGame = false;
                            this.Close();
                        }
                    }
                    break;
                case GameStates.Survive:
                    if (!GameEnded)
                    {
                        if (CurrentChamber != 5)
                        {
                            GameAnimations.ResetAll();
                            CurrentChamber++;
                            if (PointAwayChances != 0)
                            {
                                StateOfTheGame = GameStates.PointDirection;
                            }
                            else
                            {
                                PointingAway = false;
                                StateOfTheGame = GameStates.Fire;
                            }
                        }
                    }
                    else
                    {
                        if (e.X > 520 && e.X < 555 && e.Y > pnl_canvas.Height - 25) //user clicked on Yes
                        {
                            NewGame = true;
                            this.Close();
                        }
                        else if (e.X > 560 && e.Y > pnl_canvas.Height - 25) //user clicked on No
                        {
                            NewGame = false;
                            this.Close();
                        }
                    }

                    break;
                case GameStates.DeusExMachina:
                    if (GameEnded)
                    {
                        if (e.X > 520 && e.X < 555 && e.Y > pnl_canvas.Height - 25) //user clicked on Yes
                        {
                            NewGame = true;
                            this.Close();
                        }
                        else if (e.X > 560 && e.Y > pnl_canvas.Height - 25) //user clicked on No
                        {
                            NewGame = false;
                            this.Close();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void PointDirectionTimer_Tick(object sender, EventArgs e)
        {
            StateOfTheGame = GameStates.Fire;
            PointDirectionTimer.Stop();
        }

        public void Game_Die()
        {
            //Flash from bullet everytime we die
            Anim_Fire.Advance();
            GameAudio = new SoundPlayer(Properties.Resources.Fire);
            GameAudio.Play();
            Game_BulletsFired += 1;

            if (GodMode)
            {
                Game_Survived();
                return;
            }
            else if (ExtraLife) //not godmode but is extralife
            {
                GameEnded = false;
                CurrentChamber -= 1; //go back a move
                if (ExtraChance)
                {
                    PointAwayChances = 3;
                }
                else
                {
                    PointAwayChances = 2;
                }
                return;
            }

            if (new Random().Next(1, 100) == 100)
            {
                Game_DeusExMachina(); //God saves ya
            }
            else
            {
                StateOfTheGame = GameStates.Death;
                GameEnded = true;
                GameWon = false;
                Game_Highscore = Math.Max(Game_CurrentScore, Game_Highscore);
                Game_Deaths += 1;
            }
            Game_TimesPlayed += 1;
        }

        public void Game_Survived()
        {
            GameEnded = true;
            GameWon = true;
            StateOfTheGame = GameStates.Survive;
            Game_TimesPlayed += 1;
            Game_Highscore = Math.Max(Game_CurrentScore, Game_Highscore);
        }

        private void Game_DeusExMachina()
        {
            //Life isn't fair neither is this game... here's a bunch of points
            Game_CurrentScore += 50; //God likes you, so I like you
            GameWon = true;
            DeusExMachina = true;
            StateOfTheGame = GameStates.DeusExMachina;
            Game_DeiExMachinas += 1;
            Game_TimesPlayed += 1;
            Game_Highscore = Math.Max(Game_CurrentScore, Game_Highscore);
        }

        private void Save_Game()
        {
            if (!Game_Saved)
            {
                try
                {
                    bool playerFound = false;
                    int playerIndex = 0;
                    string[] Scores = File.ReadAllLines(frm_Menu.HighScoresFilename);

                    if (Scores != null)
                    {
                        for (int i = 0; i < Scores.Length; i++) //check to see if player name can be found in Scores
                        {
                            if (Scores[i].Split(',')[0] == frm_PlayerProfile.profileName)
                            {
                                playerFound = true;
                                playerIndex = i;
                                break;
                            }
                            else
                            {
                                playerIndex = -1;
                            }
                        }
                        if (playerIndex == -1) //if player slot is not found create a new player slot
                        {
                            List<string> tmpList = new List<string>(Scores);
                            tmpList.Add("");
                            Scores = tmpList.ToArray();
                            playerIndex = Scores.Length - 1;
                        }
                        StringBuilder recordstring = new StringBuilder();
                        for (int field = 0; field < frm_Menu.HighScoresFileFieldsCount; field++)
                        {
                            if (playerFound)
                            {
                                switch (field)
                                {
                                    case 0: //Profile name
                                        recordstring.Append(frm_PlayerProfile.profileName + ",");
                                        break;
                                    case 1: //Profile Picture name
                                        recordstring.Append(frm_PlayerProfile.profilePicName + ",");
                                        break;
                                    case 2: //Score
                                        recordstring.Append(Math.Max(Convert.ToInt32(Scores[playerIndex].Split(',')[2]), Game_Highscore) + ",");
                                        break;
                                    case 3: //Times Played
                                        recordstring.Append((Convert.ToInt32(Scores[playerIndex].Split(',')[3]) + Game_TimesPlayed).ToString() + ",");
                                        break;
                                    case 4: //Deaths
                                        recordstring.Append((Convert.ToInt32(Scores[playerIndex].Split(',')[4]) + Game_Deaths).ToString() + ",");
                                        break;
                                    case 5: //Shots Fired
                                        recordstring.Append((Convert.ToInt32(Scores[playerIndex].Split(',')[5]) + Game_BulletsFired).ToString() + ",");
                                        break;
                                    case 6: //Close Calls
                                        recordstring.Append((Convert.ToInt32(Scores[playerIndex].Split(',')[6]) + Game_CloseCalls).ToString() + ",");
                                        break;
                                    case 7: //Dei Ex Machina
                                        recordstring.Append((Convert.ToInt32(Scores[playerIndex].Split(',')[7]) + Game_DeiExMachinas).ToString());
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                switch (field)
                                {
                                    case 0: //Profile name
                                        recordstring.Append(frm_PlayerProfile.profileName + ",");
                                        break;
                                    case 1: //Profile Picture name
                                        recordstring.Append(frm_PlayerProfile.profilePicName + ",");
                                        break;
                                    case 2: //Score
                                        recordstring.Append(Game_Highscore.ToString() + ",");
                                        break;
                                    case 3: //Times Played
                                        recordstring.Append(Game_TimesPlayed.ToString() + ",");
                                        break;
                                    case 4: //Deaths
                                        recordstring.Append(Game_Deaths.ToString() + ",");
                                        break;
                                    case 5: //Shots Fired
                                        recordstring.Append(Game_BulletsFired.ToString() + ",");
                                        break;
                                    case 6: //Close Calls
                                        recordstring.Append(Game_CloseCalls.ToString() + ",");
                                        break;
                                    case 7: //Dei Ex Machina
                                        recordstring.Append(Game_DeiExMachinas.ToString());
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        Scores[playerIndex] = recordstring.ToString();
                        File.WriteAllLines(frm_Menu.HighScoresFilename, Scores);
                        Game_Saved = true;
                        //update the Main form
                        frm_Menu.SetProfileStats();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Pnl_canvas_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    class Animation
    {
        private Assembly assembly;
        private Stream ImageStream;
        //private
        private int _index;
        private Image[] _ImageSet;
        private bool loop = false;
        private bool ended = false;

        public Animation()
        {
            assembly = Assembly.GetExecutingAssembly();
            _ImageSet = null;
        }
        public Animation(Image[] newImageSet)
        {
            assembly = Assembly.GetExecutingAssembly();
            _ImageSet = newImageSet;
            _index = 0;
        }
        public Animation(Image[] newImageSet, int newCurrentImage)
        {
            assembly = Assembly.GetExecutingAssembly();
            _ImageSet = newImageSet;
            _index = newCurrentImage;
        }

        public void AddImage(Image newImage)
        {

            if (_ImageSet != null)
            {
                List<Image> tmpImages = new List<Image>();
                foreach (Image _image in _ImageSet)
                {
                    tmpImages.Add(_image);
                }
                tmpImages.Add(newImage);
                _ImageSet = tmpImages.ToArray();
            }
            else
            {
                _ImageSet = new Image[] { newImage };
            }
        }
        public void AddImage(string newImage)
        {

            if (_ImageSet != null)
            {
                List<Image> tmpImages = new List<Image>();
                foreach (Image _image in _ImageSet)
                {
                    tmpImages.Add(_image);
                }
                ImageStream = assembly.GetManifestResourceStream(newImage);
                tmpImages.Add(new Bitmap(ImageStream));
                _ImageSet = tmpImages.ToArray();
            }
            else
            {
                ImageStream = assembly.GetManifestResourceStream(newImage);
                _ImageSet = new Image[] { new Bitmap(ImageStream) };
            }
        }

        public Image CurrentImage()
        {
            if (_ImageSet == null || _ImageSet.Length < 1)
            {
                return null;
            }
            return _ImageSet[_index];
        }
        public void Advance()
        {
            if (_index == _ImageSet.Length - 1)
            {
                if (loop)//Only reset CurrentImage if loop
                {
                    _index = 0;
                }
                else
                {
                    ended = true;
                }
            }
            else
            {
                ended = false;
                _index++;
            }

        }

        public void Reset()
        {
            _index = 0;
            ended = false;
        }

        public Image[] ImageSet
        {
            get
            {
                return _ImageSet;
            }

            set
            {
                _ImageSet = value;
            }
        }

        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }

        public bool Loop
        {
            get
            {
                return loop;
            }

            set
            {
                loop = value;
            }
        }

        public bool Ended
        {
            get
            {
                return ended;
            }
        }
    }

    class Animations
    {
        //private
        private Animation[] AnimationSet;

        public Animations()
        {
            AnimationSet = null;
        }
        public Animations(Animation[] newAnimationSet)
        {
            AnimationSet = newAnimationSet;
        }

        public void AddAnimation(Animation newAnimation)
        {

            if (AnimationSet != null)
            {
                List<Animation> tmpAnimation = new List<Animation>();
                foreach (Animation anim in AnimationSet)
                {
                    tmpAnimation.Add(anim);
                }
                tmpAnimation.Add(newAnimation);
                AnimationSet = tmpAnimation.ToArray();
            }
            else
            {
                AnimationSet = new Animation[] { newAnimation };
            }
        }
        public void AddAnimation(Animation newAnimation, string[] ImageSet)
        {
            foreach (string Image in ImageSet)//fill the animation with images
            {
                newAnimation.AddImage(Image);
            }
            if (AnimationSet != null)
            {
                List<Animation> tmpAnimation = new List<Animation>();
                foreach (Animation anim in AnimationSet)
                {
                    tmpAnimation.Add(anim);
                }
                tmpAnimation.Add(newAnimation);
                AnimationSet = tmpAnimation.ToArray();
            }
            else
            {
                AnimationSet = new Animation[] { newAnimation };
            }
        }

        public void SetAllLoop(bool _loop)
        {
            foreach (Animation anim in AnimationSet)
            {
                anim.Loop = _loop;
            }
        }

        public void ResetAll()
        {
            foreach (Animation anim in AnimationSet)
            {
                anim.Reset();
            }
        }

        public Animation[] GetAnimationSet
        {
            get
            {
                return AnimationSet;
            }

            set
            {
                AnimationSet = value;
            }
        }
    }
}
