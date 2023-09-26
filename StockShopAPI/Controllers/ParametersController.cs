using System;
using Microsoft.AspNetCore.Mvc;
using StockShopAPI.Models;
using StockShopAPI.Repositories;

namespace StockShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private ParameterRepository _parameterRepository;

        public ParametersController(ParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParameter(Parameter parameter)
        {
            await _parameterRepository.CreateParameter(parameter);

            return Ok();
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetParameters(int categoryId)
        {
            var parameters = await _parameterRepository.GetParameters(categoryId);

            return Ok(parameters);
        }

        [HttpPost("Options")]
        public async Task<IActionResult> CreateOption(PredefinedChoice option)
        {
            await _parameterRepository.CreateOption(option);

            return Ok();
        }

        [HttpGet("Options/{parameterId:int}")]
        public async Task<IActionResult> GetOptions(int parameterId)
        {
            var options = await _parameterRepository.GetOptions(parameterId);

            return Ok(options);
        }
    }
}

