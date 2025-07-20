using AutoMapper;
using Challenge_ATM_API.Entities;
using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge_ATM_API.Endpoints.UserEndpoints
{
    [Tags("User")]
    public class CreateUserEndpoint : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserEndpoint(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("user/createUser")]
        [SwaggerOperation(
            Summary = "Create User",
            Description = "Create User and create Card by default",
            OperationId = "CreateUser"
            )
        ]
        public async Task<ActionResult<CreateUserDtoResponse>> CreateUser([FromBody] CreateUserDtoRequest req)
        {
            try
            {
                return Ok(await _userService.CreateUser(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class CreateUserDtoRequest
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
    }

    public class CreateUserDtoResponse
    {
        public int Id { get; set; }
        public required string PIN { get; set; }
        public required string NumberCard { get; set; }
    }
}
