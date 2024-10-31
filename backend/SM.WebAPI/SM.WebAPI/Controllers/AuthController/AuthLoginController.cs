﻿using Microsoft.AspNetCore.Mvc;
using SM.Auth.ApplicationService.UserModule.Abtracts;
using SM.Auth.ApplicationService.UserModule.Implements;
using SM.Auth.Dtos;
using SM.Auth.Dtos.CRUDModule;
using SM.Auth.Dtos.LoginModule;
using System;
using System.Threading.Tasks;

namespace SM.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuthLoginService _authLoginService;

        public UserController(IAuthService authService , IAuthLoginService authLoginService)
        {
            _authService = authService;
            _authLoginService = authLoginService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> AuthLogin(LoginUserDto loginInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _authLoginService.AuthLogin(loginInput);
                if (result == null)
                {
                    return Unauthorized(new AuthResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new AuthResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            try
            {
                var result = await _authService.AuthRegisterAsync(authRegisterDto);

                if (result.EC == 0) // Assuming EC = 0 means success
                {
                    return Ok(new AuthResponeDto
                    {
                        EM =  result.EM,
                        EC = result.EC,
                        DT = result.DT // hoặc bất kỳ dữ liệu nào bạn muốn trả về
                    });
                }
                else
                {
                    return BadRequest(new AuthResponeDto
                    {
                        EM = result.EM, // Error message from service
                        EC = result.EC,
                        DT = null
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }

        [HttpDelete("remove/{userId}")]
        public async Task<IActionResult> Remove(int userId)
        {
            try
            {
                var result = await _authService.AuthRemove(userId);
                if (result.EC == 0)
                {
                    return Ok(new AuthResponeDto
                    {
                        EM = result.EM,
                        EC = result.EC,
                        DT = result.DT // hoặc bất kỳ dữ liệu nào bạn muốn trả về
                    });
                }
                else
                {
                    return BadRequest(new AuthResponeDto
                    {
                        EM = result.EM,
                        EC = result.EC,
                        DT = null
                    });
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new AuthResponeDto
                {
                    EM = $"User with ID {userId} not found.",
                    EC = 1,
                    DT = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> Update(int userId, AuthUpdateDto authUpdateDto)
        {
            if (authUpdateDto == null)
            {
                return BadRequest(new AuthResponeDto
                {
                    EM = "Invalid user data.",
                    EC = 1,
                    DT = null
                });
            }

            try
            {
                var result = await _authService.AuthUpdate(userId, authUpdateDto);
                if (result.EC == 0)
                {
                    return Ok(new AuthResponeDto
                    {
                        EM = result.EM,
                        EC = result.EC,
                        DT = result.DT // hoặc bất kỳ dữ liệu nào bạn muốn trả về
                    });
                }
                else
                {
                    return BadRequest(new AuthResponeDto
                    {
                        EM = result.EM,
                        EC = result.EC,
                        DT = null
                    });
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new AuthResponeDto
                {
                    EM = $"User with ID {userId} not found.",
                    EC = 1,
                    DT = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new AuthResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _authService.AuthGetAll();
                return Ok(result); // Return 200 OK with user list
            }
            catch (Exception ex)
            {
                // Log the exception (if needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}