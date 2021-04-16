using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            return await _context.QuizItem.ToListAsync();
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

            return quizItem;
        }

        // PUT: api/QuizItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizItem(int id, QuizItem quizItem)
        {
            if (id != quizItem.QuizItemId)
            {
                return BadRequest();
            }

            _context.Entry(quizItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuizItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuizItem>> PostQuizItem(QuizItem quizItem)
        {
            _context.QuizItem.Add(quizItem);
            await _context.SaveChangesAsync();

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

            _context.QuizItem.Remove(quizItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizItemExists(int id)
        {
            return _context.QuizItem.Any(e => e.QuizItemId == id);
        }
    }
}
