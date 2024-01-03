using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace HopShop.WEBApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case UserNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AccountNumberExceededException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InvalidAccountTypeException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UserDoesNotExistException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AccountNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case InsufficientFundsException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case IncorrectTransferAmountException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
