using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Timers;
using Dice;
using GameStatistics;
using CustomMessageBox;

/*
 * Primary window form (partial class) for the game of Farkle.  Conversion from
 * Delphi program originally written 2009-01-03.  Delphi environment lost,
 * converted in case changes needed.
 * Note, Delphi 3 environment was restored, so updating both C# and Delphi 3
 * versions when changing.
 * 
 * Author: Michael G. Slack
 * Written: 2020-05-20
 * Version: 1.2.0.0
 * 
 * ----------------------------------------------------------------------------
 * 
 * Updated: 2021-01-28 - Added 'keep all' button to keep all of the rolled dice.
 *          2021-12-24 - Changed to use custom messagebox assembly along with
 *                       some of the messages.  Changed so can control sounds
 *                       while still using the icons.
 * 
 */
namespace Farkle
{
    public partial class MainWin : Form
    {
        #region Public Constants
        public const int PLAYER_NAME_LENGTH = 20;
        public const int MAX_PLAYERS = 6;
        public const int MAX_COMP_NAME = 9;
        public const int NUM_DICE = 6;
        public const int SAFETY_SCORE_DIF = 200;
        public const int DEF_MIN_SCORE = 500;
        public const int DEF_WIN_SCORE = 10000;
        #endregion

        #region Private Constants
        private const string HTML_HELP_FILE = "Farkle_help.html";
        private const string COMP_PLAYER_NAMES_FN = "farkle_names.txt";
        private const string PLAYER_DISP_FMT = "{0,-20} {1}";
        private const string SOUND_NAMESPACE = "Farkle.sounds.";
        private const string SOUND_DICE = "farkle_rolldice.wav";
        private const string SOUND_FARKLE = "farkle_farkle.wav";
        private const string STAT_GAMES_PLAYED = "Games Played";
        private const string STAT_MOST_POINTS_IN_TURN = "Most Points Scored in One Turn";
        private const string FARKLE_MSG = "Farkle!";
        private const string FARKLE_BAD_DICE = 
            "You've kept dice that aren't scoring dice. Please remove before rolling or ending turn.";
        private const string FARKLE_END_TURN = "Are you sure you want to end your turn?";
        #endregion

        #region Registry Constants
        private const string REG_NAME = @"HKEY_CURRENT_USER\Software\Slack and Associates\Games\Farkle";
        private const string REG_KEY1 = "Verbose";
        private const string REG_KEY2 = "Sounds";
        private const string REG_KEY3 = "Strict";
        private const string REG_KEY4 = "Safe";
        private const string REG_KEY5 = "FourTwo";
        private const string REG_KEY6 = "Sort";
        private const string REG_KEY7 = "ScoreTo";
        private const string REG_KEY8 = "Player";
        private const string REG_KEY9 = "PosX";
        private const string REG_KEYA = "PosY";
        #endregion

        #region Other Instances/Variables
        private Color diceColor = Color.White;
        private Color noDiceColor = Color.Teal;
        private Color defPlayerColor = Color.Silver;
        private Color curPlayerColor = Color.White;
        private Color firstPlayerColor = Color.Olive;
        private Color wonPlayerColor = Color.Lime;
        private Die[] rolledDice = new Die[NUM_DICE];
        private Die[] keptDice = new Die[NUM_DICE];
        private Label[] playerDisplays = new Label[MAX_PLAYERS];
        private string[] players = new string[MAX_PLAYERS];
        private int[] playerScores = new int[MAX_PLAYERS];
        private Statistics stats = new Statistics(REG_NAME);
        private FarkleGame farkleEng = new FarkleGame();
        private readonly string[] defCompPlayerNames = new string[MAX_COMP_NAME] {
            "Telly Belly", "Lucky Ducky", "Moody Boody",
            "Nuckle Buckle", "Jokey Lokey", "DumbA$$",
            "Roofus Doofus", "Jacky Macky", "Howdy Dowdy"};
        private StringCollection compPlayerNames = new StringCollection();
        private SoundPlayer diceSound = null;
        private bool dSoundLoaded = false;
        private SoundPlayer farkleSound = null;
        private bool fSoundLoaded = false;
        private System.Timers.Timer timer = new System.Timers.Timer(2500);
        private bool compPlayerTwo = false, timedOut = true, disableSomeEvents = false;
        private int keptCount = 0, keptScore = 0, oldKeptScore = 0, numPlayers = 0, mostPointsInTurn = 0,
                    firstPlayerWin = -1, winningPlayer = -1, currentPlayer = -1, winScore = DEF_WIN_SCORE;
        #endregion

