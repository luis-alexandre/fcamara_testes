using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperDigital.API.Contracts;
using SuperDigital.Domain.Exceptions;
using SuperDigital.Domain.Service;

namespace SuperDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IMovimento _movimento;

        public ContaCorrenteController(IMovimento movimento, ILogger<ContaCorrenteController> logger)
        {
            _logger = logger;
            _movimento = movimento;
        }

        /// <summary>
        /// Obtém uma conta corrente com seus lançamentos.
        /// </summary>
        /// <param name="id">Número da Conta Corrente</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _movimento.ObterMovimentacaoAsync(id);

                if(result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Realiza uma transferência entre duas contas correntes.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] RealizarTransferenciaRequest request)
        {
            try
            {
                if(request == null)
                {
                    throw new ArgumentException("Request inválido.", nameof(request));
                }

                var result = await _movimento.RealizarTransferenciaAsync(request.ContaOrigemNumero,
                                                                         request.ContaDestinoNumero,
                                                                         request.Valor);

                if (result)
                {
                    return Ok();
                }

                return NoContent();

            }
            catch(ArgumentException ae)
            {
                _logger.LogError(ae.Message, ae);
                return BadRequest(ae.Message);
            }
            catch(EntityNotFoundException nfx)
            {
                _logger.LogError(nfx.Message, nfx);
                return NotFound(nfx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
