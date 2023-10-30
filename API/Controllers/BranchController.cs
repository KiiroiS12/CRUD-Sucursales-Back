using API.DataAccess.Context;
using API.Models.Data;
using API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        public readonly AppDbContext _context;
        public readonly string _controller = "Sucursales";

        public BranchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get", Name = "get")]
        public async Task<ActionResult<GeneralResponse>> GetBranches()
        {
            try
            {
                var branches = await _context.Branches
                    .Join(_context.Currencies,
                    b => b.IdCurrency,
                    c => c.Id,
                    (b, c) => new { branch = b, currency = c })
                    .Select(x => new
                    {
                        id = x.branch.Id,
                        code = x.branch.Code,
                        description = x.branch.Description,
                        address = x.branch.Address,
                        identification = x.branch.Identification,
                        dateCreate = x.branch.DateCreate,
                        idCurrency = x.currency.Id,
                        currency = x.currency.Description
                    }).ToListAsync();

                if (branches.Count <= 0)
                {
                    return new GeneralResponse
                    {
                        title = _controller,
                        message = $"No hay sucursales.",
                        status = 404,
                        data = new { }
                    };
                }

                return new GeneralResponse
                {
                    title = _controller,
                    message = "Proceso exitoso.",
                    status = 200,
                    data = branches
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

        [HttpPost("Add", Name = "add")]
        public async Task<ActionResult<GeneralResponse>> AddBranch(BranchDto request)
        {
            try
            {
                await _context.AddAsync(new Branch
                {
                    Code = request.code,
                    Description = request.description,
                    Address = request.address,
                    Identification = request.identification,
                    DateCreate = request.dateCreate,
                    IdCurrency = request.idCurrency
                });

                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    title = _controller,
                    message = "Sucursal creada con exito.",
                    status = 200,
                    data = new { }
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

        [HttpPost("Update", Name = "update")]
        public async Task<ActionResult<GeneralResponse>> UpdateBranch(BranchDto request)
        {
            try
            {
                Branch? branch = await _context.Branches.Where(x => x.Id.Equals(request.id)).FirstOrDefaultAsync();

                if (branch == null)
                {
                    return new GeneralResponse
                    {
                        title = _controller,
                        message = $"La sucursal no existe.",
                        status = 404,
                        data = new { }
                    };
                }

                branch.Code = request.code;
                branch.Description = request.description;
                branch.Address = request.address;
                branch.Identification = request.identification;
                branch.DateCreate = request.dateCreate;
                branch.IdCurrency = request.idCurrency;

                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    title = _controller,
                    message = "Sucursal actualizada con exito.",
                    status = 200,
                    data = new { }
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

        [HttpDelete("Delete", Name = "delete")]
        public async Task<ActionResult<GeneralResponse>> DeleteBranch(int id)
        {
            try
            {
                Branch? branch = await _context.Branches.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

                if (branch == null)
                {
                    return new GeneralResponse
                    {
                        title = _controller,
                        message = $"La sucursal no existe.",
                        status = 404,
                        data = new { }
                    };
                }

                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();

                return new GeneralResponse
                {
                    title = _controller,
                    message = "Sucural removida con exito.",
                    status = 200,
                    data = new { }
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
