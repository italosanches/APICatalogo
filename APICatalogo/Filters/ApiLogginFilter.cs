using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLogginFilter : IActionFilter
    {
        private readonly ILogger<ApiLogginFilter> _logger;

        public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // executa antes da Action
            _logger.LogInformation("Executando OnActionExecuting");
            _logger.LogInformation($"{context.HttpContext.Request.Body}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Após a action
            _logger.LogInformation($"{context.HttpContext.Response.StatusCode}");

        }
    }
}
