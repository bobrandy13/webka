using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;
using System.Collections;
using System.Net;
using kafka_for_web.DataAccess;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly KafkaContext _context;

        public ConsumerController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Consumer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumers()
        {
            return await _context.Consumers.ToListAsync();
        }

        // GET: api/Consumer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumer>> GetConsumer(long id)
        {
            var consumer = await _context.Consumers.FindAsync(id);

            if (consumer == null)
            {
                return NotFound();
            }

            return consumer;
        }

        // PUT: api/Consumer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumer(long id, Consumer consumer)
        {
            if (id != consumer.Id)
            {
                return BadRequest();
            }

            _context.Entry(consumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerExists(id))
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

        /// <summary>
        /// Method <c>fetchMessage()</c> checks for new messages depending on the offset
        /// </summary>

        // POST: api/Consumer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consumer>> PostConsumer(Consumer consumer)
        {
            _context.Consumers.Add(consumer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumer", new { id = consumer.Id }, consumer);
        }

        // DELETE: api/Consumer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumer(long id)
        {
            var consumer = await _context.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }

            _context.Consumers.Remove(consumer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Consider: How would concurrent requests be handled?
        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToTopic(string consumerName, string topicName, ConsumerOptionalParams optionalParams, CancellationToken cancellationToken)
        {
            var consumer = _context.Consumers.FirstOrDefault(consumer => consumer.Name == consumerName);
            if (consumer == null) return NotFound("The provided consumer did not exist."); 
            
            var topic = _context.Topics.FirstOrDefault(topic => topic.Name == topicName);
            if (topic == null) return NotFound("The provided topic did not exist.");
            
            var cluster = _context.Clusters.FirstOrDefault(cluster => cluster.Id == topic.ClusterId);
            if (cluster == null) return NotFound("The provided cluster did not exist.");
            
            // TODO: Fix this.
            var offset = _context.Offsets.FirstOrDefault(offset => offset.ConsumerId == consumer.Id);

            if (offset == null)
            {
                _context.Offsets.Add(new ConsumerOffsets() { Consumer = consumer, ConsumerId = consumer.Id, Offset = 1, TopicId = topic.Id});
            }
            else
            {
                offset.Offset++; 
            }

            await _context.SaveChangesAsync(cancellationToken);

            var timeout = TimeSpan.FromSeconds(10);
            var deadline = DateTime.UtcNow.Add(timeout);

            while (DateTime.UtcNow < deadline)
            {
                // TODO: Perform operations to check if there are new logs every second. 
                
                if (NewMessage()) 
                    return Ok(new {Message = "There is a new message. "});
                await Task.Delay(1000, cancellationToken); 

            }

            return new ObjectResult(new { Message = "Message has been successfully read." ?? "Long polling timeout...", status = "New message received!" });
        }

        private static bool NewMessage()
        {
            return false; 
        }

        // This does the job of a leader node. 
        private void RebalanceWorkload(long consumerGroupName, string topicName)
        {
            // Find out how many consumers are joined to this topic;
            var consumerCount = _context.Subscriptions.Count(subscriptions => subscriptions.ConsumerGroupId == consumerGroupName);

            var topic = _context.Topics.Where(topic => topic.Name == topicName);
            // find out how many partitions are being read from. 

            return;
        }


        private bool ConsumerExists(long id)
        {
            return _context.Consumers.Any(e => e.Id == id);
        }
    }
}
