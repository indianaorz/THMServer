using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace THMServer.Controllers
{
    [ApiController]
    [Route("[controller]/{id}")]
    public class HighscoreController : ControllerBase
    {
        private const string FILENAME = "../highscores/";
        private readonly ILogger<HighscoreController> _logger;

        public HighscoreController(ILogger<HighscoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Highscore Get(string id)
        {
            Highscore highscore = new Highscore();
            var fileName = FILENAME + id + ".json";
            if (System.IO.File.Exists(fileName))
            {
                var file = System.IO.File.ReadAllText(fileName);
                var highscores = JsonConvert.DeserializeObject<IEnumerable<HighscoreEntry>>(file);
                highscore.highscores = highscores.ToList();
                return highscore;
            }
            highscore.highscores = new List<HighscoreEntry>();
            return highscore;
        }

        [HttpPost]
        public Highscore PostHighscore(string id, HighscoreEntry highscore)
        {
            var fileName = FILENAME + id + ".json";
            var highscores = Get(id);
            if(highscores == null)
            {
                highscores = new Highscore();
                highscores.highscores = new List<HighscoreEntry>();
            }

            highscores.highscores.Add(highscore);

            if (!System.IO.File.Exists(fileName))
            {
                System.IO.Directory.CreateDirectory(FILENAME);
                var file = System.IO.File.Create(fileName);
                file.Close();
            }
            System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(highscores.highscores.ToList()));

            return highscores;
        }
    }
}
