using API.DataAccess.Context;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        public readonly AppDbContext _context;
        public readonly string _controller = "Moneda";

        public CurrencyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCurrencies", Name = "getCurrencies")]
        public async Task<ActionResult<GeneralResponse>> GetCurrencies()
        {
            try
            {
                var currencies = await _context.Currencies
                    .Select(x => new
                    {
                        id = x.Id,
                        description = x.Description
                    })
                    .ToListAsync();

                if (currencies.Count <= 0)
                {
                    return new GeneralResponse
                    {
                        title = _controller,
                        message = $"No hay tipos de moneda.",
                        status = 404,
                        data = new { }
                    };
                }

                return new GeneralResponse
                {
                    title = _controller,
                    message = "Proceso exitoso.",
                    status = 200,
                    data = currencies
                };
            }
            catch
            {
                #region Error              
                return BadRequest(new GeneralResponse
                {
                    title = _controller,
                    message = "Ha ocurrido un error en el sistema. Contacte al administrador.",
                    status = 500,
                    data = new { }
                });
                #endregion
            }
        }
    }
}
