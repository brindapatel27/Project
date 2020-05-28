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
    public class TopicsController : ControllerBase
    {
        private readonly importantTopicsContext _context;

        public TopicsController(importantTopicsContext context)
        {
            _context = context;
        }


        //Get all the topics details
        // GET: api/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topics>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }


        //Get details of particular topic by its id
        // GET: api/Topics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topics>> GetTopics(int id)
        {
            var topics = await _context.Topics.FindAsync(id);

            if (topics == null)
            {
                return NotFound();
            }

            return topics;
        }


        //Get details of a topic by its name
        //GET: api/Topics/GetByName/ImpTopics
        [HttpGet("GetByName/{TopicName}")]
        public async Task<ActionResult<Topics>> GetByName(string TopicName)
        {
            var topics = await _context.Topics.Where(t => t.TopicName == TopicName).FirstOrDefaultAsync();
            if (topics == null)
            {
                return NotFound();
            }

            return topics;
        }


        //Get details of particular topic including its keywords details by its id
        // GET: api/Topics/GetTopicDetails/1
        [HttpGet("GetTopicDetails/{id}")]
        public async Task<ActionResult<Topics>> GetTopicDetails(int id)
        {
            var topics = await _context.Topics.SingleAsync(t => t.TopicId == Convert.ToInt32(id));

            _context.Entry(topics)
                    .Collection(topic => topic.Contain)
                    .Query()
                    .Include(con => con.Keyword)
                    .Load();
          
            if (topics == null)
            {
                return NotFound();
            }

            return topics;
        }


        //Get all the topics and related keywords details
        // GET: api/Topics/GetAll
         [HttpGet("GetAll")]
         public async Task<ActionResult<IEnumerable<Topics>>>GetAll()
         {
            var topics =  await _context.Topics.Include(topic => topic.Contain)
                                         .ThenInclude(con => con.Keyword)
                                         .Take(100).ToListAsync();
           /* var topics = await _context.Topics.ToListAsync();

            _context.Entry(topics).Collection(topic => topic.Contain)
                                  .Query()
                                  .Include(con => con.Keyword)
                                  .Load();*/
            if (topics == null)
              {
                  return NotFound();
              }

              return topics;
         }


        //Update a topic's details by its id
        // PUT: api/Topics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopics(int id, Topics topics)
        {
            if (id != topics.TopicId)
            {
                return BadRequest();
            }

            _context.Entry(topics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicsExists(id))
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


        //Add new topic
        // POST: api/Topics
        [HttpPost]
        public async Task<ActionResult<Topics>> PostTopics(Topics topics)
        {
            _context.Topics.Add(topics);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TopicsExists(topics.TopicId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTopics", new { id = topics.TopicId }, topics);
        }

        //delete a topic an all its corresponding keywords details by its id
        // DELETE: api/Topics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Topics>> DeleteTopics(int id)
        {
            var contain = await _context.Contain.Where(topic => topic.TopicId == id).ToListAsync();
            if (contain == null)
            {
                return NotFound();
            }

            _context.Contain.RemoveRange(contain);
            await _context.SaveChangesAsync();

            var topics = await _context.Topics.FindAsync(id);
            if (topics == null)
            {
                return NotFound();
            }

            _context.Topics.Remove(topics);
            await _context.SaveChangesAsync();

            return topics;
        }

        private bool TopicsExists(int id)
        {
            return _context.Topics.Any(e => e.TopicId == id);
        }
    }
}
