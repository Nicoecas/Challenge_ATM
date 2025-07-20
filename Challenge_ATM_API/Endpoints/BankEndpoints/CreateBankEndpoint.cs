using AutoMapper;
using Challenge_ATM_API.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Challenge_ATM_API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Challenge_ATM_API.Endpoints.BankEndpoints
{
    [Tags("Bank")]
    public class CreateBankEndpoint : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public CreateBankEndpoint(IBankService bankService,
            IMapper mapper)
        {
            _bankService = bankService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("bank/createBank")]
        [SwaggerOperation(
            Summary = "Create Bank",
            Description = "Create Bank like a support entity from system",
            OperationId = "CreateBank"
            )
        ]
        public async Task<ActionResult<CreateBankDtoResponse>> CreateBank([FromBody] CreateBankDtoRequest req)
        {
            try
            {
                Bank bank = _mapper.Map<Bank>(req);
                return Ok(new CreateBankDtoResponse { Id = await _bankService.CreateBank(bank) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class CreateBankDtoRequest
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
    }

    public class CreateBankDtoResponse
    {
        public int Id { get; set; }
    }
}
