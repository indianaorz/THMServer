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
    [Route("[controller]")]
    public class HighscoreController : ControllerBase
    {
        private const string FILENAME = "../highscores.json";
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HighscoreController> _logger;

        public HighscoreController(ILogger<HighscoreController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Highscore> Get()
        {
            if (System.IO.File.Exists(FILENAME))
            {
                var file = System.IO.File.ReadAllText(FILENAME);
                var highscores = JsonConvert.DeserializeObject<IEnumerable<Highscore>>(file);
                return highscores;
            }
            return new List<Highscore>();
        }

        [HttpPost]
        public IEnumerable<Highscore> PostHighscore(Highscore highscore)
        {
            var highscores = Get()?.ToList();
            if(highscores == null)
            {
                highscores = new List<Highscore>();
            }

            highscores.Add(highscore);

            if (!System.IO.File.Exists(FILENAME))
            {
                System.IO.File.Create(FILENAME);
            }
            System.IO.File.WriteAllText(FILENAME, JsonConvert.SerializeObject(highscores.ToList()));

            return highscores;
        }
    }
}
