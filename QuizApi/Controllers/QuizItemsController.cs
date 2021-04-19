using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizItemsController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizItemsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/QuizItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizItem>>> GetQuizItem()
        {
            var quizItems = await _context.QuizItem.ToListAsync();
            foreach (var item in quizItems)
            {
                var anwers = _context.QuizAnswers.Where(a => a.QuizItemId == item.QuizItemId).Select(a=> a.Answer).ToArray();
                item.Answers = anwers;
                var options = _context.QuizOptions.Where(a => a.QuizItemId == item.QuizItemId).Select(a => a.Option).ToArray();
                item.Options = options;
            }
            return quizItems;
        }

        // GET: api/QuizItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizItem>> GetQuizItem(int id)
        {
            var quizItem = await _context.QuizItem.FindAsync(id);

            if (quizItem == null)
            {
                return NotFound();
            }
            var anwers = _context.QuizAnswers.Where(a => a.QuizItemId == quizItem.QuizItemId).Select(a => a.Answer).ToArray();
            quizItem.Answers = anwers;
            var options = _context.QuizOptions.Where(a => a.QuizItemId == quizItem.QuizItemId).Select(a => a.Option).ToArray();
            quizItem.Options = options;
            return quizItem;
        }

        // PUT: api/QuizItems/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutQuizItem([FromBody]QuizItem quizItem)
        {
            
            var dbItem = _context.QuizItem.Find(quizItem.QuizItemId);
            _context.Entry(dbItem).State = EntityState.Detached;
            if (dbItem == null)
            {
                return BadRequest();
            }
            _context.Entry(quizItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                if (quizItem.Answers != null && quizItem.Answers.Any())
                {
                    var oldAnswers = _context.QuizAnswers.Where(a => a.QuizItemId == quizItem.QuizItemId);
                    _context.QuizAnswers.RemoveRange(oldAnswers);
                    _context.SaveChanges();
                    foreach (var item in quizItem.Answers)
                    {
                        var answer = new QuizAnswer
                        {
                            Answer = item,
                            QuizItemId = quizItem.QuizItemId
                        };
                        _context.QuizAnswers.Add(answer);
                    }
                    await _context.SaveChangesAsync();
                }

                if (quizItem.Options != null && quizItem.Options.Any())
                {
                    var oldOptions = _context.QuizOptions.Where(a => a.QuizItemId == quizItem.QuizItemId);
                    _context.QuizOptions.RemoveRange(oldOptions);
                    _context.SaveChanges();
                    foreach (var item in quizItem.Options)
                    {
                        var option = new QuizOption
                        {
                            Option = item,
                            QuizItemId = quizItem.QuizItemId
                        };
                        _context.QuizOptions.Add(option);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateConcurrencyException)
            {
                
            }

            return Ok("Updated successfully!");
        }

        // POST: api/QuizItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuizItem>> PostQuizItem([FromBody]QuizItem quizItem)
        {
            _context.QuizItem.Add(quizItem);
            await _context.SaveChangesAsync();
            if(quizItem.Answers != null && quizItem.Answers.Any())
            {
                foreach (var item in quizItem.Answers)
                {
                    var answer = new QuizAnswer
                    {
                        Answer = item,
                        QuizItemId = quizItem.QuizItemId
                    };
                    _context.QuizAnswers.Add(answer);
                }
                await _context.SaveChangesAsync();
            }

            if (quizItem.Options != null && quizItem.Options.Any())
            {
                foreach (var item in quizItem.Options)
                {
                    var option = new QuizOption
                    {
                        Option = item,
                        QuizItemId = quizItem.QuizItemId
                    };
                    _context.QuizOptions.Add(option);
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetQuizItem", new { id = quizItem.QuizItemId }, quizItem);
        }

        // DELETE: api/QuizItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizItem(int id)
        {
            var quizItem = await _context.QuizItem.FindAsync(id);
            if (quizItem == null)
            {
                return NotFound();
            }
            var anwers = _context.QuizAnswers.Where(a => a.QuizItemId == quizItem.QuizItemId).ToList();
           
            var options = _context.QuizOptions.Where(a => a.QuizItemId == quizItem.QuizItemId).ToList();

            _context.QuizAnswers.RemoveRange(anwers);
            _context.QuizOptions.RemoveRange(options);
            _context.QuizItem.Remove(quizItem);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully!");
        }
    }
}
