using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.ViewModels.BadRequest;
using Domain.DomainObjects;
using System;
using System.Net;

namespace API.Filters
{
    public class ApiExceptionFilter : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (!(context.Exception is Exception))
            {
                return;
            }

            switch (context.Exception)
            {

                case DomainException e:
                    context.Result = new ObjectResult(new BadRequest(e.Message)) { StatusCode = (int)HttpStatusCode.BadRequest };

                    break;

                case NotFoundException _:

                    context.Result = new ObjectResult("") { StatusCode = (int)HttpStatusCode.NotFound };

                    break;


                case Exception _:

                    context.Result = new ObjectResult(new { Message = $"System instability has occurred, but we are already resolving it." }) { StatusCode = (int)HttpStatusCode.InternalServerError };

                    break;
            }

            context.Exception = null;
        }
    }
}
