using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }
        // Executa antes do metodo aciton -> Grava a data e o modelstate
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executando em ->> OnActionExecuting");
            _logger.LogInformation("#######################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
            _logger.LogInformation("#######################################");

        }

        // Executa após metodo action -> Grava a data e o statuscode
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando em ->> OnActionExecuted");
            _logger.LogInformation("#######################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState : {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("#######################################");
        }
    }
}
