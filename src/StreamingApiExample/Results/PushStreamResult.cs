namespace StreamingApiExample.Results
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    public class PushStreamResult : IActionResult
    {
        private readonly Action<Stream, CancellationToken> _onStream;
        private readonly string _contentType;
        private readonly CancellationToken _stopToken;

        public PushStreamResult(Action<Stream, CancellationToken> onStream,
                                string contentType,
                                CancellationToken stopToken)
        {
            _onStream = onStream;
            _contentType = contentType;
            _stopToken = stopToken;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.GetTypedHeaders().ContentType = new MediaTypeHeaderValue(_contentType);
            _onStream(context.HttpContext.Response.Body, _stopToken);
            return Task.CompletedTask;
        }
    }
}
