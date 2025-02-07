using IO.Swagger.DTOs;
using IO.Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// TextController
    /// </summary>
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly IMonkeyManagerService _monkeyManagerService;
        private readonly ITextGeneratorService _textGeneratorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="monkeyManagerService"></param>
        /// <param name="textGeneratorService"></param>
        public TextController(IMonkeyManagerService monkeyManagerService, ITextGeneratorService textGeneratorService)
        {
            _monkeyManagerService = monkeyManagerService;
            _textGeneratorService = textGeneratorService;
        }

        /// <summary>
        /// Start writing text
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/v1/StartWriting")]
        [Produces("application/json")]
        [SwaggerOperation("StartWriting")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public virtual IActionResult StartWriting([FromBody] MonkeyInputDTO monkeyInputDTO)
        {
            var monkeys = new List<MonkeyOutputDTO>();

            for(int i = 0; i < monkeyInputDTO.NumberOfMonkeys; i++)
            {
                var monkeyOutputDto = _monkeyManagerService.StartTask(_textGeneratorService.GenerateText);
                monkeys.Add(monkeyOutputDto);
            }

            return Ok(monkeys);
        }
    }
}
