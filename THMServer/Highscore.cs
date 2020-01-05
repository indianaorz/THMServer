using System;
using System.Collections.Generic;

namespace THMServer
{
    public class HighscoreEntry
    {
        public string name { get; set; }
        public float score { get; set; }

        public float streak { get; set; }

        public string dateTime { get; set; }

    }

    public class Highscore
    {
        public List<HighscoreEntry> highscores { get; set; }

    }

}
