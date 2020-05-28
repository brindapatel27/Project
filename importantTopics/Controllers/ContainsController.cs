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
    public class ContainsController : ControllerBase
    {
        private readonly importantTopicsContext _context;

        public ContainsController(importantTopicsContext context)
        {
            _context = context;
        }


        // GET: api/Contains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contain>>> GetContain()
        {
            return await _context.Contain.ToListAsync();
        }


        //get all keyword's id's for a topic id
        // GET: api/Contains/5
        [HttpGet("{id1}")]
        public async Task<ActionResult<IEnumerable<Contain>>> GetContain(int id1)
        {
            var contain = await _context.Contain.Where(e => e.TopicId == id1).ToListAsync();

            if (contain == null)
            {
                return NotFound();
            }

            return contain;
        }

       
        //Add a new keyword id to a topic
        // POST: api/Contains
        [HttpPost]
        public async Task<ActionResult<Contain>> PostContain(Contain contain)
        {
            _context.Contain.Add(contain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContainExists(contain.TopicId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContain", new { id = contain.TopicId }, contain);
        }

        //delete a keyword for a topic by providing topic id and keyword id
        // DELETE: api/Contains/{topicid},{keywordid}
        [HttpDelete("{id1},{id2}")]
        public async Task<ActionResult<Contain>> DeleteContain(int id1, int id2)
        {
            var contain = await _context.Contain.FindAsync(id1,id2);
            if (contain == null)
            {
                return NotFound();
            }

            _context.Contain.Remove(contain);
            await _context.SaveChangesAsync();

            return contain;
        }

        //delete all the entires with a given topic id
        // DELETE: api/Contains/2
        [HttpDelete("{id1}")]
        public async Task<ActionResult<IEnumerable<Contain>>> DeleteContain(int id1)
        {
            var contain = await _context.Contain.Where(topic => topic.TopicId==id1).ToListAsync();
            if (contain == null)
            {
                return NotFound();
            }

            _context.Contain.RemoveRange(contain);
            await _context.SaveChangesAsync();

            return contain;
        }

        private bool ContainExists(int id)
        {
            return _context.Contain.Any(e => e.TopicId == id);
        }
    }
}
