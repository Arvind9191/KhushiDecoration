namespace ShubhDecoration.Helper
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
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            { 
                await _next(httpContext);
            }
            catch (Exception ex)
            { 
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        } 
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        { 
            context.Response.ContentType = "text/html";
            context.Response.Redirect("/Home/Error"); 
            return Task.CompletedTask;
        }
    }
}
