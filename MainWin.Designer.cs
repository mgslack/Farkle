namespace Farkle
{
    partial class MainWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.rDicePnl = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rolled6 = new Dice.Die();
            this.rolled5 = new Dice.Die();
            this.rolled4 = new Dice.Die();
            this.rolled3 = new Dice.Die();
            this.rolled2 = new Dice.Die();
            this.rolled1 = new Dice.Die();
            this.kDicePnl = new System.Windows.Forms.Panel();
            this.keptScoreLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kept6 = new Dice.Die();
            this.kept5 = new Dice.Die();
            this.kept4 = new Dice.Die();
            this.kept3 = new Dice.Die();
            this.kept2 = new Dice.Die();
            this.kept1 = new Dice.Die();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scoreTo = new System.Windows.Forms.NumericUpDown();
            this.scoreToLbl = new System.Windows.Forms.Label();
            this.safeMode = new System.Windows.Forms.CheckBox();
            this.soundsOn = new System.Windows.Forms.CheckBox();
            this.sortRolls = new System.Windows.Forms.CheckBox();
            this.count42 = new System.Windows.Forms.CheckBox();
            this.strictScoring = new System.Windows.Forms.CheckBox();
            this.verbose = new System.Windows.Forms.CheckBox();
            this.newGame = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.helpBtn = new System.Windows.Forms.Button();
            this.rollDice = new System.Windows.Forms.Button();
            this.endTurn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.player6 = new System.Windows.Forms.Label();
            this.player5 = new System.Windows.Forms.Label();
            this.player4 = new System.Windows.Forms.Label();
            this.player3 = new System.Windows.Forms.Label();
            this.player2 = new System.Windows.Forms.Label();
            this.player1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.keepBtn = new System.Windows.Forms.Button();
            this.rDicePnl.SuspendLayout();
            this.kDicePnl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rDicePnl
            // 
            this.rDicePnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rDicePnl.Controls.Add(this.keepBtn);
            this.rDicePnl.Controls.Add(this.label1);
            this.rDicePnl.Controls.Add(this.rolled6);
            this.rDicePnl.Controls.Add(this.rolled5);
            this.rDicePnl.Controls.Add(this.rolled4);
            this.rDicePnl.Controls.Add(this.rolled3);
            this.rDicePnl.Controls.Add(this.rolled2);
            this.rDicePnl.Controls.Add(this.rolled1);
            this.rDicePnl.Location = new System.Drawing.Point(22, 21);
            this.rDicePnl.Name = "rDicePnl";
            this.rDicePnl.Size = new System.Drawing.Size(646, 83);
            this.rDicePnl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Rolled Dice";
            // 
            // rolled6
            // 
            this.rolled6.BackColor = System.Drawing.Color.Teal;
            this.rolled6.BorderColor = System.Drawing.Color.Black;
            this.rolled6.CornerRadius = 6;
            this.rolled6.Face = Dice.DieFace.dfn0;
            this.rolled6.ForeColor = System.Drawing.Color.Black;
            this.rolled6.Location = new System.Drawing.Point(543, 4);
            this.rolled6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled6.MaxFaces = Dice.DieFace.dfn6;
            this.rolled6.Name = "rolled6";
            this.rolled6.Rotation = Dice.DieRotation.Default;
            this.rolled6.RoundedCorners = true;
            this.rolled6.Size = new System.Drawing.Size(72, 73);
            this.rolled6.TabIndex = 5;
            this.rolled6.Tag = "5";
            this.rolled6.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // rolled5
            // 
            this.rolled5.BackColor = System.Drawing.Color.Teal;
            this.rolled5.BorderColor = System.Drawing.Color.Black;
            this.rolled5.CornerRadius = 6;
            this.rolled5.Face = Dice.DieFace.dfn0;
            this.rolled5.ForeColor = System.Drawing.Color.Black;
            this.rolled5.Location = new System.Drawing.Point(457, 4);
            this.rolled5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled5.MaxFaces = Dice.DieFace.dfn6;
            this.rolled5.Name = "rolled5";
            this.rolled5.Rotation = Dice.DieRotation.Default;
            this.rolled5.RoundedCorners = true;
            this.rolled5.Size = new System.Drawing.Size(72, 73);
            this.rolled5.TabIndex = 4;
            this.rolled5.Tag = "4";
            this.rolled5.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // rolled4
            // 
            this.rolled4.BackColor = System.Drawing.Color.Teal;
            this.rolled4.BorderColor = System.Drawing.Color.Black;
            this.rolled4.CornerRadius = 6;
            this.rolled4.Face = Dice.DieFace.dfn0;
            this.rolled4.ForeColor = System.Drawing.Color.Black;
            this.rolled4.Location = new System.Drawing.Point(370, 4);
            this.rolled4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled4.MaxFaces = Dice.DieFace.dfn6;
            this.rolled4.Name = "rolled4";
            this.rolled4.Rotation = Dice.DieRotation.Default;
            this.rolled4.RoundedCorners = true;
            this.rolled4.Size = new System.Drawing.Size(72, 73);
            this.rolled4.TabIndex = 3;
            this.rolled4.Tag = "3";
            this.rolled4.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // rolled3
            // 
            this.rolled3.BackColor = System.Drawing.Color.Teal;
            this.rolled3.BorderColor = System.Drawing.Color.Black;
            this.rolled3.CornerRadius = 6;
            this.rolled3.Face = Dice.DieFace.dfn0;
            this.rolled3.ForeColor = System.Drawing.Color.Black;
            this.rolled3.Location = new System.Drawing.Point(281, 4);
            this.rolled3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled3.MaxFaces = Dice.DieFace.dfn6;
            this.rolled3.Name = "rolled3";
            this.rolled3.Rotation = Dice.DieRotation.Default;
            this.rolled3.RoundedCorners = true;
            this.rolled3.Size = new System.Drawing.Size(72, 73);
            this.rolled3.TabIndex = 2;
            this.rolled3.Tag = "2";
            this.rolled3.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // rolled2
            // 
            this.rolled2.BackColor = System.Drawing.Color.Teal;
            this.rolled2.BorderColor = System.Drawing.Color.Black;
            this.rolled2.CornerRadius = 6;
            this.rolled2.Face = Dice.DieFace.dfn0;
            this.rolled2.ForeColor = System.Drawing.Color.Black;
            this.rolled2.Location = new System.Drawing.Point(192, 4);
            this.rolled2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled2.MaxFaces = Dice.DieFace.dfn6;
            this.rolled2.Name = "rolled2";
            this.rolled2.Rotation = Dice.DieRotation.Default;
            this.rolled2.RoundedCorners = true;
            this.rolled2.Size = new System.Drawing.Size(72, 73);
            this.rolled2.TabIndex = 1;
            this.rolled2.Tag = "1";
            this.rolled2.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // rolled1
            // 
            this.rolled1.BackColor = System.Drawing.Color.Teal;
            this.rolled1.BorderColor = System.Drawing.Color.Black;
            this.rolled1.CornerRadius = 6;
            this.rolled1.Face = Dice.DieFace.dfn0;
            this.rolled1.ForeColor = System.Drawing.Color.Black;
            this.rolled1.Location = new System.Drawing.Point(107, 4);
            this.rolled1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rolled1.MaxFaces = Dice.DieFace.dfn6;
            this.rolled1.Name = "rolled1";
            this.rolled1.Rotation = Dice.DieRotation.Default;
            this.rolled1.RoundedCorners = true;
            this.rolled1.Size = new System.Drawing.Size(72, 73);
            this.rolled1.TabIndex = 0;
            this.rolled1.Tag = "0";
            this.rolled1.Click += new System.EventHandler(this.rolledX_Click);
            // 
            // kDicePnl
            // 
            this.kDicePnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kDicePnl.Controls.Add(this.keptScoreLbl);
            this.kDicePnl.Controls.Add(this.label2);
            this.kDicePnl.Controls.Add(this.kept6);
            this.kDicePnl.Controls.Add(this.kept5);
            this.kDicePnl.Controls.Add(this.kept4);
            this.kDicePnl.Controls.Add(this.kept3);
            this.kDicePnl.Controls.Add(this.kept2);
            this.kDicePnl.Controls.Add(this.kept1);
            this.kDicePnl.Location = new System.Drawing.Point(22, 110);
            this.kDicePnl.Name = "kDicePnl";
            this.kDicePnl.Size = new System.Drawing.Size(646, 83);
            this.kDicePnl.TabIndex = 1;
            // 
            // keptScoreLbl
            // 
            this.keptScoreLbl.AutoSize = true;
            this.keptScoreLbl.Location = new System.Drawing.Point(16, 34);
            this.keptScoreLbl.Name = "keptScoreLbl";
            this.keptScoreLbl.Size = new System.Drawing.Size(24, 17);
            this.keptScoreLbl.TabIndex = 10;
            this.keptScoreLbl.Text = "[0]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Kept Dice";
            // 
            // kept6
            // 
            this.kept6.BackColor = System.Drawing.Color.Teal;
            this.kept6.BorderColor = System.Drawing.Color.Black;
            this.kept6.CornerRadius = 6;
            this.kept6.Face = Dice.DieFace.dfn0;
            this.kept6.ForeColor = System.Drawing.Color.Black;
            this.kept6.Location = new System.Drawing.Point(543, 4);
            this.kept6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept6.MaxFaces = Dice.DieFace.dfn6;
            this.kept6.Name = "kept6";
            this.kept6.Rotation = Dice.DieRotation.Default;
            this.kept6.RoundedCorners = true;
            this.kept6.Size = new System.Drawing.Size(72, 73);
            this.kept6.TabIndex = 6;
            this.kept6.Tag = "5";
            this.kept6.Click += new System.EventHandler(this.keptX_Click);
            // 
            // kept5
            // 
            this.kept5.BackColor = System.Drawing.Color.Teal;
            this.kept5.BorderColor = System.Drawing.Color.Black;
            this.kept5.CornerRadius = 6;
            this.kept5.Face = Dice.DieFace.dfn0;
            this.kept5.ForeColor = System.Drawing.Color.Black;
            this.kept5.Location = new System.Drawing.Point(457, 4);
            this.kept5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept5.MaxFaces = Dice.DieFace.dfn6;
            this.kept5.Name = "kept5";
            this.kept5.Rotation = Dice.DieRotation.Default;
            this.kept5.RoundedCorners = true;
            this.kept5.Size = new System.Drawing.Size(72, 73);
            this.kept5.TabIndex = 5;
            this.kept5.Tag = "4";
            this.kept5.Click += new System.EventHandler(this.keptX_Click);
            // 
            // kept4
            // 
            this.kept4.BackColor = System.Drawing.Color.Teal;
            this.kept4.BorderColor = System.Drawing.Color.Black;
            this.kept4.CornerRadius = 6;
            this.kept4.Face = Dice.DieFace.dfn0;
            this.kept4.ForeColor = System.Drawing.Color.Black;
            this.kept4.Location = new System.Drawing.Point(370, 4);
            this.kept4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept4.MaxFaces = Dice.DieFace.dfn6;
            this.kept4.Name = "kept4";
            this.kept4.Rotation = Dice.DieRotation.Default;
            this.kept4.RoundedCorners = true;
            this.kept4.Size = new System.Drawing.Size(72, 73);
            this.kept4.TabIndex = 4;
            this.kept4.Tag = "3";
            this.kept4.Click += new System.EventHandler(this.keptX_Click);
            // 
            // kept3
            // 
            this.kept3.BackColor = System.Drawing.Color.Teal;
            this.kept3.BorderColor = System.Drawing.Color.Black;
            this.kept3.CornerRadius = 6;
            this.kept3.Face = Dice.DieFace.dfn0;
            this.kept3.ForeColor = System.Drawing.Color.Black;
            this.kept3.Location = new System.Drawing.Point(281, 4);
            this.kept3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept3.MaxFaces = Dice.DieFace.dfn6;
            this.kept3.Name = "kept3";
            this.kept3.Rotation = Dice.DieRotation.Default;
            this.kept3.RoundedCorners = true;
            this.kept3.Size = new System.Drawing.Size(72, 73);
            this.kept3.TabIndex = 3;
            this.kept3.Tag = "2";
            this.kept3.Click += new System.EventHandler(this.keptX_Click);
            // 
            // kept2
            // 
            this.kept2.BackColor = System.Drawing.Color.Teal;
            this.kept2.BorderColor = System.Drawing.Color.Black;
            this.kept2.CornerRadius = 6;
            this.kept2.Face = Dice.DieFace.dfn0;
            this.kept2.ForeColor = System.Drawing.Color.Black;
            this.kept2.Location = new System.Drawing.Point(192, 4);
            this.kept2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept2.MaxFaces = Dice.DieFace.dfn6;
            this.kept2.Name = "kept2";
            this.kept2.Rotation = Dice.DieRotation.Default;
            this.kept2.RoundedCorners = true;
            this.kept2.Size = new System.Drawing.Size(72, 73);
            this.kept2.TabIndex = 2;
            this.kept2.Tag = "1";
            this.kept2.Click += new System.EventHandler(this.keptX_Click);
            // 
            // kept1
            // 
            this.kept1.BackColor = System.Drawing.Color.Teal;
            this.kept1.BorderColor = System.Drawing.Color.Black;
            this.kept1.CornerRadius = 6;
            this.kept1.Face = Dice.DieFace.dfn0;
            this.kept1.ForeColor = System.Drawing.Color.Black;
            this.kept1.Location = new System.Drawing.Point(107, 4);
            this.kept1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kept1.MaxFaces = Dice.DieFace.dfn6;
            this.kept1.Name = "kept1";
            this.kept1.Rotation = Dice.DieRotation.Default;
            this.kept1.RoundedCorners = true;
            this.kept1.Size = new System.Drawing.Size(72, 73);
            this.kept1.TabIndex = 1;
            this.kept1.Tag = "0";
            this.kept1.Click += new System.EventHandler(this.keptX_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scoreTo);
            this.groupBox1.Controls.Add(this.scoreToLbl);
            this.groupBox1.Controls.Add(this.safeMode);
            this.groupBox1.Controls.Add(this.soundsOn);
            this.groupBox1.Controls.Add(this.sortRolls);
            this.groupBox1.Controls.Add(this.count42);
            this.groupBox1.Controls.Add(this.strictScoring);
            this.groupBox1.Controls.Add(this.verbose);
            this.groupBox1.Location = new System.Drawing.Point(285, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 136);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // scoreTo
            // 
            this.scoreTo.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.scoreTo.Location = new System.Drawing.Point(243, 102);
            this.scoreTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.scoreTo.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.scoreTo.Name = "scoreTo";
            this.scoreTo.Size = new System.Drawing.Size(86, 22);
            this.scoreTo.TabIndex = 7;
            this.scoreTo.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.scoreTo.Leave += new System.EventHandler(this.scoreTo_Leave);
            // 
            // scoreToLbl
            // 
            this.scoreToLbl.AutoSize = true;
            this.scoreToLbl.Location = new System.Drawing.Point(167, 104);
            this.scoreToLbl.Name = "scoreToLbl";
            this.scoreToLbl.Size = new System.Drawing.Size(70, 17);
            this.scoreToLbl.TabIndex = 6;
            this.scoreToLbl.Text = "Score &To:";
            // 
            // safeMode
            // 
            this.safeMode.AutoSize = true;
            this.safeMode.Checked = true;
            this.safeMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.safeMode.Location = new System.Drawing.Point(243, 49);
            this.safeMode.Name = "safeMode";
            this.safeMode.Size = new System.Drawing.Size(98, 21);
            this.safeMode.TabIndex = 5;
            this.safeMode.Text = "Safe &Mode";
            this.safeMode.UseVisualStyleBackColor = true;
            // 
            // soundsOn
            // 
            this.soundsOn.AutoSize = true;
            this.soundsOn.Checked = true;
            this.soundsOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soundsOn.Location = new System.Drawing.Point(243, 22);
            this.soundsOn.Name = "soundsOn";
            this.soundsOn.Size = new System.Drawing.Size(101, 21);
            this.soundsOn.TabIndex = 4;
            this.soundsOn.Text = "Sounds &On";
            this.soundsOn.UseVisualStyleBackColor = true;
            // 
            // sortRolls
            // 
            this.sortRolls.AutoSize = true;
            this.sortRolls.Location = new System.Drawing.Point(12, 103);
            this.sortRolls.Name = "sortRolls";
            this.sortRolls.Size = new System.Drawing.Size(91, 21);
            this.sortRolls.TabIndex = 3;
            this.sortRolls.Text = "Sort Ro&lls";
            this.sortRolls.UseVisualStyleBackColor = true;
            // 
            // count42
            // 
            this.count42.AutoSize = true;
            this.count42.Checked = true;
            this.count42.CheckState = System.Windows.Forms.CheckState.Checked;
            this.count42.Location = new System.Drawing.Point(12, 76);
            this.count42.Name = "count42";
            this.count42.Size = new System.Drawing.Size(224, 21);
            this.count42.TabIndex = 2;
            this.count42.Text = "&Count 4 of a kind/pair as 3 pair";
            this.count42.UseVisualStyleBackColor = true;
            // 
            // strictScoring
            // 
            this.strictScoring.AutoSize = true;
            this.strictScoring.Checked = true;
            this.strictScoring.CheckState = System.Windows.Forms.CheckState.Checked;
            this.strictScoring.Location = new System.Drawing.Point(12, 49);
            this.strictScoring.Name = "strictScoring";
            this.strictScoring.Size = new System.Drawing.Size(154, 21);
            this.strictScoring.TabIndex = 1;
            this.strictScoring.Text = "&Strict Scoring Rules";
            this.strictScoring.UseVisualStyleBackColor = true;
            // 
            // verbose
            // 
            this.verbose.AutoSize = true;
            this.verbose.Location = new System.Drawing.Point(12, 22);
            this.verbose.Name = "verbose";
            this.verbose.Size = new System.Drawing.Size(151, 21);
            this.verbose.TabIndex = 0;
            this.verbose.Text = "&Verbose Messages";
            this.verbose.UseVisualStyleBackColor = true;
            // 
            // newGame
            // 
            this.newGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.newGame.Location = new System.Drawing.Point(297, 351);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(116, 25);
            this.newGame.TabIndex = 5;
            this.newGame.Text = "&New Game";
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exitBtn.Location = new System.Drawing.Point(297, 382);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(116, 25);
            this.exitBtn.TabIndex = 6;
            this.exitBtn.Text = "E&xit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // helpBtn
            // 
            this.helpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.helpBtn.Location = new System.Drawing.Point(421, 382);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(116, 25);
            this.helpBtn.TabIndex = 7;
            this.helpBtn.Text = "&Help";
            this.helpBtn.UseVisualStyleBackColor = true;
            this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
            // 
            // rollDice
            // 
            this.rollDice.Enabled = false;
            this.rollDice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rollDice.Location = new System.Drawing.Point(421, 351);
            this.rollDice.Name = "rollDice";
            this.rollDice.Size = new System.Drawing.Size(116, 25);
            this.rollDice.TabIndex = 8;
            this.rollDice.Text = "&Roll Dice";
            this.rollDice.UseVisualStyleBackColor = true;
            this.rollDice.Click += new System.EventHandler(this.rollDice_Click);
            // 
            // endTurn
            // 
            this.endTurn.Enabled = false;
            this.endTurn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.endTurn.Location = new System.Drawing.Point(543, 351);
            this.endTurn.Name = "endTurn";
            this.endTurn.Size = new System.Drawing.Size(116, 25);
            this.endTurn.TabIndex = 9;
            this.endTurn.Text = "&End Turn";
            this.endTurn.UseVisualStyleBackColor = true;
            this.endTurn.Click += new System.EventHandler(this.endTurn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.player6);
            this.panel1.Controls.Add(this.player5);
            this.panel1.Controls.Add(this.player4);
            this.panel1.Controls.Add(this.player3);
            this.panel1.Controls.Add(this.player2);
            this.panel1.Controls.Add(this.player1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(21, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 173);
            this.panel1.TabIndex = 10;
            // 
            // player6
            // 
            this.player6.AutoSize = true;
            this.player6.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player6.Location = new System.Drawing.Point(4, 137);
            this.player6.Name = "player6";
            this.player6.Size = new System.Drawing.Size(16, 17);
            this.player6.TabIndex = 11;
            this.player6.Text = "6";
            // 
            // player5
            // 
            this.player5.AutoSize = true;
            this.player5.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player5.Location = new System.Drawing.Point(4, 116);
            this.player5.Name = "player5";
            this.player5.Size = new System.Drawing.Size(16, 17);
            this.player5.TabIndex = 11;
            this.player5.Text = "5";
            // 
            // player4
            // 
            this.player4.AutoSize = true;
            this.player4.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player4.Location = new System.Drawing.Point(4, 95);
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(16, 17);
            this.player4.TabIndex = 11;
            this.player4.Text = "4";
            // 
            // player3
            // 
            this.player3.AutoSize = true;
            this.player3.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player3.Location = new System.Drawing.Point(4, 70);
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(16, 17);
            this.player3.TabIndex = 11;
            this.player3.Text = "3";
            // 
            // player2
            // 
            this.player2.AutoSize = true;
            this.player2.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player2.Location = new System.Drawing.Point(4, 47);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(16, 17);
            this.player2.TabIndex = 11;
            this.player2.Text = "2";
            // 
            // player1
            // 
            this.player1.AutoSize = true;
            this.player1.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.player1.Location = new System.Drawing.Point(4, 21);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(16, 17);
            this.player1.TabIndex = 11;
            this.player1.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "======= Players / Scores =======";
            // 
            // keepBtn
            // 
            this.keepBtn.Enabled = false;
            this.keepBtn.Location = new System.Drawing.Point(19, 33);
            this.keepBtn.Name = "keepBtn";
            this.keepBtn.Size = new System.Drawing.Size(75, 23);
            this.keepBtn.TabIndex = 11;
            this.keepBtn.Text = "&Keep all";
            this.keepBtn.UseVisualStyleBackColor = true;
            this.keepBtn.Click += new System.EventHandler(this.keepBtn_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 423);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.endTurn);
            this.Controls.Add(this.rollDice);
            this.Controls.Add(this.helpBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.newGame);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.kDicePnl);
            this.Controls.Add(this.rDicePnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farkle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
            this.Load += new System.EventHandler(this.MainWin_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWin_KeyUp);
            this.rDicePnl.ResumeLayout(false);
            this.rDicePnl.PerformLayout();
            this.kDicePnl.ResumeLayout(false);
            this.kDicePnl.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rDicePnl;
        private System.Windows.Forms.Panel kDicePnl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox safeMode;
        private System.Windows.Forms.CheckBox soundsOn;
        private System.Windows.Forms.CheckBox sortRolls;
        private System.Windows.Forms.CheckBox count42;
        private System.Windows.Forms.CheckBox strictScoring;
        private System.Windows.Forms.CheckBox verbose;
        private System.Windows.Forms.Label scoreToLbl;
        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button helpBtn;
        private System.Windows.Forms.Button rollDice;
        private System.Windows.Forms.Button endTurn;
        private Dice.Die rolled6;
        private Dice.Die rolled5;
        private Dice.Die rolled4;
        private Dice.Die rolled3;
        private Dice.Die rolled2;
        private Dice.Die rolled1;
        private Dice.Die kept6;
        private Dice.Die kept5;
        private Dice.Die kept4;
        private Dice.Die kept3;
        private Dice.Die kept2;
        private Dice.Die kept1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown scoreTo;
        private System.Windows.Forms.Label keptScoreLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label player3;
        private System.Windows.Forms.Label player2;
        private System.Windows.Forms.Label player1;
        private System.Windows.Forms.Label player6;
        private System.Windows.Forms.Label player5;
        private System.Windows.Forms.Label player4;
        private System.Windows.Forms.Button keepBtn;
    }
}

