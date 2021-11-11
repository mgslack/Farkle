using System;
using System.Collections.Specialized;
using System.Media;
using System.Windows.Forms;

/*
 * Dialog to get player names from user.  If only one player name entered,
 * will assume computer will play a second player.
 * 
 * ----------------------------------------------------------------------------
 * 
 * REVISED: 2021-01-28 - Commented out 'initialization' of user name array, not
 *                       needed and was leading to a bug with names from registry.
 * 
 */
namespace Farkle
{
    public partial class PlayersDlg : Form
    {
        #region Properties
        private string[] _players = new string[MainWin.MAX_PLAYERS];
        public string[] Players { get { return _players; } set { _players = value; } }

        private int _numPlayers = 0;
        public int NumPlayers { get { return _numPlayers; } }

        private bool _soundsOn = true;
        public bool SoundsOn { set { _soundsOn = value; } }

        private bool _compPlayer = false;
        public bool CompPlayer { get { return _compPlayer; } }

        private StringCollection _compPlayerNames = new StringCollection();
        public StringCollection CompPlayerNames { set { _compPlayerNames = value; } }
        #endregion

        // -------------------------------------------------------------------------

        #region Private Instances
        private TextBox[] playerArr = new TextBox[MainWin.MAX_PLAYERS];
        #endregion

        // -------------------------------------------------------------------------

        public PlayersDlg()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------------

        #region Event Handlers
        private void PlayersDlg_Load(object sender, EventArgs e)
        {
            playerArr[0] = player1; playerArr[1] = player2; playerArr[2] = player3;
            playerArr[3] = player4; playerArr[4] = player5; playerArr[5] = player6;

            //for (int i = 0; i < MainWin.MAX_PLAYERS; i++)
            //    _players[i] = "";
        }

        private void PlayersDlg_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < MainWin.MAX_PLAYERS; i++)
                playerArr[i].Text = _players[i];

            player1.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string errMsg = "";

            for (int i = 0; i < MainWin.MAX_PLAYERS; i++)
            {
                _players[i] = playerArr[i].Text.Trim();
            }

            // check for first player name
            if ("".Equals(_players[0]))
            {
                errMsg = "Must enter a name in at least the first field.";
                playerArr[0].Focus();
            }

            // check to make sure no skips
            if ("".Equals(errMsg))
            {
                for (int i = MainWin.MAX_PLAYERS -1; i >= 1; i--)
                {
                    if (!"".Equals(_players[i]) && "".Equals(_players[i-1])) {
                        errMsg = "Cannot skip name fields when entering players names.";
                        playerArr[i].Focus();
                        break;
                    }
                }
            }

            // check to see if computer playing - only one name entered
            if ("".Equals(errMsg) && "".Equals(_players[1]))
            {
                Random rnd = new Random();
                int i = rnd.Next(_compPlayerNames.Count);

                _compPlayer = true;
                _players[1] = _compPlayerNames[i];
            }

            // get number of players, will be at least two
            _numPlayers = 2;
            for (int i = MainWin.MAX_PLAYERS - 1; i >= 2; i--)
            {
                if (!"".Equals(_players[i]))
                {
                    _numPlayers = i + 1; break;
                }
            }

            // return or show error
            if ("".Equals(errMsg))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                if (_soundsOn) SystemSounds.Beep.Play();
                MessageBox.Show(errMsg, "Players Names Error");
                DialogResult = DialogResult.None;
            }
        }
        #endregion
    }
}
