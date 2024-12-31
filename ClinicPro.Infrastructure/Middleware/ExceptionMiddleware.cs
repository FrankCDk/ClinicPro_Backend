using ClinicPro.Core.Common;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicPro.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = exception.Message
            };

            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized; // 401
                    errorResponse.Message = "No autorizado.";
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    errorResponse.Message = "Solicitud inválida.";
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    errorResponse.Message = "Recurso no encontrado.";
                    break;

                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    errorResponse.Message = "Error de validación.";
                    break;

                case NotImplementedException:
                    statusCode = HttpStatusCode.NotFound;
                    errorResponse.Message = "Método no implementado.";
                    break;

                case MySqlException mySqlEx when mySqlEx.Number == 1062: // Error de clave duplicada en MySQL
                    statusCode = HttpStatusCode.Conflict; // 409
                    errorResponse.Message = "Ya existe un registro con la información detallada.";
                    break;

                default:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    errorResponse.Message = exception.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
