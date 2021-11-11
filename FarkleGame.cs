using System;
using Dice;

/*
 * Class with Farkle scoring rules and other utility methods to allow for
 * validation and computer player.
 * 
 * ----------------------------------------------------------------------------
 * 
 * REVISED: yyyy-mm-dd - xxxxx.
 * 
 */
namespace Farkle
{
    public class FarkleGame
    {
        #region Private Constants
        private const int CM_MIN_POINTS = 300;   // comp move: min points to stop if no default maximum
        private const int CM_STOP_POINTS = 1000; // comp move: stop rolling if greater than this score
        private const int CNT_SZ = 7;            // size of array to count dice face in
        #endregion

        // -------------------------------------------------------------------------

        #region Comp Move Scruct
        public struct CM_Return
        {
            public CM_Return(bool rAgain, int kScore)
            {
                rollAgain = rAgain; keptScore = kScore;
            }

            public bool rollAgain { get; set; }
            public int keptScore { get; set; }
        }
        #endregion

        // -------------------------------------------------------------------------

        #region Private Methods
        private int scoreOneOrFive(int die)
        {
            int ret = 0;

            if (die == 1) ret = 100; else if (die == 5) ret = 50;

            return ret;
        }

        /*
         * Method can have dice sorted or not
         */
        private int scoreThreeDice(int[] dice)
        {
            int ret = 0;

            if (dice[0] == dice[1] && dice[1] == dice[2])
            { // three of a kind
                if (dice[0] == 1) ret = 300; else ret = dice[0] * 100;
            }
            else
            { // just 1's and 5's
                for (int i = 0; i < 3; i++) ret += scoreOneOrFive(dice[i]);
            }

            return ret;
        }

        /*
         * Method assumes dice have been sorted
         */
        private int scoreFourDice(int[] dice)
        {
            int ret = 0, c1;

            if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3])
            { // four of a kind
                ret = 1000;
            }
            else
            {
                if ((dice[0] == dice[1] && dice[1] == dice[2]) ||
                    (dice[1] == dice[2] && dice[2] == dice[3]))
                { // three of a kind plus 1 or 5
                    if (dice[1] == 1) ret = 300; else ret = dice[1] * 100;
                    if (dice[0] == dice[1]) c1 = dice[3]; else c1 = dice[0];
                    ret += scoreOneOrFive(c1);
                }
                else
                { // only 1's and 5's
                    for (int i = 0; i < 4; i++) ret += scoreOneOrFive(dice[i]);
                }
            }

