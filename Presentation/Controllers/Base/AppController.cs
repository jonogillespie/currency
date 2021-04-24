using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Base
{
    public class AppController : Controller
    {
        private IMediator Mediator => 
            (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));
        
        protected async Task<ActionResult<T>> Query<T>(IRequest<T> query) 
            => Ok(await Mediator.Send(query));
    }
}