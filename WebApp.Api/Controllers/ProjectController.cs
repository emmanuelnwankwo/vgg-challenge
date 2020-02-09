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
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository projectRepository;
        private IActionRepository actionRepository;
        private ErrorResponse errorResponse;
        private ProjectResponse projectResponse;
        private ProjectsResponse projectsResponse;
        private ActionResponse actionResponse;
        private ActionsResponse actionsResponse;
        public ProjectController(IProjectRepository _projectRepository, IActionRepository _actionRepository)
        {
            projectRepository = _projectRepository;
            actionRepository = _actionRepository;
        }

        [HttpGet("{projectId}", Name = "GetOneProject")]
        public ActionResult<ProjectResponse> GetOneProject(int projectId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.GetOneProject(projectId);
                    projectResponse = new ProjectResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(projectResponse);
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
                        Message = "Project not found"
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
        public ActionResult<ProjectsResponse> Get()
        {
            try
            {
                List<ProjectResponseData> responseData = projectRepository.GetAllProjects();
                projectsResponse = new ProjectsResponse
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = responseData
                };
                return Ok(projectsResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
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

        [HttpPost]
        public ActionResult<ProjectResponse> Create([FromBody] ProjestRequest projestRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.CreateProject(projestRequest);
                    if (responseData == null)
                    {
                        errorResponse = new ErrorResponse
                        {
                            Status = 400,
                            Message = ModelState.ValidationState.ToString()
                        };
                        return BadRequest(errorResponse);
                    }
                    projectResponse = new ProjectResponse
                    {
                        Status = 201,
                        Message = "Created Successfully",
                        ResponseData = responseData
                    };
                    return Created("", projectResponse);
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
                if (error.Contains("Cannot insert duplicate key row in object"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 409,
                        Message = "Project name already exits"
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

        [HttpPut("{projectId}")]
        public ActionResult Put(int projectId, [FromBody] ProjestRequest projestRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.UpdateAll(projectId, projestRequest);
                    projectResponse = new ProjectResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(projectResponse);
                }
                projectResponse = new ProjectResponse
                {
                    Status = 400,
                    Message = ModelState.ValidationState.ToString()
                };
                return BadRequest(projectResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
                    };
                }
                else if (error.Contains("Cannot insert duplicate key row in object"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 409,
                        Message = "Project name already exits"
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

        [HttpPatch("{projectId}")]
        public ActionResult Patch(int projectId, [FromBody] ProjestPatchRequest projestPatchRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.UpdateCompleted(projectId, projestPatchRequest);
                    projectResponse = new ProjectResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(projectResponse);
                }
                projectResponse = new ProjectResponse
                {
                    Status = 400,
                    Message = ModelState.ValidationState.ToString()
                };
                return BadRequest(projectResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
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


        [HttpDelete("{projectId}")]
        public ActionResult<ProjectResponse> Delete(int projectId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isDeleted = projectRepository.DeleteProject(projectId);
                    if (isDeleted)
                    {
                        projectResponse = new ProjectResponse
                        {
                            Status = 200,
                            Message = "Success",
                        };
                        return Ok(projectResponse);
                    }
                }
                projectResponse = new ProjectResponse
                {
                    Status = 400,
                    Message = ModelState.ValidationState.ToString()
                };
                return BadRequest(projectResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
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

        #region Action
        [HttpPost("{projectId}/actions")]
        public ActionResult<ActionResponse> CreateAction(int projectId, [FromBody] ActionRequest actionRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ActionResponseData responseData = actionRepository.CreateAction(projectId, actionRequest);
                    if (responseData == null)
                    {
                        errorResponse = new ErrorResponse
                        {
                            Status = 400,
                            Message = ModelState.ValidationState.ToString()
                        };
                        return BadRequest(errorResponse);
                    }
                    actionResponse = new ActionResponse
                    {
                        Status = 201,
                        Message = "Created Successfully",
                        ResponseData = responseData
                    };
                    return Created("", actionResponse);
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
                if (error.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = $"Project id: {projectId} does not exists"
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

        [HttpGet("{projectId}/actions")]
        public ActionResult<ActionsResponse> GetAllActionsProject(int projectId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<ActionResponseData> responseData = actionRepository.GetAllActionsInProject(projectId);
                    actionsResponse = new ActionsResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(actionsResponse);
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
                        Message = "Project not found"
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


        [HttpGet("{projectId}/actions/{actionId}")]
        public ActionResult<ActionResponse> GetOneActionProject(int projectId, int actionId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ActionResponseData responseData = actionRepository.GetActionInProject(projectId, actionId);
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
                if (error.Contains("Project not found"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
                    };
                }
                else if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Action not found for this project"
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

        [HttpPut("{projectId}/actions/{actionId}")]
        public ActionResult<ActionResponse> UpdateOneAction(int projectId, int actionId, ActionRequest actionRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ActionResponseData responseData = actionRepository.GetActionInProject(projectId, actionId, actionRequest);
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
                if (error.Contains("Project not found"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
                    };
                }
                else if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Action not found for this project"
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

        [HttpDelete("{projectId}/actions/{actionId}")]
        public ActionResult<ActionResponse> DeleteOneAction(int projectId, int actionId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isDeleted = actionRepository.DeleteAction(projectId, actionId);
                    if (isDeleted)
                    {
                        actionResponse = new ActionResponse
                        {
                            Status = 200,
                            Message = "Success"
                        };
                        return Ok(actionResponse);
                    }
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
                if (error.Contains("Project not found"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Project not found"
                    };
                }
                else if (error.Contains("Sequence contains no elements"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = "Action not found for this project"
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

        #endregion
    }
}
