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
        private ErrorResponse errorResponse;
        private ProjectResponse projectResponse;
        private ProjectsResponse projectsResponse;
        public ProjectController(IProjectRepository _projectRepository)
        {
            projectRepository = _projectRepository;
        }

        [HttpGet("{id}", Name = "GetOne")]
        public ActionResult<ProjectResponse> GetOne(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.GetOneProject(id);
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

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProjestRequest projestRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.UpdateAll(id, projestRequest);
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

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] ProjestPatchRequest projestPatchRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectResponseData responseData = projectRepository.UpdateCompleted(id, projestPatchRequest);
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


        [HttpDelete("{id}")]
        public ActionResult<ProjectResponse> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isDeleted = projectRepository.DeleteProject(id);
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
    }
}
