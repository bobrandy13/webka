using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kafka_for_web.DataAccess;
using Microsoft.AspNetCore.Http;
using Kafka_for_web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.Models;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ProducerController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Producer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producer>>> GetProducers()
        {
            return await _context.Producers.ToListAsync();
        }

        // GET: api/Producer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producer>> GetProducer(long id)
        {
            var producer = await _context.Producers.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // POST: api/Producer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducer", new { id = producer.Id }, producer);
        }

        // * Post a new message
        [HttpPost("/message")]
        // Takes parameter producerID. 
        public async Task<IActionResult> PostMessage(Message message)
        {
            var producer = await _context.Producers.FindAsync(message.ProducerId);

            if (producer == null)
            {
                return NotFound("Producer not found");
            }

            // TODO: should decide which partition to save under 
            var partition = HashFunction.Hash(message);

            // FIX: Still requires 2 database calls. Is quite slow.
            var topic = await _context.Topics.FindAsync(message.TopicId);
            if (topic == null) return BadRequest("Topic not found");
            
            var cluster = await _context.Clusters.FindAsync(topic.ClusterId);
            if (cluster == null) return BadRequest("Cluster not found");

            var logPath = $"logs/{cluster.Name}/{topic.Name}/partition{partition}/log.txt";

            Logger.Write(logPath, message);

            return CreatedAtAction("GetProducer", new { id = producer.Id }, producer);
        }

        private bool ProducerExists(long id)
        {
            return _context.Producers.Any(e => e.Id == id);
        }
    }
}
