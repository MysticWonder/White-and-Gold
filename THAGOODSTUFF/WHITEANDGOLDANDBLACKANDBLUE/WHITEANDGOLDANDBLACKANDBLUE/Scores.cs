using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WHITEANDGOLDANDBLACKANDBLUE
{
    class Scores
    {
        StreamWriter sw;
        StreamReader sr;
        int highscore;
        List<int> scorelist;

        public int Highscore { get { return highscore; } set { highscore = value; } }

        public Scores()
        {
            highscore = 0;
            scorelist = new List<int>();
        }

        public int WriteScore(int score)
        {
            sw = new StreamWriter("scores.txt");
            sw.Write(score);
            sw.Close();
            sr = new StreamReader("scores.txt");
            string text = "";
            int maxScore = 0;
            while ((text = sr.ReadLine()) != null)
            {
                int scoreInt = int.Parse(text);
                scorelist.Add(score);
                maxScore = scorelist.Min();
                foreach (int s in scorelist)
                {
                    if (s > maxScore)
                    {
                        maxScore = s;
                    }
                }


            }
            sr.Close();
            return maxScore;
        }

        public int ReadScore()
        {
            sr = new StreamReader("scores.txt");
            string text = "";
            int maxScore = 0;
            while ((text = sr.ReadLine()) != null)
            {
                int score = int.Parse(text);
                scorelist.Add(score);
                maxScore = scorelist.Min();
                foreach (int s in scorelist)
                {
                    if (s > maxScore)
                    {
                        maxScore = s;
                    }
                }


            }
            return maxScore;
        }
    }
}
