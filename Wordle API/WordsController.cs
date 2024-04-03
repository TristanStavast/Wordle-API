using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wordle_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        //Declaring WordList and setting it to the txt file
        public string WordList = "WordList.txt";

        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            using(StreamReader sr = new StreamReader(WordList))
            {
                string line;
                string selected = null;
                int lineSeen = 0;
                var rand = new Random();

                while((line = sr.ReadLine()) != null)
                {
                    if(rand.Next(++lineSeen) == 0)
                    {
                        selected = line;
                    }
                }
                return selected;
            }
        }


        // POST api/<ValuesController>
        [HttpPost("guess")]
        public IActionResult UserGuess([FromBody] GuessRequest guess)
        {
            string guessedWord = guess.Word;

            if(WordExists(guessedWord))
            {
                return Ok($"'{guessedWord}' is a valid word!");
            }
            else
            {
                return BadRequest($"'{guessedWord}' is not a valid word!");
            }
            
        }

        private bool WordExists(string word)
        {
            string[] words = System.IO.File.ReadAllLines(WordList);

            foreach(string wor in words)
            {
                if(wor.Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
