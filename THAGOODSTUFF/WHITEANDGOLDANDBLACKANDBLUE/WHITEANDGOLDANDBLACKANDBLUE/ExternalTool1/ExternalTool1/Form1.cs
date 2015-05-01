using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExternalTool1
{
    public partial class Form1 : Form
    {
        string currentDifficulty { get; set; }
        string previousDifficulty { get; set; } 
        StreamWriter externalWriter { get; set; }
        StreamReader externalReader { get; set; }
        bool difficultyChanged { get; set; }

        //We want this to change the difficulty of the game
        //Step 1: Have form read in current game difficulty (normal is default)
        //Check for missing difficulty file (creates default setting if missing)
        //Step 2: Display the form with the correct previous value's radio button already selected
        //Step 3: Register a change in the state of the Radio Button
        //Step 4: When user clicks ok, open game difficulty file and make necessary adjustments
        //Step 5: If everything completes automatically close the settings window after confirmation notification
        //
        //Note: Currently separate from the game, it will modify a file at a changeable filepath for testing & implementaiton
        public Form1()
        {
            InitializeComponent();

            currentDifficulty = null;
            previousDifficulty = null;
            externalReader = null;
            externalWriter = null;
            difficultyChanged = false;

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(),"gameDifficulty.txt")))
            {
                //Prepping the Form -- 1
                //1A
                currentDifficulty = null;
                externalReader = new StreamReader("gameDifficulty.txt");
                currentDifficulty = externalReader.ReadLine();
                externalReader.Close();

                //1B
                switch (currentDifficulty)
                {
                    case "Easy":
                        this.radioButtonEasy.Checked = true;
                        break;
                    case "Medium":
                        this.radioButtonMedium.Checked = true;
                        break;
                    case "Hard":
                        this.radioButtonHard.Checked = true;
                        break;
                }
            }
                //1 Error "Catch"
            else
            {
                this.radioButtonMedium.Checked = true;
                externalWriter = new StreamWriter("gameDifficulty.txt");
                externalWriter.WriteLine("Medium");
                externalWriter.Close();
                currentDifficulty = "Medium";
            }
            previousDifficulty = currentDifficulty;
            //Showing Correct Form -- 2(complete)
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Write new game difficulty -- 4
            externalWriter = new StreamWriter("gameDifficulty.txt");
            externalWriter.WriteLine(currentDifficulty);
            externalWriter.Close();
            //Confirmation & Close -- 5
            if (difficultyChanged && previousDifficulty != currentDifficulty)
            {
                MessageBox.Show("Difficulty changed to " + currentDifficulty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Difficulty not changed.");
                this.Close();
            }
        }
        //Register Changes in Radio buttons --3
        private void radioButtonEasy_CheckedChanged(object sender, EventArgs e)
        {
            currentDifficulty = "Easy";
            difficultyChanged = true;
        }

        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            currentDifficulty = "Medium";
            difficultyChanged = true;
        }

        private void radioButtonHard_CheckedChanged(object sender, EventArgs e)
        {
            currentDifficulty = "Hard";
            difficultyChanged = true;
        }
    }
}
