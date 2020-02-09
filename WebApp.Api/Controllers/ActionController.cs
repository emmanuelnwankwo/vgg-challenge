using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.BusinessLayer.Interface;
using WebApp.Core.Dtos;

namespace WebApp.Api.Controllers
{
    [Route("api/action")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private IActionRepository actionRepository;
        private ErrorResponse errorResponse;
        private ActionResponse actionResponse;
        private ActionsResponse actionsResponse;
        private ProjectsResponse projectsResponse;
        public ActionController(IActionRepository _actionRepository)
        {
            actionRepository = _actionRepository;
        }

        [HttpGet("{id}", Name = "GetOne")]
        public ActionResult<ProjectResponse> GetOne(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ActionResponseData responseData = actionRepository.GetOneAction(id);
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


        [HttpGet]
        public ActionResult<ActionsResponse> Get()
        {
            try
            {
                List<ActionResponseData> responseData = actionRepository.GetAllActions();
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
}
