namespace ExternalTool1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonOK = new System.Windows.Forms.Button();
            this.radioButtonEasy = new System.Windows.Forms.RadioButton();
            this.radioButtonMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonHard = new System.Windows.Forms.RadioButton();
            this.labelLevelDifficulty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(120, 82);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(89, 36);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // radioButtonEasy
            // 
            this.radioButtonEasy.AutoSize = true;
            this.radioButtonEasy.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonEasy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEasy.Location = new System.Drawing.Point(21, 44);
            this.radioButtonEasy.Name = "radioButtonEasy";
            this.radioButtonEasy.Size = new System.Drawing.Size(66, 24);
            this.radioButtonEasy.TabIndex = 1;
            this.radioButtonEasy.Text = "Easy";
            this.radioButtonEasy.UseVisualStyleBackColor = false;
            this.radioButtonEasy.CheckedChanged += new System.EventHandler(this.radioButtonEasy_CheckedChanged);
            // 
            // radioButtonMedium
            // 
            this.radioButtonMedium.AutoSize = true;
            this.radioButtonMedium.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonMedium.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonMedium.Location = new System.Drawing.Point(120, 44);
            this.radioButtonMedium.Name = "radioButtonMedium";
            this.radioButtonMedium.Size = new System.Drawing.Size(89, 24);
            this.radioButtonMedium.TabIndex = 2;
            this.radioButtonMedium.Text = "Medium";
            this.radioButtonMedium.UseVisualStyleBackColor = false;
            this.radioButtonMedium.CheckedChanged += new System.EventHandler(this.radioButtonMedium_CheckedChanged);
            // 
            // radioButtonHard
            // 
            this.radioButtonHard.AutoSize = true;
            this.radioButtonHard.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonHard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonHard.Location = new System.Drawing.Point(233, 44);
            this.radioButtonHard.Name = "radioButtonHard";
            this.radioButtonHard.Size = new System.Drawing.Size(66, 24);
            this.radioButtonHard.TabIndex = 3;
            this.radioButtonHard.Text = "Hard";
            this.radioButtonHard.UseVisualStyleBackColor = false;
            this.radioButtonHard.CheckedChanged += new System.EventHandler(this.radioButtonHard_CheckedChanged);
            // 
            // labelLevelDifficulty
            // 
            this.labelLevelDifficulty.AutoSize = true;
            this.labelLevelDifficulty.BackColor = System.Drawing.Color.Transparent;
            this.labelLevelDifficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevelDifficulty.Location = new System.Drawing.Point(77, 4);
            this.labelLevelDifficulty.Name = "labelLevelDifficulty";
            this.labelLevelDifficulty.Size = new System.Drawing.Size(172, 25);
            this.labelLevelDifficulty.TabIndex = 4;
            this.labelLevelDifficulty.Text = "Game Difficulty";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(319, 121);
            this.Controls.Add(this.labelLevelDifficulty);
            this.Controls.Add(this.radioButtonHard);
            this.Controls.Add(this.radioButtonMedium);
            this.Controls.Add(this.radioButtonEasy);
            this.Controls.Add(this.buttonOK);
            this.Name = "Form1";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonEasy;
        private System.Windows.Forms.RadioButton radioButtonMedium;
        private System.Windows.Forms.RadioButton radioButtonHard;
        private System.Windows.Forms.Label labelLevelDifficulty;
    }
}

