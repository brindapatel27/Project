/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using importantTopics.Model;


namespace importantTopics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsFromTopicController : ControllerBase
    {
        private readonly importantTopicsContext _context;

        public KeywordsFromTopicController(importantTopicsContext context)
        {
            _context = context;
        }

        public Task<ActionResult<Keywords>> GetKeywordsFromTopic(int id)
        {
            return _context.Contain
                    .Join(
                   _context.Keywords,
                   Tid => Contain.TopicID,
                   
        (Tid) => new
        {
            Kid = Keyword.Tid,
            Kname = customer.FirstName + "" + customer.LastName,
        }
    )
        }
    }
}*/
