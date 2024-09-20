using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using TriviaAppBL.Interfaces;
using TriviaAppBL.Models;

namespace TriviaBackend.Controllers
{
    [ApiController]
    [Route("api/trivia")]
    public class TriviaController : ControllerBase
    {
        private readonly IServicio_APITrivia _servicioApiTrivia;

        public TriviaController(IServicio_APITrivia servicio_APITrivia) { 
            _servicioApiTrivia = servicio_APITrivia;
        }


        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var response = await _servicioApiTrivia.GetCategories();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor." + ex.Message);
            }
        }

        [HttpPost("questions")]
        public async Task<IActionResult> GetQuestions([FromBody] TriviaConfig config)
        {
            try
            {
                var response = await _servicioApiTrivia.GetQuestions(config);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor." + ex.Message);
            }
        }

    }
}