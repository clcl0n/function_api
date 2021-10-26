using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly FunctionCalculationService _functionCalculationService;

        public CalculationController(FunctionCalculationService functionCalculationService)
        {
            _functionCalculationService = functionCalculationService;
        }

        [HttpPost("{key}")]
        public IActionResult Calculate([FromRoute] int key, [FromBody] PerformCalculationRequestDTO request)
        {
            try
            {
                (double newFunctionResult, double? oldFunctionResult) = _functionCalculationService.Calculate(key, request.Input);
                return Ok(
                    new PerformCalculationResponseDTO
                    {
                        InputValue = request.Input,
                        ComputedValue = newFunctionResult,
                        PreviousValue = oldFunctionResult
                    }
                );
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to calculate or store valculated value.");
            }
        }
    }
}