        #region Custom Events
        private event EventHandler StartRound;
        private event EventHandler CompMove;
        private event EventHandler NextPlayer;
        #endregion

        // --------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        #region Private Methods
        private void DoEvent(EventHandler handler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void LoadRegistryValues()
        {
            int winX = -1, winY = -1, chk;
            string tempVal;

            try
            {
                // reading bool as integer from registry for compatibility with older code
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY1, 0);  // def = false
                verbose.Checked = chk > 0;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY2, 1);  // def = true
                soundsOn.Checked = chk > 0;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY3, 1);  // def = true
                strictScoring.Checked = chk > 0;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY4, 1);  // def = true
                safeMode.Checked = chk > 0;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY5, 1);  // def = true
                count42.Checked = chk > 0;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY6, 0);  // def = false
                sortRolls.Checked = chk > 0;
                //tempVal = (string)Registry.GetValue(REG_NAME, REG_KEY7, DEF_WIN_SCORE);
                //if (tempVal != null) winScore = Convert.ToInt32(tempVal);
                winScore = (int)Registry.GetValue(REG_NAME, REG_KEY7, DEF_WIN_SCORE);
                scoreTo.Value = winScore;
                for (int i = 1; i <= MAX_PLAYERS; i++)
                {
                    tempVal = (string)Registry.GetValue(REG_NAME, REG_KEY8 + i, "");
                    if (tempVal == null) tempVal = "";
                    players[i - 1] = tempVal;
                }
                winX = (int)Registry.GetValue(REG_NAME, REG_KEY9, winX);
                winY = (int)Registry.GetValue(REG_NAME, REG_KEYA, winY);
            }
            catch (Exception) { /* ignore, go with defaults MessageBox.Show(e.Message); */ }

            if ((winX != -1) && (winY != -1)) this.SetDesktopLocation(winX, winY);
        }

        private void WriteRegistryValues()
        {
            Registry.SetValue(REG_NAME, REG_KEY1, verbose.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY2, soundsOn.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY3, strictScoring.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY4, safeMode.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY5, count42.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY6, sortRolls.Checked, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY7, scoreTo.Value, RegistryValueKind.DWord);
            for (int i = 1; i <= MAX_PLAYERS; i++)
            {
                string s = players[i-1];
                if (i == 2 && compPlayerTwo) s = "";
                if (!"".Equals(s))
                {
                    Registry.SetValue(REG_NAME, REG_KEY8 + i, s);
                }
                else
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_NAME.Substring(18), true))
                        key.DeleteValue(REG_KEY8 + i, false);
                }
            }
        }
        
        private void SetupContextMenu()
        {
            ContextMenu mnu = new ContextMenu();
            MenuItem mnuStats = new MenuItem("Game Statistics");
            MenuItem sep = new MenuItem("-");
            MenuItem mnuAbout = new MenuItem("About");

            mnuStats.Click += new EventHandler(mnuStats_Click);
            mnuAbout.Click += new EventHandler(mnuAbout_Click);
            mnu.MenuItems.AddRange(new MenuItem[] { mnuStats, sep, mnuAbout });
            this.ContextMenu = mnu;
        }

        private void InitControlsAndEvents()
        {
            rolledDice[0] = rolled1; rolledDice[1] = rolled2; rolledDice[2] = rolled3;
            rolledDice[3] = rolled4; rolledDice[4] = rolled5; rolledDice[5] = rolled6;
            keptDice[0] = kept1; keptDice[1] = kept2; keptDice[2] = kept3;
            keptDice[3] = kept4; keptDice[4] = kept5; keptDice[5] = kept6;
            playerDisplays[0] = player1; playerDisplays[1] = player2; playerDisplays[2] = player3;
            playerDisplays[3] = player4; playerDisplays[4] = player5; playerDisplays[5] = player6;
            timer.Elapsed += timer_OnTimedEvent;
            timer.AutoReset = false;
            StartRound += CustomStartRound;
            CompMove += CustomCompMove;
            NextPlayer += CustomNextPlayer;
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                players[i] = ""; playerScores[i] = 0; playerDisplays[i].Text = "";
            }
        }

        private Stream GetResourceStream(string path)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(path);
        }

        private void LoadSoundPlayers()
        {
            string path = SOUND_NAMESPACE + SOUND_DICE;

            try
            {
                diceSound = new SoundPlayer(GetResourceStream(path));
                dSoundLoaded = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Wav (" + path + "): " + e.Message, this.Text);
                diceSound = null;
            }

            path = SOUND_NAMESPACE + SOUND_FARKLE;
            try
            {
                farkleSound = new SoundPlayer(GetResourceStream(path));
                fSoundLoaded = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Wav (" + path + "): " + e.Message, this.Text);
                farkleSound = null;
            }
        }

        private void LoadNameList()
        {
            string fn = Application.StartupPath + Path.DirectorySeparatorChar + COMP_PLAYER_NAMES_FN;
            bool loadDefaults = false;

            try
            {
                using (StreamReader file = new StreamReader(fn))
                {
                    string line = "";

                    while ((line = file.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length > PLAYER_NAME_LENGTH) line = line.Substring(0, PLAYER_NAME_LENGTH);
                        compPlayerNames.Add(line);
                    }
                }
            }
            catch
            {
                loadDefaults = true;
            }

            if (loadDefaults || compPlayerNames.Count == 0)
                for (int i = 0; i < MAX_COMP_NAME; i++) compPlayerNames.Add(defCompPlayerNames[i]);
        }

        private bool StartNew()
        {
            bool ret = true;
            MessageBoxIcon snd = MessageBoxIcon.None;

            if (verbose.Checked && currentPlayer != -1 && winningPlayer != -1)
            {
                if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                DialogResult res = MsgBox.Show(this, "Are you sure you want to start a new game?", this.Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, snd);
                ret = (res == DialogResult.Yes);
            }

            return ret;
        }

        private bool GetPlayers()
        {
            bool ret = false;
            PlayersDlg dlg = new PlayersDlg();

            dlg.SoundsOn = soundsOn.Checked;
            dlg.Players = players;
            if (compPlayerTwo) dlg.Players[1] = "";
            dlg.CompPlayerNames = compPlayerNames;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ret = true;
                players = dlg.Players;
                numPlayers = dlg.NumPlayers;
                compPlayerTwo = dlg.CompPlayer;
            }
            dlg.Dispose();

            return ret;
        }

        private void InitGameAndGlobals()
        {
            Random rnd = new Random();
            MessageBoxIcon snd = MessageBoxIcon.None;

            keepBtn.Enabled = false; rollDice.Enabled = false; endTurn.Enabled = false;
            ClearRolled();
            ClearKept(true);
            firstPlayerWin = -1; winningPlayer = -1;
            for (int i = 0; i < MAX_PLAYERS; i++)
                playerScores[i] = 0;
            currentPlayer = rnd.Next(numPlayers);
            DisplayPlayerPanel();
            if (soundsOn.Checked) snd = MessageBoxIcon.Information;
            MsgBox.Show(this, players[currentPlayer] + " will start.", this.Text, MessageBoxButtons.OK,
                MessageBoxIcon.Information, snd);
            stats.GameName = this.Text;
            stats.StartGameNoGSS(false);
        }

        private void ClearRolled()
        {
            for (int i = 0; i < NUM_DICE; i++)
            {
                rolledDice[i].Face = DieFace.dfn0;
                rolledDice[i].BackColor = noDiceColor;
            }
        }

        private void ClearKept(bool incScore)
        {
            for (int i = 0; i < NUM_DICE; i++)
            {
                keptDice[i].Face = DieFace.dfn0;
                keptDice[i].BackColor = noDiceColor;
            }

            if (incScore)
            {
                oldKeptScore = 0; keptScore = 0;
                ShowKeptScore();
            }
        }

        private void ShowKeptScore()
        {
            keptScoreLbl.Text = "[" + keptScore + "]";
        }

        private void DisplayPlayerPanel()
        {
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (currentPlayer == i)
                    playerDisplays[i].BackColor = curPlayerColor;
                else if (firstPlayerWin == i && winningPlayer == -1)
                    playerDisplays[i].BackColor = firstPlayerColor;
                else if (winningPlayer == i)
                    playerDisplays[i].BackColor = wonPlayerColor;
                else
                    playerDisplays[i].BackColor = defPlayerColor;

                if (!"".Equals(players[i]))
                    playerDisplays[i].Text = String.Format(PLAYER_DISP_FMT, players[i], playerScores[i]);
                else
                    playerDisplays[i].Text = "";
            }
        }

        private void DisplayDice(int[] dice)
        {
            for (int i = 0; i < NUM_DICE; i++)
            {
                rolledDice[i].Face = (DieFace)dice[i];
                if (dice[i] != 0)
                    rolledDice[i].BackColor = diceColor;
                else
                    rolledDice[i].BackColor = noDiceColor;
            }
            Application.DoEvents();
        }

        private int DisplayCompMove(int[] kept)
        {
            int ret = 0;

            for (int i = 0; i < NUM_DICE; i++)
                if (kept[i] != 0)
                {
                    keptDice[i].Face = (DieFace)kept[i];
                    keptDice[i].BackColor = diceColor;
                    rolledDice[i].Face = DieFace.dfn0;
                    rolledDice[i].BackColor = noDiceColor;
                }
            ShowKeptScore();

            // determine how many die comp will roll next time
            for (int i = 0; i < NUM_DICE; i++) if (rolledDice[i].Face != DieFace.dfn0) ret++;
            if (ret == 0) ret = 6;

            return ret;
        }

        private void ShowScoreDialog()
        {
            MessageBoxIcon snd = MessageBoxIcon.None;

            if (soundsOn.Checked) snd = MessageBoxIcon.Information;
            MsgBox.Show(this, players[currentPlayer] + " got " + keptScore + " points.", this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Information, snd);
        }

        private bool CheckScore(int[] kDice)
        {
            int[] rDice = new int[NUM_DICE];
            int kscore, ascore, dif;
            DialogResult dRet;
            bool ret = true;
            MessageBoxIcon snd = MessageBoxIcon.None;

            if (safeMode.Checked)
            {
                for (int i = 0; i < NUM_DICE; i++) rDice[i] = (int)rolledDice[i].Face;
                kscore = farkleEng.getKeptScore(kDice, count42.Checked);
                ascore = farkleEng.getTotalScore(rDice, kDice, count42.Checked);
                dif = ascore - kscore;
                if (dif > SAFETY_SCORE_DIF)
                {
                    if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                    dRet = MsgBox.Show(this, "You've left " + dif + " points behind. " +
                         "Are you sure you want to roll?", this.Text,
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question, snd);
                    ret = (dRet == DialogResult.Yes);
                }
            }

            return ret;
        }

        private void FinishStatistics()
        {
            int mostPoints = stats.CustomStatistic(STAT_MOST_POINTS_IN_TURN);

            stats.IncCustomStatistic(STAT_GAMES_PLAYED);
            if (mostPoints == 0 || mostPointsInTurn > mostPoints)
                stats.SetCustomStatistic(STAT_MOST_POINTS_IN_TURN, mostPointsInTurn);
            mostPointsInTurn = 0; // reset
            stats.GameDone();
        }

        private void WaitTimer(int ms)
        {
            timedOut = false;
            timer.Interval = ms;
            timer.Start();
            do { Application.DoEvents(); } while (!timedOut);
        }
        #endregion

        // --------------------------------------------------------------------

        #region Event Handlers
        private void MainWin_Load(object sender, EventArgs e)
        {
            InitControlsAndEvents();
            LoadRegistryValues();
            SetupContextMenu();
            LoadSoundPlayers();
            LoadNameList();
            stats.GameName = this.Text;
        }

        private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteRegistryValues();
            if (this.WindowState == FormWindowState.Normal)
            {
                Registry.SetValue(REG_NAME, REG_KEY9, this.Location.X);
                Registry.SetValue(REG_NAME, REG_KEYA, this.Location.Y);
            }
        }

        private void MainWin_KeyUp(object sender, KeyEventArgs e)
        {
            if (!disableSomeEvents && !scoreTo.Focused)
            {
                int K = e.KeyValue;

                if (K >= 97 && K <= 102)  // keypad 1 - 6
                    K -= 48;
                if (K >= 49 && K <= 54)   // 1 - 6
                {
                    int idx = K - 49;
                    if (keptDice[idx].Face == DieFace.dfn0)
                        rolledX_Click(rolledDice[idx], EventArgs.Empty);
                    else
                        keptX_Click(keptDice[idx], EventArgs.Empty);
                }
            }
        }

        private void rolledX_Click(object sender, EventArgs e)
        {
            if (!disableSomeEvents)
            {
                int whch = Convert.ToInt32(((Die)sender).Tag);
                int[] dice = new int[NUM_DICE];

                if (rolledDice[whch].Face != DieFace.dfn0)
                {
                    keptCount++;
                    keptDice[whch].Face = rolledDice[whch].Face;
                    keptDice[whch].BackColor = diceColor;
                    rolledDice[whch].Face = DieFace.dfn0;
                    rolledDice[whch].BackColor = noDiceColor;
                    for (int i = 0; i < NUM_DICE; i++) dice[i] = (int)keptDice[i].Face;
                    keptScore = oldKeptScore + farkleEng.getKeptScore(dice, count42.Checked);
                    ShowKeptScore();
                    rollDice.Enabled = keptScore != 0 && keptScore != oldKeptScore;
                }
            }
        }

        private void keptX_Click(object sender, EventArgs e)
        {
            if (!disableSomeEvents)
            {
                int whch = Convert.ToInt32(((Die)sender).Tag);
                int[] dice = new int[NUM_DICE];

                if (keptDice[whch].Face != DieFace.dfn0)
                {
                    keptCount--;
                    rolledDice[whch].Face = keptDice[whch].Face;
                    rolledDice[whch].BackColor = diceColor;
                    keptDice[whch].Face = DieFace.dfn0;
                    keptDice[whch].BackColor = noDiceColor;
                    for (int i = 0; i < NUM_DICE; i++) dice[i] = (int)keptDice[i].Face;
                    keptScore = oldKeptScore + farkleEng.getKeptScore(dice, count42.Checked);
                    ShowKeptScore();
                    rollDice.Enabled = keptScore != 0 && keptScore != oldKeptScore;
                }
            }
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            if (StartNew() && GetPlayers())
            {
                InitGameAndGlobals();
                
                if (compPlayerTwo && currentPlayer == 1)
                    DoEvent(CompMove);
                else
                    DoEvent(StartRound);
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
            var asm = Assembly.GetEntryAssembly();
            var asmLocation = Path.GetDirectoryName(asm.Location);
            var htmlPath = Path.Combine(asmLocation, HTML_HELP_FILE);

            try
            {
                Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBoxIcon snd = MessageBoxIcon.None;

                if (soundsOn.Checked) snd = MessageBoxIcon.Error;
                MsgBox.Show(this, "Cannot load help: " + ex.Message, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Error, snd);
            }
        }

        private void keepBtn_Click(object sender, EventArgs e)
        {
            // keep all rolled dice currently in play
            for (int i = 0; i < NUM_DICE; i++)
                if (rolledDice[i].Face != DieFace.dfn0) rolledX_Click(rolledDice[i], EventArgs.Empty);
        }

        private void rollDice_Click(object sender, EventArgs e)
        {
            int[] dice = new int[NUM_DICE];
            int n = 0;
            MessageBoxIcon snd = MessageBoxIcon.None;

            for (int i = 0; i < NUM_DICE; i++)
                if (rolledDice[i].Face == DieFace.dfn0) n++;
            if (n != NUM_DICE)
            { // rolling again, have dice left on table??
                if (keptCount == 0)
                {
                    if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                    MsgBox.Show(this, "Need to keep some dice before rolling again!", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Question, snd);
                    return;
                }
                // reset count to number to roll again (less than 6)
                n = 0;
                for (int i = 0; i < NUM_DICE; i++)
                    if (rolledDice[i].Face != DieFace.dfn0) n++;
            }

            for (int i = 0; i < NUM_DICE; i++) dice[i] = (int)keptDice[i].Face;
            if (!farkleEng.validateKeptDice(dice, count42.Checked))
            {
                if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                MsgBox.Show(this, FARKLE_BAD_DICE, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, snd);
                return;
            }

            if (!strictScoring.Checked && n == NUM_DICE && keptScore != 0)
            {
                if (verbose.Checked) ShowScoreDialog();
                playerScores[currentPlayer] += keptScore;
                keptScore = 0;
                ShowKeptScore();
                DisplayPlayerPanel();
            }

            if (n != NUM_DICE && !CheckScore(dice)) return; // safety check!!!

            if (dSoundLoaded && soundsOn.Checked)
            {
                diceSound.Play(); WaitTimer(750);
            }

            ClearKept(false);
            oldKeptScore = keptScore; keptCount = 0;
            if (sortRolls.Checked)
                farkleEng.rollDiceSorted(dice, n);
            else
                farkleEng.rollDice(dice, n);
            DisplayDice(dice);
            keepBtn.Enabled = true; rollDice.Enabled = false; endTurn.Focus();
        }

        private void endTurn_Click(object sender, EventArgs e)
        {
            DialogResult ret = DialogResult.Yes;
            int[] dice = new int[NUM_DICE];
            int tscore;
            MessageBoxIcon snd = MessageBoxIcon.None;

            if (verbose.Checked)
            {
                if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                ret = MsgBox.Show(this, FARKLE_END_TURN, this.Text, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, snd);
            }
            else
            { 
                if (keptCount == 0 && safeMode.Checked)
                { // check dice for points or not and if rolled (safety mode)
                    if (rolledDice[0].Face == DieFace.dfn0)
                    {
                        if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                        ret = MsgBox.Show(this, "It appears as if you haven't rolled. " +
                            "Are you sure you want to pass?", this.Text,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, snd);
                    }
                    else
                    {
                        for (int i = 0; i < NUM_DICE; i++) dice[i] = (int)rolledDice[i].Face;
                        tscore = farkleEng.getKeptScore(dice, count42.Checked);
                        if (tscore > 0)
                        {
                            if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                            ret = MsgBox.Show(this, "You have points to score with. " + FARKLE_END_TURN,
                                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, snd);
                        }
                    }
                }
            }

            if (ret == DialogResult.Yes && keptCount != 0)
            { // kept dice during round
                for (int i = 0; i < NUM_DICE; i++) dice[i] = (int)keptDice[i].Face;
                if (!farkleEng.validateKeptDice(dice, count42.Checked))
                {
                    if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                    MsgBox.Show(this, FARKLE_BAD_DICE, this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information, snd);
                    ret = DialogResult.Cancel;
                }
                else
                { // potentially add to score
                    if (strictScoring.Checked && keptScore < DEF_MIN_SCORE)
                    {
                        if (soundsOn.Checked) snd = MessageBoxIcon.Question;
                        ret = MsgBox.Show(this, "You haven't made the minimum score (" + DEF_MIN_SCORE +
                            "). " + FARKLE_END_TURN, this.Text, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, snd);
                    }
                    if (ret == DialogResult.Yes)
                    {
                        if (!strictScoring.Checked || keptScore >= DEF_MIN_SCORE)
                        {
                            playerScores[currentPlayer] += keptScore;
                            if (keptScore > mostPointsInTurn) mostPointsInTurn = keptScore;
                        }
                    }
                }
            }

            if (ret == DialogResult.Yes)
            {
                if (fSoundLoaded && soundsOn.Checked && keptCount == 0)
                {
                    farkleSound.Play(); WaitTimer(750);
                }
                if (verbose.Checked)
                {
                    if (keptCount > 0)
                        ShowScoreDialog();
                    else
                    {
                        if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                        MsgBox.Show(this, FARKLE_MSG, this.Text, MessageBoxButtons.OK,
                            MessageBoxIcon.Information, snd);
                    }
                }
                DoEvent(NextPlayer);
            }
        }

        private void mnuStats_Click(object sender, EventArgs e)
        {
            stats.ShowStatistics(this);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();

            about.ShowDialog(this);
            about.Dispose();
        }

        private void timer_OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timedOut = true;
            // timer.Stop();
        }

        private void scoreTo_Leave(object sender, EventArgs e)
        {
            winScore = (int)scoreTo.Value;
        }
        #endregion

        // --------------------------------------------------------------------

        #region Custom Event Handler Methods
        private void CustomStartRound(object sender, EventArgs e)
        {
            keepBtn.Enabled = false; rollDice.Enabled = true;
            endTurn.Enabled = true; endTurn.Focus();
        }

        private void CustomCompMove(object sender, EventArgs e)
        {
            int[] cDice = new int[NUM_DICE], ckDice = new int[NUM_DICE];
            FarkleGame.CM_Return ret = new FarkleGame.CM_Return(true, 0);
            int n = 6, tally = 0, wp = 0;

            keepBtn.Enabled = false; rollDice.Enabled = false; endTurn.Enabled = false;
            disableSomeEvents = true;
            if (firstPlayerWin > -1)
            {
                wp = playerScores[firstPlayerWin] - playerScores[currentPlayer] + 50;
                if (strictScoring.Checked && wp < DEF_MIN_SCORE) wp = DEF_MIN_SCORE;
            }

            while (ret.rollAgain)
            {
                ClearKept(!strictScoring.Checked && n == 6);
                if (dSoundLoaded && soundsOn.Checked)
                {
                    diceSound.Play(); WaitTimer(750);
                }
                if (sortRolls.Checked) farkleEng.rollDiceSorted(cDice, n); else farkleEng.rollDice(cDice, n);
                DisplayDice(cDice);
                WaitTimer(500);
                ret = farkleEng.playCompMove(cDice, ckDice, ret.keptScore, n, firstPlayerWin, wp,
                    strictScoring.Checked, count42.Checked);
                keptScore = ret.keptScore;
                n = DisplayCompMove(ckDice);
                if (ret.keptScore == 0)
                { // Farkle!
                    if (fSoundLoaded && soundsOn.Checked)
                    {
                        farkleSound.Play(); WaitTimer(750);
                    }
                    if (verbose.Checked)
                    { // show message
                        MessageBoxIcon snd = MessageBoxIcon.None;
                        if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                        MsgBox.Show(this, FARKLE_MSG, this.Text, MessageBoxButtons.OK,
                            MessageBoxIcon.Information, snd);
                    }
                    else
                    { // pause
                        WaitTimer(2000);
                    }
                }
                else
                { // pause
                    WaitTimer(2000);
                }
                if (ret.rollAgain && n == 6 && !strictScoring.Checked)
                {
                    playerScores[currentPlayer] += ret.keptScore;
                    tally += ret.keptScore;
                    DisplayPlayerPanel();
                }
            }
            if (ret.keptScore > 0) playerScores[currentPlayer] += ret.keptScore;
            if (tally > 0) keptScore += tally;
            if (keptScore > mostPointsInTurn) mostPointsInTurn = keptScore;
            if (verbose.Checked) ShowScoreDialog();
            disableSomeEvents = false;
            DoEvent(NextPlayer);
        }

        private void CustomNextPlayer(object sender, EventArgs e)
        {
            MessageBoxIcon snd = MessageBoxIcon.None;

            ClearRolled();
            ClearKept(true);
            keptCount = 0;
            if (firstPlayerWin == -1 && playerScores[currentPlayer] >= winScore) firstPlayerWin = currentPlayer;
            // continue moving to next player
            currentPlayer++;
            if (currentPlayer > (numPlayers - 1)) currentPlayer = 0;
            if (currentPlayer == firstPlayerWin)
            { // have a winner (someone, may be current player)
                keepBtn.Enabled = false; rollDice.Enabled = false; endTurn.Enabled = false;
                winningPlayer = 0; firstPlayerWin = -1; currentPlayer = -1;
                // who won?
                for (int i = 1; i < numPlayers; i++)
                    if (playerScores[i] > playerScores[winningPlayer]) winningPlayer = i;
                if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                MsgBox.Show(this, players[winningPlayer] + " has won!", this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, snd);
                FinishStatistics();
                newGame.Focus();
            }
            DisplayPlayerPanel();
            if (winningPlayer == -1)
            { // no winner yet, move to next player
                if (verbose.Checked)
                {
                    if (soundsOn.Checked) snd = MessageBoxIcon.Information;
                    MsgBox.Show(this, "It's " + players[currentPlayer] + " turn.", this.Text, 
                        MessageBoxButtons.OK, MessageBoxIcon.Information, snd);
                }
                if (compPlayerTwo && currentPlayer == 1)
                {
                    DoEvent(CompMove);
                }
                else
                {
                    keepBtn.Enabled = false; rollDice.Enabled = true; endTurn.Enabled = true;
                    rollDice.Focus();
                }
            }
        }
        #endregion
    }
}
