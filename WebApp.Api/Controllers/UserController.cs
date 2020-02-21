using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult<UserResponse> Register([FromBody] UserRequest userRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserResponseData responseData = userRepository.CreateUser(userRequest);
                    if (responseData != null)
                    {
                        userResponse = new UserResponse
                        {
                            Status = 201,
                            Message = "Created Successfully",
                            ResponseData = responseData
                        };
                        return Created("", userResponse);
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
                if (error.Contains("Cannot insert duplicate key row in object"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 409,
                        Message = "Username already exits"
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

        [Authorize]
        [HttpGet("{userId}", Name = "GetOneUser")]
        public ActionResult<ProjectResponse> GetOneUser(int userId)
        {
            try
            {
                //string token = this.Request.Headers["Authorization"].ToString();
                //if (userRepository.AccessAuthentication(token))
                //{
                if (ModelState.IsValid)
                {
                    UserResponseData responseData = userRepository.GetOneUser(userId);
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
                //}
                //errorResponse = new ErrorResponse
                //{
                //    Status = 401,
                //    Message = "Access denied, invalid token"
                //};
                //return StatusCode(401, errorResponse);
            }
            catch (Exception ex)
            {

                string error = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                if (error.Contains("User not found"))
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

        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public ActionResult<UserResponse> Auth([FromBody] UserRequest userRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserResponseData responseData = userRepository.Auth(userRequest);
                    if (responseData != null)
                    {
                        userResponse = new UserResponse
                        {
                            Status = 200,
                            Message = "Success",
                            ResponseData = responseData
                        };
                        return Ok(userResponse);
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
                if (error.Contains("User does not exists"))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 404,
                        Message = error
                    };
                }
                else if (error.Contains("Password is not correct, try again."))
                {
                    errorResponse = new ErrorResponse
                    {
                        Status = 401,
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

    }
}
