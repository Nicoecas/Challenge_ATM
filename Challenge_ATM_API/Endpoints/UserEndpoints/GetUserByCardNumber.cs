using AutoMapper;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.UserEndpoints
{
    public class GetUserByCardNumber : ControllerBase
    {
        private readonly IUserService _userService;

        public GetUserByCardNumber(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("user/getUser")]
        [SwaggerOperation(
            Summary = "Get User",
            Description = "Create User and create Card by default",
            OperationId = "CreateUser"
            )
        ]
        public async Task<ActionResult<GetUserDtoResponse>> GetUser()
        {
            try
            {
                var number = User.Identity?.Name;
                if (string.IsNullOrEmpty(number))
                    return Unauthorized("Token inválido o sin número de tarjeta.");

                return Ok(await _userService.GetUser(number));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class GetUserDtoResponse
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string DNI { get; set; }
        public required string CUIT { get; set; }
    }
}
