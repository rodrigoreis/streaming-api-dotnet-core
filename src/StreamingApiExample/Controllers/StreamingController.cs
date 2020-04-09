namespace StreamingApiExample.Controllers
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Results;

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        private static ConcurrentBag<StreamWriter> _clients;

        static StreamingController()
        {
            _clients = new ConcurrentBag<StreamWriter>();
        }

        private void OnStream(Stream stream, CancellationToken stopToken)
        {
            _clients.Add(new StreamWriter(stream));
            stopToken.WaitHandle.WaitOne();
            _clients.TryTake(out _);
        }

        private static async Task SendEvent(object data, StreamEvent streamEvent)
        {
            foreach (var client in _clients)
            {
                var message = $"{JsonSerializer.Serialize(new { dados = data, streamEvent })}\n";
                await client.WriteAsync(message);
                await client.FlushAsync();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new PushStreamResult(OnStream, "text/event-stream", HttpContext.RequestAborted);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(StreamMessageModel model)
        {
            await SendEvent(model, StreamEvent.Post);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(StreamMessageModel model)
        {
            await SendEvent(model, StreamEvent.Put);
            return Ok();
        }
    }
}
