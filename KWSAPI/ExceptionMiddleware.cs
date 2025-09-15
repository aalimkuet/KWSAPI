namespace KWSAPI
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // TODO: Implement your custom exception handling logic here
            // For example, you could return a custom error response to the client.

            // For demonstration purposes, here's a simple example of returning a plain text error response.
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            Console.WriteLine(exception.Message);
            return context.Response.WriteAsync("An error occurred. Please try again later.");
        }
    }
}
