using DSAAFCA2020.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSAAFCA2020.Managers
{
    public class ScoreManager
    {
        private static string _fileName = " scores.xml"; //put in bin folder

        public List<Score> Highscores { get; set;  }

        public List<Score> Scores { get; set; }

        public ScoreManager()
            : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            //if no file create instance of scoremanager
            if (!File.Exists(_fileName))
                return new ScoreManager();


            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList();
        }

        public static void Save (ScoreManager scoreManager)
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
