using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kafka_for_web.DataAccess;
using Kafka_for_web.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Kafka_for_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly KafkaContext _context;

        public TopicController(KafkaContext context)
        {
            _context = context;
        }

        // GET: api/Topic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        // GET: api/Topic/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(long id)
        {
            var topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // PUT: api/Topic/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(long id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // A user should be able to subscribe to any given topic, and then be connected via SSE and listen to events. OR they can specify an offset to start reading from. 
        // POST: api/Topic
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            var cluster = await _context.Clusters.FindAsync(topic.ClusterId);
            if (cluster == null) return NotFound();

            Console.WriteLine(cluster.Name);

            var path = $"{System.IO.Directory.GetCurrentDirectory()}/logs/{cluster.Name}/{topic.Name}";

            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("This directory already exists, and therefore so does this cluster");
                    return BadRequest();
                }

                _context.Topics.Add(topic);
                await _context.SaveChangesAsync();
                
                var di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}, at path {1}",
                    Directory.GetCreationTime(path), path);
                
                // Partitions should automatically be created when a topic is created
                for (var i = 0; i < topic.NumPartitions; ++i)
                {
                    // make this many partitions;
                    var partitionPath = path + "/partition" + i;
                    if (Directory.Exists(partitionPath)) continue;
                    var partition = Directory.CreateDirectory(partitionPath);
                    
                    
                    // make the singular log file inside. Normally, there would be multiple log files with multiple replicas. 
                    var logPath = partitionPath + "/log.txt";
                    var logFile = System.IO.File.Create(logPath);
                    logFile.Close();
                    
                    Console.WriteLine("The partition was created successfully at {0}, at path {1}",
                        Directory.GetCreationTime(partitionPath), partitionPath);
                }

                return CreatedAtAction("GetTopic", new { id = cluster.Id }, cluster);
            }
            catch (Exception e)
            {
                Console.WriteLine("This process failed {0}", e);
            }

            Console.WriteLine(path);


            return CreatedAtAction("GetTopic", new { id = topic.Id }, topic);
        }

        // DELETE: api/Topic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(long id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return BadRequest();
            }

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicExists(long id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}