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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;
        private ErrorResponse errorResponse;
        private UserResponse userResponse;
        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<UserResponse> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserResponseData responseData = userRepository.CreateUser(userRequest);
                    if (responseData == null)
                    {
                        errorResponse = new ErrorResponse
                        {
                            Status = 400,
                            Message = ModelState.ValidationState.ToString()
                        };
                        return BadRequest(errorResponse);
                    }
                    userResponse = new UserResponse
                    {
                        Status = 201,
                        Message = "Created Successfully",
                        ResponseData = responseData
                    };
                    return Created("", userResponse);
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
                        Message = "Username exits"
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

        [HttpGet("{id}", Name = "GetOne")]
        public ActionResult<ProjectResponse> GetOne(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserResponseData responseData = userRepository.GetOneUser(id);
                    userResponse = new UserResponse
                    {
                        Status = 200,
                        Message = "Success",
                        ResponseData = responseData
                    };
                    return Ok(userResponse);
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
                        Message = "User not found"
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