            return ret;
        }

        /*
         * Method assumes dice have been sorted
         */
        private int scoreFiveDice(int[] dice)
        {
            int ret = 0, c1, c2;

            if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4])
            { // five of a kind
                ret = 2000;
            }
            else
            {
                if ((dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3]) ||
                    (dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4]))
                { // 4 of a kind, plus 1's and 5's
                    ret = 1000;
                    if (dice[0] == dice[1]) c1 = dice[4]; else c1 = dice[0];
                    ret += scoreOneOrFive(c1);
                }
                else
                {
                    if ((dice[0] == dice[1] && dice[1] == dice[2]) ||
                        (dice[1] == dice[2] && dice[2] == dice[3]) ||
                        (dice[2] == dice[3] && dice[3] == dice[4]))
                    { // three of a kind plus 1's and 5's
                        if (dice[2] == 1) ret = 300; else ret = dice[2] * 100;
                        if (dice[0] == dice[2])
                        {
                            c1 = dice[3]; c2 = dice[4];
                        }
                        else if (dice[2] == dice[4])
                        {
                            c1 = dice[0]; c2 = dice[1];
                        }
                        else
                        {
                            c1 = dice[0]; c2 = dice[4];
                        }
                        ret += scoreOneOrFive(c1) + scoreOneOrFive(c2);
                    }
                    else
                    { // 1's and 5's plus junk????
                        for (int i = 0; i < 5; i++) ret += scoreOneOrFive(dice[i]);
                    }
                }
            }

            return ret;
        }

        /*
         * Method assumes dice have been sorted
         */
        private int scoreSixDice(int[] dice, bool ft_Flg)
        {
            int ret = 0, c1 = 0, c2 = 0, c3 = 0;

            if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] &&
                dice[3] == dice[4] && dice[4] == dice[5])
            {
                ret = 3000;
            }
            else if (dice[0] == dice[1] && dice[2] == dice[3] && dice[4] == dice[5])
            { // 3 pair or 4 of a kind plus pair
                if ((dice[1] == dice[2] || dice[3] == dice[4]) && (!ft_Flg))
                {
                    ret = 1000;
                    if (dice[1] == dice[2])
                    {
                        c1 = dice[4]; c2 = dice[5];
                    }
                    else
                    {
                        c1 = dice[0]; c2 = dice[1];
                    }
                    ret += scoreOneOrFive(c1) + scoreOneOrFive(c2);
                }
                else ret = 1500; // 3 pair or counting 4 of kind plus pair as 3 pair
            }
            else if ((dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4]) ||
                     (dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4] && dice[4] == dice[5]))
            { // five of a kind, plus 1 or 5
                ret = 2000;
                if (dice[0] == dice[1]) c1 = dice[5]; else c1 = dice[0];
                ret += scoreOneOrFive(c1);
            }
            else if (dice[0] == 1 && dice[1] == 2 && dice[2] == 3 && dice[3] == 4 && dice[4] == 5 && dice[5] == 6)
            { // straight
                ret = 1500;
            }
            else if (dice[0] == dice[1] && dice[1] == dice[2] && dice[3] == dice[4] && dice[4] == dice[5])
            { // two triples
                ret = 2500;
            }
            else
            { // 3 or 4 of kind, with 1's and 5's?
                if ((dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3]) ||
                    (dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4]) ||
                    (dice[2] == dice[3] && dice[3] == dice[4] && dice[4] == dice[5]))
                { // four of a kind
                    ret = 1000;
                    if (dice[1] != dice[2])
                    {
                        c1 = dice[0]; c2 = dice[1];
                    }
                    else if (dice[3] != dice[4])
                    {
                        c1 = dice[4]; c2 = dice[5];
                    }
                    else
                    {
                        c1 = dice[0]; c2 = dice[5];
                    }
                    ret += scoreOneOrFive(c1) + scoreOneOrFive(c2);
                }
                else if ((dice[1] == dice[2] && dice[2] == dice[3]) ||
                         (dice[2] == dice[3] && dice[3] == dice[4]) ||
                         (dice[3] == dice[4] && dice[4] == dice[5]))
                { // 3 of a kind plus 1's and 5's
                    // NOTE: first die [0] cannot be part of a three of a kind for 6 valid dice
                    ret = dice[3] * 100;
                    c1 = dice[0];
                    if (dice[1] == dice[3])
                    {
                        c2 = dice[4]; c3 = dice[5];
                    }
                    else if (dice[2] == dice[4])
                    {
                        c2 = dice[1]; c3 = dice[5];
                    }
                    else
                    {
                        c2 = dice[1]; c3 = dice[2];
                    }
                    ret += scoreOneOrFive(c1) + scoreOneOrFive(c2) + scoreOneOrFive(c3);
                }
                else
                { // 1's and 5's and junk
                    for (int i = 0; i < MainWin.NUM_DICE; i++) ret += scoreOneOrFive(dice[i]);
                }
            }

            return ret;
        }
        #endregion

        // -------------------------------------------------------------------------

        #region Private Comp Move Methods
        private int[] cm_ScoreOneDie(int[] dice, int[] kept, int idx, int pts, int kscore, int used)
        {
            int[] ret = new int[2] { kscore, used };

            ret[0] += pts;
            ret[1]++;
            kept[idx] = dice[idx];

            return ret;
        }

        private int[] cm_ScoreThreeDie(int[] dice, int[] kept, int kscore, int used)
        {
            int[] ret = new int[2] { kscore, used };
            int ts1, ts2, ts3;

            if (validateKeptDice(dice, false))
            { // all three scoring die
                ret[0] += scoreThreeDice(dice);
                ret[1] = 3;
                for (int i = 0; i < 3; i++) kept[i] = dice[i];
            }
            else
            { // any scoring die???
                ts1 = scoreOneOrFive(dice[0]);
                ts2 = scoreOneOrFive(dice[1]);
                ts3 = scoreOneOrFive(dice[2]);
                if ((ts1 + ts2 + ts3) == 0)
                { // no scoring die
                    ret[0] = 0;
                }
                else
                { // add up any scoring die
                    if (ts1 > 0) ret = cm_ScoreOneDie(dice, kept, 0, ts1, ret[0], ret[1]);
                    if (ts2 > 0) ret = cm_ScoreOneDie(dice, kept, 1, ts2, ret[0], ret[1]);
                    if (ts3 > 0) ret = cm_ScoreOneDie(dice, kept, 2, ts3, ret[0], ret[1]);
                }
            }

            return ret;
        }

        private int[] cm_ScoreFourDie(int[] sdice, int[] dice, int[] kept, int kscore, int used)
        {
            int[] ret = new int[2] { kscore, used };
            int isc, kp;

            isc = scoreFourDice(sdice);
            if (isc == 0)
            { // no score in the four die
                ret[0] = 0;
            }
            else
            {
                ret[0] += isc;
                if (validateKeptDice(dice, false))
                { // all four are scoring die
                    ret[1] = 4;
                    for (int i = 0; i < 4; i++) kept[i] = dice[i];
                }
                else
                { // what die scored??
                    kp = 66;
                    if ((sdice[0] == sdice[1] && sdice[1] == sdice[2]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3]))
                    { // had a three of a kind
                        kp = sdice[1];
                    }
                    for (int i = 0; i < 4; i++)
                    { // keep the scoring die
                        if (dice[i] == 1 || dice[i] == 5 || dice[i] == kp)
                        {
                            ret[1]++;
                            kept[i] = dice[i];
                        }
                    }
                }
            }

            return ret;
        }

        private int[] cm_ScoreFiveDie(int[] sdice, int[] dice, int[] kept, int kscore, int used)
        {
            int[] ret = new int[2] { kscore, used };
            int isc, kp;

            isc = scoreFiveDice(sdice);
            if (isc == 0)
            { // no score in the five die
                ret[0] = 0;
            }
            else
            {
                ret[0] += isc;
                if (validateKeptDice(dice, false))
                { // all five die good
                    ret[1] = 5;
                    for (int i = 0; i < 5; i++) kept[i] = dice[i];
                }
                else
                { // what die scored??
                    kp = 66;
                    if ((sdice[0] == sdice[1] && sdice[1] == sdice[2] && sdice[2] == sdice[3]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3] && sdice[3] == sdice[4]) ||
                        (sdice[0] == sdice[1] && sdice[1] == sdice[2]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3]) ||
                        (sdice[2] == sdice[3] && sdice[3] == sdice[4]))
                    { // had 3 or 4 of a kind
                        kp = sdice[2];
                    }
                    for (int i = 0; i < 5; i++)
                    { // kepp the scoring die
                        if (dice[i] == 1 || dice[i] == 5 || dice[i] == kp)
                        {
                            ret[1]++;
                            kept[i] = dice[i];
                        }
                    }
                }
            }

            return ret;
        }

        private int[] cm_ScoreSixDie(int[] sdice, int[] dice, int[] kept, int kscore, int used, bool fourTwo)
        {
            int[] ret = new int[2] { kscore, used };
            int isc, kp = 0;

            isc = scoreSixDice(sdice, fourTwo);
            // check one special case (three of a kind with first three dice)
            //  - this is not scored in ScoreSixDice since it is not valid
            //    for all six dice normally
            //  - checking for 1's, but really only 2's, 3's and 4's are
            //    the problem
            //  - may have 1 or 2 fives also, so check if ISc is <= 100 too
            if (isc <= 100 && sdice[0] != 1 && sdice[0] == sdice[1] && sdice[1] == sdice[2])
            { // three of a kind and 1 or 2 fives
                kp = isc; isc = 0;
            }
            if (isc == 0 && sdice[0] == sdice[1] && sdice[1] == sdice[2])
            { // three of a kind leading off
                if (sdice[0] == 1) isc = 300; else isc = sdice[0] * 100;
                isc += kp;
            }

            // rest of the score calculations
            if (isc == 0)
            { // no score in the six die!!!! (natural Farkle)
                ret[0] = 0;
            }
            else
            {
                ret[0] += isc;
                if (validateKeptDice(dice, fourTwo))
                { // all six die score
                    ret[1] = 6;
                    for (int i = 0; i < 6; i++) kept[i] = dice[i];
                }
                else
                { // what die scored then
                    kp = 66;
                    if ((sdice[0] == sdice[1] && sdice[1] == sdice[2] && sdice[2] == sdice[3] && sdice[3] == sdice[4]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3] && sdice[3] == sdice[4] && sdice[4] == sdice[5]) ||
                        (sdice[0] == sdice[1] && sdice[1] == sdice[2] && sdice[2] == sdice[3]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3] && sdice[3] == sdice[4]) ||
                        (sdice[2] == sdice[3] && sdice[3] == sdice[4] && sdice[4] == sdice[5]) ||
                        (sdice[0] == sdice[1] && sdice[1] == sdice[2]) ||
                        (sdice[1] == sdice[2] && sdice[2] == sdice[3]) ||
                        (sdice[2] == sdice[3] && sdice[3] == sdice[4]) ||
                        (sdice[3] == sdice[4] && sdice[4] == sdice[5]))
                    { // have 3, 4 or 5 of a kind
                        kp = sdice[2];
                        // need to check if last three equal and use 4th die versus 3rd
                        if (sdice[3] == sdice[4] && sdice[4] == sdice[5]) kp = sdice[3];
                    }
                    for (int i = 0; i < 6; i++)
                    { // keep the scoring die
                        if (dice[i] == 1 || dice[i] == 5 || dice[i] == kp)
                        {
                            ret[1]++;
                            kept[i] = dice[i];
                        }
                    }
                }
            }

            return ret;
        }
        #endregion

        // -------------------------------------------------------------------------

        #region Public Methods
        public void rollDice(int[] dice, int numToRoll)
        {
            Die d = new Die();

            for (int i = 0; i < MainWin.NUM_DICE; i++) dice[i] = 0;
            for (int i = 0; i < numToRoll; i++)
            {
                d.RollDie();
                dice[i] = (int)d.Face;
            }
        }

        public void rollDiceSorted(int[] dice, int numToRoll)
        {
            rollDice(dice, numToRoll);
            for (int i = numToRoll; i < MainWin.NUM_DICE; i++) dice[i] = 99;
            Array.Sort(dice);
            for (int i = numToRoll; i < MainWin.NUM_DICE; i++) dice[i] = 0;
        }

        public int getKeptScore(int[] dice, bool fourTwo)
        {
            int[] tDice = new int[MainWin.NUM_DICE];
            int ret = 0, n = 0;

            for (int i = 0; i < MainWin.NUM_DICE; i++) tDice[i] = dice[i];

            for (int i = 0; i < MainWin.NUM_DICE; i++)
            {
                if (tDice[i] == 0) tDice[i] = 99; else n++;
            }

            if (n != 0)
            {
                Array.Sort(tDice);
                switch (n)
                {
                    case 3:
                        ret = scoreThreeDice(tDice);
                        break;
                    case 4:
                        ret = scoreFourDice(tDice);
                        break;
                    case 5:
                        ret = scoreFiveDice(tDice);
                        break;
                    case 6:
                        ret = scoreSixDice(tDice, fourTwo);
                        break;
                    default:
                        ret = scoreOneOrFive(tDice[0]) + scoreOneOrFive(tDice[1]);
                        break;
                }
            }

            return ret;
        }

        public int getTotalScore(int[] rolledDice, int[] keptDice, bool fourTwo)
        {
            int[] dice = new int[MainWin.NUM_DICE];
            int ret = 0;

            for (int i = 0; i < MainWin.NUM_DICE; i++)
            {
                if (rolledDice[i] != 0) dice[i] = rolledDice[i]; else dice[i] = keptDice[i];
            }
            ret = getKeptScore(dice, fourTwo);

            return ret;
        }

        public bool validateKeptDice(int[] dice, bool fourTwo)
        {
            int[] chkA = new int[CNT_SZ];
            bool ret = true;
            int n = 0, c = 0;

            for (int i = 0; i < CNT_SZ; i++) chkA[i] = 0;
            for (int i = 0; i < MainWin.NUM_DICE; i++)
            {
                if (dice[i] != 0)
                {
                    n++; chkA[dice[i]]++;
                }
            }

            if ((n != 0) && (chkA[2] != 0 || chkA[3] != 0 || chkA[4] != 0 || chkA[6] != 0))
            { // only need to check for 2, 3, 4 and 6's
                if ((n != 6) &&
                    ((chkA[2] > 0 && chkA[2] < 3) ||
                     (chkA[3] > 0 && chkA[3] < 3) ||
                     (chkA[4] > 0 && chkA[4] < 3) ||
                     (chkA[6] > 0 && chkA[6] < 3)))
                {
                    ret = false;
                }
                if (n == 6)
                {
                    for (int i = 0; i < CNT_SZ; i++) if (chkA[i] == 2) c++;
                    if (c != 3 && c == 1)
                    {  // check for 4/2 (three pair)
                        for (int i = 0; i < CNT_SZ; i++) if (chkA[i] == 4 && fourTwo) c = 3;
                    }
                    if (c != 3)
                    {  // not three pair, check triplet
                        c = 0;
                        for (int i = 0; i < CNT_SZ; i++) if (chkA[i] == 3) c++;
                        if (c != 2)
                        { // check for straight
                            c = 0;
                            for (int i = 0; i < CNT_SZ; i++) if (chkA[i] == 1) c++;
                            if (c != 6)
                            { // check for 3, 4, 5 of kind
                                if ((chkA[2] > 0 && chkA[2] < 3) ||
                                    (chkA[3] > 0 && chkA[3] < 3) ||
                                    (chkA[4] > 0 && chkA[4] < 3) ||
                                    (chkA[6] > 0 && chkA[6] < 3))
                                {
                                    ret = false;
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }

        public CM_Return playCompMove(int[] dice, int[] kept, int kscore, int n, int fpw, int wp,
            bool strictF, bool fourTwo)
        {
            CM_Return ret = new CM_Return(false, kscore);
            int[] sortedDice = new int[MainWin.NUM_DICE];
            int[] intRet = new int[2] { kscore, 0 }; // kscore, used
            int used = 0, left, ts1, ts2;

            for (int i = 0; i < MainWin.NUM_DICE; i++) kept[i] = 0;

            if (n > 3)
            { // sort dice in temp set
                for (int i = 0; i < MainWin.NUM_DICE; i++)
                {
                    if (dice[i] == 0) sortedDice[i] = 99; else sortedDice[i] = dice[i];
                }
                Array.Sort(sortedDice);
            }

            switch (n)
            { // score number of die rolled
                case 3:
                    intRet = cm_ScoreThreeDie(dice, kept, kscore, used);
                    break;
                case 4:
                    intRet = cm_ScoreFourDie(sortedDice, dice, kept, kscore, used);
                    break;
                case 5:
                    intRet = cm_ScoreFiveDie(sortedDice, dice, kept, kscore, used);
                    break;
                case 6:
                    intRet = cm_ScoreSixDie(sortedDice, dice, kept, kscore, used, fourTwo);
                    break;
                default:
                    ts1 = scoreOneOrFive(dice[0]);
                    ts2 = scoreOneOrFive(dice[1]);
                    if ((ts1 + ts2) == 0)
                    {
                        intRet[0] = 0;
                    }
                    else
                    {
                        if (ts1 > 0) intRet = cm_ScoreOneDie(dice, kept, 0, ts1, intRet[0], intRet[1]);
                        if (ts2 > 0) intRet = cm_ScoreOneDie(dice, kept, 1, ts2, intRet[0], intRet[1]);
                    }
                    break;
            }
            ret.keptScore = intRet[0];
            ret.rollAgain = intRet[1] > 0;

            // rules to check if comp scored, if to roll more or not
            if (ret.rollAgain)
            {
                if (fpw > -1)
                { // have winner all ready, check to see if beat them
                    if (ret.keptScore >= wp) ret.rollAgain = false; // no need to roll again, beat them
                }
                else
                {
                    left = n - intRet[1]; // have die left over after scoring
                    if (left == 1 || left == 2)
                    {
                        if (strictF) ts1 = MainWin.DEF_MIN_SCORE; else ts1 = CM_MIN_POINTS;
                        ret.rollAgain = ret.keptScore < ts1;
                    }
                    else if (left >= 3 && ret.keptScore >= CM_STOP_POINTS) // stop rolling
                        ret.rollAgain = false;
                }
            }

            return ret;
        }
        #endregion
    }
}
