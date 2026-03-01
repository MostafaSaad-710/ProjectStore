using Domaine.Exeptions.BadRequest;
using Domaine.Exeptions.NotFound;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;

namespace Store.Web.Middlewares
{
    public class GlobalErrorHandlingMidleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMidleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                #region NotFound EndPoint
                if (context.Response.StatusCode == 404)
                {
                    //1. set contant type of response
                    context.Response.ContentType = "application/json"; // or "text/plain" based on your needs
                    //2. set body of response
                    var errorResponse = new ErrorDetailes
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"endPoind {context.Request.Path} Was Not Found !!"
                    };
                    //return response to client
                    await context.Response.WriteAsJsonAsync(errorResponse);
                } 
                #endregion

            }
            catch (Exception ex)
            {
                //1. set status code of respose
                context.Response.StatusCode = ex switch
                {
                     NotFoundExeptoin => StatusCodes.Status404NotFound,
                     BadRequestExeption => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };  

                //2. set contant type of response
                context.Response.ContentType = "application/json"; // or "text/plain" based on your needs

                //3. set body of response
                var errorResponse = new ErrorDetailes
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return response to client
                await context.Response.WriteAsJsonAsync(errorResponse);

            }
        }

    }
}
