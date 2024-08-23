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
using Azure.Core;

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

        // Need to make a design decision here. Note to future self. How do we want to implement consumer groups
        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToTopic(string consumerName, string topicName,
            ConsumerOptionalParams optionalParams, CancellationToken cancellationToken)
        {
            var consumer = _context.Consumers.FirstOrDefault(consumer => consumer.Name == consumerName);
            if (consumer == null) return NotFound("The provided consumer did not exist.");

            var topic = _context.Topics.FirstOrDefault(topic => topic.Name == topicName);
            if (topic == null) return NotFound("The provided topic did not exist.");

            var cluster = _context.Clusters.FirstOrDefault(cluster => cluster.Id == topic.ClusterId);
            if (cluster == null) return NotFound("The provided cluster did not exist.");
            
            var consumerOffset = await _context.Offsets.Where(offset => offset.ConsumerId == consumer.Id)
                .FirstOrDefaultAsync();
            
            // consumer optionalParams.NumMessages 

            if (consumerOffset == null)
            {
                // TODO: Create one

                consumerOffset = new ConsumerOffsets
                {
                    ConsumerId = consumer.Id,
                    Consumer = consumer,
                    TopicId = topic.Id,
                    Topic = topic,
                    Offset = 0
                };

                _context.Offsets.Add(consumerOffset);
            }

            // 5 milliseconds.
            const int maxTimeout = 5000;

            for (var i = 0; i < maxTimeout; i += 1000)
            {
                // NOTE: If offset is already max, then it won't increment and no new message will be returned. 
                var message = CheckForNewMessage(topic.Name, cluster.Name, consumerOffset.Offset, optionalParams.NumMessages ?? 1);

                if (message != null)
                {
                    consumerOffset.Offset++;
                    await _context.SaveChangesAsync(cancellationToken);
                    return Ok(message);
                }

                await Task.Delay(1000, cancellationToken);
            }
            
            // return long polling timeout
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // TODO: Implement this method.
        private static string? CheckForNewMessage(string topicName, string clusterName, int offset, int numMessages)
        {
            // if there is only 1, then it should be consuming all the messages 
            var message = Logger.Read(topicName, clusterName, offset);
            return message;
        }

        // This does the job of a leader node. 
        private void RebalanceWorkload(long consumerGroupName, string topicName)
        {
            // Find out how many consumers are joined to this topic;
            var consumerCount =
                _context.Subscriptions.Count(subscriptions => subscriptions.ConsumerGroupId == consumerGroupName);
            
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