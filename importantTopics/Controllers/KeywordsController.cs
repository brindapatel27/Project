using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using importantTopics.Model;

namespace importantTopics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly importantTopicsContext _context;

        public KeywordsController(importantTopicsContext context)
        {
            _context = context;
        }


        // Get all the keywords
        // GET: api/Keywords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Keywords>>> GetKeywords()
        {
            return await _context.Keywords.ToListAsync();
        }

        //Get a particular keyword by its id
        // GET: api/Keywords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Keywords>> GetKeywords(int id)
        {
            var keywords = await _context.Keywords.FindAsync(id);

            if (keywords == null)
            {
                return NotFound();
            }

            return keywords;
        }

        //update a keyword by its id
        // PUT: api/Keywords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKeywords(int id, Keywords keywords)
        {
            if (id != keywords.KeywordId)
            {
                return BadRequest();
            }

            _context.Entry(keywords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeywordsExists(id))
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

        //Add a new keyword
        // POST: api/Keywords
        [HttpPost]
        public async Task<ActionResult<Keywords>> PostKeywords(Keywords keywords)
        {
            _context.Keywords.Add(keywords);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KeywordsExists(keywords.KeywordId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKeywords", new { id = keywords.KeywordId }, keywords);
        }

        //delete a keyword by its id
        // DELETE: api/Keywords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Keywords>> DeleteKeywords(int id)
        {
            var contain = await _context.Contain.Where(keyword => keyword.KeywordId == id).ToListAsync();
            if (contain == null)
            {
                return NotFound();
            }
            _context.Contain.RemoveRange(contain);
            await _context.SaveChangesAsync();

            var keywords = await _context.Keywords.FindAsync(id);
            if (keywords == null)
            {
                return NotFound();
            }

            _context.Keywords.Remove(keywords);
            await _context.SaveChangesAsync();

            return keywords;
        }

        private bool KeywordsExists(int id)
        {
            return _context.Keywords.Any(e => e.KeywordId == id);
        }
    }
}
