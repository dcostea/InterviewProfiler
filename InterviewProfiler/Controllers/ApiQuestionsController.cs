using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterviewProfiler.Models;

namespace InterviewProfiler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiQuestionsController : ControllerBase
    {
        private readonly profilerContext _context;

        public ApiQuestionsController(profilerContext context)
        {
            _context = context;
        }

        // GET: api/ApiQuestions
        [HttpGet]
        public IEnumerable<Question> GetQuestion()
        {
            return _context.Question;
        }

        // GET: api/ApiQuestions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Question.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/ApiQuestions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion([FromRoute] int id, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/ApiQuestions
        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Question.Add(question);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.QuestionId }, question);
        }

        // DELETE: api/ApiQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Question.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.QuestionId == id);
        }
    }
}