using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.Dtos;

namespace WebApp.Api.Controllers
{
    [Route("api/action")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[CustomAuthorization]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionRepository actionRepository;
        private ErrorResponse errorResponse;
        private ActionResponse actionResponse;
        private ActionsResponse actionsResponse;
        public ActionController(IActionRepository _actionRepository)
        {
            actionRepository = _actionRepository;
        }

        [HttpGet("{actionId}", Name = "GetOne")]
        public ActionResult<ProjectResponse> GetOne(int actionId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ActionResponseData responseData = actionRepository.GetOneAction(actionId);
                    actionResponse = new ActionResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(actionResponse);
                }
                errorResponse = new ErrorResponse
                {
                    Status = 400,
                    Message = ModelState.ValidationState.ToString()
                };
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Action not found"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = error
                    };
                }
                else
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 500,
                        Message = error
                    };
                }
                return StatusCode(errorResponse.Status, errorResponse);
            }
        }


        [HttpGet]
        public ActionResult<ActionsResponse> Get()
        {
            try
            {
                IEnumerable<ActionResponseData> responseData = actionRepository.GetAllActions();
                actionsResponse = new ActionsResponse
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = responseData
                };
                return Ok(actionsResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Action not found"
                    };
                }
                else
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 500,
                        Message = error
                    };
                }
                return StatusCode(errorResponse.Status, errorResponse);
            }
        }

    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    //public class CustomAuthorization : AuthorizeAttribute
    //{
    //    protected  void HandleUnauthorizedRequest(HttpActionContext actionContext)
    //    {
    //        actionContext.Response = new HttpResponseMessage
    //        {
    //            StatusCode = HttpStatusCode.Forbidden,
    //            Content = new StringContent("You are unauthorized to access this resource")
    //        };
    //    }
    //}

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    //public class CustomAuthorization : Attribute, IAsyncActionFilter
    //{
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        if (context.HttpContext.Response.StatusCode.Equals(401))
    //        {
    //            await context.HttpContext.Response.WriteAsync("Unauthorized request");
    //            return;
    //        }
    //        await next();
    //    }
    //}
}
